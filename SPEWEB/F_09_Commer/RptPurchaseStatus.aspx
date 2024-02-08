<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPurchaseStatus.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptPurchaseStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

        };

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvPurStatus":
                    tblData = document.getElementById("<%=gvPurStatus.ClientID %>");
                    break;
            }

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
        };

        function CurrentTabDetail() {
            document.getElementById("currentTabNow").value = "detailTab"
        };


    </script>

    <style type="text/css">
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

            <div class="card card-fluid mb-1">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-1" id="FromD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" id="ToD" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldateto" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2" id="divStoreName" runat="server">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnFindProject" runat="server" CssClass="label" OnClick="imgbtnFindProject_Click">
                                    <i class="fa fa-search mr-1"></i> Store Name
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 " visible="false" id="lblGroup" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblRptGroup" runat="server" CssClass="label">Group</asp:Label>
                                <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm">
                                    <asp:ListItem>Main</asp:ListItem>
                                    <asp:ListItem>Sub-1</asp:ListItem>
                                    <asp:ListItem>Sub-2</asp:ListItem>
                                    <asp:ListItem>Sub-3</asp:ListItem>
                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblSeason" runat="server" CssClass="label" Text="Season"> </asp:Label>
                                <asp:DropDownList ID="ddlSeason" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 " visible="false" id="search" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Challan</asp:Label>
                                <div class="input-group input-group-alt">
                                    <asp:TextBox runat="server" ID="txtSrcMrfNo" CssClass="form-control form-control-sm ">
                                    </asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 " style="display: none;">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkDate" runat="server" Visible="true" TabIndex="10" Text="Up to Date" CssClass="checkBox" AutoPostBack="true" OnCheckedChanged="chkDate_CheckedChanged" />
                            </div>
                        </div>

                        <div id="PnlSupplier" class="col-md-2 col-sm-2 col-lg-2" runat="server" visible="false">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="label" OnClick="imgbtnFindSupplier_Click">
                                    <i class="fa fa-search mr-1"></i> Supplier
                                </asp:LinkButton>
                                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">


                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="vdaywisepurchase" runat="server">
                            <div class="row">
                                <div class="table-responsive">

                                    <asp:GridView ID="gvPurStatus" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvPurStatus_PageIndexChanging" OnRowDataBound="gvPurStatus_RowDataBound" ShowFooter="True" Font-Size="10px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Store Desc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvprojectdesc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsupdesc" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRR No">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lgvMrrNor" Width="90px" Target="_blank"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>' runat="server"></asp:HyperLink>

                                                    <asp:Label ID="lblMrrGenno" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'
                                                        Width="90px"></asp:Label>
                                                    <asp:Label ID="lblMrractcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchChln" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Challan No." onkeyup="Search_Gridview(this,4, 'gvPurStatus')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvchlno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlno")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRR Ref.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrrNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRR Date ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrrDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchCustomer" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Custom No." onkeyup="Search_Gridview(this,9, 'gvPurStatus')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpono" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                        Width="85px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBillNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchMaterial" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Material Name" onkeyup="Search_Gridview(this,12, 'gvPurStatus')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                        Width="45px" CssClass="text-center"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchSpcf" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Specification" onkeyup="Search_Gridview(this,14, 'gvPurStatus')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpecifi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchColor" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Color" onkeyup="Search_Gridview(this,15, 'gvPurStatus')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpcfColor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                        Width="70px" CssClass="text-center"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvLblTotl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Text="Total :"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Ref.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrdref" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrref")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entry User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate22" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                        <asp:View ID="ViewResSummary" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvPurSum" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvPurSum_PageIndexChanging" ShowFooter="True" Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprojectdesc0" runat="server" Height="16px" Text='<%# DataBinder.Eval(Container.DataItem, "pactdesc").ToString() %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt0" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtS" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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

                        <asp:View ID="Pendingbill" runat="server">
                        </asp:View>

                        <asp:View ID="Purchasetrk" runat="server">

                            <div class="col-md-12">
                                <div class="card card-fluid">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="currentTabNow" ClientIDMode="Static" Style="display: none" />

                                                <header class="card-header">
                                                    <ul class="nav nav-tabs card-header-tabs">
                                                        <li class="nav-item">
                                                            <a class="nav-link active show" data-toggle="tab" href="#home" runat="server" id="hometab" onclick="CurrentTabDetail()" clientidmode="static">Details</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" data-toggle="tab" href="#summary" id="summarytab" onclick="CurrentTabSummary()">Summary</a>
                                                        </li>

                                                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-left: auto">
                                                            <div class="form-group">
                                                                <asp:LinkButton ID="imgbtnFindReqno01" runat="server" CssClass="label" OnClick="imgbtnFindReqno01_Click">Requisition</asp:LinkButton>
                                                                <asp:DropDownList ID="ddlReqNo01" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblPOQR" runat="server" CssClass="label" OnClick="imgbtnFindReqno01_Click">PO / Scan</asp:Label>
                                                                <asp:TextBox ID="txtPOQR" AutoPostBack="true" OnTextChanged="txtPOQR_TextChanged" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3 col-sm-3 col-lg-3 mt-3">
                                                            <p class="btn-sm btn-warning"><b><i>Hover on each Ref. No. to see the full details.</i></b></p>
                                                        </div>

                                                    </ul>

                                                </header>

                                                <div class="card-body">
                                                    <div id="myTabContent" class="tab-content">

                                                        <div class="tab-pane fade active show" id="home" runat="server" clientidmode="static">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive">

                                                                        <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPurstk01_RowDataBound"
                                                                            ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea">

                                                                            <PagerSettings Position="Top" />
                                                                            <RowStyle />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sl">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Width="140px"
                                                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                                                        (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                                                        (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                                                            Width="70px"></asp:Label>

                                                                                        <asp:Label ID="lblGrp" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="lblGenno" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "genno1")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="lblactcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="lblReqType" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="Labelssircode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="LabelReqNo" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                        <asp:Label ID="LabelMsrNo" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Ref. No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvgrefno" runat="server" Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")).Substring(0, Math.Min(15, Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")).Length)))+ " ..."  %>'
                                                                                            Width="100px" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Material Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                                            Width="180px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Unit ">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                                            Width="30px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Specification">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                                            Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="60px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Rate">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="60px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="60px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Supplier Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                                            Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="User Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                                            Width="150px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:HyperLink ID="HypLinkPrint" Target="_blank" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
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
                                                            </div>
                                                        </div>

                                                        <div class="tab-pane fade" id="summary" clientidmode="static">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive">

                                                                        <asp:GridView ID="gvPurstk02" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                                            CssClass=" table-responsive  table-hover table-bordered grvContentarea">
                                                                            <PagerSettings Position="Top" />
                                                                            <RowStyle />
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Sl">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Material Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvMaterials4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                                            Width="140px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Specification">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvSpecification2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                                            Width="320px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Req. Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvreqqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlreqqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Survey Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvmsrqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlmsrqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Order Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvpoqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "poqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlpoqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="LC Link<br/>Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvlcqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtllcqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Receive Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvrcvqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlrcvqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="QC Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvqcqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlqcqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Costing Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lgvbillqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label runat="server" ID="lgvTtlbillqty02" Width="70px" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:View>

                        <asp:View ID="ViewBudgetBal" runat="server">
                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnFindMat" runat="server" CssClass="label" OnClick="imgbtnFindMat_Click">Material</asp:LinkButton>
                                        <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">

                                <asp:GridView ID="gvBgdBal" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="512px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requsition No" FooterText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrfNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReqDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFareqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFprogqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Place">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdrQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mrr. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmrrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right; margin-top: 0px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>
                            <asp:Panel ID="Panelbgdbal" runat="server" Visible="False">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtConfirmation" runat="server" Font-Bold="True" Font-Size="14px"
                                                ForeColor="Yellow" Style="text-align: Left; text-decoration: underline;" Text="Confirmation:"
                                                Width="120px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtOpenig1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: left" Text="Budgeted Qty" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvalBudget" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtSuppinchain" runat="server" Font-Bold="True" Font-Size="14px"
                                                ForeColor="Yellow" Style="text-align: Left; text-decoration: underline;" Text="Supply/ In-process:"
                                                Width="120px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: left" Text="Opening" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvalOpenig" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Yellow" Style="text-align: left" Text="Requisition" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvalRequisition" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxttransfer" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: left" Text="Transfer" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvaltrans" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69"></td>
                                        <td colspan="2">
                                            <div style="width: 230px; border-bottom: 1px solid yellow;">
                                            </div>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtOpenig3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: left" Text="Total Qty" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvalTotalSupp" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="Yellow" Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style68">
                                            <asp:Label ID="lbltxtOpenig2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: left" Text="Balance" Width="120px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblvalBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                                Style="text-align: right" Width="100px"></asp:Label>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style69">&nbsp;
                                        </td>
                                        <td class="style67" colspan="2">
                                            <div style="width: 230px; border-top: 1px solid yellow;">
                                            </div>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                        </asp:View>

                        <asp:View ID="ViewPTracking01" runat="server">
                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnFindReqno02" runat="server" CssClass="label" OnClick="imgbtnFindReqno02_Click">Requisition</asp:LinkButton>
                                        <asp:DropDownList ID="ddlReqNo02" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>


                            <div class="row">
                                <asp:GridView ID="gvPurstk" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="734px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="App. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                    Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="False" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Ref.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrderRef" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pordref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvProCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Text="Total: " Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvReqQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvfappQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvforQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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
                                <asp:GridView ID="gvPurstk2" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="1010px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="MR No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mrrno").ToString() %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="MR Ref">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mrrref").ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMrDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderno").ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "billno").ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Bill Ref">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "billref").ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBillDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTotal0" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Text="Total: " Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="MRR Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMrrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvfMrrQty0" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Bill Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvfBillQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="55px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
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

                        <asp:View ID="ProWisePur" runat="server">

                            <div class="col-md-12 Containerss table-responsive" style="height: 400px;">
                                <asp:GridView ID="gvProPur" runat="server" AutoGenerateColumns="False" OnRowCreated="gvProPur_RowCreated"
                                    ShowFooter="True" CssClass="table   table-hover table-bordered grvContentarea">

                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMat" runat="server">Material Name</asp:Label>
                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" ToolTip="Export Excel" CssClass="btn btn-xs btn-danger"><span class="fa fa-file-excel-o"></span></asp:HyperLink>

                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvlbl" runat="server" Style="text-align: right">Total</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r1")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty01" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt01" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r2")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty02" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt02" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq03" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r3")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty03" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty03" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate03" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt03" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt03" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq04" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r4")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty04" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty04" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate04" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt04" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt04" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq05" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r5")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty05" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty05" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate05" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt05" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt05" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq06" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r6")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty06" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty06" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate06" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt06" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt06" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq07" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r7")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty07" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty07" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate07" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt07" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt07" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq08" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r8")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty08" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty08" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate08" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt08" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt08" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq09" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r9")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty09" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty09" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate09" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt09" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt09" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requision <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvReq10" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "r10")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "q10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvqty10" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "u10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "a10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvamt10" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Purchase <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvtqty" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Avarage <br> Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Purchase <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvtamt" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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
