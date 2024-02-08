<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="StoreMtTrnsReqEntry.aspx.cs" Inherits="SPEWEB.F_11_RawInv.StoreMtTrnsReqEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        label {
            margin-bottom: 0rem;
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
                                <label for="FromDate">Date</label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="row">
                                <div class="col-5">
                                    <div class="form-group">
                                        <label for="ToDate">Req. No.</label>
                                        <div class="form-inline text-center">
                                            <asp:Label ID="lblCurTransNo1" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                            <asp:Label ID="txtCurTransNo2" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-7">
                                    <div class="form-group">
                                        <label id="cusRef_field" runat="server" for="ddlUserName">MTRF No.</label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1 text-center" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="control-label d-block" OnClick="lbtnPrevTransList_Click"
                                    TabIndex="3">Previous Order</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-1" runat="server">
                            <div class="form-group mb-0">
                                <label id="lblPage" runat="server" class="control-label" for="ddlUserName">Page Size</label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" 
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
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
                    <div class="row">
                        <div class="col-md-2" runat="server" id="divDdlprjlistfrom">
                            <div class="form-group">
                                <label id="FromDate_field" runat="server" for="FromDate">From Store</label>
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>

                        </div>


                        <div class="col-md-2" runat="server" id="divDdlprjlistto">
                            <div class="form-group">
                                <label id="toStore_field" runat="server" for="toStore">Receive Store</label>
                                <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-2" id="loanpnl2" runat="server">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Order</label>

                                <asp:DropDownList ID="ddlmlccode" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="" Text="Select Order"></asp:ListItem>
                                </asp:DropDownList>


                            </div>
                        </div>

                        <div class="col-md-2" id="loanpnl3" runat="server">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">To Order</label>
                                <asp:DropDownList ID="ddlmlccOrderTo" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>

                            </div>
                        </div>



                        <div class="col-md-3" id="loanpnl" visible="false" runat="server">
                            <div class="form-group mb-0">
                                <label class="control-label" id="Label1" runat="server" for="FromDate">Supplier</label>
                                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-3" id="divDdlLoanNo" visible="false" runat="server">
                            <div class="row">
                                <div class="col-10">
                                    <div class="form-group">
                                        <label class="control-label">Loan No</label>
                                        <asp:DropDownList ID="ddlLoanNo" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2" style="margin-top: 20px;">
                                    <asp:LinkButton runat="server" ID="lbtnSync" OnClick="lbtnSync_Click" CssClass="btn btn-sm btn-primary"><i class='fas fa-sync-alt'></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-1">

                            <div class="form-group">
                                <asp:CheckBox ID="CheckStckChkr" Text="According to stock" Checked="false" Visible="true" CssClass="control-label" runat="server" />
                                <label class="control-label" for="ddlUserName"></label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px">

                    <asp:Panel ID="pnlgrd" runat="server" Visible="False">
                        <div class="row mb-3">

                            <label class="col-md-1 control-label" for="FromDate">Resource List</label>

                            <asp:DropDownList ID="ddlreslist" runat="server" CssClass="col-md-3 chzn-select form-control" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>



                            <label class="col-md-1 control-label" for="FromDate">Specification</label>

                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="col-md-3 chzn-select form-control">
                            </asp:DropDownList>

                            <div class="col-md-4">
                                <asp:LinkButton ID="lnkselect0" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                <asp:Label ID="lblVoucherNo" runat="server" CssClass="lblTxt lblName"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    OnPageIndexChanging="grvacc_PageIndexChanging"
                                    OnRowDeleting="grvacc_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" DeleteText='<span class="fa fa-trash btn-sm" style="color:red; font-size: 10px;"></span>' />



                                        <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMatCode" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>


                                        <%--3--%>
                                        <asp:TemplateField HeaderText="Form Order" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbFoOrder" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Order" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbToOrder" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

                                        </asp:TemplateField>

                                        <%--5--%>
                                        <asp:TemplateField HeaderText="Resource Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

                                        </asp:TemplateField>

                                        <%-- 6--%>
                                        <asp:TemplateField HeaderText="Specification" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Specification" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvspecf" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcf")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="Size" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvSize" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color" >
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvcolor" runat="server" Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                        


                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="Label13" runat="server"
                                                    Style="font-size: 11px; text-align: center;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Quantity">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvBalqty" runat="server" Style="text-align: right"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lblBalqty" runat="server"
                                                    Style="font-size: 11px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                                VerticalAlign="Middle" Font-Size="X-Small" />
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                    Style="text-align: right; font-size: 11px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Visible="false"
                                                    CssClass="btn btn-danger btn-sm primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" Enabled="<%# this.RateEditable() %>" BorderStyle="Solid"
                                                    Style="text-align: right; font-size: 11px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lblamt" runat="server"
                                                    Style="font-size: 11px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                                VerticalAlign="Middle" Font-Size="X-Small" />
                                            <ItemStyle Font-Size="X-Small" />
                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" />

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
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="control-label">Narration</asp:Label>
                                    <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>


                                </div>
                            </div>


                        </div>
                    </asp:Panel>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
