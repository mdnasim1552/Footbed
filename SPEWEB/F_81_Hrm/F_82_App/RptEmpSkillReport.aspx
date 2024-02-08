<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpSkillReport.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpSkillReport" %>

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
                                         <div class="col-md-3 pading5px" id="Daterange" runat="server" visible="false">
                                      
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Width="68">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox " AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox " AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>


                                     <div class="col-md-3 col-sm-3 col-lg-3 pading5px"  visible="false">
                                        <asp:TextBox ID="txtEmpSrcInfo" runat="server" CssClass="inputTxt inputName  inpPixedWidth hidden"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="lblTxt lblName" OnClick="ibtnEmpListAllinfo_Click"> Employee </asp:LinkButton>
                                        <asp:DropDownList ID="ddlEmpNameAllInfo" Width="200" runat="server" CssClass="chzn-select pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
 
                                        <div class="col-md-1" id="Okbtn" runat="server" visible="false">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                        </div>
                                   
               <%--                     <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        
                                        <asp:Label ID="lblfrmdt" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txtfrmdt" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdt_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtfrmdt">
                                        </cc1:CalendarExtender>

                      

                                    </div>

                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        
                                        <asp:Label ID="lbltodt" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                        <asp:TextBox ID="txttodt" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodt_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txttodt">
                                        </cc1:CalendarExtender>

                      

                                    </div>--%>

                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
