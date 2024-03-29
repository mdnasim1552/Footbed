﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccMultiReport.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccMultiReport" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        <div class="card-body" style="min-height: 350px;">
            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="ScheduleView" runat="server">

                    <div class="row text-center">

                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label1" runat="server" Text="Control Accounts Name" CssClass="rptheadTitel3"></asp:Label>

                                <asp:Label ID="lblRptType" runat="server" Visible="False" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblSchReportTitle" runat="server" CssClass="rptheadTitel3"
                                    Text="ACCOUNTS CONTROL SCHEDULE"></asp:Label>

                                <asp:Label ID="LblSchReportPeriod" runat="server" CssClass="rptheadTitel3"
                                    Text="Reporting Period"></asp:Label>

                                <asp:Label ID="LblSchReportTitle2" runat="server"
                                    Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="911px" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter"
                            OnRowDataBound="gvSchedule_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Dr. &lt;br&gt; Cr."
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>



                </asp:View>

                <asp:View ID="LedgerView" runat="server">


                    <div class="row text-center">

                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="LblLgReportTitle" runat="server" Text="l e d g e r" CssClass="rptheadTitel3"></asp:Label><br />
                                <asp:Label ID="LblLgLedgerHead" runat="server" CssClass="rptheadTitel3"
                                    Text="Control Accounts Name"></asp:Label>

                                <asp:Label ID="LblLgReportPeriod" runat="server" CssClass="rptheadTitel3"
                                    Text="Control Accounts Name"></asp:Label>

                                <asp:Label ID="Label10" runat="server"
                                    Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLedger_RowDataBound" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")).ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" CssClass="GridLebelL"
                                            Font-Underline="False" Target="_blank"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="85px"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <asp:ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <asp:ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </asp:View>


                <asp:View ID="VoucherView" runat="server">

                    <div class="row text-center">

                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="LblVUVouTitle" runat="server" Text="Voucher Title" CssClass="rptheadTitel1"></asp:Label>

                                <asp:Label ID="LblVUControlDesc" runat="server" Text="Control Accounts Name" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblControlCode1" runat="server" Text="Control Code" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUControlCode" runat="server" Text="Control Code" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVouDate1" runat="server" Text="Date" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUVouDate" runat="server" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblRefNo1" runat="server" Text="Cheq./Ref. No." CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVURefNo" runat="server" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVouNum1" runat="server" Text="Voucher No. " CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUVouNum" runat="server" Text="Cheq./Ref. No." CssClass="rptheadTitel3"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvVoucher" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                  <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="280px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouQty" runat="server" Style="text-align: right" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <asp:ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouRate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <asp:ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouDrAmt" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" CssClass="GridLebel" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouCrAmt" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" CssClass="GridLebel" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVouRemarks" runat="server" CssClass="GridLebel"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="120px"></asp:Label>
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
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="LblVUInWord" runat="server" Text="Inword" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUSrinfo1" runat="server" Text="Add. Ref." CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUSrinfo" runat="server" Text="Inword" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblNarration1" runat="server" Text="Narration" CssClass="rptheadTitel3"></asp:Label>
                                <asp:Label ID="LblVUNarration" runat="server" Text="Narration" CssClass="rptheadTitel3"></asp:Label>
                            </div>

                        </div>
                    </div>
                </asp:View>


                <asp:View ID="SpLedgerVeiw" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="lblHeaderName" runat="server" Text="Account Special Ledger" CssClass="rptheadTitel1"></asp:Label><br />

                                <asp:Label ID="lblResName" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label><br />
                                <asp:Label ID="LblLgResRptPeriod" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>



                    </div>
                    <div class="row ">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="dgv2_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                  <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtrnrate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>




                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>

                <asp:View ID="DeailsTB" runat="server">

                    <div class="row text-center">

                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label2" runat="server" Text="ACCOUNTS DETAILS SCHEDULE" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblRptPeriod" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="LblSchReportTitle5" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <asp:GridView ID="grvDTB" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvDTB_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="400px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Dr. &lt;br&gt; Cr."
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>

                </asp:View>

                <asp:View ID="ViewAccRecFin" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="lblAccFec" runat="server" Text="Account Special Ledger" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblAccRecCustomer" runat="server" Text="Resource Name" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="lblAccleb" runat="server" Text="Reporting Period" CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>

                    <div class="row ">
                        <asp:GridView ID="grvAccRecFin" runat="server" AutoGenerateColumns="False" BorderWidth="2px" ShowFooter="True" OnRowDataBound="grvAccRecFin_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>

                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel"
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusernamesp" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="100px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>

                </asp:View>

                <asp:View ID="ViewRecPaySchu" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label3" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblRDate" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="lblRecPayCode" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grvRecPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True"
                            OnRowDataBound="grvRecPay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:"
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px; color: Black; font-weight: bold"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>

                <asp:View ID="ViewDetTBRP" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label4" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblDetRP" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="lblActRp" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grvDetTbRp" runat="server" AutoGenerateColumns="False"
                             ShowFooter="True"
                            OnRowDataBound="grvDetTbRp_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px; color: Black; font-weight: bold"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="400px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:"
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </asp:View>



                <asp:View ID="ViewPrjRepRP" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label5" runat="server" Text="ACCOUNTS CONTROL SCHEDULE" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblDuType" runat="server" Text="Reporting Period" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="lblActcodePRJ" runat="server" Text="Accounts Schedule for ...." CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grvPrjRptRP" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvPrjRptRP_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle  Font-Size="12px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="SubCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSubcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "subcode1").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="14px" HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlblDesc" runat="server"
                                            CssClass="GridLebelL" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total Amount:" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderStyle-Font-Size="14px" HeaderText="Resource  Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" __designer:wfdid="w38"
                                            CssClass="GridLebelL" Font-Size="12px" Style="color: Black; font-weight: bold" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>'
                                            Width="400px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Receipt Amt" ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Payment Amt" ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
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
                </asp:View>

                <asp:View ID="View1" runat="server">
                    <div class="row text-center">
                        <div class="col-md-12 col-sm-12 col-lg-12  ">

                            <div class="form-group ">
                                <asp:Label ID="Label3123" runat="server" Text="Date" CssClass="rptheadTitel1"></asp:Label><br />
                                <asp:Label ID="lblDate" runat="server" CssClass="rptheadTitel2"></asp:Label><br />
                                <asp:Label ID="Label13" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label><br />
                                <asp:Label ID="lblResDesc" runat="server" Text="Resourch Name" CssClass="rptheadTitel3"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="row ">
                        <asp:GridView ID="gvMonIsuPay" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" OnRowDataBound="gvMonIsuPay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgcatCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ResCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcUcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSupname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAccDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVnum" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounum1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPVDate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchnono" runat="server" Width="100px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Cleared Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreconamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbcldate" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>'
                                            Width="70px"></asp:Label>
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
                </asp:View>

            </asp:MultiView>


        </div>
    </div>


</asp:Content>

