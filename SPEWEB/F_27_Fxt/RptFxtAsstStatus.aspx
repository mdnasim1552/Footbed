<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptFxtAsstStatus.aspx.cs" Inherits="SPEWEB.F_27_Fxt.RptFxtAsstStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        label {
            margin-bottom: 0rem;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="View1" runat="server">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="card card-fluid">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Project Name"></asp:Label>
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtSrcProject" runat="server" Width="150px" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-sm btn-primary" OnClick="ibtnFindProject_Click"><i class="fa fa-search"></i></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:CheckBox ID="ChkBalance" runat="server" Text="Without Zero Balance" Style="margin-bottom: 0rem" />

                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1 text-center" style="margin-top: 20px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="lblRptGroup" runat="server" Text="Group"></asp:Label>

                                            <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem>Main</asp:ListItem>
                                                <asp:ListItem>Sub-1</asp:ListItem>
                                                <asp:ListItem>Sub-2</asp:ListItem>
                                                <asp:ListItem>Sub-3</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="rbtnList1" runat="server"
                                                RepeatColumns="6" RepeatDirection="Horizontal" Width="350px" ForeColor="Black" CssClass="rbtnList1" Style="margin-top: 25px">
                                                <asp:ListItem>With Details</asp:ListItem>
                                                <asp:ListItem>Without Details</asp:ListItem>
                                                <asp:ListItem>With Value</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>





                    <div class="card card-fluid" style="min-height: 450px">
                        <div class="card-body">
                            <div class="row">

                                <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Style="text-align: left" Width="810px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgpactdesc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receive Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReceiveDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="left" />
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Receive Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQty" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTrnsDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnsdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTqty" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRentPerday" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="font-size: 12px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="80px" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblFoterAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
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
                        </div>
                    </div>

                </asp:View>

                <asp:View ID="Depreciation" runat="server">
                    <asp:Panel ID="Panel3" runat="server">

                        <div class="card card-fluid">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Text="To"></asp:Label>
                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-1 text-center" style="margin-top: 27px">
                                        <asp:CheckBox ID="chkStraight" runat="server" TabIndex="10" Text="Straight" />
                                    </div>

                                    <div class="col-md-1 text-center" style="margin-top: 20px">
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnShow_Click">Show</asp:LinkButton>
                                    </div>

                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="lblpage" runat="server">Page</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" Width="90px"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                                <asp:ListItem Value="500">500</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-1 text-center" style="margin-top: 20px">
                                        <asp:Label ID="txtDays" runat="server" Visible="false" CssClass="btn btn-sm btn-danger"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card card-fluid" style="min-height: 450px">
                            <div class="card-body">
                                <div class="row">
                                    <asp:GridView ID="grDep" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                                        ShowFooter="True" Style="text-align: left" Width="810px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid1" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl.No." Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActcode" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <table style="border: none;">
                                                        <tr>
                                                            <td style="border: none;">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                    Text="Assets" Width="50"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: left" Text="Total :" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance as at ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOpening" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Addition During The Period">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAddition" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTAddition" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Sales Declaration">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvsalesdec" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFsalesdec" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Revaluation During The Period">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdisposal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDisposal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total as at">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="left" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate Of Dep. %">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepPar" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Depreciation as at">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepOpen" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndep")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDepOpen" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Adjustment ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvadjment" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFadjment" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Depreciation (Opening) ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcurDepop" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdepop")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTcurDepop" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Depreciation (Current)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcurDepcut" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpdepcur")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTcurDepcur" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Depreciation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDepTotal" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todep")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTDepTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="70px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="W.D Values as at ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCBal" runat="server"
                                                        Style="font-size: 12px; text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFTCBal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="80px" />
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
                            </div>
                        </div>
                    </asp:Panel>
                </asp:View>

            </asp:MultiView>

            <%--<tr>
                                            <td class="style19">
                                                &nbsp;
                                            </td>
                                            <td class="style74">
                                                &nbsp;</td>
                                            <td class="style70">
                                                &nbsp;</td>
                                            <td class="style75">
                                                <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" BorderColor="#FFCC00"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="14px"
                                                    RepeatColumns="6" RepeatDirection="Horizontal" Width="350px" ForeColor="Black">
                                                    <asp:ListItem>With Details</asp:ListItem>
                                                    <asp:ListItem>Without Details</asp:ListItem>
                                                    <asp:ListItem>With Value</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="style19">
                                                &nbsp;
                                            </td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                        </tr>--%>
            <%--<tr>
                                            <td class="style19">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text="Project Name:" Width="90px" CssClass="style23" Font-Size="12px"></asp:Label>
                                            </td>
                                            <td class="style74">
                                                <asp:TextBox ID="txtSrcProject" runat="server" BorderStyle="None" 
                                                    CssClass="txtboxformat" Font-Bold="True" Width="90px"></asp:TextBox>
                                            </td>
                                            <td class="style70">
                                                <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ibtnFindProject_Click" Style="width: 18px" />
                                            </td>
                                            <td class="style75">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Width="350px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server" QueryPattern="Contains"
                                                    TargetControlID="ddlProjectName"></cc1:ListSearchExtender>
                                            </td>
                                            <td class="style19">
                                                <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="#000"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                                    Style="color: #FFFFFF">Ok</asp:LinkButton>
                                            </td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                            <td class="style19">
                                                &nbsp;</td>
                                        </tr>--%>

            <%--<tr>
                                            <td class="style21">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text="Date:" Width="90px" CssClass="style23" Font-Size="12px" 
                                                    BorderStyle="None"></asp:Label>
                                            </td>
                                            <td class="style67">
                                                <asp:TextBox ID="txtdate" runat="server" CssClass="txtboxformat" Font-Bold="True"
                                                    Width="90px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                            </td>
                                            <td class="style22">
                                                <asp:CheckBox ID="ChkBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: left" Text="Without Zero Balance" Width="140px" />
                                            </td>
                                            <td class="style28">
                                                <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                                    Style="font-weight: 700; text-align: right" Text="Group :" Width="55px"></asp:Label>
                                            </td>
                                            <td class="style29">
                                                <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="21px" Style="text-transform: capitalize" Width="80px">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
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
                                        </tr>--%>


            <%--<tr>
                                            <td class="style63">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Text="From:" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style64">
                                                <asp:TextBox ID="txtFromdate" runat="server" CssClass="txtboxformat"
                                                    Font-Bold="True" Width="85px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>
                                            </td>
                                            <td class="style69">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Text="To:" Width="25px"></asp:Label>
                                            </td>
                                            <td class="style68">
                                                <asp:TextBox ID="txtTodate" runat="server" CssClass="txtboxformat"
                                                    Font-Bold="True" Width="85px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366"
                                                    BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    Font-Size="12px" OnClick="lbtnShow_Click"
                                                    Style="color: #FFFFFF; text-align: center; height: 17px;" Width="80px">Show</asp:LinkButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
            <%--<tr>
                                            <td class="style63">&nbsp;</td>
                                            <td class="style64">&nbsp;</td>
                                            <td class="style69">&nbsp;</td>
                                            <td class="style68">
                                                <asp:Label ID="txtDays" runat="server" BackColor="#000" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Blue"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
