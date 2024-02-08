<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="InvReport.aspx.cs" Inherits="SPEWEB.F_11_RawInv.InvReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script runat="server">


</script>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=ddlAccProject]').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    includeFilterClearBtn: true
                })
            });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

            let options = { ScrollHeight: 400 };
            let gv1 = $('#<%=this.gvCenStore.ClientID %>');
            gv1.Scrollable(options);

            $('.chzn-select').chosen({ search_contains: true });
        };


        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");

            tblData = document.getElementById("<%=gvCenStore.ClientID %>");

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

    </script>

    <style>
        .multiselect {
            width: 350px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }

        .caret {
            display: none !important;
        }

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
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblDatefrom" runat="server" CssClass="control-label"
                                    Text="From:"></asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" Style="padding-left: 4px; padding-right: 2px;"
                                    CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbldateto" runat="server" CssClass="control-label"
                                    Text="To:"></asp:Label>
                                <asp:TextBox ID="txtDateto" runat="server" Style="padding-left: 4px; padding-right: 2px;"
                                    CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-1 text-center">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;" OnClick="lbtnOk_Click"
                                    TabIndex="8">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label" Text="Size:"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                    <asp:ListItem Value="600">600</asp:ListItem>
                                    <asp:ListItem Value="700">700</asp:ListItem>
                                    <asp:ListItem Value="800">800</asp:ListItem>
                                    <asp:ListItem Value="900">900</asp:ListItem>
                                    <asp:ListItem Value="1000">1000</asp:ListItem>
                                    <asp:ListItem Value="1200">1200</asp:ListItem>
                                    <asp:ListItem Value="2500">2500</asp:ListItem>
                                    <asp:ListItem Value="3000">3000</asp:ListItem>
                                    <asp:ListItem Value="3500">3500</asp:ListItem>
                                    <asp:ListItem Value="5000">5000</asp:ListItem>


                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" id="divDdlAccProject">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="control-label" Text="Store:"></asp:Label>
                                <asp:CheckBox ID="CbNotZero" Style="margin-left: 20px;" runat="server" />Without Zero Quantity
                                <asp:ListBox ID="ddlAccProject" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblRptGroup" runat="server" CssClass="control-label" Text="Group:"></asp:Label>

                                <asp:DropDownList ID="ddlRptGroup" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server">
                            <asp:Label runat="server" ID="lblCodeBook">Mat. Group</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlCodeBook" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" runat="server" id="divSubGroup">
                            <asp:Label runat="server" ID="lblGroup">Sub Group</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblType" runat="server" CssClass="" Text="Type"></asp:Label>
                            <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem Value="4">Main</asp:ListItem>
                                <asp:ListItem Value="9">Sub</asp:ListItem>
                                <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1" runat="server" id="DaySize" visible="false">
                            <asp:Label ID="LblDay" runat="server" CssClass="" Text="Day"></asp:Label>
                            <asp:DropDownList ID="ddlDaySize" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem Value="10">10 days</asp:ListItem>
                                <asp:ListItem Value="30">30 days</asp:ListItem>
                                <asp:ListItem Value="90">90 days</asp:ListItem>
                                <asp:ListItem Value="180">180 days</asp:ListItem>
                                <asp:ListItem Value="365">365 days</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 550px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="General" runat="server">
                            <div class="table-responsive" style="min-height: 400px;">
                                <asp:GridView ID="gvCenStore" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvCenStore_RowDataBound1" OnPageIndexChanging="gvCenStore_PageIndexChanging" AllowSorting="true" OnSorting="gvCenStore_Sorting">
                                    <PagerSettings Position="Top" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMatCode" Font-Size="9px" runat="server" Text='<%# "(" + DataBinder.Eval(Container.DataItem, "subgrpdesc").ToString() + ")-" + DataBinder.Eval(Container.DataItem, "matcode").ToString() %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Name">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcMatdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Materials Name" onkeyup="Search_Gridview(this, 2, 'gvCenStore')"></asp:TextBox><br />
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblspecfcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfcod").ToString() %>' />
                                                <asp:Label ID="lblsubcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subcode").ToString() %>' />
                                                <asp:HyperLink ID="lblgvMatdescryption" Font-Size="10px" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px" Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" Font-Size="10px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" Font-Size="10px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="45px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lbltotal" Text="Total" Font-Bold="True" Font-Size="10px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvOpQtyttl" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOpnAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpAmt1" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvReQtyTTL" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"
                                                    Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpAmt2" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tra.In / Loan Rec. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrninqtyQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvtrninqtyQtyTTL" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tra.Out / Loan Ret. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTrQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvTrQtyTTL" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnsAmt" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrnsAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIssQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvIssQtyttl" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIssAmt" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFIssAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty ⥮" SortExpression="stqty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStQty" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvStQtyttl" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Avg Rate ⥮" SortExpression="strate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStRate" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "strate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Amt ⥮" SortExpression="stcamt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStAmt" Font-Size="10px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFStkAmt" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" />
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

                        <asp:View ID="QtyBasis" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvQBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvQBasis_RowDataBound2" OnPageIndexChanging="gvQBasis_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqbSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblgvQBMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="180px" Font-Size="X-Small"></asp:Label>--%>

                                                <asp:Label ID="lblspcfcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfcod").ToString() %>' />
                                                <asp:Label ID="lblsubjcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subcode").ToString() %>' />
                                                <asp:HyperLink ID="lnkgvQMatdescrp" Font-Size="10px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="250px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFopnqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBReQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFrecqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer In">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTrnin" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrninqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px "></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBTrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrnqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px "></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBIssQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmatisqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBStQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFstqty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="120px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
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

                        <asp:View ID="AmtBasis" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAmtBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvAmtBasis_RowDataBound3" OnPageIndexChanging="gvAmtBasis_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblgvABMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="180px" Font-Size="X-Small"></asp:Label>--%>

                                                <asp:Label ID="lblspcfkcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfcod").ToString() %>' />
                                                <asp:Label ID="lblsbjtcode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subcode").ToString() %>' />
                                                <asp:HyperLink ID="lnkgvAmBMatdescrp" Font-Size="10px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="250px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFOpnAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABOpAmt" runat="server" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABOpAmt1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFRecAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABtrnsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFtrnsAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABIssAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFIssAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="90px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="100px" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFStkAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
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

                        <asp:View ID="MatUnused" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvmatunused" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnPageIndexChanging="MatUnused_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirdesc").ToString() %>'
                                                    Width="220px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="250px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lbltotal" Text="Total : "></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABOpAmt2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvTtlStk" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="X-Small" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Stock Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFStkttlAmt" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="#000"
                                                    Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABFRate" runat="server"
                                                    Text='<%#  ( (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt"))) / (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty"))) ).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Last Purchase">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlastPurchase" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastpurchase")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastpurchase")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <ItemStyle HorizontalAlign="center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Use">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlastuse" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastuse")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastuse")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unused Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunusdday" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nuseday").ToString() %>'
                                                    Width="125px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
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

                        <asp:View ID="Orderwisestock" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOrdewiseStock" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnPageIndexChanging="gvOrdewiseStock_PageIndexChanging">
                                    <PagerSettings Position="Top" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mlcdesc").ToString() %>'
                                                    Width="220px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="250px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px" Font-Size="X-Small"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Amt." Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFOpnAmt" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Amt." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Amt." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrnsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIssQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Amt." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIssAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avg Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "strate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock Amt" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
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

                    </asp:MultiView>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
