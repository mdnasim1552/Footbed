<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PackingListDetails.aspx.cs" Inherits="SPEWEB.F_19_EXP.PackingListDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <style>
                th {
                    text-align: center;
                }
            </style>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row my-3">
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblRefNo" CssClass="font-weight-bold"></asp:Label> <br>
                            <asp:LinkButton ID="LinkButtonExportToExcel" runat="server" Style="font-weight:bold;margin-bottom:5px !important;text-decoration: underline;color:green;" ToolTip="Export To Excel" Visible="true" OnClick="PopulateTemplateExcelFile">Export</asp:LinkButton> 
                        </div>
                    </div>

                    <div class="row">
                        <asp:GridView ID="gvPacking" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" ShowHeader="true" RowStyle-Font-Size="11px">
                            <PagerSettings Position="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Crtn No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCartonNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "crtnno") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Art. No">

                                    <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Art. No" Width="50"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvArtNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "styledesc") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HS Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvHSCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "hscode") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Forma">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFormaCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lformadesc") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Customer Order/PO"></asp:Label>
                                        <asp:LinkButton ID="ibtnPoSync" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Customer Order/PO? Customer Order/PO will change according to Current Order')" OnClick="ibtnSync_Click"><span class="fa fa-sync-alt"></span> Sync</asp:LinkButton>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCCCodrno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custordno") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                     <HeaderTemplate>
                                        <asp:Label runat="server" Text="Customer Ref./Style"></asp:Label>
                                        <asp:LinkButton ID="ibtnRefSync" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Customer Ref./Style? Customer Ref./Style will change according to Current Order')" OnClick="ibtnRefSync_Click"><span class="fa fa-sync-alt"></span> Sync</asp:LinkButton>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCustRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "custrefno") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "colordesc") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bar / EAN /<br/>Supplier Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBarcodeNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "barcodrefno") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Label Type/<br/>Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTypeOfLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lebltype") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderno") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <%--Size Start--%>
                                <asp:TemplateField HeaderText="Size-01">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF1" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size-02">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF2" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF2" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-03">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF3" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF3" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-04">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF4" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF4" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-05">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF5" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF5" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-06" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF6" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF6" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-07" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF7" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF7" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-08" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF8" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF8" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-09" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF9" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF9" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-10" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF10" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF10" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-11" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF11" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF11" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-12" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF12" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF12" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-13" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF13" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF13" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-14" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF14" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF14" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size-15" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvF15" runat="server" BackColor="Transparent"
                                            Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="flblgvF15" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--Size End--%>

                                <asp:TemplateField HeaderText="Qnty Pairs">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQntyPairs" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Qtypairs")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Ctns">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTotalCtns" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlcrtns")).ToString("#,##0;(#,##0); ") %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Pairs">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTotalPairs" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Carton<br/>Measurement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBoxMeasurement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "boxmeas") %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="G.W./<br/>Carton">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGwPerCrtn" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grswgtpercrtn")).ToString("#,#0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total<br/>G. W.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTtlGw" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble( DataBinder.Eval(Container.DataItem, "ttlgrswgt")).ToString("#,#0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total<br/>N. W.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTtlNw" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble( DataBinder.Eval(Container.DataItem, "ttlnetwgt")).ToString("#,#0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="N.W./<br/>Carton">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNwPerCrtn" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netwgtpercrtn")).ToString("#,#0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CBM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCbm" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbm")).ToString("#,#0.0000;(#,##0.0000); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
