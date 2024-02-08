<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AllVoucherTopSheet.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AllVoucherTopSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">


        function Search_Gridview(strKey, cellNr) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvAccVoucher.ClientID %>");
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



        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };

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

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblref" runat="server" CssClass="label">Ref. No</asp:Label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddlvoucher" runat="server" CssClass=" chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="BD">Bank Payment</asp:ListItem>
                                    <asp:ListItem Value="CD">Cash Payment</asp:ListItem>
                                    <asp:ListItem Value="BC">Bank Deposit</asp:ListItem>
                                    <asp:ListItem Value="CC">Cash Deposit</asp:ListItem>
                                    <asp:ListItem Value="CT">Contra Voucher</asp:ListItem>
                                    <asp:ListItem Value="JV">Journal Voucher</asp:ListItem>
                                    <asp:ListItem Value="" Selected="True">All Voucher</asp:ListItem>
                                    <asp:ListItem Value="PV" Enabled="false">Post Dated Voucher</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                       
                       <div class="col-md-2 col-sm-2 col-lg-2 ">



                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown" style="margin-top: 20px;">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>


                                        <asp:HyperLink ID="HpblnkNew" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/GeneralAccounts?Mod=Accounts&vounum=" CssClass="dropdown-item"> New Voucher</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/SuplierPayment?tcode=99&tname=Payment Voucher&Mod=Accounts" CssClass="dropdown-item"> Sup Payment Voucher</asp:HyperLink>

                                        <asp:HyperLink ID="hlnkLedger" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccLedgerAll" CssClass="dropdown-item">Ledger</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/RptAccDayTransData" CssClass="dropdown-item">Daily transaction</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_23_MAcc/AccountInterface" CssClass="dropdown-item">Interface</asp:HyperLink>
                                
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccBankRecon?Type=Acc" CssClass="dropdown-item">Bank Reconcilation</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/F_62_Mis/RptAccIncome?Type=IncomeMonthly" CssClass="dropdown-item">Cost Details</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccCodeBook?InputType=Accounts" CssClass="dropdown-item">Accounts Code</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint" CssClass="dropdown-item">Details Code</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccResourceCode?Type=Matcode" CssClass="dropdown-item">Materials Code Opening</asp:HyperLink>


                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="checkbox" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height:350px;">
                        <asp:GridView ID="gvAccVoucher" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccVoucher_RowDataBound"
                            ShowFooter="True" Width="1063px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvoudat" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchrefnum" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Cheque No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ref. No">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchrefnum2" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Ref. No" onkeyup="Search_Gridview(this,4)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtINgvteamdesc2" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarration" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Party Name" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblogvpayto" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Voucher Amount" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="V. print">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print"><span class="fa fa-print"></span></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkVoucherEdit" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="C. Print">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="hlnkChequePrint" runat="server" Target="_blank" ToolTip="Cheque Print"><span class="fa fa-print"></span></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearusrname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="User Name" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblogvusrname" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                            Width="80px"></asp:Label>
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
            </div>







            



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

