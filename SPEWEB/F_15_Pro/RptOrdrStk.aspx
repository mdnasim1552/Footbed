<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptOrdrStk.aspx.cs" Inherits="SPEWEB.F_15_Pro.RptOrdrStk" %>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">

                                <asp:Label ID="Label6" runat="server" CssClass="label"> Order No. </asp:Label>

                                <asp:TextBox ID="txtOrdsrch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                <%-- <asp:LinkButton ID="imgbtnFindOrd" CssClass="btn btn-primary btn-sm" runat="server" OnClick="imgbtnFindOrd_Click"><span class="fa fa-search"></span></asp:LinkButton>--%>
                            </div>

                        </div>


                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">

                                <asp:LinkButton ID="imgbtnFindOrd" CssClass="btn btn-primary btn-sm" Style="margin-top: 21px; margin-left: -15px;" runat="server" OnClick="imgbtnFindOrd_Click"><span class="fa fa-search"></span></asp:LinkButton>

                            </div>

                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3" Style="margin-top: 21px;">

                            <div class="form-group">

                                <asp:DropDownList ID="ddlOrder" runat="server"  CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>

                                <%--<asp:LinkButton ID="lbtnOk" runat="server" Style="" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>--%>
                            </div>

                        </div>


                        <div class="col-md-1 col-sm-1 col-lg-1">

                            <div class="form-group">

                                <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 21px; margin-left: -10px;" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>

                        </div>



                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: -60px;">


                            <div class="form-group">

                                <asp:Label ID="Label7" runat="server" CssClass="label"> From </asp:Label>

                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>


                            </div>

                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: 20px;">


                            <div class="form-group">

                                <asp:Label ID="Label8" runat="server" CssClass="label">To</asp:Label>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            </div>

                        </div>


                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: 70px;">

                            <div class="form-group">

                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="card card-fluid">

        <div class="card-body">

            <div class="row" style="min-height: 400px;">

                <asp:GridView ID="gvStk" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" OnPageIndexChanging="gvStk_PageIndexChanging"
                    ShowFooter="True" Style="text-align: left"  Width="500px">
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

                        <asp:TemplateField HeaderText="Description Of materials">
                            <ItemTemplate>
                                <asp:Label ID="lblgvMat" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                    Width="250px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specification">
                            <ItemTemplate>
                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblgvUnit" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Budgeted Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFbomqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvbomqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFOpnqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvOpnQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receved Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFRcvqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvRcvQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transfer In">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFTinqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvTinQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transfer Out">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFToutQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvToutQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Received">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFTRecqtq" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvTRecQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remaining Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFremaqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvremaqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remaqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issue Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFIsueqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvIsueQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Qty">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFBalqty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvBalQty" runat="server" BackColor="Transparent"
                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acstock")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
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



</asp:Content>




