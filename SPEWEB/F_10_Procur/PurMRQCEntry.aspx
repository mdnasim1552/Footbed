<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurMRQCEntry.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurMRQCEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function OpenQCModal() {

            $('#qcmodal').modal('show');
        }
        function CLoseMOdal() {
            $('#qcmodal').modal('hide');

        }
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

               <%-- var gv1 = $('#<%=this.gvPQCInfo.ClientID %>');
                gv1.Scrollable();--%>

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="moduleItemWrpper">
                <div class="card card-fluid">
                    <div class="card-body">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="row" style="height: 65px;">
                                <div class="form-group col-sm-1">
                                    <asp:Label ID="lblCurDate" runat="server" CssClass="" Text="QC Date"></asp:Label>
                                    <asp:TextBox ID="txtCurMRRDate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server"
                                        Format="dd.MM.yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                                </div>

                                <div class="form-group col-sm-2">
                                    <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="QC No"></asp:Label>
                                    <asp:Label ID="lblCurMRRNo1" runat="server" CssClass=" xsDropDow inputTxt disabled readonlyValue" TabIndex="1">QC00-</asp:Label>
                                    <asp:TextBox ID="txtCurMRRNo2" runat="server" CssClass="form-control form-control-sm" TabIndex="1">000000</asp:TextBox>
                                </div>

                                <div class="form-group col-sm-1">
                                    <asp:Label ID="Label9" runat="server" CssClass=" smLbl_to" Text="Ref. No.:"></asp:Label>
                                    <asp:TextBox ID="txtMRRRef" runat="server" CssClass="form-control form-control-sm" TabIndex="1">00000</asp:TextBox>
                                </div>

                                <div class="form-group col-sm-2">
                                    <asp:Label ID="lblOrderList" runat="server" CssClass="lblTxt lblDate" Text="Order List"></asp:Label>
                                    <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                </div>

                                <div class="checkbox col-sm-1">
                                    <asp:CheckBox ID="chkdupMRR" runat="server" Text="Dup.MRR" Style="margin-top: 20px" CssClass="btn btn-sm btn-primary" Visible="false" />
                                </div>

                                <div class="form-group col-sm-1">
                                    <asp:LinkButton ID="lbtnOk" Style="margin-top: 20px" CssClass="btn btn-sm btn-primary" runat="server" OnClick="lbtnOk_Click" TabIndex="6">Ok</asp:LinkButton>
                                </div>

                                <div class="form-group col-sm-1">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="LbtnReqItemShow" OnClick="LbtnReqItemShow_Click" runat="server" CssClass="btn btn-sm btn-warning" Text="Expand"></asp:LinkButton>
                                    </div>
                                </div>

                                <div runat="server" id="FieldPrintType" class="col-md-2" visible="true">
                                    <asp:Label ID="lblPrntType" runat="server" CssClass="smLbl_to">Print Type</asp:Label>
                                    <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass="chzn-select form-control form-control-sm">
                                        <asp:ListItem Value="1">Leather/Non-Leather</asp:ListItem>
                                        <asp:ListItem Value="2">Label & Hang Tag</asp:ListItem>
                                        <asp:ListItem Value="3">Outsole</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-sm-2">
                                    <asp:LinkButton ID="lblPreMRR" runat="server" CssClass="pl-0 nav-link py-0" Text="Previous QC" OnClick="ImgbtnPreMRR_Click" ForeColor="Blue"></asp:LinkButton>
                                    <asp:TextBox ID="txtSrchPreMRR" runat="server" TabIndex="5" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                </div>

                                <div class="form-group col-sm-1">
                                    <asp:LinkButton ID="ImgbtnPreMRR" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 22px;" Visible="false" OnClick="ImgbtnPreMRR_Click" TabIndex="6"><span class="fa fa-search"> </span></asp:LinkButton>
                                </div>

                                <div class="form-group col-sm-1">
                                    <asp:DropDownList ID="ddlPrevMRRList" runat="server" CssClass="form-control form-control-sm" Style="margin-top: 22px;" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

                <asp:Panel ID="Panel1" runat="server" Visible="false" style="margin-top:-20px;">
                    <div class="card card-fluid mt-n4 mb-1" >
                        <div class="card-body">
                            <div class="row" style="height: 60px;">
                                <div class="form-group col-md-2">
                                    <asp:Label ID="lblResList1" runat="server" CssClass="" Text="Chalan No:"></asp:Label>
                                    <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-4">
                                    <asp:LinkButton ID="lblResList" runat="server" CssClass="lblTxt lblName" Text="Materials List" OnClick="ImgbtnFindRes_Click" ForeColor="Blue"></asp:LinkButton>
                                    <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control form-control-sm" TabIndex="1" Visible="false"></asp:TextBox>

                                    <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-sm btn-primary" runat="server" Visible="false" OnClick="ImgbtnFindRes_Click" TabIndex="2"><span class="fa fa-search"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlResList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" Style="margin-top: 20px" CssClass="btn btn-sm btn-primary">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSelectResAll" runat="server" OnClick="lbtnSelectResAll_Click" Style="margin-top: 20px" CssClass="btn btn-sm btn-primary">Select All</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div >
                                <asp:GridView ID="gvPQCItem" runat="server" Visible="false"
                                    AutoGenerateColumns="False" OnRowDataBound="gvPQCItem_RowDataBound" 
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="ItemDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LbtnRecItemCalculate" OnClick="LbtnRecItemCalculate_Click" runat="server" CssClass="btn btn-xs btn-success">Adjust <span class="fa fa-repeat"></span></asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSSpecification" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left"  />
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
                                            <ItemStyle HorizontalAlign="left" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvQcUnit" runat="server" CssClass="form-control"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left"  Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checking Method">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlQccheckmethod" runat="server" CssClass="form-control">
                                                    <asp:ListItem>AQL</asp:ListItem>
                                                    <asp:ListItem>4 Point</asp:ListItem>
                                                    <asp:ListItem>Percentage</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recv Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSOrdqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qc Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgvSRecBalqty" runat="server" Style="text-align: right" BorderStyle="None" CssClass="bg-twitter"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lgvISRecqty" runat="server" Style="text-align: right" BorderStyle="None" CssClass="bg-twitter"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvRSumRecqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checked Details/Problematic">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvchckdetails" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chckdetails")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Findings/Rejection">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvFindings" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finding")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Result" ControlStyle-Width="100px" >
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlQcStatus" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Fail"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Pass" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Not Confirmed"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<label class="switch">
                                                    <asp:CheckBox ID="ChckStatus" runat="server" />
                                                    <span class="slider round"></span>
                                                </label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvQcRemarks" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
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
                           <div style="margin-top:20px; margin-bottom:10px;">
                                <asp:GridView ID="gvPQCInfo" runat="server" AllowPaging="False" AutoGenerateColumns="False" OnRowDataBound="gvPQCInfo_RowDataBound"
                                    ShowFooter="True" Width="16px" Margin-Top="20px" Font-Size="Smaller" OnRowDeleting="gvPQCInfo_RowDeleting" CssClass="table-striped table-hover table-bordered">
                                    <PagerSettings Visible="False" />

                                    <Columns>
                                        <%--  <asp:CommandField ShowDeleteButton="True" /> --%>
                                        <asp:CommandField ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" HeaderText="" ItemStyle-Font-Size="10px"
                                            ShowDeleteButton="True">
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:CommandField>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: center"
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

                                        <asp:TemplateField HeaderText="Req No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="P.O No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvorderno1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                    Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                    Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                    Width="300px">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbnTotal" Font-Bold="true" runat="server">Total</asp:Label>

                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBomNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecuptodate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvRecF" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Qty.">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnDelMRR" runat="server" Font-Bold="True" Font-Size="13px" Visible="false"
                                                    Height="16px" Style="text-align: center;"
                                                    Width="70px" OnClick="lbtnDelMRR_Click">Delete</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvBalFQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Recive UP" Visible="false">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRecvup" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recup")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QC Qty.">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMRRQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvQCFQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate" Visible="False">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMRRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMRRAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" ForeColor="Black" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Chalan Qty" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvChlnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                                <div class="form-group">
                                                    <div class="col-md-6 pading5px">
                                                        <asp:DropDownList ID="ddlval" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%-- <asp:TextBox ID="txtgvLoc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "location").ToString() %>' Width="80px"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMRRNote" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "purqcnote").ToString() %>' Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LbtnToClear" OnClick="LbtnToClear_Click" Width="110%" runat="server" CssClass=" btn btn-sm btn-warning text-white">Clear <span class="fa fa-recycle"></span></asp:LinkButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qc">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnQcUpdate" OnClick="LbtnQcUpdate_Click" CssClass="" runat="server"><span class="fas fa-pen"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="" />--%>
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
                </asp:Panel>

                <div id="qcmodal" class="modal fade bd-example-modal-lg" role="dialog">
                    <div class="modal-dialog modal-xl" role="document">
                        <div class="modal-content ">
                            <div class="modal-header">
                                <h4 class="modal-title" id="qcModalTitle"><span class="fa fa-table mr-2"></span>Qc Details Information Update </h4>

                            </div>
                            <div class="modal-body">

                                <asp:GridView ID="gvqcDeails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvqcDeails_RowDataBound"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Visible="False" />

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checking Method">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlcheckmethod" runat="server" CssClass="form-control form-control-sm" Width="200px">
                                                    <asp:ListItem>AQL</asp:ListItem>
                                                    <asp:ListItem>4 Point</asp:ListItem>
                                                    <asp:ListItem>Percentage</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receiv Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRecvqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvUom" runat="server" CssClass="form-control form-control-sm" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checked Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvCckqty" runat="server" CssClass="form-control form-control-sm text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pass Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvpassqty" runat="server" CssClass="form-control form-control-sm text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checked Details.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCheckDetails" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chckdetails")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Findings">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblgvFindings" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finding")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddlPassFail" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Text="Fail"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Pass" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Not Confirmed"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<label class="switch">
                                                        <asp:CheckBox ID="ChckStatus" runat="server" />
                                                        <span class="slider round"></span>
                                                    </label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>' Width="150px"></asp:TextBox>
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
                            <div class="modal-footer ">
                                <asp:LinkButton ID="LbtnUpdateQcDetails" runat="server" OnClick="LbtnUpdateQcDetails_Click" OnClientClick="CLoseMOdal();" CssClass="btn btn-sm btn-success">
                                        <i class="fa fa-save"></i> Update 
                                </asp:LinkButton>
                                <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
