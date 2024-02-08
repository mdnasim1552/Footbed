<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="Material_Issue.aspx.cs" Inherits="SPEWEB.F_11_RawInv.Material_Issue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".DeleteClick").click(function () {
                if (!confirm("Do you want to delete")) {
                    return false;
                }
            });
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
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                                <asp:Label ID="Label6" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblrefNo" runat="server" CssClass="label">Ref. No.</asp:Label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo2" runat="server" CssClass="label" Text="Issue No."></asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" Text="ISU00-" CssClass="form-control form-control-sm small"></asp:Label>
                                    <asp:Label ID="txtCurNo2" runat="server" Text="00000" CssClass="form-control form-control-sm small" Style="margin-right:20px"></asp:Label>
                                    &nbsp;
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="label" OnClick="ImgbtnFindPrevious_Click"
                                    TabIndex="3">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPreList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">List (Customer/User)</asp:Label>
                                <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Store Name</asp:Label>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        

                    </div>


                    <div class="row" id="PanelSub" runat="server" visible="false">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Indents Group</asp:Label>
                                <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>

                      
                        <%--<div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lblProcess0" runat="server" CssClass="label" OnClick="imgbtnResourceCost_Click" Text="Materials Name"></asp:LinkButton>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="ddlResourcesCost" AutoPostBack="true" OnSelectedIndexChanged="ddlResourcesCost_SelectedIndexChanged" runat="server" CssClass="form-control chzn-select">
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Specifications</asp:Label>


                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlSpecres" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>

                                </div>
                            </div>

                        </div>--%>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Indents List</asp:Label>

                                <asp:DropDownList ID="ddlResList" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Specification</asp:Label>

                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Con. unit</asp:Label>

                                <asp:DropDownList ID="ddlunit" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnSelect_Click"><span class="fa fa-check"></span></asp:LinkButton>

                                <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>
                                <asp:Label ID="lblddlProjectTo" runat="server" CssClass="inputtextbox" Visible="False" Width="295px"></asp:Label>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="card card-fluid">
                    <div class="card-body" style="min-height: 400px;">
                        <div class="row">
                            <asp:GridView ID="gvIssue" runat="server" OnRowDataBound="gvIssue_RowDataBound" AutoGenerateColumns="False" OnRowDeleting="gvIssue_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="501px">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                    <%--<asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger DeleteClick" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />--%>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource Description">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="200px" Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="220px"></asp:Label>
                                            <asp:Label ID="lblgvspcfcod" Visible="false" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Department Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvempname" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv. Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunit" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstkqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvReqqty" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="#8492ed"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvconqty" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="#8492ed"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                    <asp:TemplateField HeaderText="Con Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvissueqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--         <asp:TemplateField HeaderText="Refund">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvrefundqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvBalqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty"))-Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvstkrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvamt" runat="server"
                                                Style="font-size: 11px; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            HorizontalAlign="right" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvremarks" runat="server" Style="text-align: left" BorderStyle="Solid" BorderWidth="1" BorderColor="#8492ed"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="150px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                        <div class="col-md-5" style="margin-top: 20px">
                            <asp:Panel ID="PnlRemarks" runat="server" Visible="false">
                                <asp:Label ID="LblRemarks" runat="server" CssClass="form-label">Narration</asp:Label>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm"></asp:TextBox>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
