<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpLeave.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_84_Lea.HREmpLeave" %>

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
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label26" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label27" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label28" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label30" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1" id="divCardNo" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblEmpSearch" runat="server" Text="Card"></asp:Label>
                                <asp:TextBox ID="txtEmpSearch" runat="server" CssClass="form-control form-control-sm" placeholder="Card: 10001"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divLvRule" runat="server" visible="false">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Yearly Leave</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="True" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblyearstrtdate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtyearstrtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtyearstrtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtyearstrtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblyearenddate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtyearenddate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtyearenddate_CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtyearenddate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblColor" runat="server" BackColor="Violet" CssClass="label" Width="250px" Style="text-align: left;"> * Above 6 Month and below 1 Year 
                                </asp:Label>
                                <asp:Label ID="lblColor2" runat="server" BackColor="SkyBlue" CssClass="label" Width="250px" Style="text-align: left;"> 
                                                    * below 6 Month
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="leaveRule" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkLeave" runat="server" AutoPostBack="True" OnCheckedChanged="chkLeave_CheckedChanged" Visible="False" TabIndex="13" Text="Leave" CssClass="checkbox" />
                                        <asp:LinkButton ID="LbtnAutoGen" runat="server" CssClass="btn btn-primary btn-sm pull-left" Visible="False" OnClick="lnkbtnGenLeave1_Click" ToolTip="Auto Calculate & Add Leave">Auto Generate</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlleave" runat="server" BackColor="#F8F8F8" Visible="False">
                                <div class="row">
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="LinkButton1" runat="server" CssClass="label">Earn <br />Leave</asp:Label>
                                            <asp:TextBox ID="txternleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" runat="server" CssClass="label">Causual Leave</asp:Label>
                                            <asp:TextBox ID="txtcsleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label22" runat="server" CssClass="label">Sick <br />Leave</asp:Label>
                                            <asp:TextBox ID="txtskleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label24" runat="server" CssClass="label">Paternity Leave</asp:Label>
                                            <asp:TextBox ID="txtpaternity" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label25" runat="server" CssClass="label">Special <br />Leave</asp:Label>
                                            <asp:TextBox ID="TxtSpecialLeave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label31" runat="server" CssClass="lblTxt lblName">Maternity Leave</asp:Label>
                                            <asp:TextBox ID="txtmtleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Without Pay Leave</asp:Label>
                                            <asp:TextBox ID="txtWPayleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName">Abortion Leave</asp:Label>
                                            <asp:TextBox ID="txtAborLeave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Leave for Training</asp:Label>
                                            <asp:TextBox ID="txtTrainleave" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="100px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group" style="margin-top: 40px;">
                                            <asp:LinkButton ID="lnkbtnGenLeave" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnGenLeave_Click" ToolTip="Leave Generate">Generate</asp:LinkButton>
                                        </div>
                                    </div>

                                </div>


                            </asp:Panel>
                        </div>
                    </div>
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 450px;">
                            <div class="table-responsive">
                                <asp:GridView ID="gvLeaveRule" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvLeaveRule_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15">
                                    <PagerSettings Position="Top" Mode="NumericFirstLast" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonstatus" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monstatus")) %>'
                                                    Width="110px"></asp:Label>
                                                <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp. ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempid" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID CARD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnFUpLeave" runat="server" OnClick="lnkbtnFUpLeave_Click" CssClass="btn btn-success btn-sm" ToolTip="Update Company Leave Rule">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Earned Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Casual Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sick Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Maternity Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvml" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Without Pay Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvWPl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Abortion Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAbrleav" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abrleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Training Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvTrL" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trpleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Paternity Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtpaternity" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paternity")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Special Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSpcLeave" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spcleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Compensation Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCompleave" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "compleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leave Advance / Adjustment">
                                            <ItemTemplate>
                                                <asp:Label ID="lablAdvAdj" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advleav")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="LeaveApp" runat="server">
                    <div class="card card-fluid mb-1">
                        <div class="card-body">
                            <asp:Panel ID="PnlEmp" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblLvEmp" runat="server">Employee Name
                                                 <asp:LinkButton ID="imgbtnlAppEmpSeaarch" runat="server" OnClick="imgbtnlAppEmpSeaarch_Click" Font-Underline="false" ToolTip="Get Employee"><i class="fa fa-search"></i></asp:LinkButton>
                                            </asp:Label>
                                            <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Designation</asp:Label>
                                            <asp:Label ID="lblDesignation" runat="server" CssClass=" small form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" CssClass="label">Joining Date</asp:Label>
                                            <asp:Label ID="lblJoiningDate" runat="server" CssClass=" small form-control form-control-sm"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Panel ID="pnlReplacer" Visible="false" runat="server">
                                                <asp:Label ID="lblsign" runat="server" CssClass="label">Replacer Emp.  Name</asp:Label>
                                                <asp:DropDownList ID="ddlReplaceremp" runat="server" CssClass="form-control form-control-sm chzn-select "></asp:DropDownList>
                                            </asp:Panel>
                                            <asp:Label ID="lbltrnleaveid" runat="server" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblRptType" runat="server" CssClass="label">Report Type</asp:Label>
                                            <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control form-control-sm">
                                                <asp:ListItem>Bangla</asp:ListItem>
                                                <asp:ListItem>English</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="Pnlapply" runat="server" Visible="False">
                                    <div class="row">
                                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" CssClass="label">Apply Date</asp:Label>
                                                <asp:TextBox ID="txtaplydate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtaplydate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtaplydate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" CssClass="label">Appr. Date</asp:Label>
                                                <asp:TextBox ID="txtApprdate" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtApprdate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtApprdate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-lg-1">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:LinkButton ID="lnkbtnRef" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnRef_Click" ToolTip="Click for Refresh">Refresh</asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-1 col-sm-1 col-lg-1">
                                            <div class="form-group" style="margin-top: 20px;">
                                                <asp:CheckBox ID="chkPreLeave" runat="server" AutoPostBack="True" OnCheckedChanged="chkPreLeave_CheckedChanged" TabIndex="13" Text="Prev. Leave" CssClass="checkbox" />
                                            </div>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-lg-4">
                                            <asp:Panel ID="PnlPreLeave" runat="server" Visible="False">
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-5 col-lg-5">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label13" runat="server" CssClass="label">Prev. Leave</asp:Label>
                                                            <asp:DropDownList ID="ddlPreLeave" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                                        <div class="form-group" style="margin-top: 20px;">
                                                            <asp:LinkButton ID="lnkbtnPreLeave" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnPreLeave_Click">Show</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAttnStatus" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive mb-2">
                                            <asp:GridView ID="gvAttnStatus" runat="server" AutoGenerateColumns="False"
                                                CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="01" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day1")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="02" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day2")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="03" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday03" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day3")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="04" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday04" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day4")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="05" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday05" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day5")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="06" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday06" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day6")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="07" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday07" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day7")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="08" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday08" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day8")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="09" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday09" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day9")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="10" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday10" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day10")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="11" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday11" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day11")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="12" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday12" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day12")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="13" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday13" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day13")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="14" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday14" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day14")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="15" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday15" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day15")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="16" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday16" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day16")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="17" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday17" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day17")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="18" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday18" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day18")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="19" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday19" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day19")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="20" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday20" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day20")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="21" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday21" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day21")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="22" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday22" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day22")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="23" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday23" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day23")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="24" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday24" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day24")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="25" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday25" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day25")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="26" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday26" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day26")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="27" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday27" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day27")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="28" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday28" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day28")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="29" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday29" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day29")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="30" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday30" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day30")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="31" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvday31" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "day31")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="gvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <section class="card" style="min-height: 350px;">
                                <header class="card-header pt-1 pb-1" id="lblleaveApp" runat="server" visible="false">Leave Application</header>
                                <div class="table-responsive pl-2">
                                    <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescription" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Applied For">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="solid" BorderWidth="1px" BorderColor="green" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lapplied")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" BackColor="Transparent" Font-Size="12px"
                                                        Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnUpdateLeave" OnClientClick="return confirm('Do you want to Save this Leave?')" CssClass="btn btn-xs btn-success" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" OnClick="lnkbtnUpdateLeave_Click" ToolTip="Click to Update Leave">Update </asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave Std. Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvenjoydt1" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                        Width="90px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CssClass="btn btn-xs btn-danger" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" OnClick="lbtnDelete_Click" ToolTip="Click to Deelte Leave">Delete</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave End Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvenjoydt2" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                        Width="90px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                        BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gcod" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgcodapply" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="150px"></asp:Label>
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
                                    </asp:GridView>
                                </div>

                                <asp:Panel ID="PnlRmrks" runat="server" Visible="False">
                                    <div class="row pl-2">
                                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" CssClass="label">Reason(s) :</asp:Label>
                                                <asp:TextBox ID="txtLeavLreasons" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" CssClass="label">Address of enjoing time:</asp:Label>
                                                <asp:TextBox ID="txtaddofenjoytime" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pl-2">
                                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label16" runat="server" CssClass="label">Remarks :</asp:Label>
                                                <asp:TextBox ID="txtLeavRemarks" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-6 col-sm-6 col-lg-6 ">
                                            <div class="form-group">
                                                <asp:Label ID="Label17" runat="server" CssClass="label">While on Leav., Duties will Perform by:</asp:Label>
                                                <asp:TextBox ID="txtdutiesnameandDesig" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </section>
                        </div>
                        <div class="col-md-7 col-sm-7 col-lg-7 ">
                            <section class="card" style="min-height: 350px;">
                                <header class="card-header pt-1 pb-1" id="lblleaveStatus" runat="server" visible="false">Leave Status</header>
                                <div class="table-responsive pl-2">
                                    <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening </br> Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Entitlement">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlentitled" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave </br>This Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvtaken" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present </br>Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvprsntbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvapply" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvaprve" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing </br> Bal.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlvclsbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave </br>Std. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave</br> End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                        Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Leave</br> Day's">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="log-divider" id="lblleaveInformation" runat="server" visible="false">
                                    <span>
                                        <i class="fa fa-fw fa-dollar-sign"></i>Previous Leave Information</span>
                                </div>
                                <div class="table-responsive pl-2">
                                    <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvleaveInfo_RowDataBound"
                                        OnRowDeleting="gvleaveInfo_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvltrnleaveid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                            <asp:TemplateField HeaderText="Desription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvledescription" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="120px">
                                                                
                                                                
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Apply Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvapplydate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="From Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlstartdate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstrtdat")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlenddate" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lenddat"))%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave </br> Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvleavedays" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreason" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvremarks" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                                        Width="140px"></asp:Label>
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
                                    </asp:GridView>
                                </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="LeaveAppform" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body">
                            <asp:Panel ID="PnlEmplApp" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:LinkButton ID="imgbtnlFEmpSeaarch" runat="server" CssClass="label" OnClick="imgbtnlFEmpSeaarch_Click">Employee Name</asp:LinkButton>
                                            <asp:DropDownList ID="ddlEmpNamelApp" runat="server" OnSelectedIndexChanged="ddlEmpNamelApp_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 " style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" CssClass="label">Company</asp:Label>
                                            <asp:Label ID="lblComPanylApp" runat="server" CssClass=" smLbl_to"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                                            <asp:Label ID="lblSectionlApp" runat="server" CssClass=" smLbl_to"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" CssClass="label">Designation</asp:Label>
                                            <asp:Label ID="lblDesignationlApp" runat="server" CssClass=" form-control form-control-sm"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" CssClass="label">Joining Date</asp:Label>
                                            <asp:Label ID="lblJoiningDatelApp" runat="server" CssClass=" form-control form-control-sm"></asp:Label>
                                        </div>

                                    </div>
                                    <div class="col-md-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                            <asp:TextBox ID="txtformdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtformdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtformdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="lbltodate" runat="server" CssClass="label">From</asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        </div>
                                    </div>





                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:RadioButtonList ID="rblstFormType" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="220px" TabIndex="16" Visible="False">
                                                <asp:ListItem>Type 1</asp:ListItem>
                                                <asp:ListItem>Type 2</asp:ListItem>
                                                <asp:ListItem>Type 3</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>


                        </div>
                    </div>
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 300px;">
                            <div class="table-responsive">
                                <asp:GridView ID="gvLeaveStatus01" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="208px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desription">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDescription1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entitlement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This. Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvltaken1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Present Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavedt21" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy ") %>'
                                                    Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Leave Day's">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvenjoyday1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
