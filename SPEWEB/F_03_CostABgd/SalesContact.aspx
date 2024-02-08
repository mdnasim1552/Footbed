<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true"
    CodeBehind="SalesContact.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.SalesContact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

        function SelectAllCheckboxes(chk) {

            var tblData1 = document.getElementById("<%=gvOrderTerms.ClientID %>");
            var i = 0

            $('#<%=gvOrderTerms.ClientID %>').find("input:checkbox").each(function () {

                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {

                    if (this != chk) {

                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
        }

        function RidirectToLcinfo(url) {
            window.open(url, target = '_blank');
        }
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
                                <asp:Label ID="LblDate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPfiNo" runat="server" CssClass="label">PFI No.</asp:Label>
                                <div class="d-flex pr-0">
                                    <asp:TextBox ID="txtPfiNo1" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="txtPfiNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="OkBtn" OnClick="OkBtn_Click" runat="server" CssClass="btn btn-primary btn-sm mr-1">Ok</asp:LinkButton>
                            </div>
                        </div>
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="small" for="DdlSeason">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblBuyer" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Master L/C"></asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Order Type"></asp:Label>
                                <asp:DropDownList ID="ddlArticle" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnPrevPfi" runat="server" CssClass="text-primary" OnClick="lnkbtnPrevPfi_Click">
                                    <i class="fa fa-search mr-1"></i> Prev. PFI
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevPfi" runat="server" CssClass=" form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblproduct" runat="server" CssClass="label" Text="Customer Order"></asp:Label>
                                <asp:DropDownList ID="ddlprocode" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlprocode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Style-Color-Size"></asp:Label>
                                <asp:DropDownList ID="ddlDetail" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group" style="margin-top: 20px; padding: 0px !important">
                                <asp:LinkButton ID="Add" runat="server" OnClick="Add_Click" ToolTip="Single Size" CssClass="btn btn-sm btn-success small"><span class="fa fa-check"></span></asp:LinkButton>
                                <asp:LinkButton ID="AddAll" runat="server" OnClick="AddAll_Click" ToolTip="All Size" CssClass="btn btn-sm btn-success small"><span class="fa fa-check-double"></span></asp:LinkButton>
                                <asp:LinkButton ID="AddAllOrder" runat="server" OnClick="AddAllOrder_Click" ToolTip="All Order" CssClass="btn btn-sm btn-warning small"><span class="fa fa-check-square"></span></asp:LinkButton>
                                <asp:LinkButton ID="RefBtn" runat="server" OnClick="RefBtn_Click" ToolTip="Refresh" CssClass="btn btn-sm btn-danger small"><span class="fa fa-sync"></span></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Pro Invoice No</asp:Label>
                                <asp:TextBox ID="txtInvoiceno" runat="server" CssClass="form-control form-control-sm" placeholder="AW22/XXXX/0001"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblDiscount" runat="server" CssClass="label">Discount %</asp:Label>
                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control form-control-sm" type="number" placeholder=""></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPrep" runat="server" CssClass="label">Pre-Payment %</asp:Label>
                                <asp:TextBox ID="txtPrep" runat="server" CssClass="form-control form-control-sm" type="number" placeholder=""></asp:TextBox>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <asp:GridView ShowFooter="true" ID="gvSalCon" runat="server" OnRowDeleting="gvSalCon_RowDeleting" OnRowDataBound="gvSalCon_RowDataBound" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvstyle" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvColor" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvsize" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cus Order No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCustorderno" runat="server" BorderColor="#549de5" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: left; background-color: transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custordno")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust Ref no">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCustname" runat="server" BorderColor="#549de5" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custrefno")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvOrdrNo" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvLast" runat="server" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lastformadesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article NO">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvart" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbAddMore" runat="server" CommandArgument="lbAddMore"
                                            OnClick="AddMore_Click" Width="30px" CssClass="text-facebook"><i class="fa fa-plus"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFNetTotal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvordrqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFNetTotal1" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvordrqty1" runat="server" BorderColor="#549de5" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty1")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvPrice" runat="server" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvValue" runat="server" Style="text-align: right;"
                                            Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty1"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate"))).ToString("#,##0.00;") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HS CODE" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvHsCode" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hscode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDelvdate" runat="server" BorderColor="#549de5" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "delvdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="60px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvDelvdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy"
                                            TargetControlID="txtgvDelvdate"></cc1:CalendarExtender>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle />
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>



                    <asp:Label ID="Label4" runat="server" CssClass="" Text="Terms &amp; Conditions:"></asp:Label>




                    <asp:GridView ID="gvOrderTerms" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"  OnRowDeleting="gvOrderTerms_RowDeleting"
                        CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="18px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>'
                                        Width="150px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" Font-Size="16px"
                                        Text=" : "></asp:Label>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>'
                                        Width="650px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: left; background-color: Transparent"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>'
                                        Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCol" runat="server" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>



            </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
