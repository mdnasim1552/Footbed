<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpInformation02.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpInformation02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>

        .input-group .form-control:not(:first-child):not(:last-child) {
            height: 32px !important;
        }

        .input-group-btn:last-child > .btn {
            height: 32px !important;
        }


        .dropdown-toggle {
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: flex;
            vertical-align: middle;
        }

        .overflow-hidden {
            overflow-y: auto;
        }

       

        


        .multiselect {
            width: 100% !important;
            border: 1px solid;
            height: 29px;
            line-height: 17px;
            border-color: #cfd1d4;
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }
        /*.multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }*/

        /*.multiselect-text {
            width: 100% !important;
        }*/

        .caret {
            display: none !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px;
            line-height: 30px;
        }

        .chzn-container-single .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .dropdown-menu.show {
            width: 100% !important;
        }

    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
            $('[id*=ddlEmpNameAllInfo]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,

            });
            $('[id*=ReportListBox]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,

            });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });


            $('[id*=ddlEmpNameAllInfo]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,

            });
            $('[id*=ReportListBox]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,

            });
            
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
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblline" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server" CssClass="label">Employee                                    
                                <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="label" OnClick="ibtnEmpListAllinfo_Click" ToolTip="Get Employee"><i class="fas fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;"></asp:ListBox>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="Daterange" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="Daterange2" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="ReportType" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblRptType" runat="server" CssClass="label">Report Type</asp:Label>
                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Application Form</asp:ListItem>
                                    <asp:ListItem Value="1">Appointment Letter</asp:ListItem>
                                    <asp:ListItem Value="2">Workers Confirm. Letter</asp:ListItem>
                                    <asp:ListItem Value="3">Evaluation Form</asp:ListItem>
                                    <asp:ListItem Value="4">CPF</asp:ListItem>
                                    <asp:ListItem Value="5">CPF2(N Form)</asp:ListItem>
                                    <asp:ListItem Value="6">CPF3</asp:ListItem>
                                    <asp:ListItem Value="7">Nominee Form</asp:ListItem>
                                    <asp:ListItem Value="8">Age Form</asp:ListItem>
                                    <asp:ListItem Value="9">Training Letter</asp:ListItem>
                                    <asp:ListItem Value="10">Envelope English (Permanent)</asp:ListItem>
                                    <asp:ListItem Value="11">Envelope Bangla (Permanent)</asp:ListItem>
                                    <asp:ListItem Value="12">Envelope English (Present)</asp:ListItem>
                                    <asp:ListItem Value="13">Envelope Bangla (Present)</asp:ListItem>
                                    <asp:ListItem Value="14">Office Envelope</asp:ListItem>
                                    <asp:ListItem Value="15">Joining Letter</asp:ListItem>
                                    <asp:ListItem Value="16">Bank Opening</asp:ListItem>
                                    <asp:ListItem Value="17">Appointment Top Sheet</asp:ListItem>
                                    <asp:ListItem Value="18">Salary Certificate</asp:ListItem>
                                    <asp:ListItem Value="19">Probationary Evaluation Form</asp:ListItem>
                                    <asp:ListItem Value="20">Suspension</asp:ListItem>
                                    <asp:ListItem Value="21">Workers Extension Letter</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                         <div class="col-md-3 col-sm-3 col-lg-3 " id="DivListBox" runat="server" visible="false">
                             <asp:Label ID="Label8" runat="server" CssClass="label">Report Type</asp:Label>
                                <asp:ListBox ID="ReportListBox" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;">
                                    <asp:ListItem Value="1">Appointment Letter</asp:ListItem>
                                    <asp:ListItem Value="0">Application Form</asp:ListItem>
                                    <asp:ListItem Value="8">Age Form</asp:ListItem>
                                     <asp:ListItem Value="9">Training Letter</asp:ListItem>
                                    <asp:ListItem Value="7">Nominee Form</asp:ListItem>
                                    <asp:ListItem Value="2">Workers Confirm. Letter</asp:ListItem>
                                    <asp:ListItem Value="3">Evaluation Form</asp:ListItem>
                                    <asp:ListItem Value="4">CPF</asp:ListItem>
                                    <asp:ListItem Value="5">CPF2(N Form)</asp:ListItem>
                                    <asp:ListItem Value="6">CPF3</asp:ListItem>                                                                   
                                    <asp:ListItem Value="10">Envelope English (Permanent)</asp:ListItem>
                                    <asp:ListItem Value="11">Envelope Bangla (Permanent)</asp:ListItem>
                                    <asp:ListItem Value="12">Envelope English (Present)</asp:ListItem>
                                    <asp:ListItem Value="13">Envelope Bangla (Present)</asp:ListItem>
                                    <asp:ListItem Value="14">Office Envelope</asp:ListItem>
                                    <asp:ListItem Value="15">Joining Letter</asp:ListItem>
                                    <asp:ListItem Value="16">Bank Opening</asp:ListItem>
                                    <asp:ListItem Value="17">Appointment Top Sheet</asp:ListItem>
                                    <asp:ListItem Value="18">Salary Certificate</asp:ListItem>
                                    <asp:ListItem Value="19">Probationary Evaluation Form</asp:ListItem>
                                    <asp:ListItem Value="20">Suspension</asp:ListItem>
                                </asp:ListBox>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2 " id="divIssuDate" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblIssuDate" runat="server" CssClass="label">Issue Date</asp:Label>
                                <asp:TextBox ID="txtIssuDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtIssuDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtIssuDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblEmp" runat="server" CssClass="label">Format</asp:Label>
                                <asp:DropDownList ID="ddlRptFormat" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">Executive</asp:ListItem>
                                    <asp:ListItem Value="1">Factory Staff</asp:ListItem>
                                    <asp:ListItem Value="2">Worker</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divEmpStatus" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblEmpStatus" runat="server" CssClass="label">Emp. Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="idSignatory" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Signatory</asp:Label>
                                <asp:DropDownList ID="DdlSignatory" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 " id="Okbtn" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 " id="MergePDF" runat="server">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" Text="Merge"/>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
