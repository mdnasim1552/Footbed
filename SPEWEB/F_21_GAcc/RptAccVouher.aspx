<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptAccVouher.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccVouher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblVoucherNo" runat="server" CssClass="label">Voucher No</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblvalVoucherNo" runat="server" Style="width: 100%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblVouDate" runat="server" CssClass="label">Voucher Date</asp:Label>
                                <asp:Label ID="lblvalVoucherDate" runat="server" CssClass="form-control form-control-sm small "></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblBankDescription" runat="server" CssClass="label">Bank Name</asp:Label>
                                <asp:Label ID="lblValBankDescription" runat="server" CssClass="form-control form-control-sm small "></asp:Label>
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Style="text-align: left" Width="685px" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">

                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Text="Total"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server" Font-Names="Verdana"
                                            Font-Size="11px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                            Width="400px"></asp:Label>
                                        <asp:Label ID="lblAccdesc" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Visible="False" Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Quantity">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Font-Size="12px" ForeColor="Black"
                                            Style="text-align: right" TabIndex="79"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRate" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" ReadOnly="True" Style="text-align: right" TabIndex="80"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" ReadOnly="True" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="Black" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="82"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtFgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" ReadOnly="True" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="Black" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                            ForeColor="Black" TabIndex="83"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            CssClass="GridTextboxL" Font-Size="12px" ForeColor="Black" TabIndex="99"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblRefNum" runat="server" CssClass="label" Visible="false">Ref. No/Cheq. No/Slip no</asp:Label>
                                <asp:Label ID="lblvalRefNum" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblSrInfo" runat="server" CssClass="label" Visible="false">Other ref.(if any)</asp:Label>
                                <asp:Label ID="lblvalSirinfo" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <div class="form-group">
                                <asp:Label ID="lblPayto" runat="server" CssClass="label" Visible="false">Pay to</asp:Label>
                                <asp:Label ID="lblvalpayto" runat="server" CssClass="form-control form-control-sm " Visible="false" TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                            <div class="form-group">
                                <asp:Label ID="lblNarration" runat="server" CssClass="label" Visible="false">Narration</asp:Label>
                                <asp:Label ID="lblvalNarration" runat="server" CssClass="form-control form-control-sm " Visible="false" TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:Label>
                                            <asp:Label ID="lblisunum" runat="server" Visible="False"></asp:Label>

                            </div>
                        </div>

                    </div>
                </div>
            </div>







            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

