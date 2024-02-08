<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ConvertMaterialAssortment.aspx.cs" Inherits="SPEWEB.F_01_Mer.ConvertMaterialAssortment" %>

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
                        <div class="col-md-1">
                            <asp:Label ID="lCurReqdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                            <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                        </div>
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="small" for="DdlSeason">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 ">
                            <asp:LinkButton ID="ImgbtnFindOrder" CssClass="lblTxt lblName" runat="server" OnClick="ImgbtnFindOrder_Click" TabIndex="2" Text="Order No"></asp:LinkButton>
                            <asp:DropDownList ID="ddlOrderList" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            <asp:Label ID="lblddlOrder" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                        </div>
                        <div class="col-md-3" style="margin-top: 21px">
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

                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblResList" runat="server" CssClass=" label " Text="Material"></asp:Label>

                                <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Specifications"></asp:Label>
                                <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                    <asp:ListBox ID="ddlResSpcf" SelectionMode="Multiple" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 18px">

                            <asp:LinkButton ID="lbtnAddMat" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lbtnAddMat_Click">Out</asp:LinkButton>


                        </div>
                       

                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Material/OutSole</asp:Label>
                                <asp:DropDownList ID="ddlResList1" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResList1_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSpecification" runat="server" CssClass="label" Text="Specification"></asp:Label>

                                <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; width: 200px">
                                    <asp:ListBox ID="ddlResSpcf1" SelectionMode="Multiple" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>

                                </div>


                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="FG Sizes"></asp:Label>

                                <asp:DropDownList ID="ddlSizes" runat="server" CssClass="form-control chzn-select inputTxt"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 18px">

                            <asp:LinkButton ID="lbtnAddMatIn" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lbtnAddMatIn_Click">IN</asp:LinkButton>

                            <asp:LinkButton ID="lbtnAddMatALL" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lbtnAddMatALL_Click">ALL IN</asp:LinkButton>


                        </div>
                        <div class="col-md-3">
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
                                                        Width="330px"></asp:Label>
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

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Stxtgvreqty01" BorderColor="ForestGreen" BorderWidth="1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="80px" BorderStyle="Solid"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvLblType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ctype")) %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Font-Size="XX-Small" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>

                        </asp:MultiView>


                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


