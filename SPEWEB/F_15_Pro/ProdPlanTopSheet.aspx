<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProdPlanTopSheet.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProdPlanTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=ddlBuyer]').multiselect({
                    includeSelectAllOption: true
                })
            });
        }
    </script>
     <style>
        .multiselect  {
            width:270px !important;
           border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
           
        }
        .multiselect-container{
            overflow: scroll;
            max-height: 300px !important;
        }
        .caret {
            display: none !important;
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
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="FromD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="ToD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldateto" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2 " id="sType" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblRptGroup" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddltype" runat="server" Width="200px" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="sum" Selected="True">Summary</asp:ListItem>
                                    <asp:ListItem Value="order">Order Wise</asp:ListItem>
                                    <asp:ListItem Value="style">Style Wise</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 " id="Buyerarea" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:ListBox ID="ddlBuyer" runat="server" CssClass="form-control form-control-sm"  SelectionMode="Multiple"></asp:ListBox>

                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="~/F_15_Pro/EntryProTarget" runat="server" Target="_blank" CssClass="btn btn-success btn-xs">Production Plan</asp:HyperLink>

                            </div>
                        </div>


                    </div>


                </div>
            </div>



            </div>


              <div class="card card-fluid">
                <div class="card-body" style="min-height:400px;">
                   
                        <asp:MultiView ID="Multivew" runat="server">
                            <asp:View ID="ProdTop1" runat="server">
                                <div class="row">
                                <asp:GridView ID="gvprodplan" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Order Name">
                                            <HeaderTemplate>
                                                <table>

                                                    <tr>

                                                        <th class="pull-right">
                                                            <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="80px">Order Name</asp:HyperLink>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvmlcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc"))  %>' Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpstatus" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalRecv" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalRecv" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalrecv")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalFRecv" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr01" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr01" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr02" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr02" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr03" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr03" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr04" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr04" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr05" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr05" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr06" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr06" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr07" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr07" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr08" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty8")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr08" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr09" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty9")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr09" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr10" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty10")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr10" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr11" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty11")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr11" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr12" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty12")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr12" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr13" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty13")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr13" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr14" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty14")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr14" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr15" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty15")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr15" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr16" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty16")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr16" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr17" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty17")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr17" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr18" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty18")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr18" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr19" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty19")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr19" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr20" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty20")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr20" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr21" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty21")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr21" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr22" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty22")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr22" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr23" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty23")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr23" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr24" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty24")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr24" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr25" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty25")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr25" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr26" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty26")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr26" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr27" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty27")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr27" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr28" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty28")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr28" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr29" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty29")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr29" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr30" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty30")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr30" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrpr31" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty31")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblrpFr31" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                                    </div>
                            </asp:View>
                            <asp:View ID="ProdMan" runat="server">
                                <div class="row">
                                <asp:GridView ID="gvprodman" AutoGenerateColumns="False" OnRowDataBound="gvprodman_RowDataBound" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="GRM No" Width="50"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lvgrrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrrno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batchcode" Visible="false">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcbatchcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storid")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcStorDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcSuplDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Master LC">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcbatchDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcRefno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcIsuno" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mgrrdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstockqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvIsuQty" runat="server" Style="font-size: 11px; text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="LblApstats" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appstatus")) %>'></asp:Label>
                                                <asp:HyperLink ID="LbtnApp" runat="server" ><span class="fa fa-pen"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="HyInprPrint"  runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>

                                                <asp:LinkButton ID="LbtnDelete" runat="server" OnClick="LbtnDelete_Click" OnClientClick="return confirm('Do you want to delete?')" ><span class="fa fa-trash"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRMEntry" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmentrystatus")) %>'></asp:Label>
                                                <asp:HyperLink ID="HyRMEntry" runat="server" ><span class="fa fa-pen"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="HyRMPrint"  runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>

                                                <asp:Label ID="LblRMApstats" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmappstatus")) %>'></asp:Label>
                                                <asp:HyperLink ID="HtRMApp" runat="server"><span class="fa fa-edit"></span>
                                                </asp:HyperLink>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                    </div>
                            </asp:View>
                            <asp:View ID="MaterialAdjustment" runat="server">
                                <div class="row">
                                <asp:GridView ID="gvStockadj" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvStockadj_RowDataBound"
                                    ShowFooter="True" Width="730px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MOnthid#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvoucher" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adjno"))%>' Width="30px"></asp:Label>
                                                <asp:Label ID="lblstatsus" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "status"))%>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/C No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcentrid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrid")) %>'
                                                    Width="90px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adj No" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adjno1")) %>'
                                                    Width="80px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Batch" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcod")) %>'
                                                    Width="90px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adjdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrdesc")) %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRepNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvitem" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrnamount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrnamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypEdit" Target="_blank" runat="server" ><span class="fa fa-edit"></span></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypApprv" ToolTip="Approve" runat="server" Target="_blank" ><span class="fa fa-pen"></span>
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">

                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypPrintbtn" ToolTip="Print" runat="server" Target="_blank" ><span class="fa fa-print"></span>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                                    </div>
                            </asp:View>
                        </asp:MultiView>


                  
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>






