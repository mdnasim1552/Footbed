<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ReProductionEntry.aspx.cs" Inherits="SPEWEB.F_15_Pro.ReProductionEntry" %>

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
                                        <asp:Label ID="lblGrrDate" runat="server" CssClass="col-form-label text-dark" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="col-form-label text-dark">Req. No.</asp:Label>
                                        <div class="form-inline text-center">
                                            <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm" Text="RPR00-"></asp:Label>
                                            <asp:Label ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm" Text="00000"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblmrfno" runat="server" CssClass="col-form-label text-dark" Text="Reference"></asp:Label>
                                        <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblBatchWip" runat="server" CssClass="col-form-label text-dark" Text="Batch/WIP:"></asp:Label>
                                        <asp:DropDownList ID="ddlProdinfo" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>             

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindGrrList" runat="server" CssClass="col-form-label" Text="Previous No"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevRPRList" runat="server" CssClass="form-control form-control-sm chzn-select" OnClick="ImgbtnFindGrrList_Click"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-sm btn-danger" Style="margin-top: 20px;" Visible="false"></asp:Label>
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

                                <asp:GridView ID="gvReProd" runat="server" AllowPaging="False"
                                    AutoGenerateColumns="False" OnRowDeleting="gvReProd_RowDeleting"
                                    ShowFooter="True" Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="GRR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrate" runat="server" Style="text-align: left" Visible="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:Label>
                                                <asp:Label ID="lblgrrno" runat="server" Style="text-align: left"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "grrno").ToString() %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="prodcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprodcode" runat="server" Style="text-align: left"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblscode" runat="server" Style="text-align: right"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "scode").ToString() %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Name">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnGenerater" runat="server" CssClass="btn btn-danger primaryBtn"
                                                    OnClick="lbtnGenerate_Click" Style="text-align: center">Generate</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirdesc" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirdesc").ToString() %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lot #" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbatchdesc" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "batchdesc").ToString() %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsqty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvreqqty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                <asp:GridView ID="grvMatList" runat="server" AllowPaging="False"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="prodcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprodcode" runat="server" Style="text-align: right"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblscode" runat="server" Style="text-align: left"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "scode").ToString() %>' Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirdesc" runat="server"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B><br>": "")+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim()%>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfresqty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fresqty")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvnewqty" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "newqty")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
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

                    <asp:Panel ID="PanelOther" runat="server" Visible="false">
                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtOrderNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </fieldset>

                    </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


