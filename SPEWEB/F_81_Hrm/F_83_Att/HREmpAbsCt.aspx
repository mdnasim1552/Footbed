<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpAbsCt.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HREmpAbsCt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="label" OnClick="imgbtnEmployee_Click">Emp. Name</asp:LinkButton>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Designation</asp:Label>
                                <asp:Label ID="lblDesignation" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                       
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </div>

            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height:250px;">
                        <div class="col-md-12 col-sm-12 col-lg-12 ">
                            <div class="form-group">
                                <asp:CheckBoxList ID="chkDate" runat="server"  CssClass="chkBoxControl"
                                    ForeColor="#000" RepeatDirection="Horizontal" Width="900px"
                                    RepeatColumns="7">
                                </asp:CheckBoxList>

                            </div>
                        </div>
                         
                    </div>


                </div>
            </div>





            <div class="form-group hidden" style="display:none;">
                <div class="col-md-1 pading5px">
                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                </div>
                <div class="col-md-4 pading5px asitCol4">
                    <asp:Label ID="lblDeptName" runat="server" CssClass="form-control"></asp:Label>
                </div>

                <div class="col-md-1 pading5px">
                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                </div>
                <div class="col-md-4 pading5px asitCol4">
                    <asp:Label ID="lblSection" runat="server" CssClass="form-control"></asp:Label>
                </div>


            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

