<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MatAvailability.aspx.cs" Inherits="SPEWEB.F_15_Pro.MatAvailability" %>

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

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .progress-bar {
            background-color: #f73535;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="contentPart">
                <asp:Panel ID="Panel1" runat="server">

                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblDate" runat="server" CssClass="col-form-label text-dark" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-9">

                                    <asp:Panel ID="PnlOth" runat="server" Visible="False">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblproduct" runat="server" CssClass="col-form-label text-dark" Text="Product Name"></asp:Label>
                                                    <asp:TextBox ID="txtsearchProduct" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                                    <div class="input-group input-group-sm input-group-alt">
                                                        <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control form-control-sm chzn-select" Width="350px"></asp:DropDownList>
                                                        <div class="input-group-append">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" Visible="false" CssClass="input-group-text" OnClick="imgbtnsrchProduct_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-sm btn-primary" Style="margin-top:20px">Select</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lbtnSelectAll_Click" CssClass="btn btn-sm btn-info" Style="margin-top:20px">Select All</asp:LinkButton>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-sm btn-danger" Visible="false"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewProduct" runat="server">
                            <div class="row">
                                <div class="col-md-4">
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-group">
                                            <asp:Label ID="lblProSt" runat="server" CssClass="lblTxt lblName" Text="Product Status" Visible="False"></asp:Label>
                                        </div>
                                    </fieldset>

                                    <asp:GridView ID="gvBudget" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvBudget_PageIndexChanging" OnRowDeleting="gvBudget_RowDeleting"
                                        PageSize="15" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>

                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText="ProdCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodcode")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description Of Item">

                                             <%-- <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True"
                                                                    Font-Size="14px" OnClick="lbtnTotal_Click"
                                                                    CssClass="btn btn-primary primaryBtn" Width="50px">Total :</asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "produnit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Budget</br> Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvbgdQty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="10px"></asp:TextBox>
                                                </ItemTemplate>
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
                                <div class="col-md-8">


                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-group">
                                            <asp:Label ID="lblCost" runat="server" CssClass="lblTxt lblName" Text="Cost Status" Visible="False"></asp:Label>
                                        </div>
                                    </fieldset>


                                    <asp:GridView ID="gvBudgetQty" runat="server" AutoGenerateColumns="False" PageSize="20"
                                        ShowFooter="True" OnRowDataBound="gvBudgetQty_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>

                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="product_ <br> code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvprocode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "product_code")) %>'
                                                        Width="40px" Font-Size="8px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <table style="border: none;">
                                                        <tr>
                                                            <td style="border: none;">
                                                                <asp:Label ID="Label4" runat="server"
                                                                    Text="Description Of Item"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                    <asp:Label ID="LblSircode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvspcfdesc" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Budget</br>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBgdQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Issue Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvIsuQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Required</br>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Minimum</Br>Stock Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMinstock" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minstock")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net Required </Br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNetReqQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netreq")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stock </br>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStcBgdQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Available</br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAvQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avaqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtgvQty" BorderStyle="None" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table style="width: 20px;">
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                                    OnCheckedChanged="chkall_CheckedChanged" /></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkack" runat="server" Width="20px" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>

            <%--<div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="formBtn ">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lnkbtnSaveSupl" runat="server" <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn pull-right " Style="margin: 0 5px;"  ><span class="flaticon-delete47 text-danger "></span>Close</asp:LinkButton> CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;"><i class="fa fa-floppy-o text-primary"></i> Save</asp:LinkButton>
                                            <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn text-danger" OnClick="btnClose_Click" Style="margin: 0 5px;"><i class="fa fa-times text-danger"></i>Close</asp:LinkButton>

                                            <asp:HyperLink ID="lnkbtnAdd" runat="server" CssClass="btn  btn-primary primaryBtn"Style="margin: 0 5px;"  NavigateUrl="~/F_17_Acc/AccInv.aspx">Add</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

