<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DailyEmpPunchModification.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.DailyEmpPunchModification" %>

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
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
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
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtnMissPunch" runat="server" AutoPostBack="True"
                                    CssClass="checkbox"
                                    Font-Bold="True" Font-Size="12px" ForeColor="Black" RepeatLayout="Table"
                                    OnSelectedIndexChanged="rbtnMissPunch_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Missed In Punch</asp:ListItem>
                                    <asp:ListItem>Missed Out Punch</asp:ListItem>
                                    <asp:ListItem>Doubtful Punch</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlyearmon" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlyearmon_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:DropDownList ID="ddlDayLst" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                <asp:Label ID="lbltodate" runat="server" CssClass=" label" Visible="false">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox " Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-contro-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDiv" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="listProject" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1-half col-sm-1-half col-lg-1-half">
                            <div class="form-group">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="label ">Emp. Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLoc" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-half col-md-half col-lg-half">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-half col-md-half col-lg-half ml-2">
                            <div class="form-group ">
                                <asp:Label ID="lblPageSize" runat="server" CssClass="label" Text="Page Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1100</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 350px">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvMissInPunch" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" runat="server" Width="253px" AllowPaging="True"
                                    OnPageIndexChanging="gvMissInPunch_PageIndexChanging" OnRowDataBound="gvMissInPunch_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="UpdateChk" runat="server" AutoPostBack="true" CssClass="chkBoxControl" />

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="UpdateChkHead" runat="server" AutoPostBack="true" OnCheckedChanged="UpdateChkHead_CheckedChanged" CssClass="chkBoxControl" />

                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipid" Width="150px" Visible="False" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                <asp:Label ID="gvminidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvminempname" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipempdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipempdept" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipempsection" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empsection")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvminempline" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Off InTime">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="gvmipoffintime" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="txtgvinDate" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adate")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvinDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvinDate"></cc1:CalendarExtender>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Off OutTime">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmipoffouttime" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlAtStatus" runat="server">
                                                    <asp:ListItem Value="P">Present</asp:ListItem>
                                                    <asp:ListItem Value="A">Absent</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtmipremarks" Width="150px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <FooterStyle CssClass="" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:GridView ID="gvMissOutPunch" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    runat="server" Width="253px" AllowPaging="True"
                                    OnPageIndexChanging="gvMissOutPunch_PageIndexChanging"
                                    OnRowDataBound="gvMissOutPunch_RowDataBound">

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="UpdategvMissOutPunchChk" runat="server" AutoPostBack="true" CssClass="chkBoxControl" />

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="UpdategvMissOutPunchChkHead" runat="server" AutoPostBack="true" OnCheckedChanged="UpdategvMissOutPunchChkHead_CheckedChanged" CssClass="chkBoxControl" />

                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempid" Width="150px" Visible="False" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                <asp:Label ID="gvmopidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempname" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempdept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempsection" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empsection")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopempline" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Off InTime">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmopoffintime" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="txtgvDate" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adate")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Off OutTime">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="gvmopoffouttime" Width="100px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateAll" runat="server" Font-Bold="True" CssClass="btn btn-sm btn-success" Font-Size="12px"
                                                    OnClick="lnkbtnUpdateAll_Click"> Update Same </asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlAtStatus" runat="server">
                                                    <asp:ListItem Value="P">Present</asp:ListItem>
                                                    <asp:ListItem Value="A">Absent</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtmopremarks" Width="150px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <FooterStyle CssClass="" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View3" runat="server">
                                <asp:GridView ID="gvDoubtfulPunch" AutoGenerateColumns="false" OnRowDataBound="gvDoubtfulPunch_RowDataBound"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" runat="server" Width="253px" AllowPaging="True" OnPageIndexChanging="gvDoubtfulPunch_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtpsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="UpdategvDoubtfulPunchChk" runat="server" AutoPostBack="true" CssClass="chkBoxControl" />

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="UpdategvDoubtfulPunchChkHead" runat="server" AutoPostBack="true" OnCheckedChanged="UpdategvDoubtfulPunchChkHead_CheckedChanged" CssClass="chkBoxControl" />

                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID Card">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempid" Width="150px" Visible="False" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                <asp:Label ID="gvdbtfpidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempname" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempdept" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempsection" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empsection")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdbtfpempline" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Off InTime">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="gvdbtfpoffintime" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="txtgvdoubtDate" Width="80px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adate")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvdoubtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvdoubtDate"></cc1:CalendarExtender>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Off OutTime">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="LightSeaGreen" BorderStyle="Solid" BorderWidth="1" ID="gvdbtfpoffouttime" Width="100px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnDUpdateAll" runat="server" Font-Bold="True" CssClass="btn btn-sm btn-success" Font-Size="12px"
                                                    OnClick="lnkbtnUpdateAll_Click"> Update Same </asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlAtStatus" runat="server">
                                                    <asp:ListItem Value="P">Present</asp:ListItem>
                                                    <asp:ListItem Value="A">Absent</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtdbtfremarks" Width="150px" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <FooterStyle CssClass="" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
