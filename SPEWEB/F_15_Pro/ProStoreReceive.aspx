<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProStoreReceive.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProStoreReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {


            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(35));

                var nextId = number + 1;

                var nextIdString = "ContentPlaceHolder1_gvGRR_lblgvQty_" + nextId.toString();

                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(35));
                    var nextId = number - 1;

                    var nextIdString = "ContentPlaceHolder1_gvGRR_lblgvQty_" + nextId.toString();

                    var x = document.getElementById(nextIdString);
                    x.focus();
                }

        }
    </script>



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
                <div class="card-body" >

                    <div class="row">

                        <div class="col-lg-1 col-md-1 col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="lblGrrDate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="lblCurNo" runat="server" CssClass="label" Text="No"></asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" Text="GRR00-" CssClass="form-control form-control-sm small"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" Text="00000" CssClass="form-control form-control-sm small"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-1" style="margin-right: 50px;">
                            <div class="form-group">
                                <asp:Label ID="lblmrfno" runat="server" CssClass="label">Ref No</asp:Label>
                                <asp:TextBox ID="txtMRFNo" runat="server" Style="height: 25px; padding: 2px 5px;" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Production</asp:Label>
                                <asp:DropDownList ID="ddlProdinfo" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlProdinfo_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" Style="height: 28px; width: 50px; padding: 2px 5px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindGrrList" runat="server" CssClass="label" OnClick="ImgbtnFindGrrList_Click">Previous GRR</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevGRRList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>


                    <asp:Panel runat="server" ID="plnQc" Visible="false">

                        <div class="row">

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Store List</asp:Label>
                                    <asp:DropDownList ID="ddlStorid" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-lg-3 col-md-3 col-sm-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPRoNO" runat="server" CssClass="label">QC List</asp:Label>
                                    <asp:TextBox ID="txtsrchPBPNO" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindpbpno" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="ImgbtnFindpbpno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    <asp:DropDownList ID="ddlQcNO" runat="server" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-lg-1 col-md-1 col-sm-1" style="margin-top: 20px;">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnSelectQc" runat="server" CssClass="btn btn-primary checkBox" Style="height: 28px; width: 80px; padding: 2px 5px;" OnClick="lbtnSelectQc_Click">Select</asp:LinkButton>
                                </div>
                            </div>


                            <div class="col-md-2 pading5px pull-right">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>

                        </div>

                    </asp:Panel>

                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">

                    <div class="row">
                        <asp:GridView ID="gvGRR" runat="server" AllowPaging="False"
                            AutoGenerateColumns="False" OnRowDeleting="gvGRR_RowDeleting"
                            ShowFooter="True" Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="prodno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodno" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "prodno").ToString() %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpqcno" runat="server" Style="text-align: left"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "pqcno").ToString() %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="prodcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodcode" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "prodcode").ToString() %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Product Name">
                                    <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdatePurPrepar" runat="server" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lbtnUpdatePurPrepar_Click" Style="text-align: center">Final Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrsirdesc" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "rsirdesc").ToString() %>'
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specifications">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspcfdesc" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot #">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbatchdesc" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "batchdesc").ToString() %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server"> TOTAL</asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC </Br>Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQcQty" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFQcqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Receive </Br>Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtsrecqty" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsrecqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grrrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC </Br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgQcAmt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFQcAmtqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Balance </Br>Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalrecqty" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balrecqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Receive Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvQty" runat="server" Style="text-align: right;" OnKeyUp="GoToNextTextBox(this, event); return false;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFRecqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Receive Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAmt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFRecAmtqty" runat="server"></asp:Label>
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


                    <asp:Panel ID="PanelOther" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="label">Narration</asp:Label>
                                    <asp:TextBox ID="txtOrderNarr" runat="server" CssClass="form-control form-control-sm small" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--<div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="formBtn ">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn text-danger" OnClick="btnClose_Click" Style="margin: 0 5px;"><i class="fa fa-times text-danger"></i>Close</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


