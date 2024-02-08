<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="ReqAdjstmntList.aspx.cs" Inherits="SPEWEB.F_10_Procur.ReqAdjstmntList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/ScrollableTablePlugin.js"></script>

    <script src="../Scripts/chosen.jquery.js"></script>--%>
    <script type="text/javascript" language="javascript">


        function PrintRdLc(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
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

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-10 pading5px" style="height: 27px !important;">
                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl_to" Text="Form"></asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtFromDate" Enabled="true"></cc1:CalendarExtender>

                                        <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">OK</asp:LinkButton>

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
                                        
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row col-md-12">
                        <asp:MultiView ID="MultiView" runat="server">
                            <asp:View ID="ReqAdjst" runat="server">
                                <asp:GridView ID="gvreqlist" AutoGenerateColumns="False" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
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
                                                                Text="REQ No" Width="50"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgreqno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requisition Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcIsuno" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRF No">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcMatDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="REQ QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBilqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance QTY">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvBalQty" runat="server" Style="font-size: 11px; text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table style="width: 50px;">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkall" CssClass="btn btn-xs btn-success" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkall_CheckedChanged" /></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<asp:CheckBox ID="chkack" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ack"))=="True" %>'
                                            Width="50px" />--%>
                                                <asp:CheckBox ID="chkack" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ack"))=="True" ? true : false %>'
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ack"))=="True" ? false : true%>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="ProAdjst" runat="server">
                                <asp:GridView ID="gvproadjst" AutoGenerateColumns="False" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
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
                                                                Text="REQ No" Width="50"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgpbno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batchcode" Visible="false">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcbatchcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcBtchdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requisition Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcReqdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="REQ QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRqqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Production QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance QTY">
                                            <ItemTemplate>

                                                <asp:Label ID="lgvBalanceQty" runat="server" Style="font-size: 11px; text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table style="width: 50px;">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallpro" Checked="true" CssClass="btn btn-xs btn-success" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallpro_CheckedChanged" /></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<asp:CheckBox ID="chkack" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ack"))=="True" %>'
                                            Width="50px" />--%>
                                                <asp:CheckBox ID="chkack" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compstatus"))=="True" ? true : false %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="ProdMan" runat="server">
                                <asp:GridView ID="gvprodman" AutoGenerateColumns="False" OnRowDataBound="gvprodman_RowDataBound" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
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
                                        <asp:TemplateField HeaderText="Center Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcStorDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcSuplDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch">
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
                                                <asp:HyperLink ID="LbtnApp" runat="server" CssClass="btn btn-xs btn-success"><span class="glyphicon glyphicon-ok"></span>
                                                </asp:HyperLink>
                                                <asp:LinkButton ID="lnkbtnPrint" runat="server" OnClick="lnkbtnPrint_Click" CssClass="btn btn-xs btn-success"><span class="glyphicon glyphicon-print"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>




                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>



            <%-- <asp:Label ID="lblprintstkl" runat="server" Visible="false"></asp:Label>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

