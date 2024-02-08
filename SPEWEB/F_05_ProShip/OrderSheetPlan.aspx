<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="OrderSheetPlan.aspx.cs" Inherits="SPEWEB.F_05_ProShip.OrderSheetPlan" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        //function pageLoaded() {
        //    $("input, select").bind("keydown", function (event) {
        //        var k1 = new KeyPress();
        //        k1.textBoxHandler(event);
        //    });
        //}

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindOrder" runat="server" CssClass="label" OnClick="ImgbtnFindOrder_Click">Order No</asp:LinkButton>
                                <asp:DropDownList ID="ddlOrderList" runat="server" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Order Type</asp:Label>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">..</asp:Label>
                                <asp:DropDownList ID="ddlImportStyle" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnImport" runat="server" OnClientClick="return confirm('Do you want to import Wastages');" CssClass="btn btn-primary btn-sm pull-left" OnClick="LbtnImport_Click">Import Wastage</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <div class="row">
                                <div class="col-md-2">

                                    <asp:Image ID="SmpleIMg" Style="width: 76px !important; height: 76px; border: 1px solid #4779e5;" runat="server" CssClass="img" />
                                </div>

                                <div class="col-md-10">

                                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" CssClass="table table-bordered grvContentarea">
                                        <asp:TableRow>
                                            <asp:TableCell>CLIENT</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="BuyerName" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>BRAND</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblbrand" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>Color</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblcolor" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>Article</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblarticle" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>Size Range</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblsizernge" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>Construction</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblconstruction" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>Total Order</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="TotalOrder" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>Trial Order</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="ordqty" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="3" ForeColor="Red">Missing Material or Specifications are identified </asp:TableCell>
                                            <asp:TableCell>
                                                  <div style="width:20px; height:20px; background-color:lightcoral"></div>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>

                                </div>

                            </div>
                        </div>
                        <div class="col-md-7 col-sm-7 col-lg-7 ">
                            <div class="row">
                                <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                    ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Style ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                    Width="51px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                    Width="51px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trial Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTrialOrder" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialordr")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-01" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-02" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-03" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF3" runat="server"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-04" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF4" runat="server"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-05" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF5" runat="server"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-06" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvF6" runat="server"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-07" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-08" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-09" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-10" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-11" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-12" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-13" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-14" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size-15" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
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
                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body">


                    <div class="row">
                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="479px" OnRowDataBound="gvCost_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                            Width="20px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="DEPARTMENT NAME" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvdept" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procname")) %>'
                                            Width="150px"></asp:Label>
                                        <asp:Label ID="lblgvDeptcode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'></asp:Label>
                                        <asp:Label ID="lblgvMlcode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="COMPONENT NAME" ItemStyle-Font-Size="10px">

                                    <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server"
                                                        Text="COMPONENT NAME"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel"  runat="server">Export &nbsp;<span class="fa fa-folder"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcompcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                            Width="0px"></asp:Label>
                                        <asp:Label ID="lblgvCompnent" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">

                                    <ItemTemplate>

                                        <asp:Label ID="lblgvMatCode" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmno")) %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="MATERIALS NAME" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                    <ItemTemplate>

                                        <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                            Width="150px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                    <FooterStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="Part No" Visible="false" ItemStyle-Font-Size="10px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmno")) %>'
                                            Width="60px"></asp:Label>
                                        <asp:Label ID="lblgvcolor" Visible="false" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                            Width="60px"></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="8px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spc. Code">

                                    <ItemTemplate>

                                        <asp:Label ID="lblgvSpcCode" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="SPECIFICATION NAME" ItemStyle-Font-Size="10px">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                            Width="0px"></asp:Label>
                                        <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="UNIT" ItemStyle-Font-Size="10px">

                                    <ItemTemplate>
                                        <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="CONS/ PAIR">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvconqty" runat="server" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consppair")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="WASTAGE %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prwestpc")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="40px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="WASTAGE QTY">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvwestqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prwestqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="40px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="T. REQUIRED">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlreqrd")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvttlqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" Visible="false" HeaderText="PRICE/ UNIT">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrat")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
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
            </div>






        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
