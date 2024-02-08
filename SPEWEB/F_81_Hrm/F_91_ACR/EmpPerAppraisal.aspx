﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpPerAppraisal.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_91_ACR.EmpPerAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-6 pading5px  ">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Ref No.</asp:Label>
                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to"> Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                        </cc1:CalendarExtender>

                                        <asp:Label ID="lblRefNo2" runat="server" CssClass="smLbl">ID</asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputTxt inputName" Width="60"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="inputTxt inputName" Width="60" Style="margin-left: 5px;"></asp:Label>

                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee List</asp:Label>
                                        <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblEmpname" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblprelist" runat="server" CssClass="lblTxt lblName">Pre. Appraisal</asp:Label>
                                        <asp:TextBox ID="txtPreViousList" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnPreList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnPreList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlPreList" runat="server" CssClass="form-control chzn-select inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label3" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                                    </div>
                                </div>



                            </div>
                        </fieldset>




                        <asp:GridView ID="gvPerAppraisal" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="1101px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDescription" runat="server"
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "dgdesc")) %>'
                                            Width="200px" />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:LinkButton ID="lbtnUpPerAppraisal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpPerAppraisal_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval1" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval1"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc1"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc1")) %>'
                                            Width="180px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval2" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval2"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc2"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc2")) %>'
                                            Width="180px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval3" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval3"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc3"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc3")) %>'
                                            Width="180px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgvsgval4" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sgval4"))=="True" %>'
                                            Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "smgdesc4"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "sgdesc4")) %>'
                                            Width="180px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

