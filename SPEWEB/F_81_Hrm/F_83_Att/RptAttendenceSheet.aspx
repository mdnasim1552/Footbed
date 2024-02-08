<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptAttendenceSheet.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptAttendenceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('[id*=ddlWstation]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true,
            });
            $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "Attendencelog":
                    tblData = document.getElementById("<%=Attendencelog.ClientID %>");
                    break;
            }


            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            var gvdailyattndc = $('#<%=this.gvdailyattndc.ClientID %>');

            gvdailyattndc.Scrollable();

            var gvEmpstatus = $('#<%=this.gvEmpstatus.ClientID %>');
            gvEmpstatus.Scrollable();

            var gvmonthlyattndc = $('#<%=this.gvmonthlyattndc.ClientID %>');

            gvmonthlyattndc.Scrollable();

            var gvmonthlylateattndc = $('#<%=this.gvmonthlylateattndc.ClientID %>');

            gvmonthlylateattndc.Scrollable();

            var gvmonthlyovertime = $('#<%=this.gvmonthlyovertime.ClientID %>');

            gvmonthlyovertime.Scrollable();

            var gvdailylateatt = $('#<%=this.gvdailylateatt.ClientID %>');

            gvdailylateatt.Scrollable();

            var gvdailyabsent = $('#<%=this.gvdailyabsent.ClientID %>');

            gvdailyabsent.Scrollable();

            var gvShiftingall = $('#<%=this.gvShiftingall.ClientID %>');

            gvShiftingall.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

            $('[id*=ddlWstation]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true,
            });
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

         /*Multiselect Style*/
        .multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .caret {
            display: none !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .input-group .form-control:not(:first-child):not(:last-child) {
            height: 32px !important;
        }

        .input-group-btn:last-child > .btn {
            height: 32px !important;
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
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label20" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:ListBox ID="ddlWstation" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label22" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label23" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label24" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblLocation" runat="server" CssClass="label">Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblReport" runat="server" CssClass="label">Report Type</asp:Label>
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="chzn-select form-control form-control-sm" 
                                    OnSelectedIndexChanged="ddlReport_OnSelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Selected="True">Attendance Log</asp:ListItem>
                                    <asp:ListItem Value="1">Daily Attendance</asp:ListItem>
                                    <asp:ListItem Value="2">Emp. Status</asp:ListItem>
                                    <asp:ListItem Value="3">Monthly Attendance</asp:ListItem>
                                    <asp:ListItem Value="4">Monthly Late Attendance</asp:ListItem>
                                    <asp:ListItem Value="5">Emp. Status (Late)</asp:ListItem>
                                    <asp:ListItem Value="6">Early Leave</asp:ListItem>
                                    <%--<asp:ListItem Value="7">Monthly Overtime</asp:ListItem>--%>
                                    <asp:ListItem Value="8">Shifting</asp:ListItem>
                                    <asp:ListItem Value="9">Daily Attendance Sum.</asp:ListItem>
                                    <asp:ListItem Value="10">Daily Late Attendance</asp:ListItem>
                                    <asp:ListItem Value="11">Daily Absent</asp:ListItem>
                                    <asp:ListItem Value="12">Attendance Approval</asp:ListItem>
                                    <asp:ListItem Value="13">Shifting All</asp:ListItem>
                                    <asp:ListItem Value="14">Miss Attendance</asp:ListItem>
                                    <%--<asp:ListItem Value="15">Job Card </asp:ListItem>
                                    <asp:ListItem Value="16">Section Wise Emp </asp:ListItem>--%>
                                    <asp:ListItem Value="17">Monthly Absent</asp:ListItem>
                                    <asp:ListItem Value="18">Daily Present</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divFrmDate" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divToDate" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1-half col-md-1-half col-lg-1-half ml-2" id="divChkInactPunch" runat="server">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkInactPunch" runat="server" Text="Inact. Punch" ToolTip="Inactive Punch" OnCheckedChanged="chkInactPunch_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="col-md-0-half col-sm-0-half col-lg-0-half ml-2" id="divResign" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="BtnChckResign" runat="server" Text="Resign" CssClass="checkbox" AutoPostBack="true" OnCheckedChanged="BtnChckResign_CheckedChanged" ToolTip="Resign Employee" />
                            </div>
                        </div>
                        <div class="col-sm-1-half col-md-1-half col-lg-1-half" id="divChkPrsntEmp" runat="server" visible="false" style="margin-top: 20px;">
                            <label id="chkponly" runat="server" class="switch" title="Check for Presen Emp.">
                                <asp:CheckBox ID="chkpresentOnly" runat="server" ClientIDMode="Static" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label ID="lblpresentOnly" runat="server" Text="Present Only" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 " id="divImage" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkWithImage" runat="server" Text="With Image" CssClass="checkbox" />
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divEmpName" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblempname" runat="server" CssClass="label">Emp. Name
                                    <asp:LinkButton ID="imgbtnEmpName" runat="server" OnClick="imgbtnEmpName_Click" ToolTip="Click for Employee"><span class="fa fa-search"> </span></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlEmpName_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True"
                                    TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                        <div class="col-sm-1 cl-md-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkWithoutOT" runat="server" Text="Without OT" CssClass="checkbox" ToolTip="Check to Without OT" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="Attendencelog" AutoGenerateColumns="false" runat="server" Width="800px" CssClass="table-bordered table-hover grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Width="30px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchIdcard" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Card #" onkeyup="Search_Gridview(this,1, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvidcardNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchEmp" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Name" onkeyup="Search_Gridview(this,2, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Depaertment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDept" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "depname")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvline" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>' Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Node No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchMachine" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Node No" onkeyup="Search_Gridview(this,5, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMachine" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "machine")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId01" Width="80px" runat="server" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("yyyyMMdd") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId01" runat="server" Width="90px" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("HH:mm:ss ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PF/FP/FaceId">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId01" runat="server" Width="90px" Font-Bold="True" Height="16px"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "faceId")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="dailyAtt" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvdailyattndc" AutoGenerateColumns="false" OnRowCreated="gvdailyattndc_RowCreated" CssClass="table-bordered table-hover grvContentarea"
                                    runat="server" Width="253px" AllowPaging="True" PageSize="20" OnPageIndexChanging="gvdailyattndc_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesection" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateduesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "line")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Off. In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Off. Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":
                                                Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":
                                                Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "late")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateremrks" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <PagerStyle CssClass="gvPagination" />

                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvEmpstatus" AutoGenerateColumns="false"
                                    CssClass="table-bordered table-hover grvContentarea" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSL03" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatuscard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusemp" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusdept" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusdesig" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusline" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusdate" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Join Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusjoindate" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimein")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusouttime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimeout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusactintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatusactouttime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempstatusleave" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late In">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempstatuslatein" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lateinmin")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Early Exist">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempstatusearlyext" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyexit")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempstatusot" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovtmin")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="monthlyattndc" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvmonthlyattndc" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px" AllowPaging="true"
                                    PageSize="20" OnPageIndexChanging="gvmonthlyattndc_PageIndexChanging" ViewStateMode="Enabled">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedept" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="120px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addday")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="monthlylateattndc" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvmonthlylateattndc" AutoGenerateColumns="false" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    runat="server" Width="253px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesetion" Width="100px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="01">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday01" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col1")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday02" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col2")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday03" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col3")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday04" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col4")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday05" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col5")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday06" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col6")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday07" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col7")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday08" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col8")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday09" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col9")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday10" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col10")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday11" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col11")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday12" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col12")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday13" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col13")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday14" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col14")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday15" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col15")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday16" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col16")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday17" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col17")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday18" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col18")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday19" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col19")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday20" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col20")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday21" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col21")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday22" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col22")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday23" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col23")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday24" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col24")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday25" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col25")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday26" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col26")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday27" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col27")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday28" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col28")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday29" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col29")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday30" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col30")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateday31" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "col31")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateTotal" Width="30px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallate")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="empstatuslate" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvempstatuslate" AutoGenerateColumns="false" OnRowCreated="gvdailyattndc_RowCreated" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimein")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimeout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="empstatusearly" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvempstatusearly" AutoGenerateColumns="false" OnRowCreated="gvdailyattndc_RowCreated" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimein")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "stdtimeout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="monthlyovertime" runat="server">
                            <div class=" table-responsive">
                                <asp:GridView ID="gvmonthlyovertime" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnam")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdept")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdsg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="VIShiftingData" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="ShiftingData" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "officein")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office Out Time">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "officeout")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="DailyAttSum" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvdailyattsum" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea"
                                    AllowPaging="true" runat="server" Width="253px" OnPageIndexChanging="gvdailyattsum_PageIndexChanging" ShowFooter="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedept" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvfotdailsumSttlPR" Width="50px" runat="server" Height="16px">Total</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="gvfotdailsumttlPR" Width="50px" runat="server" Height="16px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="50px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="gvfotdailsleave" Width="50px" runat="server" Height="16px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="50px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absent1")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvfotdabsent1" Width="50px" runat="server" Height="16px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Holiday">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="50px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "holiday")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="gvfotholiday" Width="50px" runat="server" Height="16px"></asp:Label>
                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="50px" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvfotdailsumttl" Width="50px" runat="server" Height="16px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="DailyLateAtt" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvdailylateatt" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea"
                                    AllowPaging="true" runat="server" Width="253px" OnPageIndexChanging="gvdailylateatt_PageIndexChanging">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatesl" runat="server" Font-Bold="True" Height="16px" Width="30px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlatedesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designation")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvoffintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offintime")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Intime">
                                            <ItemTemplate>
                                                <asp:Label ID="gvactintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latetime")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlateremrks" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="dailyAbsent" runat="server">
                            <div class="table-responsive">
                                <asp:LinkButton ID="LinkButtonExportToExcel" runat="server" Style="font-weight:bold;margin-bottom:5px !important;text-decoration: underline;color:green;" ToolTip="Export To Excel" Visible="false" OnClick="PopulateTemplateExcelFile">Export</asp:LinkButton>                               
                                <asp:GridView ID="gvdailyabsent" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <HeaderTemplate>
                                                <table style="width: 200px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <label runat="server" backcolor="#000066"
                                                                bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                                forecolor="White" style="text-align: center" width="120px">
                                                                Department</label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export To Excel" Width="30px"
                                                                CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="gvabdept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DOJ">
                                        <ItemTemplate>
                                            <asp:Label ID="gvdateofjoin" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabDay" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absday")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last P.Date">
                                        <ItemTemplate>
                                            <asp:Label ID="gvLastPresentDate" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lpredate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabline" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "line"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabMObile" Width="90px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empmobile")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="ViewAttenApproval" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvattenapproval" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabdept" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designation")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabsebtdate" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="gvabremrks" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapprby" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absapruser")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="View14" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvShiftingall" AutoGenerateColumns="false" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifemp" Width="100px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifdept" Width="100px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifdesig" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcard" Width="50px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cardno"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="01">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol1" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col1"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol1o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col1o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="02">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol2" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col2"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol2o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col2o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="03">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol3" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col3"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol3o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col3o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="04">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol4" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col4"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol4o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col4o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="05">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol5" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col5"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol5o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col5o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="06">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol6" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col6"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol6o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col6o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="07">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol7" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col7"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol7o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col7o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="08">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol8" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col8"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol8o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col8o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="09">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol9" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col9"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol9o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col9o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol10" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col10"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol10o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col10o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol11" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col11"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol11o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col11o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol12" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col12"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol12o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col12o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol13" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col13"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol13o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col13o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol14" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col14"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol14o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col14o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol15" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col15"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol15o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col15o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol16" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col16"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol16o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col16o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol17" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col17"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol17o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col17o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol18" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col18"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol18o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col18o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol19" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col19"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol19o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col19o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol20" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col20"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol20o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col20o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="21">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol21" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col21"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol21o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col21o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="22">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol22" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col22"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol22o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col22o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="23">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol23" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col23"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol23o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col23o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="24">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol24" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col24"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol24o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col24o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="25">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol25" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col25"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol25o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col25o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="26">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol26" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col26"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol26o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col26o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="27">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol27" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col27"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol27o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col27o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="28">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol28" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col28"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol28o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col28o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="29">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol29" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col29"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol29o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col29o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="30">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol30" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col30"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol30o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col30o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="31">
                                            <ItemTemplate>
                                                <asp:Label ID="gvshifcol31" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col31"))%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="gvshifcol31o" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "col31o"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <FooterStyle CssClass="" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </div>

                        </asp:View>
                        <asp:View ID="MissAttn" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMissAttn" AutoGenerateColumns="false" ShowFooter="True" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px" AllowPaging="True" OnPageIndexChanging="gvMissAttn_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Width="30px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Car #">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchIdcard" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Card #" onkeyup="Search_Gridview(this,1, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvidcardNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchEmp" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Name" onkeyup="Search_Gridview(this,2, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDept" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "depname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Node No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchMachine" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Machine" onkeyup="Search_Gridview(this,5, 'Attendencelog')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMachine" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "machine")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dayId01" Width="80px" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Log">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Width="90px" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("hh:mm:ss tt ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <asp:GridView ID="GridView2" AutoGenerateColumns="false" OnRowCreated="gvdailyattndc_RowCreated" CssClass="table table-bordered table-hover grvContentarea" runat="server" Width="253px" AllowPaging="True" PageSize="20" OnPageIndexChanging="gvdailyattndc_PageIndexChanging">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" />

                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlal1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IdCard">
                                        <ItemTemplate>

                                            <asp:Label ID="gvEmpid" Width="150px" Visible="False" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                            <asp:Label ID="gvmissidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlateempm" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlaSections" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlatedesigMsi" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Line">
                                        <ItemTemplate>
                                            <asp:Label ID="gvLateLine" Width="60px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Join Date">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblJoindate" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Off InTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlateintimeM" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Off OutTime">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlatdeintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ac. Intime">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIntime" BorderWidth="1" BorderColor="blue"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="OM"?false:true  %>'
                                                runat="server" Text=""></asp:TextBox>

                                            <asp:Label runat="server" ID="lbltstatus" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))%>'></asp:Label>

                                            <asp:Label ID="gvlateinout" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="IM"?false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ac. Outtime">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtOut" BorderWidth="1" BorderColor="blue"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="IM"?false:true  %>'
                                                Text=""></asp:TextBox>

                                            <asp:Label ID="gvlateintimdem" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="OM"?false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>

                                            <asp:LinkButton runat="server" ID="lnkUpdate" OnClick="lnkUpdate_OnClick" CssClass="btn btn-sm btn-danger">Update</asp:LinkButton>

                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <%--   <asp:TemplateField HeaderText="Late">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlateintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "late")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave/Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="gvlateremrks" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>--%>
                                </Columns>
                                <HeaderStyle CssClass="gvHeader" />
                                <FooterStyle CssClass="" />
                                <PagerStyle CssClass="gvPagination" />
                            </asp:GridView>
                        </asp:View>
                        <asp:View ID="JobCard" runat="server">
                            <asp:Panel ID="Panel1" runat="server">
                                <div>
                                    <fieldset class="scheduler-border fieldset_A">
                                        <div class="form-horizontal">

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <fieldset>
                                                        <div class="form-horizontal">

                                                            <div class="form-group">
                                                                <asp:Label ID="Label9" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">A. EMPLOYEE INFORMATION</asp:Label>

                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <asp:Label ID="Label11" runat="server" CssClass=" lblTxt lblName"> Card No</asp:Label>
                                                            <asp:Label ID="lblcard" runat="server" CssClass=" smLbl_to lblStyle"> Card</asp:Label>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Label ID="Label5" runat="server" CssClass=" lblTxt lblName"> Emp Name</asp:Label>
                                                            <asp:Label ID="lblname" runat="server" CssClass=" smLbl_to lblStyle"> Name</asp:Label>
                                                        </div>

                                                        <div class="row">
                                                            <asp:Label ID="Label6" runat="server" CssClass=" lblTxt lblName">Desgnation</asp:Label>
                                                            <asp:Label ID="lbldesg" runat="server" CssClass=" smLbl_to lblStyle"> Desgnation name</asp:Label>
                                                        </div>
                                                        <div class="row">
                                                            <asp:Label ID="Label4" runat="server" CssClass=" lblTxt lblName">Department</asp:Label>
                                                            <asp:Label ID="lbldpt" runat="server" CssClass=" smLbl_to lblStyle"> Department name</asp:Label>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <fieldset>
                                                        <div class="form-horizontal">

                                                            <div class="form-group">
                                                                <asp:Label ID="Label10" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">B.  OFFICE TIME</asp:Label>

                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label12" runat="server" CssClass=" lblTxt lblName lblName2"> In Time</asp:Label>
                                                                <asp:Label ID="lblIntime" runat="server" CssClass=" smLbl_to lblStyle2"> Date</asp:Label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblDate" runat="server" CssClass=" lblTxt lblName lblName2"> Out Time</asp:Label>
                                                                <asp:Label ID="lblout" runat="server" CssClass=" smLbl_to lblStyle2"> Date</asp:Label>
                                                            </div>


                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label13" runat="server" CssClass=" lblTxt lblName lblName2">Total Working Day</asp:Label>
                                                                <asp:Label ID="lblwork" runat="server" CssClass=" smLbl_to lblStyle2"> Working</asp:Label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label15" runat="server" CssClass=" lblTxt lblName lblName2">Total Late Day</asp:Label>
                                                                <asp:Label ID="lblLate" runat="server" CssClass=" smLbl_to lblStyle2"> Late</asp:Label>
                                                            </div>


                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label17" runat="server" CssClass=" lblTxt lblName lblName2">Total Leave Day</asp:Label>
                                                                <asp:Label ID="lblLeave" runat="server" CssClass=" smLbl_to lblStyle2"> Leave</asp:Label>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:Label ID="Label19" runat="server" CssClass=" lblTxt lblName lblName2">Total Absent Day</asp:Label>
                                                                <asp:Label ID="lblAbsent" runat="server" CssClass=" smLbl_to lblStyle2"> Absent</asp:Label>

                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label21" runat="server" CssClass=" lblTxt lblName lblName2">Total Holiday Day</asp:Label>
                                                                <asp:Label ID="lblHoliday" runat="server" CssClass=" smLbl_to lblStyle2"> Holiday</asp:Label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label14" runat="server" CssClass=" lblTxt lblName lblName2">Total OT</asp:Label>
                                                                <asp:Label ID="lblOvtime" runat="server" CssClass=" smLbl_to lblStyle2"></asp:Label>

                                                            </div>


                                                        </div>
                                                        <div class="row" style="display: none">
                                                            <asp:Label ID="Label16" runat="server" CssClass=" lblTxt lblName"> Company Name</asp:Label>
                                                            <asp:Label ID="lblcompname" runat="server" CssClass=" smLbl_to lblStyle2"> Company name</asp:Label>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                    </fieldset>
                                </div>


                            </asp:Panel>
                            <fieldset>
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <asp:Label ID="lblServHead" runat="server" CssClass="btn btn-success btn-sm" Style="font-weight: bold">C. DETAILS INFORMATION</asp:Label>

                                    </div>
                                </div>
                            </fieldset>

                            <asp:Repeater ID="RptMyAttenView" runat="server" OnItemDataBound="RptMyAttenView_ItemDataBound">

                                <HeaderTemplate>
                                    <table class="table-striped table-hover table-bordered grvContentarea grvHeader grvFooter" style="width: 40%;">
                                        <tr>
                                            <th>Date</th>
                                            <th>In Time</th>
                                            <th>Out Time</th>
                                            <th>Status</th>
                                            <th style="display: none;">Penalty</th>
                                            <th style="display: none;">Official Hour</th>
                                            <th>Late</th>
                                            <th>E.exit</th>
                                            <th id="thheader" runat="server">OT</th>

                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:Label ID="lblempdeptid" Visible="False" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")) %>'></asp:Label>
                                            <asp:Label ID="lblacintime" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "wintime")).ToString("dd-MMM-yyyy") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblactualin" runat="server"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="H" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="CL"?false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>

                                            <%--    <asp:Label ID="lblactualin" runat="server"
                                            Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="H" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "leav"))=="Lv"?false:true  %>'
                                                   
                                                   
                                                   Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>--%>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblactualout" runat="server"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="OM" || Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="H" || Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="Lv"?false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>

                                            <%-- <asp:Label ID="lblactualout" runat="server"
                                                  
                                            Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "tstatus"))=="OM" || Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="H" || Convert.ToString(DataBinder.Eval(Container.DataItem, "leav"))=="Lv"?false:true  %>'
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>--%>
                                       
                                        </td>
                                        <td>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).ToString() %>'></asp:Label>

                                        </td>
                                        <td style="display: none;"></td>
                                        <td style="display: none;"></td>

                                        <td>
                                            <asp:Label ID="lblactualattnminute" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lateinmin")).ToString() %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblearlyexit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "earlyexit")).ToString() %>'></asp:Label>

                                        </td>

                                        <td id="tdcolumn" runat="server">
                                            <asp:Label ID="lblOt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovtmin")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>

                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbltotal" Visible="False" runat="server" Style="font-weight: bold;">Total</asp:Label></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>


                                        <td>
                                            <asp:Label ID="lblTotalHour" Visible="False" runat="server" Style="font-weight: bold;">40:00</asp:Label>
                                        </td>

                                    </tr>

                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </asp:View>
                        <asp:View ID="ViewDailyPresent" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDailyPresent" AutoGenerateColumns="false" CssClass="table-bordered table-hover grvContentarea" runat="server" Width="253px"
                                    AllowPaging="True" OnPageIndexChanging="gvDailyPresent_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Employee Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntempid" Width="150px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntidcard" runat="server" Width="50px" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntemp" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntdesig" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntline" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "line")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="In Time">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntintime" Width="80px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntlate" Width="40px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latetime")) == "" ? "" : Convert.ToString(DataBinder.Eval(Container.DataItem, "latetime")) + " Min." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="gvdailyprsntremarks" Width="120px" runat="server" Height="16px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeader" />
                                    <PagerStyle CssClass="gvPagination" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

