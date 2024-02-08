<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccLedgerAll.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccLedgerAll" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

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
                        <div class="col-md-12 col-sm-12 col-lg-12 ">
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtnLedger" runat="server" ForeColor="black" CssClass=" rbtnList1" AutoPostBack="true" OnSelectedIndexChanged="rbtnLedger_SelectedIndexChanged"
                                    RepeatColumns="10"
                                    RepeatDirection="Horizontal" Style="text-align: left">
                                    <asp:ListItem Value="Ledger">Ledger</asp:ListItem>
                                    <asp:ListItem Value="SubLedger">Subsidiary Ledger</asp:ListItem>
                                    <asp:ListItem Value="DetailLedger">Special Ledger</asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">



                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="viewLedger" runat="server">
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" ForeColor="black" CssClass=" rbtnList1 small"
                                            RepeatColumns="1" RepeatDirection="Horizontal">
                                            <asp:ListItem>With Narration</asp:ListItem>
                                            <asp:ListItem>Without Narattion</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>

                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="label" OnClick="IbtnSearchAcc_Click">Get Acc. Heads</asp:LinkButton>
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="Panel1" visible="false">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ibtnFindRes" runat="server" CssClass="label" OnClick="ibtnFindRes_Click">Get Resource Heads</asp:LinkButton>
                                        <asp:DropDownList ID="ddlConAccResHead" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkShowLedger_Click">Show</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 " runat="server" id="Panel2" visible="false">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkqty" runat="server" CssClass="checkBox" Text="With qty" />

                                    </div>
                                </div>
                            </div>





                            <div class="row">
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
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>' Width="100px"></asp:Label>
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
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmount0" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
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

                        </asp:View>

                        <asp:View ID="viewSpLedger" runat="server">
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDateFromSp" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFromSp_CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateFromSp"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDatetoSp" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatetoSp_CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatetoSp"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ibtnFindResSP" runat="server" CssClass="label" OnClick="ibtnFindResSP_Click">Resource Heads</asp:LinkButton>
                                        <asp:DropDownList ID="ddlRescode" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkShowSPLedger" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkShowSPLedger_Click">Show</asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <asp:GridView ID="gvSpledger" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvSpledger_RowDataBound1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrp" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of Accounts">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vou.Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher No.">

                                            <ItemTemplate>



                                                <asp:HyperLink ID="HLgvvounum" runat="server" Font-Size="12px" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="75px"></asp:HyperLink>




                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqtysp" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOpAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvratesp" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="60px" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Dr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClAmount" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFClsAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="80px" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No/</br> Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChqNo" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnarration" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrsrinfo" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremarks" runat="server" CssClass="GridLebelL"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>
                            </div>






                        </asp:View>
                    </asp:MultiView>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

