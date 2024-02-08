<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccInterComVoucher.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccInterComVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .rbtnList1 td input {
            margin-right: 7px;
        }

        .rbtnList1 td label {
            margin-right: 20px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblDate" runat="server" CssClass="col-form-label" Text="Transfer From"></asp:Label>
                                <asp:Label ID="lblFromCmpName" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtnList1" BorderColor="BlueViolet" runat="server" AutoPostBack="True" Style="margin-top: 20px;" CssClass="rbtnList1 chkBoxControl form-control form-control-sm" RepeatColumns="5">
                                    <asp:ListItem>Payment</asp:ListItem>
                                    <asp:ListItem>Received</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="col-from-label">Voucher No</asp:Label>
                                <asp:Label ID="lblfVoucherNo" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="col-form-label">Voucher Date</asp:Label>
                                <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" OnClick="lbtnRefresh_Click">Referesh</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtsercacc" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                <asp:LinkButton ID="lblcontrolAccHead" runat="server" CssClass="text-primary" Text="Control Accounts" OnClick="imgbtnFindCAccount_Click"></asp:LinkButton>
                                <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtserheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                <asp:LinkButton ID="lblAccountHead" runat="server" CssClass="text-primary" OnClick="imgbtnFindAccount_Click" Text="Head Of Accounts"></asp:LinkButton>
                                <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm inputTxt" Height="30px" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblDramt0" runat="server" CssClass="col-form-label">Dr. Amount</asp:Label>
                                <asp:TextBox ID="txtDrAmt" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNum" runat="server" CssClass="col-form-label" Text="Ref./CheqNo"></asp:Label>
                                <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblRefNum0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSrInfo" runat="server" CssClass="col-form-label" Text="Other ref"></asp:Label>
                                <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="col-form-label" Text="Transfer To"></asp:Label>
                                <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="chzn-select chzn-single form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="col-form-label">Voucher No</asp:Label>
                                <asp:Label ID="lbltVoucherNo" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="col-form-label">Voucher Date</asp:Label>
                                <asp:TextBox ID="txttdate" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfdate_CalendarExtender3" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtsetrcacc" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindtCAccount" runat="server" CssClass="text-primary" Text="Control Accounts" OnClick="imgbtnFindtCAccount_Click"></asp:LinkButton>
                                <asp:DropDownList ID="ddlContAccHead" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlContAccHead_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:TextBox ID="txtsertoheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                <asp:LinkButton ID="lblAccountHead0" runat="server" CssClass="text-primary" Text="Head Of Accounts" OnClick="imgbtnFindtoAccount_Click"></asp:LinkButton>
                                <asp:DropDownList ID="ddlAcctoHead" runat="server" CssClass="chzn-select form-control form-control-sm inputTxt" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblDramt1" runat="server" CssClass="">Cr. Amount</asp:Label>
                                <asp:Label ID="lbltcramt" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNum1" runat="server" CssClass="col-form-label" Text="Ref./CheqNo"></asp:Label>
                                <asp:Label ID="lbltRefNum" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblRefNum2" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txttNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>                                
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSrInfo0" runat="server" CssClass="col-form-label" Text="Other Ref"></asp:Label>
                                <asp:TextBox ID="txttSrinfo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-sm btn-danger hidden" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>





