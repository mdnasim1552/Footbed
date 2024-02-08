<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccProjectReports.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccProjectReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblVoucherNo" runat="server" CssClass="label">A/C Description</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblActDesc" runat="server" Style="width: 100%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblVouDate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:Label ID="lblDate" runat="server" CssClass="form-control form-control-sm small "></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblRptGroup" runat="server" CssClass="label"> Group</asp:Label>
                                <asp:DropDownList ID="ddlReportLevelDetails" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="2">Main</asp:ListItem>
                                    <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                    <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                    <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkok_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <asp:GridView ID="dgvPS" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="dgvPS_RowDataBound" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea ">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code"
                                    Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total. &lt;br&gt; Net."
                                    HeaderStyle-Font-Size="14px" HeaderText="         Resource  Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                            Width="300px"></asp:HyperLink>
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Size="12px" HeaderText="Op.Qty"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopqty" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Size="12px" HeaderText="Op.Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblfopamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3121" runat="server" CssClass="GridLebel">-</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOpnamt1" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Size="12px" HeaderText="Cu.Qty"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCuq" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Dr.Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDr" runat="server" CssClass="GridLebel" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cr.Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCr" runat="server" CssClass="GridLebel" Width="80px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Qty"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClq" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Dr Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblfclDrAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label112" runat="server" CssClass="GridLebel">-</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClrDrAmt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl. Cr Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblfclCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblfclBalAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvClCram" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
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






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
