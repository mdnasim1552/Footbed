<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpOffDays.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HREmpOffDays" %>

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
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="PnlEmp" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="label">Month</asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="label">Employee</asp:Label>
                                    <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1-half col-sm-1-half col-lg-1-half">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:CheckBox ID="chkoffDays" runat="server" CssClass="checkbox" Text="New Off Days Setup" AutoPostBack="true" OnCheckedChanged="chkoffDays_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-md-1-half col-sm-1-half col-lg-1-half ml-2">
                                <div class="form-group">
                                    <asp:Label ID="lblDate" runat="server" CssClass="label">Date</asp:Label>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm small" placeholder="dd-MMM-yyyy" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-1-half col-sm-1-half col-lg-1-half ml-2">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkbtnoffShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnoffShow_Click" ToolTip="For Individual Offdays select Date">Previous Off Days</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>600</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                        <asp:ListItem>1200</asp:ListItem>
                                        <asp:ListItem>1500</asp:ListItem>
                                        <asp:ListItem>2000</asp:ListItem>
                                        <asp:ListItem>3000</asp:ListItem>
                                        <asp:ListItem>5000</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body mb-6" style="min-height: 450px;">
                    <asp:Panel ID="PnloffDays" runat="server" Visible="False" CssClass="mb-0 pb-0">
                        <div class="log-divider mt-1" id="lbloffDay" runat="server">
                            <span>
                                <i class="fa fa-fw fa-dollar-sign"></i>New Off Days Setup</span>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5 col-lg-5">
                                <div class="form-group">
                                    <asp:CheckBoxList ID="chkDate" runat="server" CssClass="checkbox" RepeatColumns="7" RepeatDirection="Horizontal" />
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:CheckBox ID="ChkWeekend" runat="server" CssClass="checkbox" Text="Weekend" /><br />
                                     <asp:CheckBox ID="Chkgov" runat="server" CssClass="checkbox" Text="Govt. Holiday" /><br />
                                    <asp:CheckBox ID="ChkSPH" runat="server" CssClass="checkbox" Text="Secial Holiday" /><br />
                                    <asp:CheckBox ID="ChkAdjust" runat="server" CssClass="checkbox" Text="Adjust Holiday" />                                                             
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label16" runat="server" CssClass="label">Reason's</asp:Label>
                                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Weekend...."></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3 col-lg-3">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkbtnAllUpdate" runat="server" CssClass="btn btn-success btn-sm pull-left" ToolTip="Update Off Days" OnClick="lnkbtnAllUpdate_Click">Update</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>
                    <div class="log-divider mt-1" id="Div1" runat="server">
                        <span>
                            <i class="fa fa-fw fa-dollar-sign"></i>Previous Off Days Information</span>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvoffday" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True"
                            OnPageIndexChanging="gvoffday_PageIndexChanging">
                            <PagerSettings Position="Bottom" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBtnDelOffday" OnClick="lnkBtnDelOffday_Click" Width="20px" ToolTip="Delete Offday">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton runat="server" ID="lnkBtnDelAllOffday" OnClick="lnkBtnDelAllOffday_Click" Width="20px" ToolTip="Delete All Offday">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDepartmane" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "section").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim(): "")  %>'
                                            Width="200px"> 
                                        
 
                                        
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFUpOff" runat="server" Font-Bold="True"
                                            Font-Size="12px" CssClass="btn btn-success btn-sm" OnClick="lnkbtnFUpOff_Click" ToolTip="Off Days Final Update">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidcard" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>'
                                            Width="170px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOffdate" runat="server" BorderStyle="None" Font-Size="11px" ForeColor="Black" BackColor="Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wkdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="75px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvOffdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvOffdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Reasons">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvReason" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")).ToString() %>'
                                            Width="180px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjlocation" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jlocation")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                            <PagerSettings Mode="NumericFirstLast" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


