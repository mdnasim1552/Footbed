<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMaterialTrans.aspx.cs" Inherits="SPEWEB.F_11_RawInv.RptMaterialTrans" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });



            let options = { ScrollHeight: 400 };
            let gv1 = $('#<%=this.gvMatTransfer.ClientID %>');
            gv1.Scrollable(options);

            $('.chzn-select').chosen({ search_contains: true });
        };


        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");

            tblData = document.getElementById("<%=gvMatTransfer.ClientID %>");

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

                        <div class="col-md-2" runat="server" id="divDdlStoreAcc">
                            <div class="form-group">
                                <asp:Label ID="LblStore" runat="server" CssClass="control-label" Text="Store:"></asp:Label><br />
                                <asp:DropDownList ID="DdlStoreAcc" runat="server" CssClass="form-control form-control-sm chzn-select chzn-single"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="divddlSupplier">
                            <div class="form-group">
                                <asp:Label ID="lblSupplier" runat="server" CssClass="control-label" Text="Supplier"></asp:Label><br />
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblType" runat="server" CssClass="control-label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select chzn-single">
                                    <asp:ListItem Value="JMT">Job Work Transfer</asp:ListItem>
                                    <asp:ListItem Value="LNR">Loan Requisition</asp:ListItem>
                                    <asp:ListItem Value="RTR">Return Requisition</asp:ListItem>
                                    <asp:ListItem Value="MTR">Material Transfer Requisition</asp:ListItem>
                                    <asp:ListItem Value="%" Selected="True">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 text-center" style="margin-left: -20px">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;" OnClick="lbtnOk_Click"
                                    TabIndex="8">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label"
                                    Text="Page Size:"></asp:Label>
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

                        <div class="col-md-2">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:CheckBox ID="ChbxSummary" runat="server" Text="View Summary" OnCheckedChanged="ChbxSummary_CheckedChanged" Style="margin-left: 25px;" AutoPostBack="true" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 550px;">
                <div class="card-body">

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="MatTransfer" runat="server">

                            <div class="d-flex">

                                <div>
                                    <asp:GridView ID="gvMatTransfer" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnPageIndexChanging="gvMatTransfer_PageIndexChanging">
                                        <PagerSettings Position="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblMatReqNo1" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mtreqno1").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqDat1" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatName1" Visible="true" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsirdesc").ToString() %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcf1" Visible="true" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Trnsfer From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTfactdesc1" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tfactdesc").ToString() %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Trnsfer To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTtactdesc1" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ttactdesc").ToString() %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMtrRef1" Visible="true" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mtrref").ToString() %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvLbTtlFooter" CssClass="font-weight-bold" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCnt1" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "itmcount").ToString() %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvItmCnt" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvQty1" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GatePass<br/>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGpQty1" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlGPQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Trans<br/>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTrQty1" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTransQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transfer<br/>Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalQty1" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trbalqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTransBal" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
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

                                <div id="cellSummary2" runat="server">
                                    <asp:GridView ID="gvSummary2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="500px">
                                        <PagerSettings Position="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatName2" Visible="true" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsirdesc").ToString() %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcf2" Visible="true" Font-Size="11px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvQty2" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlQty2" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GatePass<br/>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGpQty2" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlGPQty2" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Trans<br/>Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTrQty2" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTransQty2" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transfer<br/>Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalQty2" Font-Size="11px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trbalqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTransBal2" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
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

                            </div>

                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
