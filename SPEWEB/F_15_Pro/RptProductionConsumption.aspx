<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptProductionConsumption.aspx.cs" Inherits="SPEWEB.F_15_Pro.RptProductionConsumption" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            $(".multiselect ").addClass("btn-sm");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
            <%--var gvProdCons = $('#<%=this.gvProdCons.ClientID %>');
            gvProdCons.Scrollable();--%>

            $('.chzn-select').chosen({
                search_contains: true,
            });

            $(function() {
                $('[id*=ddlOrderList]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 300,
                })
            });
        }

        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;
            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvProdCons":
                    tblData = document.getElementById("<%=gvProdCons.ClientID %>");
                    break;
            }

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

    </script>

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .multiselect {
            width: 450px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
            font-size:small;
        }

        .multiselect-container>li>a>label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            font-size:12px;
            padding: 2px 2px 2px 2px !important;
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
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Text="From:"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="label" Text="To:"></asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="smLbl_to text-left">Season</asp:Label>
                                <div class="form-inline">
                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" Width="100%" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblRpType" runat="server" CssClass="label">Type:</asp:Label>
                                <asp:DropDownList ID="ddlRType" runat="server" CssClass="form-control form-control-sm">
                                    <asp:ListItem Value="DPR">DPR Wise</asp:ListItem>
                                    <asp:ListItem Value="BOM">BOM Wise</asp:ListItem>
                                    <asp:ListItem Value="DAILY">Day Wise</asp:ListItem>
                                    <asp:ListItem Value="ORDER">Order Wise</asp:ListItem>
                                    <asp:ListItem Value="SUMMARY">Summary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary btn-sm" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
             
                        <div class="col-md-2" style="margin-left:auto">
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control form-control-sm" ID="Searchbox" runat="server" Style="margin-top: 20px;"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-4 form-group">
                            <asp:Label ID="lblArticle" Visible="false" runat="server">Article List</asp:Label>
                            <div class="Multidropdown">
                                <asp:ListBox ID="ddlOrderList" runat="server" Visible="false" SelectionMode="Multiple" CssClass="form-control form-control-sm"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Visible="false" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="600">600</asp:ListItem>
                                    <asp:ListItem Value="1000">1000</asp:ListItem>
                                    <asp:ListItem Value="2000">2000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 600px;">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:MultiView ID="MultiView1" runat="server">

                                <asp:View ID="View2" runat="server">

                                    <div class="table-responsive">

                                        <asp:GridView ID="gvProdCons" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvProdCons_PageIndexChanging" ShowFooter="True">
                                            <RowStyle />

                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Req No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvStyle" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDate" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchord" Width="80px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Order No" onkeyup="Search_Gridview(this,3,'gvProdCons')" CssClass="text-center"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvordno1" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrdname" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchBm" Width="75px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="BOM" onkeyup="Search_Gridview(this,5,'gvProdCons')" CssClass="text-center"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor0" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="P. Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFOrdrqty" runat="server" Font-Size="11px"
                                                            BorderStyle="None" ForeColor="White" Style="text-align: right"
                                                            Width="50px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrdrQty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rproqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchMat" Width="150px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Material Name" onkeyup="Search_Gridview(this,7,'gvProdCons')" CssClass="text-center"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMaterial" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnit" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpecification" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="M.Req. Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFProqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatreqQty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="WH Issue">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFIsuqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIsueQty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Fl. Issue">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFflIsuQty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvflIsuQty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fisuqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bal.(Issue)">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFbalisueqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbalisueqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isubalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bal.(Floor)">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFbalFlorqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbalFlorqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fisubalqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="FG Reced. Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFfgqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvfgqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recfgqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Size="11px" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rem. FG">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFremfgqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" ForeColor="White" Style="text-align: right"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvremfgqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balfgqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>

                                    </div>

                                </asp:View>

                            </asp:MultiView>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





