<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ConsumptionSizeAdd.aspx.cs" Inherits="SPEWEB.F_01_Mer.ConsumptionSizeAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        window.onload = function () {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                //console.log("Position"+strPos);
            }
        }
        function SetDivPosition() {
            var intY = document.getElementById("grdWithScroll").scrollTop;
            console.log(intY);
            document.cookie = "yPos=!~" + intY + "~!";
        }

        function SetPosition() {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                console.log("Position" + strPos);
            }
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

         <%--   var gv1 = $('#<%=this.gvpurorder.ClientID %>');
            gv1.Scrollable();--%>
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });

            $(function () {
                $('[id*=ddlResSpcf]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    maxHeight: 250

                })
                $(".Multidropdown button").addClass("multiselect dropdown-toggle btn btn-default btn-sm");
            });


        };




    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .Multidropdown ul {
            top: -47px !important;
        }

        .Multidropdown b.caret {
            display: none !important;
        }

        .Multidropdown ul.dropdown-menu {
            min-width: 21rem;
        }

        .Multidropdown .multiselect-container > li > a > label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 3px 2px 3px 2px;
            font-size: 12px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
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

                    <div class="row">
                        <div class="col-md-1 hidden">
                            <asp:Label ID="lCurReqdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                            <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                        </div>
                        
                        <div class="col-md-1">
                            <asp:Label ID="lblSeason" runat="server" > Season </asp:Label>
                            <asp:DropDownList  runat="server" ID="ddlSeason" AutoPostBack="true" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged" CssClass="chzn-select form-control inputTxt" TabIndex="4"></asp:DropDownList>
                        </div>
                        
                        <div class="col-md-2 ">
                            <asp:LinkButton ID="ImgbtnFindOrder" CssClass="lblTxt lblName" runat="server" OnClick="ImgbtnFindOrder_Click" TabIndex="2" Text="Order No"></asp:LinkButton>
                            <asp:DropDownList ID="ddlOrderList" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            <asp:Label ID="lblddlOrder" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblStyle" runat="server" > Style </asp:Label>
                            <asp:DropDownList ID="ddlStyle" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" TabIndex="4"></asp:DropDownList>
                        </div>
                        
                        <div class="col-md-2" style="margin-top: 19px">

                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            <asp:HyperLink ID="HypMainorder" CssClass="btn btn-sm btn-warning" Target="_blank" runat="server">Order <span class="fa fa-print"></span></asp:HyperLink>
                            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                            <asp:HyperLink ID="HypBOM" CssClass="btn btn-sm btn-success" Target="_blank" runat="server">BOM <span class="fa fa-print"></span></asp:HyperLink>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblByername" runat="server" CssClass="label">Buyer Name:</asp:Label>
                            <asp:Label ID="BuyerName" runat="server" CssClass="form-control form-control-sm"></asp:Label>

                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblOrderqy" runat="server" CssClass="label">Order Qty</asp:Label>
                            <asp:Label ID="ordqty" runat="server" CssClass="form-control form-control-sm"></asp:Label>

                        </div>
                        <div class="col-md-1" style="margin-top: 19px">

                            <div title="Missing Material/Specifications in BOM" style="width: 30px; height: 30px; background-color: lightcoral; float: left"></div>
                            <div title="Sizeble Material/Specifications" style="width: 30px; height: 30px; background-color: lightskyblue; float: right"></div>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server" CssClass=" label " Text="Material"></asp:Label>

                                <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Specifications"></asp:Label>
                                <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                    <asp:ListBox ID="ddlResSpcf" SelectionMode="Multiple" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="FG Sizes"></asp:Label>

                                <asp:DropDownList ID="ddlSizes" runat="server" CssClass="form-control chzn-select inputTxt"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 18px">

                            <asp:LinkButton ID="lbtnAddMat" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lbtnAddMat_Click">Add</asp:LinkButton>


                        </div>
                        <div class="col-md-3">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem style="color: red" Selected="True" Value="0">Assorment</asp:ListItem>
                                    <asp:ListItem style="color: blueviolet" Value="1">BOM VS Consumption</asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px">
                    <div class="row">
                        <asp:MultiView ID="Multiview" ActiveViewIndex="0" runat="server">
                            <asp:View ID="ViewAssortment" runat="server">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvSizeMat" OnRowDeleting="gvSizeMat_RowDeleting" OnRowDataBound="gvSizeMat_RowDataBound" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlblgvSlNo3" runat="server" Font-Bold="True" Height="14px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="18px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Comp Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlblgvCompCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Proc Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlblgvProcde" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                        Width="60px"></asp:Label>
                                                    <asp:Label ID="lblgvFgsize" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fgsize")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Component" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlgvgComponent" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Font-Size="9px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="230px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="9px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Font-Size="XX-Small" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="280px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="9px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FG Size">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DdlFGSize" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                    <asp:Label Visible="false" ID="LblFgSize" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fgsizedesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="9px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual">
                                                <ItemTemplate>
                                                    <asp:Label ID="Stxtgvsuprate" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oldqty")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Stxtgvrate" BorderColor="ForestGreen" BorderWidth="1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="60px" BorderStyle="Solid"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Con. Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Stxtgvreqty01" BorderColor="ForestGreen" BorderWidth="1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="60px" BorderStyle="Solid"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Con. Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="Slgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bom Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="StxtgvBomqty" BorderColor="ForestGreen" BorderWidth="1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px" BorderStyle="Solid"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bom Amt">
                                                <ItemTemplate>
                                                    <asp:Label ID="SlgvBom" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Stxtgvrmrks" BorderColor="ForestGreen" BorderWidth="1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                        Width="70px" BorderStyle="Solid"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewBomvsCons" runat="server">
                                <div class="col-md-12">
                                    <div id="grdWithScroll" class="table-responsive" style="height: 350px;" onscroll="SetDivPosition()">
                                        <asp:GridView ID="gvdetails" runat="server" OnRowDataBound="gvdetails_RowDataBound" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">

                                            <PagerSettings Position="Top" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Comp Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                            Width="60px"></asp:Label>
                                                        <asp:Label Visible="false" ID="LblFgSizes" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fgsize")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Component">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgComponent" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" Font-Size="9px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="9px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="280px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="9px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Con. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="60px" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Conqty" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvsuprate" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="West Parcent" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvAppRate01" runat="server" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Con. Amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bom. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBomQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bom. amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBomamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbtnRevisedZero" OnClientClick="return confirm('Do you want to Revised zero this item?')" OnClick="LbtnRevisedZero_Click" Visible="false" CssClass="btn btn-sm btn-danger" runat="server">Revised to Zero <span class="fa fa-sync"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>

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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


