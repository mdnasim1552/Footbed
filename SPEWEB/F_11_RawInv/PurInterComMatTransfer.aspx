<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurInterComMatTransfer.aspx.cs" Inherits="SPEWEB.F_11_RawInv.PurInterComMatTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mt-0" style="min-height: 150px;">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-2">
                            <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Transfer From"></asp:Label>
                            <asp:Label ID="lblFromCmpName" runat="server" CssClass="form-control form-control-sm "></asp:Label>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                            <asp:Label ID="lblfVoucherNo" runat="server" CssClass="form-control form-control-sm "></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                            <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate"></cc1:CalendarExtender>

                        </div>
                        <div class="col-md-2" style="margin-top: 20px">
                            <asp:RadioButtonList ID="rbtnList1" BorderColor="BlueViolet" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl form-control form-control-sm" RepeatColumns="5">
                                <asp:ListItem>Transfer From</asp:ListItem>
                                <asp:ListItem>Transfer To</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-2 pull-right">
                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblProjectFromList" runat="server">From A/C Head </asp:Label>

                            <div class="ddlListPart">
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lblddlProjectFrom" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Indents Group</asp:Label>
                                <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">

                            <asp:Label ID="lblResList" runat="server">Resource List:</asp:Label>

                            <div class="ddlListPart">
                                <asp:DropDownList ID="ddlreslist" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSpecification" runat="server">Specification</asp:Label>
                            <div class="ddlListPart">
                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAccountHead" runat="server"> Inter Company </asp:Label>
                            <div class="ddlListPart">
                                <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAccHead_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbltoProject" runat="server">To A/C Head </asp:Label>

                            <div class="ddlListPart" style="float: left">
                                <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">

                            <asp:Label ID="lblfQty" runat="server">Qty</asp:Label>
                            <div class=" form-inline">
                                <asp:TextBox ID="txtfqty" runat="server" Width="40%"></asp:TextBox>
                                <div style="margin-left: 5px">
                                    <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-2" style="display: none;">
                            <asp:Label ID="lblAccountHead0" runat="server">To Inter Company</asp:Label>
                            <div class="ddlListPart">
                                <asp:DropDownList ID="ddlAcctoHead" runat="server" CssClass="chzn-select inputTxt">
                                </asp:DropDownList>

                            </div>
                        </div>



                    </div>
                </div>
            </div>


            <div class="card card-fluid mt-0" style="min-height: 250px;">
                <div class="card-body">

                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="501px" OnRowDeleting="grvacc_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcomcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'></asp:Label>
                                    <asp:Label ID="lblgvintfactcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intfactcode")) %>'></asp:Label>
                                    <asp:Label ID="lblgvpactcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>
                                    <asp:Label ID="lblgvrsircode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    <asp:Label ID="lblgvintactcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intactcode")) %>'></asp:Label>
                                    <asp:Label ID="Label2" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tpactcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbgcomnam" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From InterCompany" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbgintfactdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intfactdesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A/C Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgpactdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                        Width="180px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <%--<FooterTemplate>
                                        <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary primaryBtn" OnClick="lnktotal_Click">Total</asp:LinkButton>

                                    </FooterTemplate>--%>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" Transfer Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate" runat="server" BorderStyle="None"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                        Width="100px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblamt" runat="server"
                                        Style="font-size: 11px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                    VerticalAlign="Middle" Font-Size="12px" />
                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inter Company" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbgintactdesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intactdesc")) %>'
                                        Width="130px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-md-2">

                            <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>




                        </div>

                        <div class="col-md-6">
                           
                            <div class="form-group">
                                <asp:Label ID="lblRefNum0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>

                                <%--<asp:TextBox ID="TxtNotes" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                <asp:TextBox ID="txtNarration" TextMode="MultiLine" runat="server" class="form-control" Rows="2" ></asp:TextBox>

                            </div>
                        </div>
                        <%-- <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>

                                    </div>--%>

                        <div class="colMdbtn pading5px" style="display: none;">
                            <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnRefresh_Click">Referesh</asp:LinkButton>

                        </div>
                    </div>




                    <div class="row" style="display: none;">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Transfer To"></asp:Label>
                                    <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="chzn-select inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" Width="300px">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 pading5px">
                                    <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                    <asp:Label ID="lbltVoucherNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:Label>

                                    <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                                    <asp:TextBox ID="txttdate" runat="server" CssClass="inputTxt inputName inPixedWidth120"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttdate_CalendarExtender3" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttdate"></cc1:CalendarExtender>

                                </div>

                            </div>

                        </div>

                    </div>

                    <div class="row" style="display: none;">

                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-2 pading5px asitCol2 ">
                                    <asp:Label ID="lblRefNum1" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                    <asp:Label ID="lbltRefNum" runat="server" CssClass="inputtextbox"></asp:Label>

                                </div>
                                <div class="col-md-4 pading5px">

                                    <asp:Label ID="lblSrInfo0" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                    <asp:TextBox ID="txttSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                </div>
                                <div>
                                    <asp:Label ID="lblComAdd" runat="server" Visible="False"></asp:Label>
                                </div>



                            </div>
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblRefNum2" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txttNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>


                                <%--  <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>

                                    </div>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>






