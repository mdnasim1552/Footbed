<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptWorkOrderVsSupply.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptWorkOrderVsSupply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>

    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <style type="text/css">
        .checkbox-balance input {
            margin-right: 5px;
        }

        label {
            margin-bottom: 0rem !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .supplier label {
            margin-bottom: 0px !important;
        }
    </style>

    <script type="text/javascript">

        function Search_Gridview1(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=GvSeasonSum.ClientID %>");
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

        function CurrentTabSummary() {
            document.getElementById("currentTabNow").value = "summaryTab"
        }

        function CurrentTabDetail() {
            document.getElementById("currentTabNow").value = "detailTab"
        }

        function CurrentTabSupp() {
            document.getElementById("currentTabNow").value = ""
        }

        function Search_Gridview2(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvSeasonSumDetail.ClientID %>");
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


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            document.getElementById("currentTabNow").value = "summaryTab"

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $('#chalan').click(function () {
                $('.chalanMat').toggle("slide");
                console.log("Working");
            });

            var gvReqStatus = $('#<%=this.gvReqStatus.ClientID %>');
            gvReqStatus.Scrollable();
        }

        function showMatDescriptionModal() {
            $('#modalMatDescription').modal("show");
        }

        function closeMatDescriptionModal() {
            $('#modalMatDescription').modal("hide");
        }

        $(function () {
            $("#dvAccordian").accordion();
        });



    </script>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="card card-fluid">
                            <div class="card-body">
                                <div class="row">

                                    <div class="col-md-3 form-group">
                                        <asp:Label ID="lblSupplier" runat="server" Style="margin-right: 20px" CssClass="label">Supplier :</asp:Label>
                                        <asp:CheckBox ID="ChckShipper" runat="server" CssClass="checkbox-balance" Text="As A Shipper" />
                                        <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2 form-group">
                                        <asp:Label ID="lblStore" runat="server" Style="margin-right: 10px" CssClass="label">Store</asp:Label>
                                        <asp:CheckBox ID="ChkPrice" runat="server" Text="Print Without Price" />
                                        <asp:DropDownList ID="ddlOrderName" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 form-group">
                                        <asp:Label ID="lblseason" runat="server" CssClass="label">Season</asp:Label>
                                        <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 form-group" style="margin-top: 20px; margin-left: -5px">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-1 form-group" style="margin-top: 8px">
                                        <asp:Label ID="Label5" runat="server" Text="As on Date:" CssClass="col-form-label"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-2 form-group" style="margin-top: 16px">
                                        <div class="">
                                            <asp:CheckBox ID="ChkBalance" runat="server" CssClass="checkbox-balance" Text="Without Zero Balance" /><br />
                                            <asp:CheckBox ID="ChkShipBal" runat="server" CssClass="checkbox-balance" AutoPostBack="true" OnCheckedChanged="ChkShipBal_CheckedChanged" Text="Shipment Balance Only" />

                                        </div>
                                    </div>

                                    <div class="col-md-1 form-group text-left">
                                        <asp:Label ID="lblPage" runat="server" Text="Page Size" Visible="false" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" Visible="false" CssClass="form-control form-control-sm mt-1" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18" AutoPostBack="true">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="900">900</asp:ListItem>
                                            <asp:ListItem Value="1500">1500</asp:ListItem>
                                            <asp:ListItem Value="2500">2500</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-1 form-group" style="margin-top: 5px">
                                        <div class="row mb-1">
                                            <asp:LinkButton runat="server" ID="lkbtnPOSummary" OnClick="lkbtnPOSummary_Click" CssClass="btn btn-sm btn-outline-primary"><span class="fa fa-file-excel"></span>PO Summary</asp:LinkButton>
                                        </div>
                                        <div class="row p-1">
                                            <asp:CheckBox ID="ChckPriceSum" runat="server" CssClass="checkbox-balance" AutoPostBack="true" OnCheckedChanged="ChckPriceSum_CheckedChanged" Text="Price Sum" />
                                        </div>
                                    </div>

                                    <div class="col-md-2 form-group" style="margin-top: -20px">
                                        <asp:DropDownList ID="DDLType" runat="server" CssClass="form-control form-control-sm chzn-select" Visible="false">
                                            <asp:ListItem Value="0">Default</asp:ListItem>
                                            <asp:ListItem Value="1">Material Price Variance</asp:ListItem>
                                            <asp:ListItem Value="2">Mat-Specification Price Variance</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                            </div>
                        </div>


                        <div class="card card-fluid" style="min-height: 550px">

                            <div class="card-body py-5">
                                <div class="table-responsive" style="overflow-x: auto; width=1300px">
                                    <asp:GridView ID="gvReqStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvReqStatus_PageIndexChanging" ShowFooter="true" OnRowDataBound="gvReqStatus_RowDataBound"
                                        AllowSorting="true" OnSorting="gvReqStatus_Sorting">
                                        <PagerSettings Position="Top" />
                                        <RowStyle Font-Size="10px" />
                                        <Columns>
                                            <%--0--%>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" Width="35px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <table style="border: none;">
                                                        <tr>
                                                            <td style="border: none;">
                                                                <asp:Label ID="gvReqStatus_Label4" runat="server"
                                                                    Text="Supplier Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="gvReqStatus_hlbtntbCdataExel" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSupDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Store Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProjDesc" runat="server" Width="70px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order. No.">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lbtngvOrderModel" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                        Target="_blank" Width="75px"
                                                        NavigateUrl='<%# "~/F_09_Commer/RptPoShipmentLog.aspx?" +
                                                            "orderNo="+DataBinder.Eval(Container.DataItem, "orderno").ToString() +
                                                            "&supCod="+DataBinder.Eval(Container.DataItem, "ssircode").ToString() %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ref. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRefno" runat="server" Width="150px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <%--5--%>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server" Width="57px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Exp. Del. <br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtExpDelDat" runat="server" Width="62px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" : 
                                                                        Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") %>'> 
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lead Days">
                                                <ItemTemplate>
                                                    <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : 
                                                            (Convert.ToDateTime(Eval("expdeldat")) - Convert.ToDateTime(Eval("orderdat"))).Days.ToString() %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="55px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description of <br> Materials ⥮" SortExpression="resdesc">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblgvResDesc" runat="server" OnClick="lbtngvOrderModel_Click" Width="150px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <%--9--%>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server" Width="180px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selection Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrsResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "selection")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order Qty ⥮" SortExpression="ordrqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvApprQty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalOrderQty" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Shipped<br>Qty ⥮" SortExpression="shippedqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvShippedQty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "shippedqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalShippedQty" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="55px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ship Bal<br/>Qty ⥮" SortExpression="shipbalqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvShipBalQty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "shipbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalShipBalQty" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="55px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="55px" />
                                            </asp:TemplateField>

                                            <%--15--%>
                                            <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrice" runat="server" Font-Size="11px"
                                                        Style="text-align: right" Width="75px"
                                                        Text='<%# Eval("grp").ToString() == "A" ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("#,##0.0000;(#,##0.0000); ")+ " (" + Convert.ToString(DataBinder.Eval(Container.DataItem, "cursymbol")) + ")" : "" %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvothers" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "others")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="C%F rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcfrate" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Incoterms">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvincoter" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incotermdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>

                                            <%--19--%>
                                            <asp:TemplateField HeaderText="Order Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrderAmt" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalOrdrAmt" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="90px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvComqty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalReceived" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="65px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrderQty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalBillQty" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="65px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalqty" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalBalQty" runat="server" BackColor="Transparent" Font-Bold="True"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="65px" />
                                            </asp:TemplateField>

                                            <%--23--%>
                                            <asp:TemplateField HeaderText="Shipper">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrsBalqty" runat="server" Font-Size="9px"
                                                        Style="text-align: left" Width="100px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "shipperdesc") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
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
                    </asp:Panel>

                </asp:View>

                <asp:View ID="View2" runat="server">
                    <asp:Panel ID="Panel3" runat="server">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-group">
                                <div class="col-md-10  pading5px">
                                    <asp:Label ID="Label10" runat="server" Text="Order No:" CssClass="lblTxt lblName"></asp:Label>
                                    <asp:TextBox ID="txtSrcOrder" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtnFindOrder" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindOrder_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    <asp:DropDownList ID="ddlOrderList" runat="server" Width="233" CssClass="form-control ddlPage"></asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ddlSupplierName_ListSearchExtender0" runat="server"
                                        QueryPattern="Contains" TargetControlID="ddlSupplierName">
                                    </cc1:ListSearchExtender>
                                    <asp:LinkButton ID="lbtnOrderTk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOrderTk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-10  pading5px">
                                    <asp:Label ID="Label12" runat="server" Text="Date:" CssClass="lblTxt lblName"></asp:Label>
                                    <asp:TextBox ID="txtorddate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtorddate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtorddate"></cc1:CalendarExtender>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindOrder_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="233" CssClass="form-control ddlPage"></asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                        QueryPattern="Contains" TargetControlID="ddlSupplierName">
                                    </cc1:ListSearchExtender>

                                    <cc1:ListSearchExtender ID="ddlOrderName_ListSearchExtender2" runat="server"
                                        QueryPattern="Contains" TargetControlID="ddlOrderName">
                                    </cc1:ListSearchExtender>

                                </div>
                            </div>

                        </fieldset>
                    </asp:Panel>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvOrdertk" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" Width="982px">
                            <PagerSettings Position="Top" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjDescor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Materials">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResDescord" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnitord" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvApprQtyor" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmrrno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRR Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmrrdate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="MRR Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvmrrqty" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvotmrrdate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBillqty" runat="server" Font-Size="11px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="card card-fluid">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-2 form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-form-label">Season</asp:Label>
                                        <div class="row px-2">
                                            <asp:DropDownList ID="DdlSeason2" CssClass="col-8 col-form-control form-control-sm chzn-select" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="LbtnSeasonSummary" runat="server" CssClass="btn btn-sm btn-primary ml-3" OnClick="LbtnSeasonSummary_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-1 form-group px-0">
                                        <asp:Label ID="Label4" runat="server" Text="As on Date:" CssClass="col-form-label"></asp:Label>
                                        <asp:TextBox ID="TxtAsOnDate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label ID="LblCountry" runat="server" CssClass="col-form-label">Country</asp:Label>

                                        <asp:DropDownList ID="DdlCountry" CssClass="form-control form-control-sm chzn-select" runat="server">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 form-group">
                                        <asp:Label ID="Label6" runat="server" Text="Page Size" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:DropDownList ID="DdlPSize" runat="server" CssClass="form-control form-control-sm mt-1" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18" AutoPostBack="true">
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="900">900</asp:ListItem>
                                            <asp:ListItem Value="1500">1500</asp:ListItem>
                                            <asp:ListItem Value="2500">2500</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="card card-fluid" style="min-height: 460px">
                            <div class="card-body py-5">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="currentTabNow" ClientIDMode="Static" Style="display: none" />
                                        <section class="card card-fluid">
                                            <!-- .card-header -->
                                            <header class="card-header">
                                                <!-- .nav-tabs -->
                                                <ul class="nav nav-tabs card-header-tabs">
                                                    <li class="nav-item">
                                                        <a class="nav-link active show" data-toggle="tab" href="#home" runat="server" id="hometab" onclick="CurrentTabSummary()" clientidmode="static">Summary</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#profile" onclick="CurrentTabSupp()">Top 20 Suppliers</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" data-toggle="tab" href="#details" runat="server" id="detailstab" onclick="CurrentTabDetail()" clientidmode="static">Details</a>
                                                    </li>
                                                </ul>
                                                <!-- /.nav-tabs -->
                                            </header>
                                            <!-- /.card-header -->
                                            <!-- .card-body -->
                                            <div class="card-body">
                                                <!-- .tab-content -->
                                                <div id="myTabContent" class="tab-content">
                                                    <div class="tab-pane fade active show" id="home" runat="server" clientidmode="static">
                                                        <div class="row">

                                                            <div class="col-md-4">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="GvSeasonSum" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                                                        AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="GvSeasonSum_PageIndexChanging">
                                                                        <PagerSettings Position="Top" />
                                                                        <RowStyle Font-Size="11px" />
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Sl.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="right" Width="35px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Supplier Name">
                                                                                <HeaderTemplate>
                                                                                    <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Supplier Name" onkeyup="Search_Gridview1(this, 1)"></asp:TextBox><br />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvSSsupDesc" runat="server" Font-Size="10px"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="left" Width="180px" />
                                                                                <FooterTemplate>
                                                                                    <%--<table style="border: none;">
                                                                                        <tr>
                                                                                            <td style="border: none;">
                                                                                                <asp:Label ID="lblgvFTotal" runat="server"
                                                                                                    Text="Total"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink ID="GvSeasonSum_hlbtntbCdataExel" CssClass="text-twitter" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>--%>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Country">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCountry" runat="server"
                                                                                        Style="text-align: Center"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="right" Width="60px" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Order Qty.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvssOrderQty" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfOrderQty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="85px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="85px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amt.(USD)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvssOrderAmt" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfOrderamt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="90px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amt.(BDT)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvOrderbdtAmt" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamtbdt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfOrderamtbdt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="90px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amt.(EURO)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvOrderEuroAmt" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oramteuro")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfOrderamtEuro" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="90px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="90px" />
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

                                                            <div class="col-md-3">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="GvCountrySum" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                                                        AutoGenerateColumns="False" ShowFooter="true">
                                                                        <PagerSettings Position="Top" />
                                                                        <RowStyle Font-Size="11px" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sl.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="gvSerialNo" runat="server" Height="16px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="right" Width="35px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Country Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvCountryName" runat="server"
                                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <%--<table style="border: none;">
                                                                                        <tr>
                                                                                            <td style="border: none;">
                                                                                                <asp:Label ID="lblgvCFTotal" runat="server"
                                                                                                    Text="Total"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink ID="GvCountrySum_hlbtntbCdataExel" CssClass="text-twitter" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>--%>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Order Qty.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvCOrderQty" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfCOrderQty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="75px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amt (USD)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgvCOrdrAmt" runat="server" Font-Size="11px"
                                                                                        Style="text-align: right"
                                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblgvfCOrderAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                        Style="text-align: right" Width="75px"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <FooterStyle CssClass="grvFooter" Font-Bold="True" />
                                                                        <EditRowStyle />
                                                                        <AlternatingRowStyle />
                                                                        <PagerStyle CssClass="gvPagination" />
                                                                        <HeaderStyle CssClass="grvHeader" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-5" style="border: 1px solid #D8D8D8">
                                                                <div id="CountrySum" style="width: 550px; height: 550px; margin: 0 auto"></div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="profile">
                                                        <div class="col-md-12" style="border: 1px solid #D8D8D8">
                                                            <div id="supplierSum" style="width: 1200px; height: 700px; margin: 0 auto"></div>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane fade" id="details" runat="server" clientidmode="static">
                                                        <div class="row col-md-12 px-0" style="border: 1px solid #D8D8D8">

                                                            <div class="col-md-11 table-responsive">
                                                                <asp:GridView ID="gvSeasonSumDetail" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                                                    AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="gvSeasonSumDetail_PageIndexChanging">
                                                                    <PagerSettings Position="Top" />
                                                                    <RowStyle Font-Size="11px" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblSlNo" runat="server" Height="16px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="right" Width="35px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Supplier Name" onkeyup="Search_Gridview2(this, 1)"></asp:TextBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblSupDesc" runat="server" Font-Size="10px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="left" Width="180px" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblAddress" runat="server" Font-Size="11px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supaddress")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Country Of Origin">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblOrigin" runat="server" Font-Size="11px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Business Value (USD)">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblBusinessVal" runat="server" Font-Size="11px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Payment Terms">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblPaymntTerms" runat="server" Font-Size="11px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paymentrms")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Shipping Terms">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblShipTerms" runat="server" Font-Size="11px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipterms")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Average Lead Time">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblShipTerms" runat="server" Font-Size="11px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "avgleadtime")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblRemarks" runat="server" Font-Size="11px"
                                                                                    Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Items List">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="gvssdLblItmsLst" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemslist")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                    <FooterStyle CssClass="grvFooter" Font-Bold="True" />
                                                                    <EditRowStyle />
                                                                    <AlternatingRowStyle />
                                                                    <PagerStyle CssClass="gvPagination" />
                                                                    <HeaderStyle CssClass="grvHeader" />
                                                                </asp:GridView>
                                                            </div>

                                                            <div class="col-md-1 text-center px-0">
                                                                <asp:HyperLink Visible="false" runat="server" ID="gvssdHlinkExcel" CssClass="btn btn-sm btn-success px-1" Style="font-size: 12px;"> 
                                                                    <i class="fa fa-file-excel mr-1"></i> Download
                                                                </asp:HyperLink>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- /.tab-content -->
                                            </div>
                                            <!-- /.card-body -->
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </asp:View>
                <asp:View ID="View4" runat="server">
                    <div class="card mb-1">
                        <div class="card-body py-3">
                            <div class="row">
                                <div class="col-md-1 px-0">
                                    <asp:Label ID="lblAsOnDate" runat="server" Text="As on Date:" CssClass=""></asp:Label>
                                    <asp:TextBox ID="txtAsOnDate2" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtAsOnDate2"></cc1:CalendarExtender>
                                </div>

                                <div class="col-md-1">
                                    <asp:Label ID="lblSeason3" runat="server" CssClass="">Season</asp:Label>
                                    <asp:DropDownList ID="ddlSeason3" CssClass="col-form-control form-control-sm chzn-select" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-1">
                                    <asp:LinkButton ID="lnkBtnOk3" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 21px;" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                                </div>

                                <div class="col-md-1">
                                    <asp:Label ID="lblPageSize3" runat="server" Text="Page Size" CssClass=""></asp:Label>
                                    <asp:DropDownList ID="ddlPageSize3" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18" AutoPostBack="true">
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="150">150</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                        <asp:ListItem Value="900">900</asp:ListItem>
                                        <asp:ListItem Value="1500">1500</asp:ListItem>
                                        <asp:ListItem Value="2500">2500</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-body" style="min-height: 450px;">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLeadTime" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Style="text-align: center" Width="600px">
                                            <PagerSettings Position="Top" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblSl" runat="server" Height="16px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Order No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblOrdrNo" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>

                                                <%--<asp:TemplateField HeaderText="Season">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblSeason" runat="server" Font-Size="11px"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>

                                                <%--<asp:TemplateField HeaderText="Del. Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblDelCount" runat="server" CssClass="text-right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delcount")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="PO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblPO" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supplier">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblSupplier" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                            Width="195px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="170px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PO Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblOrdrDate" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delivery Complete Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblRcvDate" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supply Lead Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLeadTime" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltLblRemarks" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagories")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" Font-Bold="True" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="col-md-5">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLeadTimeSumry" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Style="text-align: left" Width="200px">
                                            <PagerSettings Position="Top" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltsLblSl" runat="server" Height="16px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supply Lead Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltsLblRemarks" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Categories">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltsLblCat" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagories")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="LblSumTotal" runat="server">Total</asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Number of Delivery">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltsLblDelCount" runat="server" CssClass="text-right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delcount")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Percentage(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvltsLblPrcnt" runat="server" CssClass="text-right"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "percnt")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="LblSumTotalPercnt" runat="server">100%</asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
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

                                    <div class="row">
                                        <div id="leadtime" style="width: 700px; height: 400px; margin: 0 auto"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>

            </asp:MultiView>

            <div class="modal" id="modalMatDescription" <%--data-backdrop="static"--%> tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Material List</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">



                            <asp:GridView ID="GV_MatDesc" runat="server" AutoGenerateColumns="False" CssClass="col-12 table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                <RowStyle CssClass="font-size-sm" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" Width="35px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtngvOrderNo" runat="server"
                                                OnClick="lbtngvOrderNo1_Click"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order. No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtngvOrderNo1" runat="server"
                                                OnClick="lbtngvOrderNo1_Click"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdDate" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="75px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" Width="175px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Materials">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" Width="175px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfCode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="170px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="45px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdApprQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdComqty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdOrderQty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmdBalqty" runat="server" Font-Size="11px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="65px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtShipQty" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expected Del. Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExpDelDat" runat="server" CssClass="form-control form-control-sm w-100" AutoCompleteType="Disabled" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? 
                                                                        "":
                                                                        Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") %>'> </asp:TextBox>
                                            <cc1:CalendarExtender ID="expDelDat_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExpDelDat"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>



                            <%--  --%>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnSaveMatDesc" runat="server" type="button" class="btn btn-sm btn-primary" OnClientClick="closeMatDescriptionModal();" Text="Save" OnClick="btnSaveMatDesc_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">          
        function ExecuteGraph(monthlysum, SupplierSum) {
            var monthlysum = JSON.parse(monthlysum);
            var SupplierSum = JSON.parse(SupplierSum);
            //console.log(monthlysum);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#CountrySum').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Country Wise Amount'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in monthlysum) {
                            if (monthlysum.hasOwnProperty(key)) {
                                data.push([monthlysum[key].country,
                                monthlysum[key].ordamt, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });

            $('#supplierSum').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Top 20 Suppliers'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in SupplierSum) {
                            if (SupplierSum.hasOwnProperty(key)) {
                                data.push([SupplierSum[key].ssirdesc,
                                SupplierSum[key].ordamt, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });
        }

        function ExecuteLeadTimeGraph(leadtime) {
            var leadtime = JSON.parse(leadtime);
            console.log(leadtime);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });
            $('#leadtime').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Supply Lead Time',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Percent'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.2f} %'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} %</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Supply Lead Time",
                        "colorByPoint": true,
                        "data": (function () {
                            // generate an array of random data
                            var data = [];

                            for (var key in leadtime) {
                                if (leadtime.hasOwnProperty(key)) {
                                    data.push({
                                        "name": leadtime[key].catagories + "<br>" + leadtime[key].remarks,
                                        "y": leadtime[key].percnt,
                                    });
                                }
                            }
                            return data;

                        }())

                    }
                ]

            });
        }
    </script>

</asp:Content>



