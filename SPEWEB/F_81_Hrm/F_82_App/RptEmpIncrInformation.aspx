<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpIncrInformation.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpIncrInformation" %>

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
                enableCaseInsensitiveFiltering: true,
            });
        }

    </script>
    <style>
        .multiselect {
            width: 227px !important;
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row form-inline">
                        <div>
                            <div class="form-group">
                                <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                    <asp:DropDownList ID="ddlWstation" runat="server" Width="100%" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2  pading5px">
                                    <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" Width="100%" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2  pading5px">
                                    <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                    <asp:DropDownList ID="ddlDept" runat="server" Width="100%" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2  pading5px">
                                    <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                    <asp:DropDownList ID="ddlSection" runat="server" Width="100%" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-sm-4 col-lg-4  pading5px">
                                </div>



                                <div class="col-md-2 col-sm-2 col-lg-2  pading5px">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Width="68">From</asp:Label>
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" Width="100%" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2  pading5px">
                                    <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" Width="100%" CssClass=" form-control form-control-sm " AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>



                                <div class="col-md-2 col-sm-2 col-lg-2">

                                    <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="lblTxt lblName" OnClick="ibtnEmpListAllinfo_Click"> Employee </asp:LinkButton>
                                    <asp:ListBox ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm" Width="100%" SelectionMode="Multiple"></asp:ListBox>

                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 pading5px" id="Okbtn" runat="server" visible="false" style="margin-top: 20px">
                                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                    <asp:Label ID="lblrefno" runat="server">Ref No</asp:Label>
                                    <asp:TextBox ID="txtrefno" runat="server" Width="100%" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>

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
