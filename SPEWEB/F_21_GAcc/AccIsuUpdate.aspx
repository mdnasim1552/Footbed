<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccIsuUpdate.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccIsuUpdate" %>

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



                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px;">
                    <asp:Panel ID="pnlBill" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:LinkButton ID="imgSearchBillno" runat="server" CssClass="label" OnClick="imgSearchBillno_Click">Bill List</asp:LinkButton>

                                    <asp:DropDownList ID="ddlIsuList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-10 col-sm-10 col-lg-10 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectBill" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectBill_Click">Select</asp:LinkButton>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-md-8 col-sm-8 col-lg-8 ">
                                <div class="row">
                                    <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
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
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                        Width="350px"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Font-Size="11px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                                ItemStyle-Font-Size="11px">
                                                <FooterTemplate>
                                                    <asp:Label ID="txtTgvQty" runat="server" BackColor="Transparent" ReadOnly="true"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                                        CssClass="GridTextbox" Visible="False" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
                                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField FooterText="Total" HeaderText="Rate"
                                                ItemStyle-Font-Size="11px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle ForeColor="Black" Font-Bold="true" />
                                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                                <FooterTemplate>
                                                    <asp:Label ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                                <FooterTemplate>
                                                    <asp:Label ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                        CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
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

                                            <asp:TemplateField HeaderText="Narration" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNarration" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridLebelL" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nar")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="11px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="preqno" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpreqno" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'></asp:Label>
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

                            </div>
                            <div class="col-md-4 col-sm-4 col-lg-4 ">
                                <div class="row">
                                    <div class="col-md-5 col-sm-5 col-lg-5 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="label">Ref. No/Cheq. No</asp:Label>
                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                    </div>
                                     <div class="col-md-5 col-sm-5 col-lg-5 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblSrInfo" runat="server" CssClass="label">Other Ref.</asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-10 col-sm-10 col-lg-10 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblNaration" runat="server" CssClass="label">Narration</asp:Label>
                                            <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>


            </br> </br> </br> </br>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

