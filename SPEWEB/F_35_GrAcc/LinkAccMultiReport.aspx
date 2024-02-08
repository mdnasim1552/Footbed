<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkAccMultiReport.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkAccMultiReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style58
        {
            width: 238px;
        }
        .style59
        {
            width: 138px;
        }
        .style60
        {
            width: 202px;
        }
        .style61
        {
            width: 134px;
        }
        .style62
        {
            width: 68px;
        }
        .style63
        {
            height: 21px;
        }
        .style64
        {
            width: 534px;
        }
        .style65
        {
            color: white;
        }
        .style66
        {
            width: 85px;
        }
        .style67
        {
            width: 277px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv1 = $('#<%=this.gvMonIsuPay.ClientID %>');
            gv1.Scrollable();
        }

    </script>--%>

    <table style="width: 888px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style64">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                                
                    
                    
                    
                    
                Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1; " Text="Accounts Reports View/Print Screen"
                                Width="429px" BorderStyle="Inset" BackColor="Transparent" 
                    BorderWidth="2px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                <asp:Label ID="lblRptType" runat="server" Visible="False" Width="99px"></asp:Label>
            </td>
            <td class="style59">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="False"
                                Font-Size="12px" 
                    Style="text-align: center; color: #FFFFFF;" 
                    OnClick="lnkPrint_Click" Font-Underline="False" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ScheduleView" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblSchReportTitle" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="ACCOUNTS CONTROL SCHEDULE" Width="839px" CssClass="button"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblSchReportPeriod" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="839px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblSchReportTitle2" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Accounts Schedule for ...." Width="839px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="False" 
                            BackColor="#DDFFEE" ShowFooter="True" Width="911px" 
                            onrowdatabound="gvSchedule_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Code"><ItemTemplate><asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" FooterText="Dr. &lt;br&gt; Cr." 
                                    HeaderText="Descryption of Account"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:HyperLink></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfopnamt" runat="server"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblDramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#66CCFF" />
                            <HeaderStyle BackColor="#66CCFF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="LedgerView" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblLgReportTitle" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="l e d g e r" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblLgLedgerHead" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Control Accounts Name" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblLgReportPeriod" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="843px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="False" 
                            BackColor="#C4E1FF" BorderColor="#77B655" BorderStyle="Solid" BorderWidth="2px" 
                            onrowdatabound="gvLedger_RowDataBound" ShowFooter="True" Width="987px">
                            <Columns>
                                <asp:TemplateField HeaderText="Vou.Date"><ItemTemplate><asp:Label ID="lgvvoudate" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")).ToString() %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No."><ItemTemplate><asp:HyperLink ID="HLgvVounum1" runat="server" CssClass="GridLebelL" 
                                            Font-Underline="False" Target="_blank" 
                                            Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                            Width="85px"></asp:HyperLink></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>' 
                                            Width="300px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr. Amount"><ItemTemplate><asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><asp:ItemStyle HorizontalAlign="right" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr. Amount"><ItemTemplate><asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><asp:ItemStyle HorizontalAlign="right" /></asp:TemplateField>


                               
                                             <asp:TemplateField HeaderText="Remarks"><ItemTemplate><asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                    Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name"><ItemTemplate><asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>' 
                                                    Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#5E7BAE" />
                            <HeaderStyle BackColor="#5E7BAE" Font-Bold="True" Font-Size="14px" 
                                ForeColor="White" />
                            <AlternatingRowStyle BackColor="#EEF7F7" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="VoucherView" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblVUVouTitle" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Voucher Title" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblVUControlDesc" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Control Accounts Name" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblControlCode1" runat="server" Font-Bold="True" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" 
                            Text="Control Code :" Width="110px" CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblVUControlCode" runat="server" Font-Bold="False" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top" Width="120px" 
                            CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style62">
                        &nbsp;</td>
                    <td class="style61">
                        &nbsp;</td>
                    <td class="style60">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="LblVouDate1" runat="server" Font-Bold="True" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" Text="Date :" 
                            Width="100px" CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblVUVouDate" runat="server" Font-Bold="False" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top" Width="120px" 
                            CssClass="style65"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblRefNo1" runat="server" Font-Bold="True" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" 
                            Text="Cheq./Ref. No. :" Width="110px" CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblVURefNo" runat="server" Font-Bold="False" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top" Width="255px" 
                            CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style62">
                        &nbsp;</td>
                    <td class="style61">
                        &nbsp;</td>
                    <td class="style60">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="LblVouNum1" runat="server" Font-Bold="True" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" 
                            Text="Voucher No. :" Width="100px" CssClass="style65"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblVUVouNum" runat="server" Font-Bold="False" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top" Width="120px" 
                            CssClass="style65"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style62">
                        &nbsp;</td>
                    <td class="style61">
                        &nbsp;</td>
                    <td class="style60">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="gvVoucher" runat="server" AutoGenerateColumns="False" 
                            BackColor="#99CCCC" BorderColor="#7FBF41" BorderStyle="Solid" BorderWidth="2px" 
                            ShowFooter="True" Width="859px">
                            <Columns>
                                <asp:TemplateField HeaderText="A/c Code" Visible="False"><ItemTemplate><asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Code" Visible="False"><ItemTemplate><asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcl Code" Visible="False"><ItemTemplate><asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C Description"><ItemTemplate><asp:Label ID="lblAccdesc" runat="server"  CssClass="GridLebelL" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="200px"></asp:Label></ItemTemplate></asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Details Description"><ItemTemplate><asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>' 
                                            Width="200px"></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification"><ItemTemplate><asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity"><ItemTemplate><asp:Label ID="lgvVouQty" runat="server" style="text-align: right"  CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate><asp:ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate"><ItemTemplate><asp:Label ID="lgvVouRate" runat="server"  CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="80px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><asp:ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount"><ItemTemplate><asp:Label ID="lgvVouDrAmt" runat="server" CssClass="GridLebel" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right" CssClass="GridLebel" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount"><ItemTemplate><asp:Label ID="lgvVouCrAmt" runat="server" CssClass="GridLebel" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="Right"   CssClass="GridLebel" /></asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks"><ItemTemplate><asp:Label ID="lgvVouRemarks" runat="server" CssClass="GridLebel" 
                                            style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                            Width="120px"></asp:Label></ItemTemplate></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5E7BAE" />
                            <HeaderStyle BackColor="#5E7BAE" BorderStyle="Solid" BorderWidth="2px" 
                                Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#6A8B92" BorderColor="#FF66CC" 
                                BorderStyle="Solid" BorderWidth="1px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblVUInWord" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Underline="True" 
                            style="FONT-SIZE: 14px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Inword:" Width="800px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style63">
                        <asp:Label ID="LblVUSrinfo1" runat="server" Font-Bold="True" Font-Size="14px" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" 
                            Text="Add. Ref. :" Width="110px" CssClass="style65"></asp:Label>
                    </td>
                    <td colspan="7" class="style63">
                        <asp:Label ID="LblVUSrinfo" runat="server" Font-Bold="False" Font-Size="12px" 
                            Height="16px" style="FONT-SIZE: 11px; VERTICAL-ALIGN: top" Width="682px" 
                            CssClass="style65"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblNarration1" runat="server" Font-Bold="True" Font-Size="14px" 
                            style="FONT-SIZE: 15px; VERTICAL-ALIGN: top; TEXT-ALIGN: right" 
                            Text="Narration :" Width="110px" CssClass="style65"></asp:Label>
                    </td>
                    <td colspan="7">
                        <asp:Label ID="LblVUNarration" runat="server" Font-Bold="False" 
                            Font-Size="12px" style="FONT-SIZE: 11px; VERTICAL-ALIGN: top" 
                            Width="696px" CssClass="style65"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style62">
                        &nbsp;</td>
                    <td class="style61">
                        &nbsp;</td>
                    <td class="style60">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="SpLedgerVeiw" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblHeaderName" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Account Special Ledger" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblResName" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Resource Name" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblLgResRptPeriod" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="843px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#C4E1FF" BorderColor="#77B655" BorderStyle="Solid" 
                                    BorderWidth="2px" ShowFooter="True" 
                                    onrowdatabound="dgv2_RowDataBound">
                                    <Columns>
                                     <asp:TemplateField HeaderText="Group Description"><ItemTemplate><asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" style=" text-align:left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' width="120px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vou.Date"><ItemTemplate><asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel" 
                                                    
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' width="80px"></asp:Label></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No."><ItemTemplate><asp:HyperLink id="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel" 
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                                    Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cheque/Ref #"><ItemTemplate><asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                    Width="85px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>' 
                                                    Width="250px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Qty"><ItemTemplate><asp:Label ID="lblgvtrnqty" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    width="70px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rate"><ItemTemplate><asp:Label ID="lblgvtrnrate" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'  width="70px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Dr. Amount"><ItemTemplate><asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount"><ItemTemplate><asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Balance Amt."><ItemTemplate><asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remarks"><ItemTemplate><asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                    Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>

                                                <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>' 
                                                    Width="100px"></asp:Label>
                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle BackColor="#5E7BAE" />
                                    <HeaderStyle BackColor="#5E7BAE" Font-Bold="True" Font-Size="14px" 
                                        ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#EEF7F7" />
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="DeailsTB" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="ACCOUNTS CONTROL SCHEDULE" Width="839px" CssClass="button"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblRptPeriod" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="839px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="LblSchReportTitle5" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Accounts Schedule for ...." Width="839px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="grvDTB" runat="server" AutoGenerateColumns="False" 
                            BackColor="#DDFFEE" ShowFooter="True" Width="911px" 
                            onrowdatabound="grvDTB_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false"><ItemTemplate><asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                 <asp:TemplateField HeaderText="Code"><ItemTemplate><asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="300px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" FooterText="Dr. &lt;br&gt; Cr." 
                                    HeaderText="Descryption of Account"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="300px"></asp:HyperLink></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblDramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#66CCFF" />
                            <HeaderStyle BackColor="#66CCFF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewAccRecFin" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblAccFec" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="True" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Account Special Ledger" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblAccRecCustomer" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-TRANSFORM: uppercase; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Resource Name" Width="840px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblAccleb" runat="server" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="843px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="grvAccRecFin" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#C4E1FF" BorderColor="#77B655" BorderStyle="Solid" 
                                    BorderWidth="2px" ShowFooter="True" 
                            onrowdatabound="grvAccRecFin_RowDataBound">
                                    <Columns>
                                    
                                        <asp:TemplateField HeaderText="Vou.Date"><ItemTemplate><asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel" 
                                                    
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' width="80px"></asp:Label></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No."><ItemTemplate><asp:HyperLink id="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel" 
                                                    Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")).Trim().Length==12 ? DataBinder.Eval(Container.DataItem, "vounum1") : DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                                    Font-Underline="False" Target="_blank" __designer:wfdid="w1"></asp:HyperLink></ItemTemplate></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Cheque/Ref #"><ItemTemplate><asp:Label ID="lblChequeNo" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>' 
                                                    Width="85px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:Label ID="lbldescription0" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length > 0? "<br>" + DataBinder.Eval(Container.DataItem, "resdesc"):"") + DataBinder.Eval(Container.DataItem, "venar1")  + DataBinder.Eval(Container.DataItem, "venar2") %>' 
                                                    Width="250px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Dr. Amount"><ItemTemplate><asp:Label ID="lblgvDrAmount0" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount"><ItemTemplate><asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' width="80px"></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                         <asp:TemplateField HeaderText="Balance Amt."><ItemTemplate><asp:Label ID="lblgvBalamt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                             <asp:TemplateField HeaderText="Remarks"><ItemTemplate><asp:Label ID="lgvRemarks" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                    Width="100px"></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="left" /></asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusernamesp" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>' 
                                                    Width="100px"></asp:Label>
                                                    
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#5E7BAE" />
                                    <HeaderStyle BackColor="#5E7BAE" Font-Bold="True" Font-Size="14px" 
                                        ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#EEF7F7" />
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewRecPaySchu" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" BackColor="#0033CC" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" CssClass="button" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="False" Height="20px" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="ACCOUNTS CONTROL SCHEDULE" Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblRDate" runat="server" BackColor="#0033CC" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Overline="False" 
                            Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="450px"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblRecPayCode" runat="server" BackColor="#0033CC" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Accounts Schedule for ...." Width="450px"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="grvRecPay" runat="server" AutoGenerateColumns="False" 
                            BackColor="#DDFFEE" ShowFooter="True" 
                            onrowdatabound="grvRecPay_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Code"><ItemTemplate><asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:" 
                                    HeaderText="Descryption of Account"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px; color:Black; font-weight:bold" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="400px"></asp:HyperLink></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblCramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblDramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                
                                
                            </Columns>
                            <FooterStyle BackColor="#66CCFF" />
                            <HeaderStyle BackColor="#66CCFF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewDetTBRP" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" BackColor="Blue" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" CssClass="button" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="False" Height="20px" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="ACCOUNTS CONTROL SCHEDULE" Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="lblDetRP" runat="server" BackColor="#0033CC" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Overline="False" 
                            Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="lblActRp" runat="server" BackColor="Blue" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Overline="False" 
                            Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Accounts Schedule for ...." Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="grvDetTbRp" runat="server" AutoGenerateColumns="False" 
                            BackColor="#DDFFEE" ShowFooter="True" 
                            onrowdatabound="grvDetTbRp_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false"><ItemTemplate><asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                 <asp:TemplateField HeaderText="Code"><ItemTemplate><asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                Width="300px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false"><ItemTemplate><asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px ; color:Black; font-weight:bold" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="400px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                    FooterStyle-HorizontalAlign="Right" FooterText="Total Amount:" 
                                    HeaderText="Descryption of Account"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px" 
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="400px"></asp:HyperLink></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="Left" /></asp:TemplateField>
                                
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Receipt Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblCramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Payment Amount" 
                                    ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><FooterStyle HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                
                            </Columns>
                            <FooterStyle BackColor="#66CCFF" />
                            <HeaderStyle BackColor="#66CCFF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
         <asp:View ID="ViewPrjRepRP" runat="server">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="Label5" runat="server" BackColor="Blue" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" CssClass="button" Font-Bold="True" 
                            Font-Overline="False" Font-Size="Large" Font-Underline="False" Height="20px" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="ACCOUNTS CONTROL SCHEDULE" Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="lblDuType" runat="server" BackColor="#0033CC" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Overline="False" 
                            Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Reporting Period" Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Label ID="lblActcodePRJ" runat="server" BackColor="Blue" BorderColor="White" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Overline="False" 
                            Font-Size="Smaller" Font-Underline="False" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 15px; TEXT-ALIGN: center; color: #FFFFFF;" 
                            Text="Accounts Schedule for ...." Width="450px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="grvPrjRptRP" runat="server" AutoGenerateColumns="False" BackColor="#DDFFEE"
                                    ShowFooter="True" onrowdatabound="grvPrjRptRP_RowDataBound">
                                    <Columns>
                                       
                                         <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                               Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" /><ItemStyle Font-Size="12px" /></asp:TemplateField>
      
                                          
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="ActCode" Visible="False"><ItemTemplate><asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="SubCode" Visible="False"><ItemTemplate><asp:Label ID="lblgvSubcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "subcode1").ToString() %>'></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="14px" HeaderText="" Visible="false"><ItemTemplate><asp:Label ID="gvlblDesc" runat="server" 
                                                               CssClass="GridLebelL" Font-Size="12px"
                                                               Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>' 
                                                               Width="100px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <asp:TemplateField FooterText="Total Amount:" FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-Font-Size="14px" HeaderText="Resource  Description"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" __designer:wfdid="w38" 
                                                               CssClass="GridLebelL" Font-Size="12px" Style="color:Black; font-weight:bold" Font-Underline="False" Target="_blank" 
                                                               Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc4")) %>' 
                                                               Width="400px"></asp:HyperLink></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><HeaderStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit"><ItemTemplate><asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Receipt Amt" ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvDram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><HeaderStyle Font-Size="12px" /><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>


                                          <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Payment Amt" ItemStyle-HorizontalAlign="Right"><FooterTemplate><asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate><ItemTemplate><asp:Label ID="lblgvDramt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate><HeaderStyle Font-Size="12px" /><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#66CCFF" />
                                    <HeaderStyle BackColor="#66CCFF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <table style="width:100%;">
                <tr>
                            <td colspan="7" class="style78">
                                <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                    BorderWidth="1px">
                                    <table style="width: 100%;">
                                      
                                      
                                        <tr>
                                            <td class="style66">
                                                <asp:Label ID="Label3123" runat="server" Font-Bold="True" Font-Size="14px" 
                                                    Height="16px" Style="text-align: right; color: #FFFFFF;" Text="Date" 
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td class="style67">
                                                <asp:Label ID="lblDate" runat="server" BackColor="White" BorderStyle="None" 
                                                    Font-Bold="True" ForeColor="Black" Width="300px"></asp:Label>
                                            </td>
                                            <td class="style90">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                       
                                         <tr>
                                            <td class="style66">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                    Style="text-align: right; color: #FFFFFF;" Text="Resourch Name:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style67">
                                                <asp:Label ID="lblResDesc" runat="server" BackColor="White" BorderStyle="None" 
                                                    Font-Bold="True" ForeColor="Black" Width="300px"></asp:Label>
                                            </td>
                                            <td class="style90">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                      
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                <tr>
                    <td colspan="8">
                        <asp:GridView ID="gvMonIsuPay" runat="server" AutoGenerateColumns="False" 
                                            ShowFooter="True" style="text-align: left" Width="963px" 
                                            onrowdatabound="gvMonIsuPay_RowDataBound">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center"  /></asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="AC.Code" Visible="False"><ItemTemplate><asp:Label ID="lblgvAccCod" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                            Width="49px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cat.Code" Visible="False"><ItemTemplate><asp:Label ID="lblgcatCod" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                                            Width="49px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                                <asp:TemplateField HeaderText="ResCode" Visible="False"><ItemTemplate><asp:Label ID="lgcUcode" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                            Width="50px"></asp:Label></ItemTemplate></asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supplier Name"><ItemTemplate><asp:Label ID="lgvSupname" runat="server" 

                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                          
                                                            Width="180px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center"/></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc. Description"><ItemTemplate><asp:Label ID="lgvAccDesc" runat="server"

                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                          
                                                            Width="180px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center"/></asp:TemplateField>

                                                <asp:TemplateField HeaderText="Voucher #" Visible="false"><ItemTemplate><asp:Label ID="lgvPVnum" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>' 
                                                            Width="80px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                

                                                 <asp:TemplateField HeaderText="Voucher #" ><ItemTemplate><asp:Label ID="lgvvounum1" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                            Width="70px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date"><ItemTemplate><asp:Label ID="lgvPVDate" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>' 
                                                            Width="70px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center"  /></asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Cheque No."><ItemTemplate><asp:Label ID="lgvchnono" runat="server"  Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'></asp:Label></ItemTemplate></asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Cheque Date" Visible="false"><ItemTemplate><asp:Label ID="lgvchdat" runat="server" style="text-align: left" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>' 
                                                            Width="70px"></asp:Label></ItemTemplate></asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Amount"><ItemTemplate><asp:Label ID="lgvcramt" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvCrAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px"></asp:Label></FooterTemplate><HeaderStyle HorizontalAlign="Center"  /><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>

                                                 <asp:TemplateField HeaderText=" Cleared Amount"><ItemTemplate><asp:Label ID="lgvreconamt" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="70px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFReconAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" style="text-align: right" Width="70px"></asp:Label></FooterTemplate><HeaderStyle HorizontalAlign="Center"  /><ItemStyle HorizontalAlign="Right" /><FooterStyle HorizontalAlign="right" /></asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Issue Date"><ItemTemplate><asp:Label ID="lgvbcldate" runat="server" style="text-align: left" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isudat")) %>' 
                                                            Width="70px"></asp:Label></ItemTemplate></asp:TemplateField>


                                                
                                            </Columns>
                                            <FooterStyle BackColor="#333333" />
                                            <PagerStyle HorizontalAlign="Left" ForeColor="White" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

