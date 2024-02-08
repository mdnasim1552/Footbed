<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccLedger.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccLedger" %>

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

    <style type="text/css">
        .rbtnList1 table tbody tr td input[type=radio] {
            display: initial !important;
            margin-top: 2px !important;
            position: relative !important;
            margin-left: 15px !important;
        }

        .rbtnList1 table tbody tr td label {
            margin-left: 5px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body py-3">
                    <div class="row">

                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:calendarextender id="txtDateFrom_CalendarExtender" runat="server"
                                enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtDateFrom">
                            </cc1:calendarextender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblDateto" runat="server" CssClass="" Text="To"></asp:Label>
                            <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:calendarextender id="txtDateto_CalendarExtender" runat="server"
                                enabled="True" format="dd-MMM-yyyy" targetcontrolid="txtDateto">
                            </cc1:calendarextender>
                        </div>

                        <div class="col-md-3 rbtnList1">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" Style="margin-top: 18px;" CssClass="form-control form-control-sm bg-secondary"
                                RepeatColumns="6" RepeatDirection="Horizontal">
                                <asp:ListItem>With Narration</asp:ListItem>
                                <asp:ListItem>Without Narattion</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblcontrolAccHead" runat="server" Visible="false" CssClass="" Text="Get Acc. Heads"></asp:Label>
                            <asp:TextBox ID="txtAccSearch" runat="server" Visible="false" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="text-primary" OnClick="IbtnSearchAcc_Click">
                                <i class="fa fa-search mr-1"></i> Get Acc. Heads
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 18px;" OnClick="lnkShowLedger_Click">Show</asp:LinkButton>
                        </div>


                        <div class="col-md-3">
                            <div class="msgHandSt">
                                <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="Panel1" runat="server" visible="false" class="card card-fluid mb-1">
                <div class="card-body py-3">

                    <div class="row">

                        <div class="col-md-3">
                            <asp:Label ID="lblcontrolAccResCode" runat="server" Visible="false" CssClass="" Text="Get Resource Heads"></asp:Label>
                            <asp:TextBox ID="txtSrchRes" runat="server" Visible="false" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="text-primary" OnClick="ibtnFindRes_Click">
                                <span class="fa fa-search mr-1"> </span> Get Resource Heads
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:CheckBox ID="chkqty" runat="server" Style="margin-top: 18px;" CssClass="form-control form-control-sm bg-secondary text-center" Text="With qty" />
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">

                    <div class="table-responsive">
                        <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="dgv2_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" CssClass="GridLebel"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <table style="width: 30%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                        Text="Group Description" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGrpDesc" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vou.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server" CssClass="GridLebel" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher No.">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" Width="80px" CssClass="GridLebel" Style="text-align: left;"
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
                                            Width="100px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusername" runat="server" CssClass="GridLebelL"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                            Width="90px"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>

                </div>
            </div>



        </contenttemplate>
    </asp:UpdatePanel>

</asp:Content>


