<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccProductionJV.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccProductionJV" %>

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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row mb-3">

                        <div class="col-md-2">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass="">Current Voucher</asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="form-control form-control-sm" ToolTip="You Can Change Voucher Number." ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblDate" runat="server" CssClass=""> Voucher Date </asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 21px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <%--<div class="col-md-3 pading5px pull-right">
                            <div class="msgHandSt">
                                <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>--%>

                        <div class="col-md-1">
                            <asp:ImageButton ID="ibtnvounu" runat="server" Height="20px" ImageUrl="~/Image/movie_26.gif" OnClick="ibtnvounu_Click" Width="145px" Visible="False" />
                        </div>

                        <div runat="server" id="pnlPrevBill" class="col-md-2" visible="false">
                            <asp:Label ID="lblBillList" runat="server" CssClass="lblTxt lblName" Text="Bill List" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtmrslno" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                            <asp:LinkButton ID="imgSearchMRno" runat="server" CssClass="text-primary" OnClick="imgSearchMRno_Click">
                            <i class="fa fa-search mr-1"> </i> Bill List
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlProduList" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>

                        <div runat="server" id="pnlSelectBill" class="col-md-1" visible="false">
                            <asp:LinkButton ID="lbtnSelectBill" runat="server" Style="margin-top: 18px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectBill_Click">Select</asp:LinkButton>
                        </div>

                    </div>
                </div>
            </div>



            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">

                    <asp:Panel ID="pnlBill" runat="server" Visible="False">

                        <div class="col-12 table-responsive mb-3">
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
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


                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                        ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="txtTgvQty" runat="server" BackColor="Transparent" ReadOnly="true" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                                CssClass="GridTextbox" Visible="False" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvQty" runat="server" BackColor="Transparent" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvDrAmt" runat="server" BackColor="Transparent" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="txtTgvCrAmt" runat="server" BackColor="Transparent" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvCrAmt" runat="server" BackColor="Transparent" Style="text-align: right;"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridLebelL" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Narration">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNarration" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridLebelL" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nar")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="prodid" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprodid" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodid")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                        </div>


                        <div class="col-md-4">
                            <div class="form-group mb-3">
                                <asp:Label ID="lblRefNum" runat="server" CssClass="" Text="Ref./CheqNo"></asp:Label>
                                <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label ID="lblSrInfo" runat="server" CssClass="" Text="Other ref"></asp:Label>
                                <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>

                            <div class="form-group mb-3">
                                <asp:Label ID="lblNaration" runat="server" CssClass="" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-sm btn-danger" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

