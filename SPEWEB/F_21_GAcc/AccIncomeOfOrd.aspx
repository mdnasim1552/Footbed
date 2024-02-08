<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccIncomeOfOrd.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccIncomeOfOrd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Voucher No</asp:Label>
                                <div class="form-inline">
                                    <asp:TextBox ID="txtcurrentvou" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txtCurrntlast6" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm  small" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Voucher Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>


                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblcurr" runat="server" CssClass="label">Currency</asp:Label>
                                <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Create List" Target="_blank"
                                    NavigateUrl="~/F_34_Mgt/AccConversion.aspx"><span class="fa fa-plus-circle"></span></asp:HyperLink>

                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label"> Con. Rate</asp:Label>
                                <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label"> Mode</asp:Label>
                                <asp:DropDownList ID="ddlDelMode" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtMDate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtMDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height:300px;">
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:LinkButton ID="lblInvNo" runat="server" CssClass="label" OnClick="imgSearchInvoiceno_Click">Invoice List</asp:LinkButton>

                                    <asp:DropDownList ID="ddlInvList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-10 col-sm-10 col-lg-10 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectInv" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectInv_Click">Select</asp:LinkButton>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="689px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                            <asp:Label ID="lblSpclCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                            <asp:Label ID="lblSizeid" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL"
                                                Font-Names="Verdana" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim() + "]": "") %>'
                                                Width="350px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTgvFqty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Height="22px" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount</br>(FC)" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFfcamtdr" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Width="103px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfcamtdr" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamtdr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount</br>(FC)" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTgvfcamtcr" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Height="22px" Width="103px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTgvFfcamtcr" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                ReadOnly="True"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamtcr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Font-Size="11px"></asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount</br>(BDT)" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Width="103px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount</br>(BDT)" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Height="22px" Width="103px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                ReadOnly="True"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRate" runat="server" CssClass="text-right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Invvoice. No" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrnno" runat="server" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
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
                                    <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lblSrInfo" runat="server" CssClass="label" Visible="false">Other ref.(if any)</asp:Label>
                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6 col-lg-6 ">
                                <div class="form-group">
                                    <asp:Label ID="lblNaration" runat="server" CssClass="label" Visible="false">Narration</asp:Label>
                                    <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control form-control-sm " Visible="false" TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:TextBox>

                                </div>
                            </div>

                        </div>

                    </asp:Panel>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

