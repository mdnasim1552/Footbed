<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptDaywiseShipment.aspx.cs" Inherits="SPEWEB.F_19_EXP.RptDaywiseShipment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            var gvShipPlanDetails = $('#<%=this.gvShipPlanDetails.ClientID %>');
            gvShipPlanDetails.Scrollable();
        }


        function selectedCurrentTabe() {
            currentab = document.getElementById("currentTab").value;

            if (currentab == 'SummaryTab') {

                document.getElementById("detailtab").classList.remove('active');
                document.getElementById("summarytab").classList.add('active');

                document.getElementById("details").classList.remove('active');
                document.getElementById("details").classList.remove('show');

                document.getElementById("summary").classList.add('active');
                document.getElementById("summary").classList.add('show');

            }



        }
        function onSummaryClick(currentab) {
            document.getElementById("currentTab").value = currentab;
        }


        function okClick() {
            var linkButton = document.getElementById('lbtnOk');
            console.log(linkButton);
            linkButton.click;
        }

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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divToDate">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">To</asp:Label>
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div runat="server" id="pnlBuyer" class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblbuyer" runat="server">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div runat="server" id="pnlSeason" class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblSsn" runat="server">Season</asp:Label>
                                <asp:DropDownList ID="ddlSeason" runat="server" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" id="divArticleDdl">
                            <div class="form-group">
                                <asp:Label ID="lblmlccod" runat="server" CssClass="label">Article Wise</asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                
                                <asp:Label ID="lblunrealtype" Visible="false" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="DdlUnrealType" Visible="false" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                    <asp:ListItem Value="Unrealized">Unrealized</asp:ListItem>
                                    <asp:ListItem Value="UnrealizedPartial">Unrealized-Partial</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" id="divOrderDdl">
                            <div class="form-group">
                                <asp:Label ID="lblordtype" runat="server" CssClass="label">Order Wise</asp:Label>
                                <asp:DropDownList ID="dllorderType" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click" ClientIDMode="Static">Ok</asp:LinkButton>
                            </div>
                        </div>


                        <div class="col-md-1">
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

                        <asp:TextBox ID="currentTab" runat="server" ClientIDMode="Static" style="display:none"/>

                    </div>
                </div>
            </div>


            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="General" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div class="row">

                                <asp:GridView ID="gvshipment" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="400px" OnPageIndexChanging="gvCenStore_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMLCDesc1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "mlcdesc").ToString() %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustdesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "invno").ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderno1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ordno").ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyldesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "styledesc").ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="
                                            
                                            
                                            ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColordesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "colordesc").ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsizedesc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sizedesc").ToString() %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="60px">Total:</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ship. Qty.">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFOpQty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRemarks" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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
                </asp:View>

                <asp:View ID="View1" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div class="row">
                                <asp:GridView ID="gvQBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="400px" OnPageIndexChanging="gvQBasis_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqbSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBReQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBTrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBIssQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQBStQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
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
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="View2" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div class="row">
                                <asp:GridView ID="gvAmtBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="400px" OnPageIndexChanging="gvAmtBasis_PageIndexChanging">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Materials Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Amt.">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABOpAmt1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABtrnsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFtrnsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issue Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABIssAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvABStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvABFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
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
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="View3" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div class="row table-responsive">
                                <asp:GridView ID="gvSummery" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="400px">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSummeryABSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyerName1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "buyername").ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Or CT NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcctno1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lcctno").ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="L/C Or CT Date ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcctno" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "lcctdate")).ToString() %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvinvno1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "invrefno")).ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvinvno" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvttlamt" runat="server" Text='<%#  (DataBinder.Eval(Container.DataItem, "cursymbol")).ToString()+ "  " + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QTY In Pairs">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtotlprs1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotlprs" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSummeryABIssAmt" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdt")).ToString("dd-MMM-yyyy") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvSummeryABFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EXP No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvexpno" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "expno")).ToString() %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvSummeryABFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <FooterStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Realization No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRealizationNo" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "realizationno")).ToString() %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Of Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbillno" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "billno")).ToString() %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvttlBillDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "billdate").ToString()  %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Items Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvitmname" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "itmname")).ToString() %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Realization 1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRealOne" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "real1")).ToString("#,##0.00;(#,##0.00); ") + "<br>" 
                                                                                                    + DataBinder.Eval(Container.DataItem, "realdat1").ToString() + "<br>" 
                                                                                                    + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "realcon1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Realization 2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRealTwo" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "real2")).ToString("#,##0.00;(#,##0.00); ") + "<br>" 
                                                                                                    + DataBinder.Eval(Container.DataItem, "realdat2").ToString() + "<br>" 
                                                                                                    + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "realcon2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Realization 3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRealThree" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "real3")).ToString("#,##0.00;(#,##0.00); ") + "<br>" 
                                                                                                    + DataBinder.Eval(Container.DataItem, "realdat3").ToString() + "<br>" 
                                                                                                    + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "realcon3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Short Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvttlShortAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shortamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAit" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aitamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="COMMSSION">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComm" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "commamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="POSTAGE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAitPostage" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FUND BUILDUP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFundBuild" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fundbillup")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FBPAR in USD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFbparUS" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bactobacfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FBPAR in TK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFbparTk" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bactobacbdt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CC/CD AC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCCCD" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total BDT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTtlBdt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlrealizbdt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
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
                </asp:View>

                <asp:View ID="View4" runat="server">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 100px; padding: 20px 20px 20px 20px; background-position-x: top">
                                    <div class="row" runat="server" id="calcurate">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="label">Exchange Rate</asp:Label>
                                        <div class="form-inline ">

                                            <div class="col-md-4">
                                                <asp:Label ID="Label2" Font-Bold="true" Font-Size="11px" runat="server" CssClass="label">USD</asp:Label>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtUSD" Width="85%" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="Label5" Font-Bold="true" Font-Size="11px" runat="server" CssClass="label">Euro</asp:Label>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtEuro" Width="85%" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="Label6" Font-Bold="true" Font-Size="11px" runat="server" CssClass="label">Pound</asp:Label>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPound" Width="85%" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:LinkButton ID="lbtnCalculate" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnCalculate_Click">Calculate</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-8 ">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 100px; padding: 20px 20px 20px 20px">
                                    <asp:GridView ID="gvUnrealization" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea fa-pull-right"
                                        ShowFooter="True" Width="400px">
                                        <PagerSettings Position="Top" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUnrealizationSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBuyerName" runat="server" Text='<%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Terms">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTerms" runat="server" Text='<%# Convert.ToString( DataBinder.Eval(Container.DataItem, "termdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvinvno2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "invrefno").ToString() %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfinvno" runat="server" Text="Total" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty(Pair)">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtotlprs" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvftotlprs" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="USD">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvusdunrealamt" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usdunrealamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfusdunrealamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Width="100px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Euro">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgveurounrealamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eurounrealamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfeurounrealamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Width="90px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pound">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpoununrealamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "poununrealamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvfpoununrealamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <div class="col-md-7 col-sm-7 col-lg-7 form-inline fa-pull-right" width="500px" style="background-color: antiquewhite; font: 10px" runat="server" id="totaltk">

                                        <div class="form-group  fa-pull-right" style="margin-left: 10px; width: 30%">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Size="10px" CssClass="label">USD(TK) : </asp:Label>
                                            <asp:Label ID="lblUSD" Width="60%" Font-Size="10px" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                        <div class="form-group  fa-pull-right" style="margin-left: 10px; width: 30%">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="true" Font-Size="10px" CssClass="label">Euro(TK) : </asp:Label>
                                            <asp:Label ID="lblEuro" Width="60%" Font-Size="10px" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                        <div class="form-group  fa-pull-right" style="margin-left: 10px; width: 30%">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="true" Font-Size="10px" CssClass="label">Pound(TK) : </asp:Label>
                                            <asp:Label ID="lblPound" Width="60%" Font-Size="10px" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="View5" runat="server">
                    <div class="card card-fluid">
                        <header class="card-header">
                            <!-- .nav-tabs -->
                            <ul class="nav nav-tabs card-header-tabs">
                                <li class="nav-item">
                                    <a class="nav-link active show" data-toggle="tab" href="#details" onclick="onSummaryClick('DetailTab'); okClick();" runat="server" id="detailtab" clientidmode="static" >Details</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#summary" onclick="onSummaryClick('SummaryTab');" runat="server" id="summarytab" clientidmode="static">Summary</a>
                                </li>
                            </ul>
                            <!-- /.nav-tabs -->
                        </header>

                        <div class="card-body" style="min-height: 600px;">
                            <div class="tab-content">
                                <div class="tab-pane fade active show" id="details">
                                    <div class="row">
                                        <asp:GridView ID="gvShipPlanDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea fa-pull-right"
                                            ShowFooter="True" OnPageIndexChanging="gvShipPlanDetails_PageIndexChanging">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Plan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPackPlan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packpln1").ToString() %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBuyer" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "buyerdesc").ToString() %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrderNo3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "orderno").ToString() %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust Order No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrderNo4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "custordno").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Plan Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPackPlanRef1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packplanref").ToString() %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Plan Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPackPlanDate1" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "packplandate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Article Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmlcdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mlcdesc").ToString() %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="Total" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <FooterStyle Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvTotalQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Book/Invoice Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQty1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bal. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQty2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cartoon No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCartNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cartno").ToString() %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlkbtnEdit" runat="server" Target="_blank"
                                                            NavigateUrl='<%# "~/F_19_EXP/CreatePackList?Type=Entry&actcode=" + DataBinder.Eval(Container.DataItem, "mlccod").ToString()+DataBinder.Eval(Container.DataItem, "styleid").ToString() +DataBinder.Eval(Container.DataItem, "colorid").ToString() + DataBinder.Eval(Container.DataItem, "dayid").ToString() + "&genno=" + DataBinder.Eval(Container.DataItem, "packpln").ToString() + "&sircod=" + DataBinder.Eval(Container.DataItem, "buyerid")+ "&date=" + Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "packplandate")).ToString("dd-MMM-yyyy") %>'>
                                                            <i class='fas fa-edit'></i></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnDelete" OnClick="lbtnDelete_Click" OnClientClick="return confirm('Are you sure you want delete');" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "packpln").ToString() %>'><i class="fa fa-trash" style="color:red;"></i></asp:LinkButton>
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
                                </div>

                                <div class="tab-pane fade" id="summary">
                                    <div class="row">
                                        <asp:GridView ID="gvShipPlanSummary" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea fa-pull-right"
                                            ShowFooter="True" Width="400px" OnRowEditing="gvShipPlanSummary_RowEditing" OnRowUpdating="gvShipPlanSummary_RowUpdating"
                                            OnRowCancelingEdit="gvShipPlanSummary_RowCancelingEdit" OnRowDataBound="gvShipPlanSummary_RowDataBound">
                                            <PagerSettings Position="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                                    SelectText="" ShowEditButton="True">
                                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                    <ItemStyle Font-Size="12px" ForeColor="#0000C0" />
                                                </asp:CommandField>

                                                <asp:TemplateField HeaderText="Booked">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkboxBook" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "booked").ToString() == "Yes" ? true : false %>'/>
                                                        <asp:Label ID="lblgvPackPlanRef2" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packplanref").ToString() %>'
                                                            Width="80px"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBooked" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "booked").ToString() %>'
                                                            Width="50px" ForeColor="White"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Plan Ref">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkgvPackPlanRef" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packplanref").ToString() %>' Target="_blank"
                                                            Width="80px"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pack Plan Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPackPlanDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "packplandate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvQty3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Book/Invoce Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBkInvQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Bal. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBalanceQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Articles">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvArticles" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "articles").ToString() %>'
                                                            Width="500px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ex. Factory">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDateExFac" runat="server" CssClass="form-control form-control-sm" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bookdat"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bookdat"))).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDateExFac_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateExFac"></cc1:CalendarExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExFacDate" runat="server" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bookdat"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bookdat"))).ToString("dd-MMM-yyyy") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual Ex. Factory">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDatexfactdat" runat="server" CssClass="form-control form-control-sm" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfactdat"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bookdat"))).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDatexfactdat_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatexfactdat"></cc1:CalendarExtender>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAcExFacDate" runat="server" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfactdat"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfactdat"))).ToString("dd-MMM-yyyy") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Booking Number">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtBookingNumber" runat="server" CssClass="form-control form-control-sm" Target="_blank"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "bookingnumber") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBookingNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "bookingnumber") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkDetails" runat="server" Target="_blank" Text="Details"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true"/>
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
                </asp:View>

                <asp:View ID="ViewIncntv" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 550px;">

                            <asp:GridView ID="gvIncntv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <PagerSettings Position="Top" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgviSlNo" runat="server" CssClass="text-center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgviInvNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "invno").ToString() %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice Ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgviInvRefNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "invrefno").ToString() %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgviInvDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MM-yyyy") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgviAmt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "amount").ToString() %>'
                                                Width="80px" CssClass="text-right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
