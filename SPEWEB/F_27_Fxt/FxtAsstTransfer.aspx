<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="FxtAsstTransfer.aspx.cs" Inherits="SPEWEB.F_27_Fxt.FxtAsstTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .fontbold {
            font-weight: bold !important;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-1 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-1 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">TRAN ID</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurTransNo1" runat="server" CssClass="form-control form-control-sm" Style="width: 40px"></asp:Label>
                                    <asp:Label ID="txtCurTransNo2" runat="server" CssClass="form-control form-control-sm" Style="width: 40px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server">From Location</asp:Label>
                                <asp:DropDownList ID="ddlflocation" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlflocation_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">From User</asp:Label>
                                <asp:DropDownList ID="ddlfuser" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlfuser_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-1 col-md-1" style="margin-top: 20px">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-1 col-md-1">
                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-sm btn-danger" Visible="false"></asp:Label>
                        </div>

                        <div class="col-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblPreList" runat="server" Visible="false" Text="Prev. List"></asp:Label>
                                <asp:DropDownList ID="ddlPrevISSList" Visible="false" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">

                    <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                        <div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server">To Location</asp:Label>
                                    <asp:DropDownList ID="ddltolocation" runat="server" Width="200" CssClass="form-control form-control-sm" TabIndex="6"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server">To User</asp:Label>
                                    <asp:DropDownList ID="ddltouser" runat="server" Width="200" CssClass="form-control form-control-sm" TabIndex="6"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server">Material</asp:Label>
                                    <asp:DropDownList ID="ddlmaterial" runat="server" Width="200" CssClass="form-control form-control-sm" TabIndex="6"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkselect0" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkselect_Click">Select</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnRowDeleting="grvacc_RowDeleting" ShowFooter="True" Width="501px" PageSize="15">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblrsircode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblfpactcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fpactcod")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblfempid" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fempid")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbltpactcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tpactcod")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbltuser" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tuser")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Asset Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbresdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="To Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbtpactdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tpactdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="To User">
                                <ItemTemplate>
                                    <asp:Label ID="lbtempnam" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tempnam")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Labsirunit" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Stock">
                                <ItemTemplate>
                                    <asp:Label ID="LabsirunitX" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stk")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Trans. Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True"
                                        CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>

                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



