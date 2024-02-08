<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MatAvailabilityFG.aspx.cs" Inherits="SPEWEB.F_15_Pro.MatAvailabilityFG" %>


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
                                <asp:Label ID="Label3" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Order</asp:Label>
                                <asp:DropDownList ID="ddlOrder" runat="server" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnsrchProduct" runat="server" CssClass="label" OnClick="imgbtnsrchProduct_Click">Product</asp:LinkButton>
                                <asp:DropDownList ID="ddlProduct" runat="server"  CssClass="chzn-select form-control form-control-sm" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">

                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

             <div class="card card-fluid">
                <div class="card-body">
                   
                    
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
                                            <asp:TemplateField HeaderText="Sl">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                            <asp:TemplateField HeaderText="ProdCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodcode")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description Of Item">
                                                <FooterTemplate>
                                                    <asp:Label ID="lTotal" runat="server" >Total</asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "produnit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted</br> Qty">
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
                                            <asp:Label ID="lblCost" runat="server" Style="color: brown" CssClass="lblTxt " Text="Cost Status ( *** Wastage Included)" Visible="False"></asp:Label>

                                        </div>
                                    </fieldset>


                                    <asp:GridView ID="gvBudgetQty" runat="server" AutoGenerateColumns="False" PageSize="20"
                                        ShowFooter="True" OnRowDataBound="gvBudgetQty_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="product_ <br> code" Visible="false">
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
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server">Export &nbsp;<span class="fa fa-folder"></span></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                    <asp:Label ID="LblSircode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="130px"></asp:Label>
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
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted</br> Qty">
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
                                            <asp:TemplateField HeaderText="Required </br>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqQty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Minimum </Br> Stock Qty">
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
                                            <asp:TemplateField Visible="false">
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
                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

