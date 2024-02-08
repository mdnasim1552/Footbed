<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EntryDepCharge.aspx.cs" Inherits="SPEWEB.F_27_Fxt.EntryDepCharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            //    $('#myModal').modal('show');
            $('#myModal').modal('toggle');
        }
        function CloseMOdal() {
            $('#myModal').modal('hide');
        }
        function SetTargetForRdlc(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="margin-bottom: 10px;">

                        <div class="col-md-1">
                            <asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtFromdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="Label10" runat="server" Text="To"></asp:Label>
                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtTodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1 text-center" style="margin-top: 27px;">
                            <asp:CheckBox ID="chkStraight" runat="server" TabIndex="10" Text="Straight" />
                        </div>
                        <div class="col-md-1 text-center" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lbtnShow_Click">Show</asp:LinkButton>
                        </div>

                        <div class="col-md-1">

                            <asp:Label ID="lblpage" runat="server">Page</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" Width="90px"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="500">500</asp:ListItem>



                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1 text-center" style="margin-top: 20px;">
                            <asp:Label ID="txtDays" runat="server" Visible="false" CssClass="btn btn-sm btn-danger"></asp:Label>

                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <asp:GridView ID="grDep" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="grDep_PageIndexChanging"
                            ShowFooter="True" Style="text-align: left" Width="810px">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid1" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActcode" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Assets" Width="50"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAsset" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFT" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: left" Text="Total :" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance as at ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOpening" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTOpening" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Addition During The Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAddition" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTAddition" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Sales Declaration">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalesdec" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFsalesdec" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Revaluation During The Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdisposal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTDisposal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total as at">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvTotal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate Of Dep. %">
                                    <FooterTemplate>
                                        <asp:HyperLink ID="hlnkgvFdep" runat="server"
                                            Font-Size="11px" CssClass="btn btn-sm btn-primary " Style="text-align: right" Target="_blank" Text="Voucher"
                                            Width="60px"></asp:HyperLink>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDepPar" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depreciation as at">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDepOpen" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndep")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTDepOpen" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Adjustment ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvadjment" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFadjment" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>

                                <%--     <asp:TemplateField HeaderText="Depreciation During The Period ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDepCur" runat="server"
                                        Style="font-size: 12px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdep")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTDepCur" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" Width="80px" />
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Current Depreciation (Opening) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcurDepop" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdepop")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTcurDepop" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Depreciation (Current)">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="lgvcurDepcut_Click" ID="lgvcurDepcut" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpdepcur")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTcurDepcur" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Depreciation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDepTotal" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todep")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTDepTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="right" Width="70px" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="W.D Values as at ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCBal" runat="server"
                                            Style="font-size: 12px; text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTCBal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                        <div id="myModal" class="modal animated zoomIn" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content  ">
                                    <div class="modal-header bg-primary">

                                        <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                        <h4 class="modal-title">
                                            <span class="glyphicon glyphicon-hand-right"></span>
                                            <asp:Label ID="lbmodalheading" runat="server"></asp:Label>
                                        </h4>
                                    </div>
                                    <div class="modal-body">

                                        <div class="row-fluid form-horizontal forgotform" id="">
                                        </div>
                                        <div class="">
                                            <asp:GridView ID="mgvbreakdown" runat="server"
                                                AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="572px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Asset Name">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvasstname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Voucher Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvvoudat" runat="server" Font-Bold="true" Font-Size="11px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblT" runat="server">Total</asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00; (#,##0.00); ")  %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="LblFPayVal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="As On Date">
                                                        <FooterTemplate>
                                                            <%-- <asp:LinkButton ID="mlbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                     CssClass="btn   btn-primary primarygrdBtn">Total</asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvasondate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "asdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvdays" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "today")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depreciation Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvdeprate" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depreciation Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvcurdep" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpdep")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="LblFDepval" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
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
                                    <div class="modal-footer">
                                        <asp:Label ID="Label1" runat="server">Select Your Download Medium</asp:Label>
                                        <asp:DropDownList ID="ddldowntype" runat="server" CssClass="">
                                            <asp:ListItem Value="PDF" Selected="True">PDF</asp:ListItem>
                                            <asp:ListItem Value="EXCEL">EXCEL</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnmisuprint" ToolTip="Print/Export" OnClientClick="CloseMOdal()" OnClick="lbtnmisuprint_Click" runat="server" CssClass="btn btn-xs btn-success printBtn"><span class="glyphicon glyphicon-hand-up"></span>CliCk Here</asp:LinkButton>

                                        <button type="button" class="btn btn-warning btn-xs printBtn" data-dismiss="modal">Close</button>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
