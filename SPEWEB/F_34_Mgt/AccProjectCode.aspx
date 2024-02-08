<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccProjectCode.aspx.cs" Inherits="SPEWEB.F_34_Mgt.AccProjectCode" %>

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
        }

    </script>

    <script language="javascript" type="text/javascript">

        function pageLoaded() {
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
                    <div class="row" style="min-height: 300px;">
                        <div class="col-md-2 col-sm-2 col-lg-2 " style="display: none;">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnMainCode" runat="server" CssClass="label" OnClick="imgbtnMainCode_Click">Main Group</asp:LinkButton>

                                <asp:DropDownList ID="ddlMainCode" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="ingbtnSub1" runat="server" CssClass="label" OnClick="ingbtnSub1_Click">Main Group</asp:LinkButton>

                                <asp:DropDownList ID="ddlSub1" AutoPostBack="true" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnSub2" runat="server" CssClass="label" OnClick="imgbtnSub2_Click">Group</asp:LinkButton>

                                <asp:DropDownList ID="ddlSub2" AutoPostBack="true" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="mgbtnPreDetails" runat="server" CssClass="label" OnClick="mgbtnPreDetails_Click">Previous LC</asp:LinkButton>

                                <asp:DropDownList ID="ddlProjectList" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Label ID="lblLC" runat="server" CssClass="label">NEW LC</asp:Label>
                                <asp:CheckBox ID="chkNewProject" runat="server" CssClass=" checkbox" OnCheckedChanged="chkNewProject_CheckedChanged"
                                    AutoPostBack="True" />
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Description</asp:Label>
                                <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">

                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Short Description</asp:Label>
                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>


                         <div class="col-md-12 col-sm-12 col-lg-12 " style="margin-top: -60px;">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label"> Article Name</asp:Label>
                                <asp:TextBox ID="TextBoxDesc" runat="server" width="47%" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-md-6 col-sm-6 col-lg-6 " style="margin-top: -80px;">
                            <div class="form-group">
                                <asp:CheckBox ID="CbForArticle"  AutoPostBack="true" Text="This change applicable to previous all relevant article information (Proto-Sample, Sample, Order, Re-Order)." runat="server" /> 
                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2 " style="margin-top: -60px;">
                            <div class="form-group">
                                <asp:Label ID="LbInqno" Visible="false"  runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 " style="margin-top: -60px;">
                            <div class="form-group">
                                <asp:Label ID="LbMlccod" Visible="false" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 " style="margin-top: -60px;">
                            <div class="form-group">
                                <asp:Label ID="LbSdino" Visible="false" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>



                        

                    </div>


                    <div class="row">

                        <%--<div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:CheckBox ID="CbForProSample" OnCheckedChanged="CbForProSample_CheckedChanged" AutoPostBack="true" Style="margin-left: 25px;" runat="server" />Proto sample
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:CheckBox ID="CbForSample" OnCheckedChanged="CbForSample_CheckedChanged" AutoPostBack="true" Style="margin-left: 25px;" runat="server" />Sample
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:CheckBox ID="CbForOrder" OnCheckedChanged="CbForOrder_CheckedChanged" AutoPostBack="true" Style="margin-left: 25px;" runat="server" />Order
                            </div>
                        </div>--%>

                    </div>


                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

