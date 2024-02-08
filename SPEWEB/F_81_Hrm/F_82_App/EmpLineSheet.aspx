<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpLineSheet.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.EmpLineSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Section', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');
        }
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
           <%-- var gvpayroll = $('#<%=this.gv.ClientID %>');

            gvpayroll.gridviewScroll({
                width: 1200,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 9,
                varrowtopimg: "../../Image/arrowvt.png",
                varrowbottomimg: "../../Image/arrowvb.png",
                harrowleftimg: "../../Image/arrowhl.png",
                harrowrightimg: "../../Image/arrowhr.png",
                freezesize: 10
            });--%>
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <%--  <div class="loader"></div> --%>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-6 col-sm-6 col-lg-6 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>


                                        <%--   <div class="col-md-5 col-sm-5 col-lg-5 pading5px">--%>
                                        <asp:Label ID="lblspcday" runat="server" CssClass="lblTxt lblName hidden">Special Day</asp:Label>

                                        <%--  </div>--%>
                                        <%-- <div class="col-md-3 col-sm-3 col-lg-3 pading5px">--%>
                                        <asp:DropDownList ID="ddlSpecialDay" AutoPostBack="true" Width="180px" runat="server" CssClass="chzn-select pull-left" TabIndex="16"></asp:DropDownList>
                                        <%-- </div>--%>
                                    </div>

                                </div>





                            </div>
                        </fieldset>
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                        <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class=" form-group">
                                    <div class="pull-left">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlLine" runat="server" Visible="false">
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee Line</asp:Label>
                                            <asp:DropDownList ID="ddlempline" runat="server" Width="200" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-1 col-sm-1 col-lg-1 pading5px">
                                           <asp:LinkButton ID="lnkbtnGenLeave" runat="server" CssClass="btn btn-primary primaryBtn"
                                               OnClick="lnkbtnGenLeave_Click" TabIndex="14">Line Generate</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>



                        </fieldset>
                    </div>


                    <div class="col-md-12">
                        <asp:GridView ID="grvspecday" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-bordered table-hover" Width="600px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftName" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Font-Size="11PX" Width="140px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Id Card No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftintime" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Emp. Name">
                                    <ItemTemplate>

                                        <asp:Label ID="lblempid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>

                                        <asp:Label ID="lblshiftouttime" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Font-Size="11PX" Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Line">
                                    <ItemTemplate>                                        
                                            <asp:DropDownList ID="ddlempline" runat="server" Width="150"   CssClass="chzn-select"     TabIndex="2"></asp:DropDownList>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="150" />

                                 <%--   <ItemStyle HorizontalAlign="right" />--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />

                        </asp:GridView>
                    </div>
                     

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

