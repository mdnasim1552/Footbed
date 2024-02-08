<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurBillEntry.aspx.cs" Inherits="SPEWEB.F_09_Commer.PurBillEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card card-fluid">
                        <div class="card-body ">

                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1 pading5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Bill Date: </asp:Label>
                                    <asp:TextBox ID="txtCurBillDate" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>

                                    <cc1:CalendarExtender ID="txtCurBillDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurBillDate"></cc1:CalendarExtender>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to text-left"> Bill No</asp:Label>
                                    <div class="form-inline">
                                        <asp:Label ID="lblCurBillNo1" runat="server" Width="45%" CssClass="form-control form-control-sm">PBL00- </asp:Label>
                                        <asp:TextBox ID="txtCurBillNo2" runat="server" Width="55%" CssClass="form-control form-control-sm">00000</asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 pading5px">

                                    <asp:Label ID="lblrefNo" runat="server" CssClass="smLbl_to text-left" Style="padding-left: 21px;"> Ref. No</asp:Label>

                                    <asp:TextBox ID="txtBillRef" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-lg-4 pading5px">
                                    <asp:Label ID="lblOrderList0" runat="server" CssClass="lblTxt lblName">Order</asp:Label>
                                    <div class=" form-inline">
                                        <asp:TextBox ID="txtSrchOrderrefno" runat="server" Width="15%" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:LinkButton ID="imgSearchOrderno" runat="server" CssClass="btn btn-sm btn-primary srearchBtn colMdbtn" OnClick="imgSearchOrderno_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>

                                        <asp:DropDownList ID="ddlOrderList" runat="server" Width="60%" CssClass="form-control form-control-sm" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm  btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                </div>
                          
                                <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                    <asp:Label ID="lblProjectList1" runat="server" CssClass="lblTxt lblName">Store</asp:Label>
                                    <asp:TextBox ID="lblSupplier" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>



                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                    <asp:Label ID="lblResList4" runat="server" CssClass=" smLbl_to">Req. Ref.</asp:Label>
                                    <asp:TextBox ID="lblReqno" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>


                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 pading5px" style="margin-top: 22px;">
                                    <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True" Width="93px"
                                        OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging" Visible="False" />



                                </div>

                                <div class="col-md-2 pading5px">
                                    <div class="colMdbtn pading5px">
                                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                    </div>



                                </div>



                            </div>

                            <div class="row">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblPreBill" runat="server" CssClass="lblTxt lblName" Visible="False">Prev. Bill List</asp:Label>
                                    <asp:TextBox ID="txtSrchPreBill" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="False"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnPrevBillList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="False" OnClick="lbtnPrevBillList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>

                                <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="ddlPrevBillList" runat="server" CssClass="form-control inputTxt chzn-select">
                                    </asp:DropDownList>

                                </div>



                            </div>
                        </div>

                   
                        <div class="card-body">
                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                <div class="row form-inline">

                                    <div class="col-md-4 col-sm-4 col-lg-4 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Store</asp:Label>
                                        <div class=" form-inline">
                                            <asp:TextBox ID="txtSrchProjectName" runat="server" Width="15%" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-sm btn-primary srearchBtn colMdbtn" OnClick="imgSearchProject_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>


                                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="60%" CssClass="form-control form-control-sm"
                                                OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-sm btn-primary srearchBtn colMdbtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-6 col-sm-6 col-lg-6 pading5px">
                                        <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName">Charge</asp:Label>
                                        <div class=" form-inline">
                                            <asp:TextBox ID="txtSrchCharge" runat="server" Width="10%" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <asp:LinkButton ID="imgSearchCharge" runat="server" CssClass="btn btn-sm btn-primary srearchBtn colMdbtn" OnClick="imgSearchCharge_Click"><i class="fa fa-search" aria-hidden="true"></i></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlCharge" runat="server" Width="50%" CssClass="fform-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblvalvounum" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 400px">
                            <asp:GridView ID="gvBillInfo" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="gvBillInfo_RowDeleting" OnRowDataBound="gvBillInfo_RowDataBound">
                                <PagerSettings Visible="False" />
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProject" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Req No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReqNo1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No." Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOrderno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QCC No.">

                                        <%-- <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateBill" runat="server" Font-Bold="True" 
                                                            Font-Size="13px" ForeColor="White" Height="16px" onclick="lbtnUpdateBill_Click" 
                                                            style="text-align: center; " Width="70px">Update</asp:LinkButton>
                                                    </FooterTemplate>--%>

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMRRNo1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qcno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnDeleteBill" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnDeleteBill_Click">Delete All</asp:LinkButton>

                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR Ref" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMrrref" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />


                                    <asp:TemplateField HeaderText="Materials">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMat" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67"
                                                AutoPostBack="True" Font-Bold="True" Font-Size="14px"
                                                OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                Width="250px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOM No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBomNo" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <FooterTemplate>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvSumOrdr" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QC Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMRRQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvSumQc" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMRRRate" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <%-- <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Final Update</asp:LinkButton>--%>
                                        </FooterTemplate>


                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount (Management)" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmMRRAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mmrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFMMRRAmt" runat="server" Width="80px" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvMRRAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="Solid" BorderColor="#889ae0" BorderWidth="1px" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFooterTMRRAmt" runat="server" Width="80px" Font-Bold="True"
                                                Font-Size="12px" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Short Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSQty" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRemarks" runat="server" Font-Size="9px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remrks")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <asp:Panel ID="Panel2" runat="server" Visible="False">


                                <div class="col-md-6 col-sm-6 col-lg-6">
                                                <div class="form-inline">
                                                    <asp:Label ID="lblReqNarr" runat="server" Text="Narration:"></asp:Label>

                                                    <asp:TextBox ID="txtBillNarr" runat="server" Width="100%" CssClass="form-control"  Rows="2" TextMode="MultiLine"></asp:TextBox>
                                              
                                                    <asp:CheckBox ID="CheckAccUpdate" runat="server" Text="Accounts Update" />
                                              
                                                    <asp:CheckBox ID="ChkBoxAdjust" runat="server" Text="Costing Adjust" />
                                                </div>
                                            </div>

                                       
                                        <div class="form-inline">
                                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                <asp:Label ID="lbladjjustment" runat="server" CssClass="lblTxt lblName" Text="Adjustment: "></asp:Label>
                                                <asp:DropDownList ID="ddlPayType" runat="server" Font-Size="12px" TabIndex="13"
                                                     Width="100%" AutoPostBack="True" CssClass="form-control form-control-sm"
                                                    OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                                    <asp:ListItem Value="001">None</asp:ListItem>
                                                    <asp:ListItem Value="003">Adjustment</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                <asp:Label ID="lblbillrefdate" runat="server" CssClass="lblTxt lblName" Text="Bill Date: "></asp:Label>
                                                <asp:TextBox ID="txtBillrefDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtBillrefDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtBillrefDate"></cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                <asp:Label ID="lblchequedate" runat="server" CssClass="lblTxt lblName" Text="Cheque Date: "></asp:Label>
                                                <asp:TextBox ID="txtChequeDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtChequeDate"></cc1:CalendarExtender>
                                                 </div>
                                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Exp Date: "></asp:Label>
                                                <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd-MMM-yyyy"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender_txtExpDate" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExpDate"></cc1:CalendarExtender>
                                            </div>



                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-2 pading5px asitCol2 ">
                                                <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By: " Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6 pading5px">

                                                <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                                <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approval Date: " Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="form-group">
                                            <div class="col-md-2 pading5px asitCol2 ">
                                                <asp:Label ID="lblVounum" runat="server" CssClass="lblTxt lblName" Text="Acc. Vou. No" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtAccVounum" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)" Visible="false" ReadOnly="True"></asp:TextBox>

                                            </div>

                                        </div>
                                   
                            </asp:Panel>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

       
   
</asp:Content>

