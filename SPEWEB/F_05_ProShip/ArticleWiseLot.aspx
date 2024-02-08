<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ArticleWiseLot.aspx.cs" Inherits="SPEWEB.F_05_ProShip.RptLotWiseArticle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function confirmDelete() {
            return confirm('Are you sure you want to delete item?');
        }

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Master LC</asp:Label>
                                <asp:DropDownList ID="ddlmlccod" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccod_SelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Style"></asp:Label>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1" id="seePlanlink" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:HyperLink ID="LinkButtonSeePlan" runat="server" Text="Order Plan" Target="_blank"  CssClass="btn btn-success btn-sm" TabIndex="4"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-md-2" id="OrderDeatilslink" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:HyperLink ID="LbtnOrderDetails" runat="server" Target="_blank" CssClass="btn btn-sm btn-warning text-white"><span class="fa fa-file-invoice"></span> Order Details</asp:HyperLink>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">

                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" OnRowDataBound="gv1_RowDataBound" OnRowDeleting="gv1_RowDeleting" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>

                                    <%--<asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDel"  CssClass="btn btn-default  btn-xs" ToolTip="Delete" runat="server"><span  style="color:red"><i class="fas fa-trash"></i></span> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDel" runat="server" ToolTip="Delete" Style="color: red" class="fas fa-trash"
                                                CommandName="Delete" OnClientClick='<%# "return confirmDelete();" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Buyer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorID" Visible="false" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblGvCustomer" runat="server" Style="text-transform: capitalize; text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Article Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>

                                    

                                    <asp:TemplateField HeaderText="Color/Lot">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                Width="91px"></asp:TextBox>
                                            <asp:DropDownList ID="DdlLotlist" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                   <%-- <asp:TemplateField HeaderText="Slnum" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslnum" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>

                                    <%--Size starts--%>
                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF1" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF2" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF3" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF4" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF5" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF6" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF7" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF8" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF9" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF10" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF11" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF12" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF13" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF14" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF15" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF16" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF17" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF18" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF19" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF20" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--Size Ends--%>

                                     <asp:TemplateField HeaderText="Slnum" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslnum" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color QTY" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbAddMore" runat="server" Visible="false" CommandArgument="lbAddMore" OnClick="lbAddMore_Click"
                                                Width="30px" CssClass="text-info"><i class="fa fa-plus"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                        <div class="table-responsive" style="margin-top: 10px">
                            <asp:GridView ID="gv1ratio" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sizes">

                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnPush" OnClick="lbtnPush_Click" runat="server" Font-Bold="True" Font-Size="12px"
                                                CssClass="btn btn-success btn-sm">Generate</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtgvTotal1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:TextBox>

                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <div class="clearfix">
                            </div>
                            <br />
                        </div>

                        <div>
                            <asp:Label ID="LblMessage" Visible="false" runat="server" CssClass="alert alert-info"></asp:Label>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function uploadComplete(sender) {
            // $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            //  $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }

    </script>
</asp:Content>

