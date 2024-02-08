<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurMRREntry.aspx.cs" Inherits="SPEWEB.F_15_Pro.PurMRREntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvMRRInfo":
                    tblData = document.getElementById("<%=gvMRRInfo.ClientID %>");
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

        function pageLoaded() {

            var gv1 = $('#<%=this.gvMRRInfo.ClientID %>');
            gv1.Scrollable();

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        }

        function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvMRRInfo.ClientID %>");

            var i = 0
            $('#<%=gvMRRInfo.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblCurDate" runat="server" CssClass="label" Text="Arrival Date"></asp:Label>

                                <asp:TextBox ID="txtCurMRRDate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server"
                                    Format="dd.MM.yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="MRR No"></asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurMRRNo1" runat="server" CssClass="form-control form-control-sm small">MRR00-</asp:Label>
                                    <asp:TextBox ID="txtCurMRRNo2" Width="50%" runat="server" CssClass=" form-control form-control-sm small">00000</asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass=" smLbl_to" Text="Ref. No.:"></asp:Label>
                                <asp:TextBox ID="txtMRRRef" runat="server" CssClass=" form-control form-control-sm" TabIndex="1">00000</asp:TextBox>

                            </div>
                        </div>





                        <div class="col-md-3 ">
                            <div class="form-group">
                                <asp:Label ID="lblOrderList" runat="server" CssClass="lblTxt lblDate" Text="Order List"></asp:Label>

                                <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True" TabIndex="5" CssClass=" inputtextbox form-control hidden" Visible="false" Width="200"></asp:TextBox>
                                <asp:CheckBox ID="chkdupMRR" runat="server" Text="Dup.MRR" CssClass="btn btn-primary chkBoxControl primaryBtn pull-left" Visible="false" />
                                <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary btn-sm" runat="server" OnClick="lbtnOk_Click" TabIndex="6">Ok</asp:LinkButton>

                            </div>

                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lblPreMRR" runat="server" CssClass="smLbl_to" Text="Previous MRR" OnClick="ImgbtnPreMRR_Click" ForeColor="Blue"></asp:LinkButton>
                                <asp:TextBox ID="txtSrchPreMRR" runat="server" TabIndex="5" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="ImgbtnPreMRR" CssClass="btn btn-primary srearchBtn" runat="server" Visible="false" OnClick="ImgbtnPreMRR_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevMRRList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                            </div>

                        </div>

                    </div>

                    <asp:Panel ID="Panel1" runat="server" CssClass="row" Visible="false">

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblResList1" runat="server" CssClass="label" Text="Chalan No:"></asp:Label>
                                <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Challan Date"></asp:Label>

                                <asp:TextBox ID="txtChlDate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtChlDate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-4 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lblResList" runat="server" CssClass="label" Text="Materials List" OnClick="ImgbtnFindRes_Click" ForeColor="Blue"></asp:LinkButton>


                                <asp:DropDownList ID="ddlResList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>



                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm">Select</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSelectResAll" runat="server" OnClick="lbtnSelectResAll_Click" CssClass="btn btn-primary btn-sm">Select All</asp:LinkButton>

                                <asp:LinkButton ID="LbtnReqItemShow" OnClick="LbtnReqItemShow_Click" runat="server" CssClass="btn btn-sm btn-warning" Text="Item Expand"></asp:LinkButton>

                            </div>
                        </div>


                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">

                    <div class="row mb-2">
                        <asp:GridView ID="gvRecItem" runat="server" Visible="false"
                            AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Materials">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSSpecification" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LbtnRecItemCalculate" OnClick="LbtnRecItemCalculate_Click" runat="server" CssClass="btn btn-xs btn-success">Adjust <span class="fa fa-repeat"></span></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="80px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSColor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotal" Text="Total" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="60px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSOrdqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotalOrdQty" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSRecBalqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotalReceived" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSBalqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderbal")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotalBalQty" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right"/>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Arrival Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvSMRRQty" runat="server" BorderColor="#ef5b5b" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvRSumSMRRQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Bal. Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvISBalqty" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "adjbalqty") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lgvISBalqtyCalculate" OnClick="lgvISBalqtyCalculate_Click" runat="server" CssClass="btn btn-xs btn-success">Bal.Adjust <span class="fa fa-repeat"></span></asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
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
                        <asp:GridView ID="gvMRRInfo" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvMRRInfo_RowDataBound"
                            ShowFooter="True" Width="16px" OnRowDeleting="gvMRRInfo_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />

                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqnomain" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" DeleteText="<i class='fa fa-trash' style='color:red;'></i>" />

                                <asp:TemplateField HeaderText="Req No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvReqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Materials">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchMatDesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Description of Materials" onkeyup="Search_Gridview(this, 3, 'gvMRRInfo')"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrchSpcf" BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Specification" onkeyup="Search_Gridview(this, 4, 'gvMRRInfo')"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size">
                                    <HeaderTemplate>
                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Size" onkeyup="Search_Gridview(this, 5, 'gvMRRInfo')"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSizeDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color">
                                    <HeaderTemplate>
                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Color" onkeyup="Search_Gridview(this, 6, 'gvMRRInfo')"></asp:TextBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColorDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BOM No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBomNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:Label runat="server" Width="50px" CssClass="text-right font-weight-bold" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Order Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecuptodate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty.">
                                    <FooterTemplate>
                                        <%-- <asp:LinkButton ID="lbtnDelMRR" runat="server" Font-Bold="True" Font-Size="13px"
                                                Height="16px" Style="text-align: center;"
                                                Width="70px" OnClick="lbtnDelMRR_Click" ForeColor="White">Delete</asp:LinkButton>--%>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderbal")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Arrival Qty">
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblFtrTtlArvlQty" Width="70px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRQty" runat="server" BorderColor="#ef5b5b" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMRRAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" ForeColor="Black" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Chalan Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChlnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rack No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRack" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "rackno").ToString() %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location" Visible="false">
                                    <ItemTemplate>
                                        <%-- <asp:TextBox ID="txtgvLoc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "location").ToString() %>' Width="80px"></asp:TextBox>--%>

                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">

                                                <asp:DropDownList ID="ddlval" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" TabIndex="2">
                                                </asp:DropDownList>



                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvMRRNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "mrrnote").ToString() %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCol" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        <asp:Panel ID="Panel4" runat="server" Visible="false">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px inputtxtNarration">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtMRRNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>



                                    </div>

                                </div>
                            </fieldset>
                        </asp:Panel>

                        <table class="table table-responsive tab-content table-bordered" style="display: none;">
                            <tr>
                                <td class="style15">
                                    <asp:Label ID="lblPreparedBy" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                        Style="text-align: right" Text="Prepared By:" Width="99px" Visible="False"></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" Width="100px" Visible="False"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblApprovedBy" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                        Style="text-align: right" Text="Approved By:" Width="80px" Visible="False"></asp:Label>
                                </td>
                                <td class="style71">
                                    <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" Width="120px" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblApprovalDate" runat="server" Font-Bold="True" Font-Size="12px"
                                        Height="16px" Style="text-align: right" Text="Approv.Date:" Width="66px"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" Width="100px"
                                        Visible="False"></asp:TextBox>
                                </td>
                                <td class="style69">&nbsp;
                                </td>
                                <td colspan="3">&nbsp;
                                </td>
                                <td class="style60">&nbsp;
                                </td>
                                <td class="style53">&nbsp;
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

