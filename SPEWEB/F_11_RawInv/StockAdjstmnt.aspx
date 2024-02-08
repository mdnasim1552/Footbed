<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="StockAdjstmnt.aspx.cs" Inherits="SPEWEB.F_11_RawInv.StockAdjstmnt" %>

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
    <style>
         .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 26px !important;
            border-radius: 5px !important;
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
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Label ID="lblCurReqNo1" runat="server" CssClass="label" Text="ADJ00"></asp:Label>

                                <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="form-control small form-control-sm disabled readonlyValue" ReadOnly="True" TabIndex="8">00000</asp:TextBox>

                            </div>
                        </div>
                         <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Master LC</asp:Label>
                                <asp:DropDownList ID="ddlmlccod" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccod_SelectedIndexChanged" AutoPostBack="True"  runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Style"></asp:Label>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:Label ID="lblst" runat="server" CssClass="label">Store Name</asp:Label>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                       <%-- <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="LbtnBatch" runat="server" CssClass="label" OnClick="LbtnBatch_Click">Batch</asp:LinkButton>

                                <asp:DropDownList ID="ddlbatchno" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                <asp:HyperLink ID="Addwip" CssClass="btn btn-xs btn-danger" runat="server" Visible="false" Target="_blank"><span class="fa fa-plus"></span></asp:HyperLink>


                            </div>
                        </div>--%>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-7 col-sm-7 col-lg-7">
                            <div class="row" id="propanel" visible="false" runat="server">
                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Material</asp:Label>
                                <asp:DropDownList ID="ddlProduct" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Specification</asp:Label>
                                <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnAdd_Click"><span class="fa fa-check"></span></asp:LinkButton>
                            </div>
                        </div>
                                 <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindReq" runat="server" OnClick="ImgbtnFindReq_Click" CssClass="label"
                                    TabIndex="3">Pre. Req. List</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 d-none ">
                            <div class="form-group">
                                <asp:Label ID="Label3" Visible="false" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddlPrType" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPrType_SelectedIndexChanged" runat="server" Style="width: 100px;" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Selected="True" Value="MAT">MATERIAL</asp:ListItem>
                                    <asp:ListItem Value="PRO">PRODUCT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        

                    </div>
                        </div>
                    </div>



                    
                </div>
            </div>






            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">

                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                            OnRowDeleting="grvacc_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatCode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="280px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specifications">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvSpcfdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Adjust Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resqty")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvReqty" runat="server" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrate" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                            Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" BorderColor="#660033" BorderWidth="0px"></asp:TextBox>
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
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                        VerticalAlign="Middle" Font-Size="12px" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row" id="Narationpanel" runat="server" visible="false">
                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Narration</asp:Label>
                                <asp:TextBox ID="txtReqNarr" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" Height="40px"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


