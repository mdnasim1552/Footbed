<%@ Page Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ExportRetList.aspx.cs" Inherits="SPEWEB.F_19_EXP.ExportRetList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/ScrollableTablePlugin.js"></script>

    <script src="../Scripts/chosen.jquery.js"></script>
    <script type="text/javascript" language="javascript">


        function PrintRdLc(type) {
            window.open('../RDLCViewerWin?PrintOpt=' + type, '_blank');
        }


        function openModal() {
            //    $('#myModal').modal('show');
            $('#myModal').modal('toggle');
        }
        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtTransSearch(event);
            });
            //$('#tblSKUWise').ScrollableRep({

            //});

            //
            $('.chzn-select').chosen({ search_contains: true });
        }


    </script>

    <style>
        .tblSaleExp > thead > tr > th {
            background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #dff0d8 0%, #dff0d8 49%, #d2d2d2 98%, #dff0d8 100%) repeat scroll 0 0 !important;
            height: 5px !important;
            vertical-align: middle;
        }

        .tbBouder {
            border: 1px solid black !important;
        }

            .tbBouder th, td {
                border: 1px solid #d2d2d2 !important;
            }

        .chzn-container {
            float: left;
        }

        /*table, th, td {
          border: 1px solid black ;
        }*/
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblDate" runat="server" CssClass="form-label" Text="Form"></asp:Label>
                                <asp:TextBox ID="txtFromDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFromDate" Enabled="true"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                <asp:TextBox ID="txtToDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="padding-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary primaryBtn" OnClick="lbtnOk_Click">OK</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2 pading5px pull-right" style="height: 27px !important;">
                            <div class="msgHandSt">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                    <ProgressTemplate>
                                        <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                            Text="Please Wait.........."></asp:Label>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <asp:Label ID="ConfirmMessage" CssClass="btn btn-xs btn-danger" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row col-md-12">

                        <asp:GridView ID="GvExpReturn" AutoGenerateColumns="False" OnRowDataBound="GvExpReturn_RowDataBound" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <Columns>

                                <asp:TemplateField HeaderText="SL.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Return No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "retmemo")) %>'
                                            Width="105px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Store">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStore" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                            Width="105px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoice" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                            Width="105px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Style No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStyle" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Color" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblColor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Return Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRetQty" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderQty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Pair">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotlPr" runat="server" Style="font-size: 11px; text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
<%--                                        <asp:LinkButton ID="LbtnApp" OnClientClick="return confirm('Do you want to Approve?')" OnClick="LbtnApp_Click" runat="server" CssClass="btn btn-xs btn-success"><i class="fa fa-check"></i>
                                        </asp:LinkButton>--%>

                                        <asp:HyperLink ID="LnkEdit" runat="server" CssClass="btn btn-xs btn-warning"><i class="fa fa-pen"></i>
                                        </asp:HyperLink>

                                        <asp:LinkButton ID="LbtnPrint" runat="server" CssClass="btn btn-xs btn-info font-weight-bold text-white" OnClick="LbtnPrint_Click">
                                                    <i class="fa fa-print"></i>
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="130px" HorizontalAlign="Center" VerticalAlign="Top" />

                                </asp:TemplateField>

                            </Columns>

                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
