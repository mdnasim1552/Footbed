<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LcCodeBook.aspx.cs" Inherits="SPEWEB.F_21_GAcc.LcCodeBook" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
                    <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Main</asp:Label>
                                        <asp:TextBox ID="txtsrchMainCode" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnMainCode" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnMainCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlMainCode" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlMainCode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Sub-1</asp:Label>
                                        <asp:TextBox ID="txtSrcSub1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ingbtnSub1" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ingbtnSub1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSub1" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPreSLNo" runat="server" CssClass="lblTxt lblName">Sub-2:</asp:Label>
                                        <asp:TextBox ID="txtSrcSub2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSub2" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSub2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSub2" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlSub2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 pading5px  asitCol3">

                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Pre. Project</asp:Label>
                                        <asp:TextBox ID="txtSrcDetails" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="mgbtnPreDetails" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="mgbtnPreDetails_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectList" runat="server" CssClass="form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:LinkButton ID="lnkbtnSave" runat="server" OnClick="lnkbtnSave_Click" CssClass="btn btn-danger primaryBtn">Save</asp:LinkButton>
                                    </div>


                                </div>


                                <div class="form-group">
                                    <div class="col-md-2 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Project Name:</asp:Label>
                                        <asp:CheckBox ID="chkNewProject" runat="server" Text="New Project" CssClass=" checkbox-inline" OnCheckedChanged="chkNewProject_CheckedChanged"
                                            AutoPostBack="True" />

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:TextBox ID="txtProjectName" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="margin-left: 5px;" Width="200"></asp:TextBox>
                                    </div>

                                 <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" >Type:</asp:Label>
                                        <asp:TextBox ID="txtType" runat="server" CssClass="inputTxt inputName inpPixedWidth"  Width="100"></asp:TextBox>




                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" >Short Name:</asp:Label>
                                        <asp:TextBox ID="txtShortName" runat="server" CssClass="inputTxt inputName inpPixedWidth"  Width="100"></asp:TextBox>



                                    </div>
                                    


                                </div>
                            </div>

                                <%--<div class="form-group">
                                    <div class="col-md-2 pading5px ">
                                       <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" >Type:</asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputTxt inputName inpPixedWidth"  Width="100"></asp:TextBox>



                                    </div>--%>



                        </fieldset>

                    </div>



                </div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

