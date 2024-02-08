<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPoShipmentLog.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptPoShipmentLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

            <div class="card card-fluid" style="min-height: 250px;">
                <div class="card-body">
                    <div class="modal-body">

                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" id="shipment-tab" data-toggle="tab" href="#history" role="tab" aria-controls="history" aria-selected="true">History</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" id="history-tab" data-toggle="tab" href="#shipment" role="tab" aria-controls="shipment" aria-selected="false">Shipment</a>
                            </li>
                        </ul>
                        <div class="tab-content mt-3" id="myTabContent1">
                            <div class="tab-pane fade show active mb-2" id="history" role="tabpanel" aria-labelledby="shipment-tab">
                                <asp:GridView ID="gvShipmentHistory" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvShipmentHistory_RowDataBound" ShowFooter="True" Style="text-align: left" Width="200px">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblSl" runat="server" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDelconsum" runat="server" CssClass="btn  btn-xs" Visible="false" OnClientClick="return confirm('Do you want to remove this item?');" Style="text-align: center;" OnClick="lnkbtnDelconsum_Click"
                                                    ToolTip="Remove Shipment Log"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="LOG No.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLgno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "logno")) %>'
                                                    Width="100px"></asp:Label>
                                             
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Challan No.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblchallanno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "challanno")) %>'
                                                    Width="100px"></asp:Label>
                                                 <asp:Label ID="gvlbllogno" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "logno")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvSHLblsirdesc" runat="server" OnClick="gvSHLbtnClick_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="200px"></asp:LinkButton>
                                                 <asp:Label ID="gvlblrsircode" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblspcfdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="200px"></asp:Label>
                                                 <asp:Label ID="gvlblspcfcod" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Challan Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblchallandate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "challandate")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "challandate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Expected Del. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblexpecteddeldate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expecteddeldate")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expecteddeldate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblsirunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="40px" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Shipment Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHLblshipqty" runat="server" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Flag" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSHlblFlag" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flag")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
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
                            <div class="tab-pane fade mb-2" id="shipment" role="tabpanel" aria-labelledby="history-tab">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblChallanNo">Challan No</asp:Label>
                                            <asp:TextBox runat="server" ID="txtChallanNo" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblChallandDate" runat="server">Challan Date</asp:Label>
                                            <asp:TextBox ID="txtChallanDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtChallanDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblExpDeliveryDate">Expected Del. Date</asp:Label>
                                            <asp:TextBox ID="txtExpDeliveryDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExpDeliveryDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <asp:HyperLink ID="HyInkPrintPO" runat="server" CssClass="btn btn-sm btn-primary" Target="_blank" ToolTip="PO Print">Print PO</asp:HyperLink>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
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
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
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
                                                    <ItemStyle HorizontalAlign="left" Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" Width="170px" />
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
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Invoice Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdShippedQty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shippedqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shipping Bal.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdShipBal" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rec. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdComqty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdOrderQty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Shipped Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtShipQty" Style="text-align: right" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Rec. Bal. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdBalqty" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                                            Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Sup Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmdssircode" runat="server" Font-Size="11px"
                                                            Style="text-align: right" Visible="false"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "ssircode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFBalQty" runat="server" Font-Size="11px" Height="16px"
                                                            Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="65px" />

                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12 mt-2">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblNote">Note</asp:Label>
                                            <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" Height="80px" Wrap="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
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
