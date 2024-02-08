<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptLCPosition.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptLCPosition" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            <%--var grvLC = $('#<%=this.grvLC.ClientID %>');
            grvLC.Scrollable();--%>

           <%-- var grvLC = $('#<%=this.grvLC.ClientID %>');

            grvLC.gridviewScroll({
                width: 1250,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 3
            });--%>

            $('.chzn-select').chosen({ search_contains: true });
        }

        function OpenModal() {
            $('#modal').modal('show');
        }

    </script>

    <style type="text/css">
        .radbtn td {
            width: 85px;
        }

        .chzn-container-single .chzn-single {
            height: 30px !important;
            line-height: 30px !important;
            border-radius: 5px !important;
            /*margin-top: 20px !important;*/
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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


                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lbllc" runat="server" CssClass="label">LC Type</asp:Label>
                                <asp:DropDownList ID="ddllctypelist" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:RadioButtonList runat="server" ID="rdbtnRptType" CssClass="form-control form-control-sm text-center radbtn" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Details" Value="Details" Selected="True" />
                                    <asp:ListItem Text="Summary" Value="Summary" />
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1100</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px;">
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View runat="Server" ID="ViewLCStatus">
                            <div class="row table-responsive" style="max-height: 550px; overflow: scroll;">
                                <asp:GridView ID="grvLC" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowDataBound="grvLC_RowDataBound" CssClass="table-striped table-responsive table-hover table-bordered grvContentarea">
                                    <RowStyle />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfileno" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fileno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvActdesc" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpDate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlcval" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C <br> Payment <br> Terms">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlpaytrm" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lpaytrm")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Date of Shipment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpDate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvExpdate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "expdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Beneficiary Name & Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbefnam" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "befnam")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProDesc" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Font-Size="14px" ForeColor="#000" Font-Bold="true">Total</asp:Label>
                                            </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunit" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price Per </br>Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblFAmount" runat="server" Font-Size="12px" ForeColor="#000" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="B/L, AWB/D/N No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvblno" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="B/L, AWB/D/N Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbldat" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bldat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ETA Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvetadat" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "etadat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="B/E No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvebeno" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "beno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="B/E Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbedat" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bedat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdocpos" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "docpos")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name of C&F">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcnf" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cnf")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Date</br> From Port">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDelDate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deldate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received at Factory">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvShipardate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipardate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Payment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpaydat" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delivery Terms">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdeltrm" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deltrm")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delivery Modes">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdelmod" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delmod")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvremarks" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Received Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvRcvdate" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvLcstatus" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcstatus")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <AlternatingRowStyle />

                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View runat="Server" ID="ViewLCStatusSummary">
                            <div class="row table-responsive" style="max-height: 550px; overflow: scroll;">
                                <asp:GridView ID="grvLCSummary" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass="table-striped table-responsive table-hover table-bordered grvContentarea">
                                    <RowStyle />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name Of">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsNamOf" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "befnam")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsDesc" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsLcNo" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="140px"></asp:Label>
                                                <asp:Label ID="lblgvlcsActCode" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsLcDat" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFlcsLcDat" runat="server" Font-Size="12px" ForeColor="#000" Font-Bold="true">Total:</asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLbTtlFooter" CssClass="font-weight-bold" Text="Total:"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tenor At Sight">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsTenrAtSight" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tenure")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Currency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCurrency" runat="server" CssClass="text-left pl-1" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currencydesc")) + " " + Convert.ToString(DataBinder.Eval(Container.DataItem, "symbol")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LC Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLcVal" runat="server" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcopnamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Value USD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsLcValUSD" runat="server" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="flTtlLcUSD" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Value Euro">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsLcValEuro" runat="server" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "euroval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="flTtlLcEuro" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Season">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsSeason" runat="server" CssClass="text-left pl-1" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contract">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcsContract" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "contractno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due/Payment">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblgvlcsContract" runat="server" CssClass="text-left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydat")) %>'
                                                    Width="80px"></asp:Label>--%>
                                                <asp:LinkButton ID="lblDuePay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlpaymnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" OnClick="lblgvlcsContract_Click"></asp:LinkButton>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <AlternatingRowStyle />

                                </asp:GridView>
                            </div>
                        </asp:View>

                    </asp:MultiView>

                    <div id="modal" class="modal animated slideInLeft" role="dialog">

                        <div class="modal-dialog modal-xl">

                            <div class="modal-content ">

                                <div class="modal-header">
                                    <h4 class="modal-title"><span class="fa fa-table"></span>LC Payment History</h4>

                                    <div class="float-right">

                                        <div class="d-flex">
                                            <asp:Label runat="server" ID="Label3" Font-Bold="true" Text="Lc No:&nbsp"/>
                                            <asp:Label runat="server" ID="lblLcNo"/>
                                        </div>

                                        <div class="d-flex">
                                            <asp:Label runat="server" ID="Label1" Font-Bold="true" Text="Supplier Name:&nbsp"/>
                                            <asp:Label runat="server" ID="lblSuppplyName"/>
                                        </div>

                                        
                                    </div>

                                </div>

                                <div class="modal-body mb-1">

                                    <asp:GridView ID="grvLcPay" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-responsive table-hover table-bordered grvContentarea"
                                        ShowFooter="true">
                                        <RowStyle />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvLcPaySlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Voucher Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVouNum" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Voucher Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVouDat" runat="server" Style="text-align: Left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvLbTtlFooter" CssClass="font-weight-bold" Text="Total:"/>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="fLblAmt" CssClass="font-weight-bold" Text="0"/>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Fc Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="fLblFcAmt" CssClass="font-weight-bold" Text="0"/>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bank Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankName" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Currency">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankName" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Conversion Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankName" runat="server" Style="text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conrate")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                        </Columns>

                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <AlternatingRowStyle />

                                    </asp:GridView>

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-sm btn-dark" data-dismiss="modal">Close</button>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
