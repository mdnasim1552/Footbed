<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HrLeaveApprovalForm.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.HrLeaveApprovalForm" %>

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



    <%-- <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />--%>



    <script type="text/javascript">
        $(function () {
            $('#ddlEmploye').multiselect({
                includeSelectAllOption: true
            });
            $('#btnSelected').click(function () {
                var selected = $("#ddlEmploye option:selected");
                var message = "";
                selected.each(function () {
                    message += $(this).text() + " " + $(this).val() + "\n";
                });
                alert(message);
            });
        });
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOkOrNew" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="GetUserInfo">Ok</asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 250px;">
                        <div class="col-md-6 col-sm-6 col-lg-6 ">

                            <section class="card"  id="Panel2" runat="server" visible="false">
                                <header class="card-header" >General Approval</header>
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">User Name</asp:Label>
                                            <asp:DropDownList ID="ddlUserList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3 col-sm-3 col-lg-3 ">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="Select_Click">Add</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="SelectAll_Click">Add All</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="col-md-7 col-sm-7 col-lg-7 ">

                                        <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True"
                                            OnRowDeleting="gvProLinkInfo_RowDeleting">
                                            <PagerSettings Visible="False" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                                <asp:TemplateField HeaderText="Sl" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="slno" runat="server" Height="16px" Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                                            Width="30px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User ID" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="userid" runat="server" Height="16px" Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                            Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                            Width="100px">Delete All</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User Name">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                            Font-Size="13px" OnClick="lbtnUpdate_Click"
                                                            Style="text-align: center; height: 15px;" Width="100px">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>

                                    </div>
                                </div>
                        </section>
                            
                        </div>

                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                            <section class="card"  id="Panel1" runat="server" visible="false">
                                <header class="card-header" >Managment Approval</header>
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">User Name</asp:Label>
                                            <asp:DropDownList ID="ddlUserListmgt" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3 col-sm-3 col-lg-3 ">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:LinkButton ID="lbtnSelectmgt" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectmgt_Click">Add</asp:LinkButton>
                                            <asp:LinkButton ID="alllbtnSelectmgt" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="alllbtnSelectmgt_Click">Add All</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="col-md-7 col-sm-7 col-lg-7 ">
                                        <asp:GridView ID="gvMgtUser" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True"
                                        OnRowDeleting="gvMgtUser_RowDeleting">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0Mgt" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                            <asp:TemplateField HeaderText="Sl.No." Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="slnoMgt" runat="server" Height="16px" Style="text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                                        Width="30px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User ID" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="useridMgt" runat="server" Height="16px" Style="text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnDeleteAllsMgt" runat="server" Font-Bold="True"
                                                        Font-Size="13px" Height="16px" OnClick="MgtUserDeleteAll_Click"
                                                        Width="100px">Delete All</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplDesc1Mgt" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdateMgt" runat="server" Font-Bold="True"
                                                        Font-Size="13px" OnClick="lbtnUpdateMgt_OnClick"
                                                        Style="text-align: center; height: 15px;" Width="100px">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                    </asp:GridView>


                                        </div>

                                </div>
                              

                                    
                             
                            </section>

                        </div>

                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
