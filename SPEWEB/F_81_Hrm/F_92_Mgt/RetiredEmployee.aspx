<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RetiredEmployee.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.RetiredEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
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

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Search By</asp:Label>
                                <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control form-control-sm" placeholder="Card/Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server">Employee
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" OnClick="imgbtnEmployee_Click" Font-Underline="false" ToolTip="Get Employee"><i class="fa fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server"
                                    CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-half col-sm-half col-lg-half">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnShow" Style="margin-top: 20px;" runat="server" CssClass="btn btn-primary btn-sm " OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-half col-sm-half col-lg-half ml-1" style="margin-top:25px;">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Resign</asp:Label>
                                <asp:CheckBox ID="resign" runat="server" AutoPostBack="true" OnCheckedChanged="resign_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <asp:Panel ID="PnlSepType" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="imgbtnEmpName" runat="server" CssClass="label"> Designation</asp:Label>
                                    <asp:Label ID="lblDesig" runat="server" CssClass="form-control form-control-sm" ForeColor="RoyalBlue"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="label"> Joining Date</asp:Label>
                                    <asp:Label ID="LblJoiningDate" runat="server" CssClass="form-control form-control-sm " ForeColor="RoyalBlue"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="label"> Confirm Date</asp:Label>
                                    <asp:Label ID="LblConfirmationDate" runat="server" CssClass="form-control form-control-sm " ForeColor="RoyalBlue"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblsign" runat="server" CssClass="smLbl_to">Signatory</asp:Label>
                                    <asp:DropDownList ID="ddlsign" runat="server" CssClass="form-control form-control-sm chzn-select "></asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblfrmDate" runat="server" CssClass="label">Separtion Date</asp:Label>
                                    <asp:TextBox ID="txtSepDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtSepDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSepDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblefrmDate" runat="server" CssClass="label">Inform/Apply Date</asp:Label>
                                    <asp:TextBox ID="txtefrmDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtefrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtefrmDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="label">Separation Type</asp:Label>
                                    <asp:DropDownList ID="ddlSepType" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="label">Notes</asp:Label>
                                    <asp:TextBox ID="TxtNotes" runat="server" CssClass="form-control form-control-sm" placeholder="Reason of separation..."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


