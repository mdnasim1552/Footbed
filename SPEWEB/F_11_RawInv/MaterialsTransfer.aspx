<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MaterialsTransfer.aspx.cs" Inherits="SPEWEB.F_11_RawInv.MaterialsTransfer" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>

    <style>
        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 29px !important;
            border-radius: 5px !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">Transfer ID</label>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblCurTransNo1" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="txtCurTransNo2" runat="server" CssClass=" form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="txtrefno">Ref No</label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:CheckBox ID="chkGatePass" runat="server" Visible="false" TabIndex="10" Text="From Gate Pass" CssClass=" margin-top30px btn btn-danger checkBox" AutoPostBack="true" OnCheckedChanged="chkGatePass_CheckedChanged" />
                        </div>
                        
                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="margin-top30px btn btn-success" OnClick="lbtnPrevTransList_Click" TabIndex="3">Prev <span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="margin-top30px form-control" TabIndex="6"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlprjlistfrom">From Store</label>
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server" CssClass="chzn-select form-control " AutoPostBack="true" OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group mb-0">
                                <label class="control-label" id="Label3" runat="server" for="FromDate">Supplier</label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true">
                                    <asp:ListItem Value="000000000000">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlprjlistto">To Store</label>
                                <asp:DropDownList ID="ddlprjlistto" runat="server" CssClass="chzn-select form-control "></asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-md-2" id="Order" runat="server" visible="false">
                            <div class="form-group">
                                <label class="control-label" for="ddlmlccode">Select Order</label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" CssClass="chzn-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2" id="Material" runat="server" visible="false">
                            <div class="form-group ">
                                <label class="control-label" id="Label1" runat="server" for="ddlreslist">Materials</label>
                                <asp:DropDownList ID="ddlreslist" runat="server" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlreslist_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-md-2" id="specification" runat="server" visible="false">
                            <div class="form-group ">
                                <label class="control-label" id="Label2" runat="server" for="ddlreslist">Specifications</label>
                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="chzn-select form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1" id="addBtn" runat="server">
                            <asp:LinkButton ID="lnkselect0" Visible="false" runat="server" CssClass="btn btn-primary margin-top30px" OnClick="lnkselect_Click"><span class="fa fa-plus"></span></asp:LinkButton>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="lblpage" runat="server" visible="false" class="control-label" for="ddlUserName">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
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

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" id="ddlGatePasslabel" visible="false" runat="server" for="ddlGatePass">Gate Pass No</label>
                                <asp:DropDownList ID="ddlGatePass" runat="server" CssClass="form-control chzn-select" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="">
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                        OnPageIndexChanging="grvacc_PageIndexChanging"
                        OnRowDeleting="grvacc_RowDeleting" PageSize="15">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl" ControlStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText='<span class="fa fa-trash btn-sm"></span>' />


                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMatCode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    <asp:Label ID="lblgvSpcfCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                    <asp:Label ID="lblgvMlccod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Store Description" Visible ="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStoreDesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Order">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMlcDesc" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <%-- <asp:TemplateField HeaderText="Gate Pass No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgatepref" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Resource Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Specification">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server"
                                        Style="font-size: 11px; text-align: center;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lnktotal" runat="server" Font-Bold="True">Total</asp:Label>

                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Center" />

                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Stock  Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStockqty" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>


                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="GvFQty" runat="server" Font-Bold="True"></asp:Label>

                                </FooterTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                        Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                </ItemTemplate>

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
                                    VerticalAlign="Middle" Font-Size="12px" />

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
                <asp:Panel ID="pnlGatePass" runat="server" Visible="False">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblResListgp" runat="server" CssClass="lblTxt lblName">Resource List:</asp:Label>
                                    <asp:TextBox ID="txtSearchResgp" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnFindResgp" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="lbtnFindResgp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlreslistgp" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlreslistgp_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblSpecificationgp" runat="server" CssClass="lblTxt lblName">Specification</asp:Label>


                                </div>
                                <div class="col-md-2 pading5px">
                                    <asp:LinkButton ID="lnkselectgp" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselectgp_Click">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lnkselectgpAll" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkselectgpAll_Click" Style="margin-left: 2px;">Select All</asp:LinkButton>

                                </div>
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"></asp:Label>

                            </div>

                        </div>
                    </fieldset>
                </asp:Panel>

                <div class="form-group">
                    <div class="col-md-6 pading5px">
                        <div class="input-group">
                            <span class="input-group-addon glypingraddon">
                                <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                            </span>
                            <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                        </div>
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


