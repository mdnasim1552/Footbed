<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpPunch.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptEmpPunch" %>

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
                <div class="card-body" style="min-height:100px;">
                    <div class="row">


                       
                            <div class="col-md-12">
                                <asp:RadioButtonList ID="rbtnMissPunch" runat="server" AutoPostBack="True"
                                    BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                    Font-Bold="True" Font-Size="12px" ForeColor="Black" RepeatLayout="Table"
                                    OnSelectedIndexChanged="rbtnMissPunch_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem>Missed In Punch</asp:ListItem>
                                    <asp:ListItem>Missed Out Punch</asp:ListItem>
                                    <asp:ListItem>Doubtful Punch</asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                     
                       
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to" Visible="false">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox " Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                            </div>
                           
                            
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <asp:Label ID="lblJobLoc" runat="server" CssClass=" smLbl_to">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server"  OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>
                    

                       
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <asp:Label ID="lblDiv" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server"  OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <asp:Label ID="lblDept" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <asp:Label ID="lblSection" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                <asp:DropDownList ID="listProject" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="lblTxt lblName">Emp. Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-1" style="margin-top:19px">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                            </div>
                      
                        <div class="col-md-1 ">
                                <asp:Label ID="lblPa" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="BtnChckResign" runat="server" AutoPostBack="true" OnCheckedChanged="BtnChckResign_CheckedChanged" CssClass="chkBoxControl" Visible="false" Text="Resign" />
                            </div>
                            <div class="col-md-4 pading5px">
                                <asp:Label ID="lblempname" runat="server" Visible="false" CssClass="lblTxt lblName hidden">Emp.  Name:</asp:Label>
                                <asp:TextBox ID="txtSrcEmpName" runat="server" Visible="false" CssClass="form-control form-control-sm"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnEmpName" runat="server" Visible="false" CssClass="lblTxt lblName" OnClick="imgbtnEmpName_Click">Emp.  Name:</asp:LinkButton>
                                <asp:DropDownList ID="ddlEmpName" runat="server" Visible="false" Width="233" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        

                    </div>
                </div>
            </div>



            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="table-responsive">

                    
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <asp:GridView ID="gvMissInPunch" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" runat="server" Width="253px" AllowPaging="True" OnPageIndexChanging="gvMissInPunch_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmipsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
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
                                            <asp:Label ID="gvminempline" Width="60px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Off InTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmipoffintime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Off OutTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmipoffouttime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <PagerStyle CssClass="gvPagination" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <asp:GridView ID="gvMissOutPunch" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" runat="server" Width="253px" AllowPaging="True" OnPageIndexChanging="gvMissOutPunch_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmopsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
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
                                            <asp:Label ID="gvmopempname" Width="120px" runat="server" Height="16px" Style="text-align: left"
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
                                            <asp:Label ID="gvmopempdept" Width="120px" runat="server" Height="16px" Style="text-align: left"
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
                                            <asp:Label ID="gvmopempline" Width="60px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Off InTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmopoffintime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Off OutTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvmopoffouttime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <PagerStyle CssClass="gvPagination" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <asp:GridView ID="gvDoubtfulPunch" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" runat="server" Width="253px" AllowPaging="True" OnPageIndexChanging="gvDoubtfulPunch_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="gvdbtpsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
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
                                            <asp:Label ID="gvdbtfpempline" Width="60px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Off InTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvdbtfpoffintime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Off OutTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvdbtfpoffouttime" Width="80px" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
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
