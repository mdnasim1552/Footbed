<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpInformation03.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpInformation03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="chzn-select pull-left" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="inputTxt inputName  inpPixedWidth hidden"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="lblTxt lblName" OnClick="ibtnEmpListAllinfo_Click"> Employee </asp:LinkButton>
                                        <asp:DropDownList ID="ddlEmpNameAllInfo" Width="200" runat="server" CssClass="chzn-select pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="lblRptType" runat="server" CssClass="smLbl_to">Report Type</asp:Label>
                                        <asp:DropDownList ID="ddlReportType" runat="server" Width="225" CssClass="chzn-select pull-left" TabIndex="2">
                                            <asp:ListItem>Resign Letter</asp:ListItem>
                                            <asp:ListItem>Leave Letter</asp:ListItem>
                                            <asp:ListItem>Self Support</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-md-3 col-sm-3 col-lg-3">
                                         <asp:Label ID="lblFrmDate" runat="server" CssClass="smLbl_to">From</asp:Label>
                                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="inputTxt inputName" AutoCompleteType="Disabled"></asp:TextBox>
                                         <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" BehaviorID="txtFrmDate_CalendarExtender" TargetControlID="txtFrmDate" />
                                    </div>
                                      <div class="col-md-3 col-sm-3 col-lg-3 ">
                                           <asp:Label ID="lblToDate" runat="server" CssClass="smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="inputTxt inputName" AutoCompleteType="Disabled"></asp:TextBox>
                                           <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" />
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
