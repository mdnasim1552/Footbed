<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccTransfer.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



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

            <asp:Panel ID="Panel3" runat="server" Visible="false">

                <div class="card card-fluid">
                    <div class="card-body" style="min-height: 300px;">
                        <div class="row">
                            <div class="col-md-8 col-sm-8 col-lg-8 ">
                                <div class="form-group">
                                    <asp:Label ID="imgSearchMRno" runat="server" CssClass="label">Transfer List</asp:Label>

                                    <asp:DropDownList ID="ddlTrnsList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-4 col-lg-4 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectTrns" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectTrns_Click">Select</asp:LinkButton>
                                </div>
                            </div>


                        </div>



                        <div class="row">
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea">

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
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
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
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                Width="400px"></asp:Label>
                                            <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Visible="False" Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details Description" ItemStyle-Font-Size="9px"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification" ItemStyle-Font-Size="9px"
                                        Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#FFFFCC" Text="Total:"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle Font-Size="9px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                        ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent" ReadOnly="true"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                                CssClass="GridTextbox" Height="22px" Visible="False" Width="103px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="74px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="21px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="77px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="White" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Height="22px" Width="103px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Height="22px" Width="103px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="103px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Width="103px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trns. No" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrnno" runat="server" CssClass="GridLebel" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>

                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lblRefNum" runat="server" CssClass="label">Ref. No/Cheq. No</asp:Label>
                                    <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lblSrInfo" runat="server" CssClass="label">Other Ref</asp:Label>
                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6 col-lg-6 ">
                                <div class="form-group">
                                    <asp:Label ID="lblNaration" runat="server" CssClass="label">Narration</asp:Label>
                                    <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                    </div>


                </div>
                </div>



                





            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

