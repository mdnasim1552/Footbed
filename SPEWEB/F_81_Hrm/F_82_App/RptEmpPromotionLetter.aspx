<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpPromotionLetter.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpPromotionLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

            $('[id*=ddlEmpNameAllInfo]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true
            });
        }

    </script>
    <style>
        .multiselect {
            width: 200px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 350px !important;
        }

        .caret {
            display: none !important;
        }
    </style>
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lablDivision" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblRptType" runat="server" CssClass="label">Report Type</asp:Label>
                                <asp:DropDownList ID="ddlRptType" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="01">Increment</asp:ListItem>
                                    <asp:ListItem Value="02">Promotion</asp:ListItem>
                                    <asp:ListItem Value="03">Increment & Promotion</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpName" runat="server" CssClass="label">Employee                                    
                                <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" OnClick="ibtnEmpListAllinfo_Click" ToolTip="Get Employee"><span class="fa fa-search"></span></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblIssueDate" runat="server" CssClass="label">Issue Date</asp:Label>
                                <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled" placeholder="dd-MMM-yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtIssueDate" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtIssueDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblEffectDate" runat="server" CssClass="label">Effective Date</asp:Label>
                                <asp:TextBox ID="txtEffectDate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled" placeholder="dd-MMM-yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtEffectDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEffectDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divBtnOk" runat="server" visible="false">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                         <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblRptFormat" runat="server" CssClass="label">Format</asp:Label>
                                <asp:DropDownList ID="ddlRptFormat" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="01">Executive</asp:ListItem>
                                    <asp:ListItem Value="02">Factory Staff</asp:ListItem>
                                    <asp:ListItem Value="03">Worker</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
