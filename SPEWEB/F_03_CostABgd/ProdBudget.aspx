<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProdBudget.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.ProdBudget" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            var gvBudget = $('#<%=this.gvBudget.ClientID %>');
            gvBudget.Scrollable();

            var gvBudgetRate = $('#<%=this.gvBudgetRate.ClientID %>');
            gvBudgetRate.Scrollable();

            var gvBudgetRpt = $('#<%=this.gvBudgetRpt.ClientID %>');
            gvBudgetRpt.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
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
                                <asp:Label ID="lblProcess2" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calendarextender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Budget No</asp:Label>
                                <div class="form-inline">
                                    <%--<asp:Label ID="lblCurNo1" Text="PBM00-" runat="server" CssClass="form-control form-control-sm"></asp:Label>--%>
                                    <asp:Label ID="lblBpn" Text="PBM0000000000" runat="server" CssClass="form-control form-control-sm inputName"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1" style="margin-left:-90px">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4" Style="margin-top: 20px"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1" style="margin-left:-40px">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calendarextender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calendarextender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnPreSearch" runat="server" CssClass="btn-link " OnClick="imgbtnPreSearch_Click">Previous Budget</asp:LinkButton>
                                <asp:DropDownList ID="ddlPreProlist" runat="server" CssClass="form-control chzn-select inputTxt">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-4 col-lg-4" style="margin-top:15px">
                            <asp:RadioButtonList ID="rbtnlist" runat="server" AutoPostBack="True"
                                CssClass="rbtnList1 form-control" BackColor="#DBECD5"
                                OnSelectedIndexChanged="rbtnlist_SelectedIndexChanged" RepeatColumns="6"
                                RepeatDirection="Horizontal" Visible="False">
                                <asp:ListItem>Product Selection</asp:ListItem>
                                <asp:ListItem Style="margin-left: 10px">Material Input</asp:ListItem>
                                <asp:ListItem Style="margin-left: 10px">Reports</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>   

                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewProduct" runat="server">
                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="label">Material Group</asp:Label>
                                        <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnsrchProduct" runat="server" CssClass="label" OnClick="imgbtnsrchProduct_Click">Materials Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <div class="form-inline" style="padding-top: 20px;">
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-info  btn-xs" OnClick="lbtnSelect_Click" ToolTip="Add Table">
                                                <span class="fa fa-check"></span></asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-info  btn-xs" OnClick="lbtnSelectAll_Click" Style="margin-left:5px" ToolTip="Add All">
                                                <span class="fa fa-check-double"></span></asp:LinkButton>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top:25px; margin-left:-90px">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkboxSync" runat="server" OnCheckedChanged="chkboxSync_CheckedChanged" AutoPostBack="true" Text="From Order"></asp:CheckBox>
                                    </div>
                                </div>

                                <div class="col-md-1" style="margin-left:-10px">
                                    <div class="form-group">
                                        <asp:Label ID="LblSeason" runat="server" CssClass="small" Visible="false">Season</asp:Label>
                                        <asp:DropDownList ID="ddlSeason" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblMasterLc" runat="server" CssClass="" Text="Master LC No" Visible="false"> </asp:Label>
                                        <asp:DropDownList ID="ddlBatch" runat="server" Visible="false" Width="250px" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 ml-2" runat="server" id="cellSyncBtn" visible="false">
                                    <br />
                                    <asp:Button Text="Sync" Font-Bold="true" runat="server" OnClientClick="return confirm('Do You want to Sync?');" ID="btnSync" CssClass="btn btn-sm btn-warning" OnClick="btnSync_Click"/>   
                                </div>

                                <div class="col-md-3 pading5px" style="display: none;">
                                    <asp:Panel ID="pnlxcel" runat="server">
                                        <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Excle:"></asp:Label>
                                        <div class="uploadFile">
                                            <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-md-1 pading5px" style="display: none;">
                                    <asp:LinkButton ID="btnexcuplosd" Visible="false" runat="server" OnClick="btnexcuplosd_Click" CssClass="formlbl" ForeColor="Red" Text="Upload Excel"></asp:LinkButton>
                                </div>

                                <div class="col-md-2 pading5px" style="display: none;">
                                    <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="58px"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>600</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="row">
                                <asp:GridView ID="gvBudget" runat="server" AllowPaging="False"
                                    AutoGenerateColumns="False"
                                    OnRowDeleting="gvBudget_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15" ShowFooter="True" Width="504px">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" DeleteText="" ControlStyle-CssClass="fa fa-times text-red btn-xs" />

                                        <asp:TemplateField HeaderText="ProdCode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProdCode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodcode")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProdDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc1")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product_code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvscode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description Of Item">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblgvProdDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc")) %>'
                                                    NavigateUrl='<%# Eval("prodcode", "~/F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode={0}") %>' Target="_blank"
                                                    Width="350px"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "produnit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sales Target" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "targetqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing Stock" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Production Requirments" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nproqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budgeted</br> Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFbgdQty" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbgdQty" runat="server" BackColor="Transparent" BorderStyle="None" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px" Font-Size="11px"></asp:TextBox>
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
                        </asp:View>

                        <asp:View ID="ViewRateInput" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvBudgetRate" runat="server" AllowPaging="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnRowDataBound="gvBudgetRate_RowDataBound"
                                    PageSize="20" ShowFooter="True"
                                    Width="630px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product_code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvscode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of Item">
                                            <HeaderTemplate>
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Item"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalRate" runat="server"
                                                    Font-Size="12px" OnClick="lbtnTotalRate_Click"
                                                    CssClass="btn btn-primary primaryBtn" Width="100px">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "oddesc")) + "</B>"+
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "oddesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                           "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                            Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnFinalRateUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalRateUpdate_Click">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstockqty" runat="server" Font-Size="11px" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRateBgdQty" runat="server" Font-Size="11px" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbgdRate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Budgeted Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFBgdAmount" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRateBgdAmount" runat="server" Font-Size="11px" Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.000;(#,##000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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

                        <asp:View ID="ViewbudgetReport" runat="server">

                            <div class="row">

                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnsrchAllProduct" runat="server" CssClass="label" OnClick="imgbtnsrchAllProduct_Click">Materials Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlAllProduct" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>



                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnSelect1" runat="server" Text="Ok" OnClick="lbtnSelect1_Click" CssClass="btn btn-primary btn-sm" TabIndex="4" Style="margin-top: 20px"></asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <asp:GridView ID="gvBudgetRpt" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="630px" OnRowDataBound="gvBudgetRpt_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of Item">

                                            <ItemTemplate>


                                                <asp:Label ID="lblgvResDescRpt" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")
                                                                    %>'
                                                    Width="250px"></asp:Label>



                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnitRpt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted</br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRateBgdQtyRpt" runat="server" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRateBgdRateRpt" runat="server" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Budgeted</br> Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFBgdAmountRpt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBgdamt" runat="server" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpercentage" runat="server" Font-Size="11px"
                                                    Style="text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
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

                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

