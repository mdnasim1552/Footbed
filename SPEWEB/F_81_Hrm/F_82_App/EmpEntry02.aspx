<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpEntry02.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.EmpEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
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
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>


    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee List</asp:Label>
                                <asp:TextBox ID="EmployeeList" runat="server" CssClass="inputTxt inputName inpPixedWidth" TabIndex="0"></asp:TextBox>
                                <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" TabIndex="1" OnClick="ibtnEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:Label ID="lblEmpName" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                            </div>
                            <div class="col-md-1 pading5px">

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>


                            </div>
                        

                        </div>
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Information</asp:Label>
                                <asp:TextBox ID="txtInformation" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="ibtnInformation" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnInformation_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-3 pading5px">
                                <asp:DropDownList ID="ddlInformation" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblInformation" runat="server" CssClass="form-control dataLblview" Visible="false" Height="22" Style="line-height: 1.5"></asp:Label>
                            </div>

                            <div class="col-md-3 pading5px ">

                                <asp:Label ID="lblLastCardNo" runat="server" Visible="false" CssClass=" btn btn-info primaryBtn btn-sm"></asp:Label>

                                <asp:LinkButton ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:LinkButton>

                            </div>

                        </div>
                    </div>
                </fieldset>


            </div>

            <asp:MultiView ID="MultiView1" runat="server">

                <asp:View ID="ViewPersonal" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">


                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Department</asp:Label>

                                        <asp:DropDownList ID="ddldpt" AutoPostBack="true" runat="server" CssClass="chzn-select form-control inputTxt" Width="320" TabIndex="15">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lbltDesignation" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>

                                        <asp:DropDownList ID="ddlDesignation" AutoPostBack="true" runat="server" CssClass="chzn-select form-control inputTxt" Width="320" TabIndex="15">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvPersonalInfo_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="2px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <%--<asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>--%>
                                    </FooterTemplate>
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="537px"></asp:TextBox>
                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                            Width="537px"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                        <asp:Panel ID="Panegrd" runat="server">

                                            <div class="form-group">
                                                <div class="col-md-12 pading5px">
                                                    <asp:TextBox ID="txtgrdEmpSrc" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                    <asp:LinkButton ID="ibtngrdEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtngrdEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass=" ddlPage62 inputTxt chzn-select" Width="200" AutoPostBack="true" TabIndex="2">
                                                    </asp:DropDownList>



                                                </div>
                                            </div>


                                        </asp:Panel>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>



                        <div class="form-group">
                            <div class="col-md-6">
                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn pull-right" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </asp:View>
                <asp:View ID="ViewLocation" runat="server">
                    <div class="col-md-1 pading5px">
                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="btnAdd_Click">Add</asp:LinkButton>

                        <div class="row">
                            <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="831px" OnRowDeleting="gvLocation_RowDeleting">
                                <RowStyle />
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" 
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Circle">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlcircle" runat="server" CssClass="form-control" Width="150">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Region">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control" Width="150">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Area">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlarea" runat="server" CssClass="form-control" Width="150">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="territory">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lUpdateLocation" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateLocation_Click">Update</asp:LinkButton>
                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlterritory" runat="server" CssClass="form-control" Width="150">
                                            </asp:DropDownList>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgval" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Seq" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvseq" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "seq")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                </asp:View>
            </asp:MultiView>





        </div>
    </div>



</asp:Content>

