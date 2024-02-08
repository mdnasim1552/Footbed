<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="FloorMatIssue.aspx.cs" Inherits="SPEWEB.F_15_Pro.FloorMatIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>--%>
    <script type="text/javascript" language="javascript">
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




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblrefNo" runat="server" CssClass="label">Ref. No.:</asp:Label>
                                <asp:TextBox ID="txtlSuRefNo" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo2" runat="server" CssClass="label" Text="Issue No:"></asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" Text="ISU00-" CssClass="form-control form-control-sm small"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" Text="00000" CssClass="form-control form-control-sm small"></asp:Label>
                                    &nbsp;
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>



                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Order :</asp:Label>
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlStoreName" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                                <asp:Label ID="lblStoredesc" runat="server" CssClass="form-control form-control-sm" Visible="false">Order</asp:Label>
                                <%--<asp:LinkButton ID="LinkButton1" runat="server" Visible="false" CssClass="smLbl_to" OnClick="imgbtnStorid_Click">Store :</asp:LinkButton>

                                <asp:DropDownList ID="ddlStore" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnPreIssueList" runat="server" CssClass="label" OnClick="ibtnPreIssueList_Click"
                                    TabIndex="3">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevIssueList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="panel11" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblReqList" runat="server" CssClass="label">Req. Number</asp:Label>
                                    <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindReqList" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" OnClick="ImgbtnFindReqList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlReqList" OnSelectedIndexChanged="ddlReqList_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="label">Process List</asp:Label>

                                    <asp:DropDownList ID="ddlprocess" OnSelectedIndexChanged="ddlprocess_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblMatList" runat="server" CssClass="label">Material List</asp:Label>
                                    <asp:TextBox ID="txtMatSearch" runat="server" CssClass=" inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>

                                    <asp:DropDownList ID="ddlMatList" OnSelectedIndexChanged="ddlMatList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>

                                </div>

                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="label">Specification</asp:Label>
                                    <asp:DropDownList ID="ddlspcflist" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="Con Unit"></asp:Label>
                                    <asp:DropDownList ID="ddlunit" runat="server" CssClass="smDropDown inputTxt chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to small" Text="Prod Process"></asp:Label>
                                    <asp:DropDownList ID="ddlprodProcess" runat="server"  CssClass="smDropDown inputTxt chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lbtnSelectReqList" runat="server" CssClass="btn btn-primary btn-sm small"
                                        OnClick="lbtnSelectReqList_Click"><span class="fa fa-check"></span></asp:LinkButton>
                                    <asp:LinkButton ID="lnkSelectAll" runat="server" CssClass="btn btn-primary btn-sm small" OnClick="lnkSelectAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>

                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Label ID="lblPage" runat="server" CssClass="label" Visible="false">Page size</asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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


                    </asp:Panel>
                </div>
            </div>




            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <asp:Panel ID="IssuePanel" runat="server">
                            <asp:GridView ID="gvMatIssue" runat="server" AllowPaging="True" OnRowDataBound="gvMatIssue_RowDataBound"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvMatIssue_PageIndexChanging" OnRowDeleting="gvMatIssue_RowDeleting"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                    <asp:TemplateField HeaderText="Req No1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prod Process">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcprodproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodprocdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcspcfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <%--   <asp:TemplateField HeaderText="Cons <br> P. Pair">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvConPair" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consppair")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>--%>



                                    <asp:TemplateField HeaderText="Rec. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcBalqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whisuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcRemqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvconqty" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="#8492ed"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConunit" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conuntdesc")) %>'
                                                Width="50px"></asp:Label>
                                            <asp:Label ID="lblConunt" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conunt")) %>'></asp:Label>
                                            <asp:Label ID="lbluntcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "untcod")) %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>

                                            <asp:TextBox ReadOnly="true" ID="lgvIsuQty" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Rate" Visible="false">
                                        <ItemTemplate>

                                            <asp:TextBox ID="lgvIsuRate" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />

                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>



                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="StockAlertPanel" runat="server" Visible="false">
                            <asp:GridView ID="gvStockAlert" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcBatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Process Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcspcfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv. Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlbUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstockqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcBalqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>



                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblName lblTxt"
                                                Text="Remarks:"></asp:Label>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="45px"
                                                TextMode="MultiLine" Width="400px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>




                        </asp:Panel>

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  </asp:UpdatePanel>--%>
</asp:Content>


