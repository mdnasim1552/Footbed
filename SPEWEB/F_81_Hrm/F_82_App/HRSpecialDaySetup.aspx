<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRSpecialDaySetup.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.HRSpecialDaySetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Section', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


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
                <div class="card-body">
                    <div class="row">
                         <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblyearstrtdate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Special Day</asp:Label>
                                <asp:DropDownList ID="ddlSpecialDay" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-8 col-sm-8 col-lg-8 ">
                            <div class="form-group">
                                <asp:Label ID="lblColor" runat="server"  BackColor="Violet" CssClass="lblTxt lblName" Width="370px" Style="text-align: left;"> 1. A position Leave will be added to leave adjustment Bucket 
                                </asp:Label>
                                <asp:Label ID="lblColor2" runat="server" BackColor="SkyBlue" CssClass="lblTxt lblName" Width="370px" Style="text-align: left;"> 
                                                  2. Will be deducted from leave adjustment bucket if no leave is there then negative leave added
                                </asp:Label>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>


                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">

                  <div class="row" style="min-height:350px;">
                        <asp:GridView ID="grvspecday" runat="server" AutoGenerateColumns="False" CssClass=" table table-striped table-bordered table-hover" Width="900px">
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
                                            Font-Size="11PX" Width="250px"></asp:Label>
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
                                        <asp:Label ID="lblshiftouttime" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Font-Size="11PX" Width="180px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="EOT">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAllEot" runat="server" AutoPostBack="True" OnCheckedChanged="checkAllEot_CheckedChanged" />
                                        <br />
                                        <asp:Label ID="lbleot" runat="server" Text="EOT"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkeot" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eot"))=="True" %>' OnCheckedChanged="chkeot_CheckedChanged" AutoPostBack="true"
                                            Width="50px" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Advanced Leave">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True" OnCheckedChanged="chkallView_CheckedChanged" />
                                        <br />
                                        <asp:Label ID="lblAdvance" runat="server" Text="Advance Leave"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPermit" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advleave"))=="True" %>' OnCheckedChanged="chkPermit_CheckedChanged"
                                            Width="50px" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Leave Adjustment">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkalladjus" runat="server" AutoPostBack="True" OnCheckedChanged="chkalladjus_CheckedChanged" />
                                        <asp:Label ID="lblAdjustment" runat="server" Text="Adjustment Leave"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkadj" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adjleav"))=="True" %>' OnCheckedChanged="chkadj_CheckedChanged"
                                            Width="50px" AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Bucket Leave">
                                    <HeaderTemplate>
                                        Bucket Leave<br />
                                        <small>(Adjustment -Advance) Leave</small>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbbucket" runat="server"> <i class="fa fa-shopping-cart"></i> <%# Eval("bucket") %></asp:Label>
                                    </ItemTemplate>
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

