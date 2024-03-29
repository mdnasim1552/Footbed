﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccIndentUpdate.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccIndentUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/chosen.jquery.js"></script>

    <script type="text/javascript">
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body">

                    <div class="row pb-3">
                        <div class="col-md-3">
                            <asp:Label ID="lblcurVounum" runat="server" CssClass="">Voucher No.</asp:Label>
                            <div class="d-flex">
                                <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="form-control form-control-sm bg-light" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="form-control form-control-sm bg-light" ToolTip="You Can Change Voucher Number." ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass=""> Voucher Date</asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-3">
                            <div class="msgHandSt">
                                <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">

                    <div id="pnlBill" runat="server" class="" visible="false">

                        <div class="row mb-3">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" CssClass="" Text="Head of Acc" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtAccSch" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="imgbtnAcc" runat="server" CssClass="text-primary" OnClick="imgbtnAcc_Click">
                                <i class="fa fa-search mr-1"> </i> Head of Acc
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlActCode" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlActCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <asp:Label ID="lbldetails" runat="server" CssClass="" Text="Details of Acc" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtserchReCode" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="lnkRescode" runat="server" CssClass="text-primary">
                                <i class="fa fa-search mr-1"> </i> Details of Acc
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlresuorcecode" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3">
                                <asp:Label ID="lblBillList" runat="server" CssClass="" Text="Bill List" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtSrclsdno" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="imgbtnlsdno" runat="server" CssClass="text-primary" OnClick="imgbtnlsdno_Click">
                                                <i class="fa fa-search mr-1"> </i> Bill List
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-1">
                                <asp:LinkButton ID="lbtnSelectTrns" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectTrns_Click">Select</asp:LinkButton>
                            </div>
                        </div>

                        <div class="table-responsive mb-3">
                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
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
                                            <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                                     (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                                     (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                                     "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                                     Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")   
                                                                                    %>'
                                                Width="350px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" Visible="false" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="Black" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Black" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Memono" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemono" runat="server" CssClass="GridLebelL" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono")) %>'></asp:Label>
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

                        <div class="row">
                            <div class="col-md-6">

                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="" Text="Ref. No"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="font-weight-bold" Text="Narration:"></asp:Label>
                                                </span>
                                            </div>
                                            <textarea id="txtNarration" runat="server" class="form-control" rows="3"></textarea>
                                        </div>
                                    </div>
                                </div>

                                <%--<div class="form-group">
                            <div class="colMdbtn pading5px">
                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>
                            </div>
                        </div>--%>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
