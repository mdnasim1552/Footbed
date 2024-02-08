<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProdReq.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProdReq" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvBudgetRpt = $('#<%=this.gvBudgetRpt.ClientID %>');
            gvBudgetRpt.Scrollable();

            var gvCost = $('#<%=this.gvCost.ClientID %>');
            gvCost.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtbgddate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblcurVounum" runat="server" CssClass="label">DPR No</asp:Label>
                                <div class="form-inline">
                                    <asp:TextBox ID="lblCurReqNo1" Text="DPR00-" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txtCurReqNo2" Text="00000" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm  small" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnBatsearch" runat="server" CssClass="label" OnClick="imgbtnBatsearch_Click">Order No</asp:LinkButton>
                                <asp:DropDownList ID="ddlBatch" runat="server" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Plan No</asp:Label>
                                <asp:DropDownList ID="ddlplan" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">

                            <asp:Label ID="Notic" runat="server">Missing Material/Specifications are identified with </asp:Label>

                            <div style="width: 20px; height: 20px; background-color: lightcoral; float: right"></div>

                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">

                            <div class="form-group">

                                <%--  <asp:TextBox ID="txtbatchno0" runat="server" CssClass=" inputBox50px" TabIndex="1" Visible="false"></asp:TextBox>--%>

                                <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="control-label" OnClick="imgbtnPreDPR_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlDPR" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDPR_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4 pading5px" style="display: none;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="lblTxt lblName" ForeColor="Black">Line Number</asp:LinkButton>

                                <div class="ddlListPart" style="float: left;">
                                    <asp:DropDownList ID="ddlLine" runat="server" CssClass=" inputTxt chzn-select" Width="250px">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>






            <div class="card card-fluid">
                <div class="card-body" style="min-height: 600px;">
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewbudgetReport" runat="server">
                            <div class="row">

                                <div class="col-md-5">
                                    <div style="height: 300px;">
                                        <asp:GridView ID="gvBudgetRpt" runat="server"
                                            AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvBudgetRpt_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description Of Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="10px"
                                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' Width="180px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcolorid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                            Width="35px"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnitRpt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCurbudget" runat="server" Font-Size="10px" Style="text-align: right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Req </br> Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblgvRProdqty" runat="server" Font-Size="12px" Style="text-align: right;" OnKeyUp="GoToNextTextBox(this, event); return false;"
                                                            BackColor="Transparent" BorderStyle="Solid" BorderWidth="1px" BorderColor="Teal" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cresqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFRProdqty" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="50px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                    <HeaderStyle ForeColor="Red" />
                                                    <FooterStyle ForeColor="Red" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>

                                    <div style=" width: 85%;">
                                        <asp:Label ID="lblSubContractor" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="14px">N.B: If you want to production this article into other factory, <br />please check 
                                             [<asp:CheckBox ID="chkSubContract" runat="server" AutoPostBack="true" OnCheckedChanged="chkSubContract_CheckedChanged" />]  and select the desired sub-contructor.</asp:Label>
                                    </div>

                                    <div style="margin-top:10px; width: 60%;">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSupplierName" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-7">
                                    <div style="height: 720px;">
                                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                                            OnRowDataBound="gvCost_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1c" runat="server" Font-Bold="True" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDeptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procname")) %>'
                                                            Width="100px" ForeColor="RoyalBlue" Font-Bold="true" Font-Size="9px"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description Of Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="100px" Font-Size="9px"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcResUnitRpt" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                            Width="25px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cons.Pair">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvcConsPair" runat="server" Font-Size="10px" Style="text-align: right;"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consppair")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Wastage<br>(%)" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvcWastage" runat="server" Font-Size="10px" Style="text-align: right;"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prwestpc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Requisition</br> Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvcRProdqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                                    <HeaderStyle ForeColor="Green" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock </br> Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcstockqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="50px"></asp:Label>
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
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

