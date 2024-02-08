<%@ Page Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurchaseReturn.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurchaseReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-0" style="margin-left: 10px">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">Return No.</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm" Text="PRT00-" ReadOnly="True" TabIndex="2"></asp:Label>
                            </div>
                        </div>

                        <asp:Label ID="lblrefNo0" runat="server" CssClass="form-label" Text="Ref. No" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                        <div class="col-md-1" style="margin-top: 20px; margin-left: -5px">
                            <asp:TextBox ID="txtCurNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" TabIndex="3">00000</asp:TextBox>
                        </div>

                        <div class="col-md-0">
                            <div class="form-group">
                                <asp:Label ID="lblDate" runat="server" CssClass="form-label" Text="Return Date"></asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSlctStore" runat="server" CssClass="form-label">Store</asp:Label>
                                <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lbSupplier" runat="server" CssClass="form-label">Supplier</asp:Label>
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="8" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 mt-3" style="margin-left: auto;">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-2" style="margin-right: auto;">
                            <div class="form-group">
                                <asp:LinkButton ID="imgPreVious" runat="server" CssClass="form-label" OnClick="imgPreVious_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="6" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lblSalesOrder" Visible="false" runat="server" CssClass="form-label" OnClick="imgSearchOrder_Click" ForeColor="Blue">Items</asp:LinkButton>
                                <asp:TextBox ID="txtSrchOrder" runat="server" CssClass="form-control form-control-sm" TabIndex="10" Visible="false"></asp:TextBox>

                                <div class="colMdbtn">
                                    <asp:LinkButton ID="imgSearchOrder" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false" OnClick="imgSearchOrder_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <asp:DropDownList ID="ddlIItems" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlIItems_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSpecifications" Visible="false" runat="server" CssClass="form-label">Specifications</asp:Label>
                                <asp:DropDownList ID="ddlSpecf" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSpecf_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="padding-top: 20px;">
                                <asp:LinkButton ID="LbtnAdd" Visible="false" runat="server" CssClass="btn btn-sm btn-primary" OnClick="LbtnAdd_Click">Select</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2" style="margin-left: -10px;">
                            <div class="form-group">
                                <asp:Label ID="LblCstmRef" runat="server" CssClass="form-label" Visible="false">Custom Reference</asp:Label>
                                <asp:TextBox ID="txtCustomRef" runat="server" CssClass="form-control form-control-sm" Visible="false">
                                </asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row pr-0">
                        <div class="col-md-12">

                            <asp:GridView ID="gvRetInfo" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                AutoGenerateColumns="False"
                                ShowFooter="True">
                                <PagerSettings Position="Bottom" />
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Height="30px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Procode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprocode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batchcode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbatchcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Items">
                                        <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResDesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))
                                                                            %>'
                                                Width="250px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specifications">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfDesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="300px">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvitmqty" runat="server" BackColor="Transparent"
                                                BorderStyle="Solid" BorderColor="ForestGreen" BorderWidth="1" Font-Size="11px" Style="text-align: right" Target="_blank"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Txtrate" runat="server" BackColor="Transparent"
                                                BorderStyle="Solid" BorderColor="ForestGreen" BorderWidth="1" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvItmamt" runat="server" BackColor="Transparent"
                                                Font-Size="11px" Style="text-align: right; color: red;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <%-- <asp:TemplateField HeaderText="Invoice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInvamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invamt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblgvFInvamt" Font-Size="11px" Style="text-align: right" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>--%>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>

                        <div class="col-md-6">
                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <asp:Label ID="lblReqNarr" runat="server" CssClass="form-label" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txtBillNarr" runat="server" class="form-control form-control-sm" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </asp:Panel>
                        </div>

                    </div>
                    <!-- End of Content Part-->
                </div>
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
