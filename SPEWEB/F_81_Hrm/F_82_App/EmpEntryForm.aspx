<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpEntryForm.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.EmpEntryForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

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
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewServices" runat="server">
                            <div class="row" runat="server" visible="false">
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmpType1" runat="server" CssClass="label">Employee Type
                                                <asp:LinkButton ID="ingbtnLoc" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="ingbtnLoc_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control from-contol-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCompany" runat="server" CssClass="label">Company
                                                <asp:LinkButton ID="imgbtnComp" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="imgbtnComp_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlCompName" runat="server" CssClass="form-control from-contol-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCompName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblPreSLNo" runat="server" CssClass="label">Branch
                                                <asp:LinkButton ID="imgbtnBranch" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="imgbtnBranch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control from-contol-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblSection" runat="server" CssClass="label">Section
                                                <asp:LinkButton ID="imgbtnDept" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="imgbtnDept_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control from-contol-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <a href="#" class="btn btn-info btn-sm" onclick="history.go(-1)">Back</a>
                                        <asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn  btn-primary btn-sm" Style="margin: 0 5px;"
                                            OnClick="lnkNextbtn_Click"><i class="fas fa-forward"></i> Next</asp:LinkButton>
                                        <asp:Label ID="hidempid" runat="server" CssClass="label" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chkApplist" runat="server" AutoPostBack="true" OnCheckedChanged="chkApplist_CheckedChanged" Text="From Advertisement" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblPrevEmpList" runat="server" CssClass="label">Prev. Emp. List
                                                <asp:LinkButton ID="imgbtnPreEMP" runat="server" OnClick="imgbtnPreEMP_Click" ToolTip="Get Prev. Emp. List"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="chzn-select form-control formm-contol-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chkNewEmp" runat="server" Text="New Emp." CssClass="checkbox-inline" Style="margin-left: 10px;" OnCheckedChanged="chkNewEmp_CheckedChanged"
                                            AutoPostBack="True" />
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmpName" runat="server" CssClass="label">Employee Name</asp:Label>
                                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control form-control-sm" Style="margin-left: 6px;"></asp:TextBox>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control form-contol-sm" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmpNameBn" runat="server" CssClass="label">কর্মকর্তার নাম</asp:Label>
                                        <asp:TextBox ID="txtEmpNameB" runat="server" CssClass="form-control form-control-sm" Style="margin-left: 6px;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-success btn-sm" Style="margin-top: 20px;">Save</asp:LinkButton>
                                </div>
                                <div class="col-sm-1 col-md-1 col-lg-1">
                                    <asp:Label ID="lblEmpnameB" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblempdata" runat="server"></asp:Label>
                                    <asp:Label ID="lblEmplastId" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

