<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="StoreMtReqTrnsGatePass.aspx.cs" Inherits="SPEWEB.F_11_RawInv.StoreMtReqTrnsGatePass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            var gridview = $('#<%=this.gvAprovInfo.ClientID %>');
            $.keynavigation(gridview);

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row mb-1">
                        <div class="col-md-1">
                            <div class="form-group mb-0">
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtCurAprovDate" runat="server" CssClass="form-control flatpickr-input pr-0"></asp:TextBox>
                                <cc1:calendarextender id="txtCurTransDate_CalendarExtender" runat="server"
                                    format="dd-MMM-yyyy" targetcontrolid="txtCurAprovDate">
                                </cc1:calendarextender>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group mb-0">
                                <label class="control-label" for="ToDate">Gate Pass No.</label>


                            </div>
                            <div class="form-group mb-0">
                                <asp:Label ID="lblGatePassNo1" runat="server" CssClass="form-control flatpickr-input" Width="80"></asp:Label>
                                <asp:Label ID="txtGatePassNo2" runat="server" CssClass="form-control flatpickr-input" Width="80" Style="margin-left: 5px;"></asp:Label>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group mb-0">
                                <label class="control-label" for="ddlUserName">Gate Pass </label>
                                <asp:TextBox ID="txtGatemPassNo" runat="server" CssClass="form-control flatpickr-input pr-0"></asp:TextBox>


                            </div>
                        </div>
                        <div class="col-md-1">

                            <div class="form-group mb-2">

                                <label class="control-label" for="ddlUserName"></label>


                            </div>
                            <div class="form-group mb-0">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>


                        </div>




                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevAprovList" runat="server" CssClass="control-label d-block" Style="margin-bottom: 8px" OnClick="lbtnPrevAprovList_Click"
                                    TabIndex="3">
                                    Previous Order</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="chzn-select form-control" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>


                    </div>

                </div>
            </div>
            
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <div class="card card-fluid">
                    <div class="card-body">
                        <div class="row mb-2 d-none">
                            <%--Currently this section display none by sumon--%>

                            <label class="col-md-1 control-label" for="FromDate">Requisition</label>

                            <asp:DropDownList ID="ddlResList" runat="server" CssClass="col-md-2 chzn-select form-control" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>

                            <label class="col-md-1 control-label" for="FromDate">Resource</label>

                            <asp:DropDownList ID="ddlResourcelist" runat="server" CssClass="col-md-3 chzn-select form-control" OnSelectedIndexChanged="ddlResourcelist_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>



                            <label class="col-md-1 control-label" for="FromDate">Specification</label>

                            <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="col-md-2 chzn-select form-control"
                                AutoPostBack="true">
                            </asp:DropDownList>

                            <div class="col-md-2">
                                <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectRes_Click" TabIndex="2">Select</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectAll_Click" TabIndex="3">Select All</asp:LinkButton>


                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAprovInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="16px" OnRowDeleting="gvAprovInfo_RowDeleting">
                                    <pagersettings visible="False" />
                                    <rowstyle />
                                    <columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer From">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvtfpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </itemtemplate>

                                            <footerstyle horizontalalign="Center" />
                                            <headerstyle horizontalalign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer To">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvttpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </itemtemplate>

                                            <footerstyle horizontalalign="Center" />
                                            <headerstyle horizontalalign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Order <br> To Order">
                                            <itemtemplate>
                                                <asp:Label ID="forderlbl" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "forder")) %>'
                                                    Width="200px"></asp:Label>

                                            </itemtemplate>

                                            <footerstyle horizontalalign="Center" />
                                            <headerstyle horizontalalign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))   %>'
                                                    Width="140px"></asp:Label>
                                            </itemtemplate>
                                            <headerstyle horizontalalign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvspecification" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="120px"></asp:Label>
                                            </itemtemplate>
                                            <edititemtemplate>
                                                <asp:DropDownList ID="ddlspecification" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </edititemtemplate>
                                            <headerstyle horizontalalign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="75px"></asp:Label>
                                            </itemtemplate>
                                            <footertemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="lbtnResFooterTotal_Click">
                                                    Total :</asp:LinkButton>

                                            </footertemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No.">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvmrfno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="55px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvmtrfqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </itemtemplate>
                                            <footertemplate>
                                                <asp:LinkButton ID="lbtnUpdatePurAprov" runat="server" Visible="false" CssClass="btn btn-danger btn-sm" OnClick="lbtnUpdatePurAprov_Click">Update</asp:LinkButton>

                                            </footertemplate>
                                            <itemstyle horizontalalign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Bal. Qty">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvbalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </itemtemplate>

                                            <itemstyle horizontalalign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Approved Qty.">
                                            <itemtemplate>
                                                <asp:TextBox ID="txtgvaprovedQty" runat="server" BorderColor="#007C69" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "getpqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px">
                                                </asp:TextBox>
                                            </itemtemplate>

                                            <itemstyle horizontalalign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved </br> " Visible="false">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </itemtemplate>

                                            <itemstyle horizontalalign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <itemtemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" DeleteText='<span class="fa fa-trash btn-sm"></span>' />

                                    </columns>
                                    <footerstyle cssclass="grvFooter" />
                                    <editrowstyle />
                                    <alternatingrowstyle />
                                    <pagerstyle cssclass="gvPagination" />
                                    <headerstyle cssclass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="table-responsive">
                                <asp:Panel ID="Panel3" runat="server">
                                    <fieldset class="scheduler-border fieldset_Nar">
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3 ">
                                                </div>
                                                <div class="col-md-4 pading5px">
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="lblReqNarr0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtgetpNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-2 pading5px asitCol2">
                                                </div>
                                                <div class="col-md-2 pading5px asitCol2">
                                                </div>
                                                <div class="col-md-2 pading5px asitCol2">
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <asp:Panel ID="pnlMarketSurvey" runat="server" Visible="false">

                                        <fieldset class="scheduler-border">
                                            <div class="form-horizontal">

                                                <div class="form-group">
                                                    <div class="col-md-6 pading5px ">
                                                    </div>

                                                </div>


                                            </div>
                                        </fieldset>


                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
