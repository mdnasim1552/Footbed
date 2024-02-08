<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AllIndentIsuList.aspx.cs" Inherits="SPEWEB.F_11_RawInv.AllIndentIsuList" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
           <%-- var gridview = $('#<%=this.gvPromData.ClientID %>');
            gridview.ScrollableGv();--%>

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

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="card card-fluid mb-1">
        <div class="card-body ">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 " id="FromD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 " id="ToD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="card card-fluid">

        <div class="card-body">

            <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                <ContentTemplate>
                    <div class="row" style="min-height: 400px;">

                        <asp:GridView ID="gvPromData" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvPromData_RowDataBound"
                            OnPageIndexChanging="gvPromData_PageIndexChanging" CssClass="table-striped  table-hover table-bordered grvContentarea" AllowPaging="false">
                            <RowStyle />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "issuedat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Challan No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblissueno" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIdcard" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name of Person">
                                    <HeaderTemplate>
                                        <table style="width: 250px;">
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblrsirdesc" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDept" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblissueqty" runat="server"
                                            Font-Size="X-Small"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFissueqty" runat="server" Font-Bold="True" Font-Size="X-Small"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrate" runat="server"
                                            Font-Size="X-Small"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFrate" runat="server" Font-Bold="True" Font-Size="X-Small"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblissueamt" runat="server"
                                            Font-Size="X-Small"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFissueamt" runat="server" Font-Bold="True" Font-Size="X-Small"
                                            ForeColor="#000" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>

                                        <asp:HyperLink ToolTip="Edit And Approve" Target="_blank" ID="BtnEdit" runat="server"><i class="fa fa-edit"></i>
                                                
                                        </asp:HyperLink>

                                        <asp:HyperLink ToolTip="Audit" ID="BtnAudit" Target="_blank" runat="server"><i class="fa fa-list"></i>
                                                
                                        </asp:HyperLink>

                                        <asp:HyperLink ID="HypRDDoPrint" runat="server" Target="_blank"><i class="fa fa-print"></i>
                                        </asp:HyperLink>

                                        <asp:LinkButton ID="LbtnDelete" OnClick="LbtnDelete_Click" OnClientClick="return confirm('Do You want to delete this item?');" runat="server">
                                            <i class="fa fa-trash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblisaudited" Font-Bold="true" runat="server" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isAudited")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFisaudited" runat="server" Font-Bold="True" Font-Size="X-Small"
                                            ForeColor="#000" Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />

                        </asp:GridView>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

