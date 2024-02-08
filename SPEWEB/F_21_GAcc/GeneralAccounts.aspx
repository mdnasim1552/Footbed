<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="GeneralAccounts.aspx.cs" Inherits="SPEWEB.F_21_GAcc.GeneralAccounts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        th #lblas {
            text-align: left !important;
        }
    </style>
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          <%--  $('#<%=this.txtScrchConCode.ClientID %>').focus();--%>

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);


            //            var gridview = $('#<%=this.dgv1.ClientID %>');
            //            $.keynavigation(gridview);
            //$(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            $('.chzn-select').chosen({ search_contains: true });
            //$(".chzn-select").chosen();
            //$(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        };

        function PrintRpt(printype) {

            window.open('../RptViewer.aspx?PrintOpt=' + printype + '', '_blank');
        }

    </script>
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblcurVounum" runat="server" CssClass="label">Voucher No:</asp:Label>
                                <div class="form-inline">
                                    <asp:TextBox ID="txtcurrentvou" Text="VOU00" runat="server" Style="width: 30%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox ID="txtCurrntlast6" Text="00000" runat="server" Style="width: 30%;" CssClass="form-control form-control-sm  small" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left:-60px">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblType" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddlvoucher" runat="server" CssClass="form-control form-control-sm chzn-select " AutoPostBack="True" OnSelectedIndexChanged="ddlvoucher_SelectedIndexChanged">
                                    <asp:ListItem Value="BD">Bank Payment</asp:ListItem>
                                    <asp:ListItem Value="CD">Cash Payment</asp:ListItem>
                                    <asp:ListItem Value="BC">Bank Deposit</asp:ListItem>
                                    <asp:ListItem Value="CC">Cash Deposit</asp:ListItem>
                                    <asp:ListItem Value="CT">Contra Voucher</asp:ListItem>
                                    <asp:ListItem Value="JV">Journal Voucher</asp:ListItem>
                                    <asp:ListItem Value="PV" Enabled="false">Post Dated Voucher</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                <asp:LinkButton ID="jVOkbtn" runat="server" Visible="false" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 " style="margin-left:-40px">
                            <div class="form-group">
                                <%--  <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtScrchConCode" runat="server" CssClass="" Visible="false"></asp:TextBox>--%>
                                <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="label" OnClick="ibtnFindConCode_Click">Control Code</asp:LinkButton>
                                <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Label ID="bankBalance" runat="server" ForeColor="Red" Style="color: darkblue;" CssClass=" smLbl_to small"></asp:Label>

                            </div>
                        </div>

                        <div class="col-md-1 col-sm- col-lg-1 form-group">
                            <asp:CheckBox ID="chkPrint" runat="server" Text="Cheque Print" CssClass="checkbox small" /><br />
                            <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="checkbox small" />
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblCurr" runat="server" CssClass="label" Visible="false">Currency</asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="ddlCurrency" runat="server" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="input-group-text text-success" ToolTip="Create List" Target="_blank" Visible="false"
                                            NavigateUrl="~/F_34_Mgt/AccConversion"><span class="fa fa-plus"></span></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblConv" Visible="false" runat="server" CssClass="label">Conv.Rate</asp:Label>
                                <asp:TextBox ID="lblConRate" Visible="false" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblPrivVou" runat="server" CssClass=" smLbl_to" Visible="false">Previous</asp:Label>
                                <asp:TextBox ID="txtScrchPre" runat="server" CssClass=" inputBox50px" TabIndex="1" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="control-label" OnClick="ibtnFindPrv_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">

                    <asp:Panel ID="Panel2" runat="server" Visible="False">

                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <%--    <asp:Label ID="lblactcode" runat="server" CssClass="lblTxt lblName hidden">Head of Account</asp:Label>
                                        <asp:TextBox ID="txtserceacc" runat="server" CssClass="inputtextbox hidden" TabIndex="11"></asp:TextBox>--%>
                                    <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="label" OnClick="lnkAcccode_Click">Head of Account</asp:LinkButton>
                                    <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccCodeBook?InputType=Accounts" CssClass="fa fa-plus-circle"></asp:HyperLink>

                                    <asp:DropDownList ID="ddlacccode" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlacccode_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 ">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkRescode" runat="server" CssClass="label" OnClick="lnkRescode_Click">Sub of Account</asp:LinkButton>
                                    <asp:HyperLink ID="HylSubAcc" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint" CssClass="fa fa-plus-circle"></asp:HyperLink>

                                    <asp:DropDownList ID="ddlresuorcecode" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="label" OnClick="lnkSpecification_Click" Visible="false">Specification</asp:LinkButton>
                                    <asp:DropDownList ID="ddlSpclinf" runat="server" CssClass="chzn-select form-control form-control-sm" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlSpclinf_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkOk0" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk0_Click">Add</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:Label ID="lblBalance" runat="server" ForeColor="Red" Style="padding: 5px 10px; color: darkblue;" CssClass=" smLbl_to"></asp:Label>

                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:LinkButton ID="lnkBillNo" runat="server" CssClass="label" OnClick="lnkBillNo_Click" Visible="false">Bill No</asp:LinkButton>
                                    <asp:DropDownList ID="ddlBillList" runat="server" CssClass="chzn-select form-control form-control-sm" Visible="false"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-1 col-sm-1 col-lg-1 " style="display: none;">
                                <div class="form-group">
                                    <div class="col-md-2 pading5px" style="display: none">
                                        <asp:Label ID="lblrate" runat="server" CssClass=" lblVal" Visible="false">Rate</asp:Label>
                                        <asp:TextBox ID="txtrate" runat="server" CssClass="inputtextbox" TabIndex="29" Style="text-align: right;" Visible="false"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3 pading5px asitCol3" style="display: none">
                                        <asp:Label ID="lblqty" runat="server" CssClass="lblTxt lblName" Visible="false">Quantity</asp:Label>
                                        <asp:TextBox ID="txtqty" runat="server" CssClass="inputtextbox" TabIndex="30" Visible="false" Style="text-align: right;"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2  pading5px" style="display: none">
                                        <asp:Label ID="lblremarks" runat="server" CssClass="lblVal">Remarsk</asp:Label>
                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="inputtextbox" TabIndex="24"></asp:TextBox>

                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblDramt" runat="server" CssClass="lblTxt lblName">Dr. Amount</asp:Label>
                                        <asp:TextBox ID="txtDrAmt" runat="server" CssClass="inputtextbox" Width="110" TabIndex="20" Style="text-align: right;"></asp:TextBox>

                                    </div>


                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblCramt" runat="server" CssClass="  smLbl_to">Cr. Amount</asp:Label>
                                        <asp:TextBox ID="txtCrAmt" runat="server" Width="110" CssClass="inputtextbox" TabIndex="22" Style="text-align: right;"></asp:TextBox>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:CheckBox ID="chkCopy" runat="server" TabIndex="10" Text="Copy" AutoPostBack="True" OnCheckedChanged="chkCopy_CheckedChanged" />
                                    </div>

                                </div>
                                <asp:Panel ID="PnlCopy" runat="server" Visible="False">

                                    <fieldset class="scheduler-border fieldset_Nar">
                                        <div class="form-horizontal">
                                            <div class="form-group">

                                                <div class="col-md-2 pading5px">
                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt hidden lblName">From Voucher</asp:Label>
                                                    <asp:TextBox ID="txtScrchcopyvoucher" runat="server" CssClass="inputtextbox hidden" TabIndex="1"></asp:TextBox>
                                                    <asp:LinkButton ID="ibtnCopyVoucher" CssClass="lblTxt lblName" runat="server" OnClick="ibtnCopyVoucher_Click" TabIndex="2">From Voucher</asp:LinkButton>

                                                </div>

                                                <div class="col-md-9" style="width: 350px;">
                                                    <asp:DropDownList ID="ddlcopyvoucher" runat="server" Style="width: 350px;" CssClass="form-control inputTxt chzn-select" TabIndex="3">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="lbtnCopyVoucher" runat="server" Style="margin-left: 2px" OnClick="lbtnCopyVoucher_Click" TabIndex="4">Copy</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>

                                </asp:Panel>

                                <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="" Visible="false" />
                            </div>
                        </div>

                    </asp:Panel>

                    <div class="row ">
                        <div class="col-md-8 col-sm-8 col-lg-8 ">
                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea "
                                ShowFooter="True" Width="749px" OnRowDataBound="dgv1_RowDataBound" OnRowDeleting="dgv1_RowDeleting" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating" OnRowCancelingEdit="dgv1_RowCancelingEdit">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="13px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                    <%--  <asp:CommandField ShowDeleteButton="True" ControlStyle-Width="15px" DeleteText="&lt;span class='glyphicon glyphicon-remove asitGlyp'&gt;&lt;span&gt;">
                                    <ControlStyle Width="15px" />
                                    <HeaderStyle Width="15px" />
                                    <ItemStyle Width="15px" />
                                </asp:CommandField>--%>
                                    <asp:CommandField ShowEditButton="True" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />
                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpclCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="260px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblas" Style="text-align: left !important;" runat="server">Head of Accounts</asp:Label>

                                        </HeaderTemplate>
                                        <FooterTemplate>
                                            <%-- <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True"
                                                    OnClick="lnkTotal_Click" CssClass="btn btn-primary primarygrdBtn pull-right">Total :</asp:LinkButton>--%>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Font-Size="10px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                Width="100%" Font-Names="Verdana"></asp:HyperLink>

                                            <asp:Label ID="lblAccdesc" runat="server"
                                                Font-Size="11px" Visible="False"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="100%" Font-Names="Verdana"></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>

                                            <fieldset class="scheduler-border fieldset_A">

                                                <div class="form-horizontal">

                                                    <div class="form-group">

                                                        <div class="col-md-3 pading5px asitCol3">
                                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                            <div class="col-md-3 pading5px">

                                                                <asp:DropDownList ID="ddlgrdacccode" runat="server" CssClass="form-control inputTxt chzn-select"
                                                                    TabIndex="28" Style="width: 213px;" AutoPostBack="True" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">

                                                        <div class="col-md-3 pading5px asitCol3">
                                                            <asp:Label ID="lblgvreshead" runat="server" CssClass="lblTxt lblName">Details Head</asp:Label>

                                                            <div class="col-md-3 pading5px">
                                                                <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="form-control inputTxt chzn-select"
                                                                    TabIndex="31" Style="width: 213px;">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </fieldset>
                                        </EditItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Details Description" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResdesc" runat="server"
                                                Font-Size="10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpcldesc" runat="server"
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                Width="80px" TabIndex="78"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvunit" runat="server"
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="30px" TabIndex="78"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="50px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Visible="False" Width="40px" Font-Size="12px" Style="text-align: right"
                                                ReadOnly="True"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="40px" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                                TabIndex="79"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="50px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Visible="False" Width="50px" Font-Size="12px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px" Font-Size="11px" ReadOnly="True" ForeColor="Black"
                                                Style="text-align: right" TabIndex="80"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amount" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="SkyBlue" BorderStyle="Solid" BorderWidth="2px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                                TabIndex="81"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Size="12px" ReadOnly="True"
                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle Width="90px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="SkyBlue" BorderStyle="Solid" BorderWidth="2px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                                TabIndex="82"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Size="12px" ReadOnly="True"
                                                Width="100px" Style="text-align: right"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                Width="80px" ForeColor="Black" TabIndex="83"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reconcilation" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrecndat" runat="server"
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                Width="80px" TabIndex="78"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="RpCode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrpcode" runat="server"
                                                Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                Width="80px" TabIndex="60"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Notes" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvBillno" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextboxL" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                ForeColor="Black" TabIndex="99"></asp:TextBox>
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
                        <div class="col-md-4 col-sm-4 col-lg-4 ">
                            <asp:Panel ID="pnlNarration" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-md-4 col-sm-4 col-lg-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="label">Cheque No</asp:Label>
                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-lg-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="label">Order/Ref. No</asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-4 col-lg-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Cheque List</asp:Label>
                                            <asp:DropDownList ID="ddlcheque" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-12 col-sm-12 col-lg-12 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblPayto" runat="server" CssClass="label">Pay To</asp:Label>
                                            <asp:TextBox ID="txtPayto" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                            </cc1:AutoCompleteExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" CssClass="label">Narration</asp:Label>
                                            <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" cols="20" Rows="4" Height="150px"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group" id="rcvbank" runat="server" visible="false">

                                            <div class="col-md-12 pading5px">

                                                <asp:Label ID="lblRecived" runat="server" CssClass=" lblTxt lblName" Text="Received Bank"></asp:Label>

                                                <asp:TextBox ID="txtBankNam" runat="server" CssClass="inputtextbox" Style="width: 350px;"></asp:TextBox>

                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName pull-right " Style="width: 600px; color: blue; text-align: right;"></asp:Label>

                    </br></br></br></br></br>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

