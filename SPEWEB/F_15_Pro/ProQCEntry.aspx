<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProQCEntry.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProQCEntry" %>

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

            //  alert(type);
            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(37));

                var nextId = number + 1;
                var nextIdString = "ContentPlaceHolder1_gvGRR_txtgvrejqc_" + nextId.toString();
                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(37));
                    var nextId = number - 1;
                    var nextIdString = "ContentPlaceHolder1_gvGRR_txtgvrejqc_" + nextId.toString();
                    var x = document.getElementById(nextIdString);
                    x.focus();
                }

        }
        function GoToNextTextBox1(currentTxtId, e) {

            //  alert(type);
            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(38));

                var nextId = number + 1;
                var nextIdString = "ContentPlaceHolder1_gvGRR_txtgvActQty_" + nextId.toString();
                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(38));
                    var nextId = number - 1;
                    var nextIdString = "ContentPlaceHolder1_gvGRR_txtgvActQty_" + nextId.toString();
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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblGrrDate" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblCurNo" runat="server" CssClass="label" Text="No:"></asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" Text="ISU00-" CssClass="form-control form-control-sm small"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" Text="00000" CssClass="form-control form-control-sm small"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblmrfno" runat="server" CssClass="label">QC No</asp:Label>
                                <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">QC Date</asp:Label>
                                <asp:TextBox ID="txtQcDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtQcDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblPRoNO" runat="server" CssClass="label">Production List</asp:Label>
                                <asp:TextBox ID="txtsrchPBPNO" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="ImgbtnFindpbpno" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="ImgbtnFindpbpno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlProNO" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindGrrList" runat="server" CssClass="label" OnClick="ImgbtnFindGrrList_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevGRRList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <asp:Panel ID="PanelTime" runat="server" Visible="False">
                            <div class="col-md-4">
                                <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" Text="Hour"></asp:Label>
                                <asp:DropDownList ID="ddlhour" runat="server" Width="150" CssClass="inputTxt chzn-select">
                                </asp:DropDownList>
                            </div>

                        </asp:Panel>

                    </div>

                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <asp:GridView ID="gvGRR" runat="server"
                            AutoGenerateColumns="False"
                            ShowFooter="True" Width="400px" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDeleting="gvGRR_RowDeleting">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                <asp:TemplateField HeaderText="prodno" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodno" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "prodno").ToString() %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="itmcod" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodcode" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "prodcode").ToString() %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Materials Name">
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
                                    <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdatePurPrepar" runat="server" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lbtnUpdatePurPrepar_Click" Style="text-align: center">Final Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcfdesc" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot #">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbatchdesc" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "batchdesc").ToString() %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFtotal" runat="server" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFRecqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC Done</br> Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcompqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "compqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                        <asp:Label ID="lblQcBal" runat="server" Visible="false"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFQcDoneqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="QC </Br>Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvActQty" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" OnKeyUp="GoToNextTextBox1(this, event); return false;"
                                            Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFQcqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC </Br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActAmt" runat="server"
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


                                <asp:TemplateField HeaderText="Reject</Br>Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrejqc" runat="server" BorderColor="#99CCFF" OnKeyUp="GoToNextTextBox(this, event); return false;"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejqc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFrejqty" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                    <HeaderStyle HorizontalAlign="center" VerticalAlign="top" ForeColor="Red" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject </Br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrejamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFrejamtqty" runat="server"></asp:Label>
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
                                <div class="col-md-6 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="label"> Narration:</asp:Label>
                                        <asp:TextBox ID="txtOrderNarr" runat="server" CssClass="form-control form-control-sm small" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </div>



                                </div>
                            </div>
                        </asp:Panel>
                    
                    <div class="row">
                        <asp:Panel ID="pnlHQC" runat="server" Visible="False" BorderColor="Indigo" BorderWidth="2px" BorderStyle="Solid" GroupingText="Hourly QC">
                            <asp:GridView ID="gvHourlyQc" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderTemplate>
                                            <table>

                                                <tr>

                                                    <th class="pull-right">
                                                        <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="80px">Name</asp:HyperLink>
                                                    </th>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc"))  %>' Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpstatus" runat="server" Font-Bold="True" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qcstatusdesc")) %>' Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalRecv" runat="server" Font-Bold="True" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "qcdate")).ToString("dd-MMM-yyyy") %>' Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalrecv")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalFRecv" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr01" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t1")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr01" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr02" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t2")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr02" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr03" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t3")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr03" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr04" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t4")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr04" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr05" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t5")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr05" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr06" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t6")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr06" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr07" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t7")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr07" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr08" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t8")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr08" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr09" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t9")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr09" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr10" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t10")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr10" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr11" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t11")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr11" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr12" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t12")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr12" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr13" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t13")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr13" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr14" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t14")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr14" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr15" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t15")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr15" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr16" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t16")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr16" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr17" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t17")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr17" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr18" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t18")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr18" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr19" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t19")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr19" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr20" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t20")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr20" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr21" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t21")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr21" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr22" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t22")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr22" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr23" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t23")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr23" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrpr24" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t24")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblrpFr24" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="65px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


