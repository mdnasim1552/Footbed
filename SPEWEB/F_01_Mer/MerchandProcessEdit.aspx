<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MerchandProcessEdit.aspx.cs" Inherits="SPEWEB.F_01_Mer.MerchandProcessEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

    protected void ddlmaterialname_TextChanged(object sender, EventArgs e)
    {

    }
</script>


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

        };

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
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label runat="server" for="DdlSeason">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindOrder" runat="server" CssClass="label" OnClick="ImgbtnFindOrder_Click">Order No</asp:LinkButton>
                                <asp:DropDownList ID="ddlOrderList" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Style</asp:Label>
                                <asp:DropDownList ID="ddlStyle" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="SAM">Sample</asp:ListItem>
                                    <asp:ListItem Value="CON">Consumption</asp:ListItem>
                                    <asp:ListItem Value="BOM">Bill Of Material</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Buyer No</asp:Label>
                                <asp:TextBox ID="BuyerName" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="">Order Qty.</asp:Label>
                                <asp:TextBox ID="ordqty" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">

                    <div id="grdWithScroll" class="table-responsive" style="height: 500px;">
                        <asp:GridView ID="gvdetails" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvdetails_RowDataBound" OnRowCancelingEdit="gvpurorder_RowCancelingEdit" OnRowEditing="gvpurorder_RowEditing" OnRowUpdating="gvpurorder_RowUpdating">

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
                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Component">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgComponent" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True"  CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>"  />
                                <asp:TemplateField HeaderText="Material Code">
                                    <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Code" Width="50"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlmaterialname" CssClass="chzn-select" runat="server" Width="250px" OnSelectedIndexChanged="ddlmaterialname_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf. Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlspecification" CssClass="chzn-select" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Component Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCompent" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlComponent" CssClass="chzn-select" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Conqty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvConqty" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1" runat="server" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="West Parcent">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvWst" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1" runat="server" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="80px" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                            Width="80px"></asp:Label>
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



                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


