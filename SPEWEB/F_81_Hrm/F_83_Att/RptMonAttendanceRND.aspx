<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMonAttendanceRND.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptMonAttendanceRND" %>

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

            var gvmonthlyattndc = $('#<%=this.gvmonthlyattndc.ClientID %>');

            gvmonthlyattndc.gridviewScroll({
                width: 1250,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../../Image/arrowvt.png",
                varrowbottomimg: "../../Image/arrowvb.png",
                harrowleftimg: "../../Image/arrowhl.png",
                harrowrightimg: "../../Image/arrowhr.png",
                freezesize: 9
            });

            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>


    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }


        .grvContentarea > thead > tr > th, .grvContentarea > tbody > tr > th, .grvContentarea > tfoot > tr > th, .grvContentarea > thead > tr > td, .grvContentarea > tbody > tr > td, .grvContentarea > tfoot > tr > td {
            padding: 0 2px 0 1px;
        }

        .PopCal {
            z-index: 10005;
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

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:RadioButtonList ID="rbtnAtten" runat="server" AutoPostBack="True"
                                            BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                            Font-Bold="True" Font-Size="8px" ForeColor="Black" RepeatLayout="Table"
                                            OnSelectedIndexChanged="rbtnAtten_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>Monthly Atten.</asp:ListItem><%--3--%>                                            
                                        </asp:RadioButtonList>

                                    </div>
                                    <div class="col-md-2 pull-right">
                                        <a href="#" style="display: none;" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" style="display: none;" href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">Next</a>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lblPa" runat="server" CssClass=" smLbl_to" Text="Page Size"></asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" >
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
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Job Location</asp:Label>
                                        <asp:DropDownList ID="ddlJobLocation" runat="server" Width="200" OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                    </div>


                                    <div class="col-md-2">
                                        <label id="chkbod" runat="server" class="switch" visible="false">
                                            <asp:CheckBox ID="chkcompanrmission" runat="server" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label runat="server" Text="Permission Wise" ID="lblcomparmission" CssClass="control-label" Visible="false"></asp:Label>

                                    </div>
                                </div>

                                <div class="">
                                    <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Emp. Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="50%" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                        <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="70%" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="60%" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                        <asp:DropDownList ID="listProject" runat="server" CssClass="chzn-select pull-left" Width="60%"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                        <asp:Label ID="Label18" runat="server" CssClass="lblTxt lblName">Emp. Line</asp:Label>
                                        <asp:DropDownList ID="ddlempline" runat="server" Width="50%" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chkWithImage" runat="server" AutoPostBack="true" CssClass="chkBoxControl" Visible="true" Text="With Image" />

                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lblempname" runat="server" Visible="false" CssClass="lblTxt lblName hidden">Emp.  Name:</asp:Label>
                                        <asp:TextBox ID="txtSrcEmpName" runat="server" Visible="false" CssClass="inputTxt inputName  hidden inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnEmpName" runat="server" CssClass="lblTxt lblName" Visible="False" OnClick="imgbtnEmpName_Click">Emp.  Name:</asp:LinkButton>

                                        <asp:DropDownList ID="ddlEmpName" Visible="False" runat="server" Width="233" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                        </fieldset>
                    </div>
                </div>
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="monthlyattndc" runat="server">
                        <asp:GridView ID="gvmonthlyattndc" AutoGenerateColumns="false" CssClass="table table-bordered table-hover grvContentarea" runat="server" Width="253px" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvmonthlyattndc_PageIndexChanging" ViewStateMode="Enabled">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First" LastPageText="Last" />
                            <Columns>

                                <asp:TemplateField HeaderText="Serial No">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlatedept" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlateidcard" runat="server" Width="120px" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addday")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle CssClass="grvHeader" />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
