<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptOProVsShip.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.RptOProVsShip" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
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


    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 30px !important;
        }
    </style>



    <div class="card card-fluid mb-1">
        <div class="card-body">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>




                    <div class="row">


                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">

                                <asp:Label ID="Label6" runat="server" CssClass="label">Order No.</asp:Label>

                                <asp:TextBox ID="txtOrdsrch" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>

                                <%--<asp:LinkButton ID="imgbtnFindOrd" CssClass="btn btn-primary btn-sm pull-left" runat="server" OnClick="imgbtnFindOrd_Click" TabIndex="2"><span class="fa fa-search"></span></asp:LinkButton>--%>
                            </div>

                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnFindOrd" CssClass="btn btn-primary btn-sm pull-left" Style="margin-top: 21px; margin-left: -15px;" runat="server" OnClick="imgbtnFindOrd_Click" TabIndex="2"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="chzn-select chzn-single form-control form-control-sm" runat="server" AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-3 col-sm-3 col-lg-3" style="margin-top: 20px;">

                            <div class="form-group">

                                <asp:DropDownList ID="ddlOrder" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" TabIndex="18"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" Style="margin-top: 20px;" OnClick="lbtnOk_Click" TabIndex="8">Ok</asp:LinkButton>
                            </div>
                        </div>



                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: 50px;">

                            <div class="form-group">

                                <asp:Label ID="Label7" runat="server" CssClass="label">Date</asp:Label>

                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small" AutoCompleteType="Disabled"></asp:TextBox>

                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>
                        </div>



                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: 50px;">

                            <div class="form-group">

                                <asp:Label ID="lblPage" runat="server" CssClass=" label">Page Size</asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="form-control form-control-sm">

                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>

                                </asp:DropDownList>

                            </div>

                        </div>


                    </div>



                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </div>



    <div class="card card-fluid">
        <div class="card-body" style="min-height: 500px;">

            <asp:MultiView ID="MultiView1" runat="server" >


                <asp:View ID="View2" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                        ShowFooter="True" Style="text-align: left" Width="500px">
                        <PagerSettings Position="Top" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Style">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStyle" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor0" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFOrdrqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOrdrQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Production Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFShpqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvShpQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance(Production)">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFBProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="90px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvBProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balpro")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance(Shipment)">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFBShpqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="90px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvIBShpQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balship")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </asp:View>


                <asp:View ID="View1" runat="server">

                    <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvAnalysis_PageIndexChanging"
                        ShowFooter="True" Style="text-align: left" Width="840px">
                        <PagerSettings Position="Top" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Style">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStyle" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor0" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFOrdrqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOrdrQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Production Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment Qty">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFShpqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvShpQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance(Production)">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFBProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="90px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvBProQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balpro")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance(Shipment)">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFBShpqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                        Width="90px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvIBShpQty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balship")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />

                    </asp:GridView>

                </asp:View>



                <asp:View ID="ViewProductVsCon" runat="server">

                    <asp:Label ID="Label8" runat="server" CssClass="lable" Font-Bold="True">Style</asp:Label>

                    <div class="row">

                        <asp:GridView ID="gvStyle" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left; margin-left: 10px; margin-bottom: 20px;" Width="500px">
                            <PagerSettings Position="Top" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description Of Items">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFOrdrqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrdrQty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFProqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProQty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </div>


                    <asp:Label ID="Label9" runat="server" CssClass="label" Font-Bold="True">Materials</asp:Label>

                    <div class="row">


                        <asp:GridView ID="gvRes" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="True"
                            AutoGenerateColumns="False" ShowFooter="True" Style="text-align: left; margin-left: 10px" Width="800px" OnPageIndexChanging="gvRes_PageIndexChanging">
                            <PagerSettings Position="Top" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid1" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description Of Items">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Master Budget">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFOrdrqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrdrQty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bud.Qty As Per Prod">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFProqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProQty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Isuqty Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFIsuepqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIsuepqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Variance Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFVarqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Font-Bold="true" ForeColor="Black" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvVarqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Variance(%)">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPerVar" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pervar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>

                </asp:View>

            </asp:MultiView>

        </div>

    </div>


</asp:Content>





