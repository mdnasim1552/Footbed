<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpOvertime.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_86_All.EmpOvertime" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

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
    </style>
    <script type="text/javascript" language="javascript">
        function openModal() {
            //    $('#myModal').modal('show');
            $('#myModal').modal('toggle');
        }
        function CloseMOdal() {
            $('#myModal').modal('hide');
        }
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvEmpMbill":
                    tblData = document.getElementById("<%=gvEmpMbill.ClientID %>");
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
                    <asp:Panel ID="fltrSection1" runat="server" Visible="true">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="label">Employee Type</asp:Label>
                                    <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="label">Division</asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                    <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Section</asp:Label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1 col-lg-1" id="divLine" runat="server" visible="false">
                                <div class="form-group">
                                    <asp:Label ID="lblEmpLine" runat="server" CssClass="label">Line</asp:Label>
                                    <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <div class="form-group">
                                    <asp:Label ID="lblJob" runat="server" CssClass="label">Job Location</asp:Label>
                                    <asp:DropDownList ID="ddlJob" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divDate" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:DropDownList ID="ddlyearmon" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divGrade" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Grade</asp:Label>
                                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divEmpStatus" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Employee Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divLnkbtnShow" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow02" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divCard" runat="server">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="label" OnClick="imgbtnSearchEmployee_Click"
                                    ToolTip="Searh by Card" Font-Underline="false">Card &nbsp;<span class="fa fa-search"></span></asp:LinkButton>
                                <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control form-control-sm" placeholder="Card: 10001"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divIssueDate" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblIssueDate" runat="server" CssClass="label">Issue Date</asp:Label>
                                <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtIssueDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtIssueDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divPageSize" runat="server" visible="false">
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
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divUpload" runat="server" visible="false">
                            <asp:Label ID="Label4" runat="server" CssClass="label">Excel Upload</asp:Label>
                            <br />
                            <div class="btn btn-secondary btn-sm fileinput-button">
                                <i class="fa fa-plus fa-fw"></i>
                                <span>Add files...</span>
                                <asp:FileUpload ID="fileuploadExcel" runat="server" Height="26px"
                                    onchange="submitform();" />
                            </div>
                            <asp:LinkButton ID="btnexcuplosd" runat="server" CssClass=" btn btn-sm btn-success" Text="ADJUST" ToolTip="Click to Adjust Amount" OnClick="btnexcuplosd_Click"></asp:LinkButton>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divChkEmp" runat="server" visible="false" style="margin-top: 20px;">
                            <label id="chkbod" runat="server" class="switch" title="Check for Add Emp.">
                                <asp:CheckBox ID="chkAddEmp" runat="server" ClientIDMode="Static" OnCheckedChanged="chkAddEmp_CheckedChanged" AutoPostBack="true" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label ID="lblAddEmp" runat="server" Text="Add Employee" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="divAddEmp" runat="server" visible="false">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server" CssClass="label">Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnAddEmployee" runat="server" CssClass="btn btn-success  btn-sm pull-left" OnClick="lbtnAddEmployee_Click">Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewOvertime" runat="server">
                            <div class="row" id="divDedHour" runat="server" visible="false">
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">Ded. Hour</asp:Label>
                                        <asp:TextBox ID="txtdedicationHour" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkbtnGenLeave" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnGenLeave_Click" ToolTip="Add Deduction Hour">Generate</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <asp:GridView ID="gvEmpOverTime" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvEmpOverTime_PageIndexChanging" ShowFooter="True"
                                    Width="831px" OnRowDeleting="gvEmpOverTime_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                        <asp:TemplateField HeaderText="Card #">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchArt" BorderStyle="None" runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this,3, 'gvEmpOverTime')"></asp:TextBox><br />
                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                           <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lTotal" runat="server" OnClick="lTotal_Click" CssClass="btn btn-primary btn-sm">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" BackColor="White" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSection" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Employee Name & Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpNamed" runat="server"
                                                    Text=' <%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>

                                                <asp:Label ID="lblEmpidOT" runat="server" Visible="false"
                                                    Text=' <%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:LinkButton ID="lUpdate" runat="server" OnClick="lUpdate_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fixed Hour">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvFixed" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixhour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hourly">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvhourly" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Visible="false"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourly")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                                <asp:LinkButton OnClick="gvOtHourly_Click" ID="gvOtHourly" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourly")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:LinkButton>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling(7PM-10PM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling(10:1PM-2AM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling(2AM-6PM)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvc3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3hour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fixed Rate" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFixedrate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fixrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Hourly(Rate)" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvhourlyrate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hourrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvc1rate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1rate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvc2rate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2rate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ceiling(Rate)" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvc3rate" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3rate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Dedication Hour">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtDeduction"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todedihour")).ToString("#,##0.00;(#,##0.00); ") %>' BackColor="Transparent"
                                                    BorderStyle="Solid" BorderWidth="1" BorderColor="blue"
                                                    Style="text-align: right" Width="70px"></asp:TextBox>


                                            </ItemTemplate>


                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAmt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFhour" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                        </asp:View>
                        <asp:View ID="BankPayment" runat="server">

                            <asp:GridView ID="gvBankPay" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvBankPay_PageIndexChanging"
                                ShowFooter="True" Width="931px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lgIdCard" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lgProName" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Employee Name & Designation">
                                        <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lnkFiUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>

                                        <ItemTemplate>
                                            <asp:Label ID="lgvEmpName" runat="server"
                                                Text='<%#"<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))  %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bank Serial No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lbgvBankSno" runat="server" BackColor="Transparent"
                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankseno")) %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bank AC No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvBACNo" runat="server" BackColor="Transparent"
                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankacno")) %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFBamt" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvAmt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvRemarks" runat="server" BackColor="Transparent" Font-Size="11px"
                                                BorderColor="#660033" BorderWidth="0px"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewHolidays" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvEmpHoliday" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvEmpHoliday_PageIndexChanging" ShowFooter="True"
                                        Width="474px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="ChkAllEmp" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="ChkAllEmp_CheckedChanged" Text="ALL " Width="50px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkHoliday" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hstatus"))=="True" %>'
                                                        Width="50px" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectionholiday" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="180px" Font-Bold="True"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                <asp:LinkButton ID="lUpdateHoliday" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" OnClick="lUpdateHoliday_Click"
                                                    Style="text-decaration: none;">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        </asp:View>
                        <asp:View ID="ViewMobileBill" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3 col-sm-3 col-lg-3">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkcopy" runat="server" CssClass="checkbox" Text="Copy From Prev. Month" AutoPostBack="true" OnCheckedChanged="chkcopy_CheckedChanged" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2 col-lg-2">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CheckNew" runat="server" Checked="true" CssClass="checkbox" Text="Mobile No Serial Wise" />
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2 col-lg-2">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CheckSum" runat="server" Checked="false" CssClass="checkbox" Text="Location Summary" />
                                            </div>
                                        </div>
                                        <div class="col-md-9 col-sm-9 col-lg-9">
                                            <asp:Panel ID="pnlCopy" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label6" runat="server" CssClass="label">Month</asp:Label>
                                                            <asp:DropDownList ID="ddlpreyearmon" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 col-sm-1 col-lg-1">
                                                        <div class="form-group" style="margin-top: 20px;">
                                                            <asp:LinkButton ID="lbtnCopy" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnCopy_Click">Copy</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:GridView ID="gvEmpMbill" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpMbill_PageIndexChanging"
                                            ShowFooter="True" Width="498px" OnRowDeleting="gvEmpMbill_RowDeleting">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpId" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchArt" BorderStyle="None" runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this,2, 'gvEmpMbill')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>


                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateMbill" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateMbill_Click">Update</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpDesignation" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>
                                                    <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateMbill" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateMbill_Click">Update</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDepartment" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                            Width="180px" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvSectionmb" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="180px" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile Number">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvphoneno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                            Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Limit">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvMbill" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFMbillamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile Bill">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpayamt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px" Font-Size="11px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFpayamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Access">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvpactamt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFactamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
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
                        <asp:View ID="VieweNCAHSLEAVE" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvEmpELeave" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmpMbill_PageIndexChanging"
                                        ShowFooter="True" Width="572px" OnRowDeleting="gvEmpELeave_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <%--   <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalEnLeave" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalEnLeave_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno0" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbntUpdateEnLeave" runat="server" Visible="false" OnClick="lbntUpdateEnLeave_Click"
                                                    CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpDesig" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbntUpdateEnLeave" runat="server" Visible="false" OnClick="lbntUpdateEnLeave_Click"
                                                        CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <table style="width: 30%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="mLabel4" Font-Size="Smaller" runat="server" Font-Bold="True"
                                                                    Text="Section" Width="40px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="mhlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center" Width="40px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSectionLvEncash" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="80px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGrade" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")) %>'
                                                        Width="50px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvJoinDate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvLineDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="50px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gross Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGrossSalary" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cut Off Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCutOffDate" runat="server" Height="16px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cutoffdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Leave">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEnjLoeave" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "enjleave")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Absent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAbsent" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "absday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weekly Holiday">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWeeklyholiday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "workdayoff")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FL & GH">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvGovtHoliday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "flghday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Work Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTotalWorkDay" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totlwrkday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Physical Present Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvphysiclday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "physiclday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Earn Leave">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvElave" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "eleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payble EL">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvEnCleave" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ecleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="El Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvElAmount" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ecleaveamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                        <asp:View ID="ViewOtherDeduction" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvEmpOtherded" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging=" gvEmpOtherded_PageIndexChanging"
                                        ShowFooter="True" Width="685px" OnRowDeleting="gvEmpOtherded_RowDeleting" PagerSettings-Mode="NumericFirstLast">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>">
                                                <ControlStyle ForeColor="Red" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSection" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Card #">
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalOtherDed" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalOtherDed_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server"
                                                        Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--<asp:LinkButton ID="lbntUpdateOtherDed" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateOtherDed_Click"
                                                >Update</asp:LinkButton>--%>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Leave Deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvleaveded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFleaveded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear PF Deduccion">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvarairded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTarrearded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Advanced deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvsaladv" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saladv")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFSaladv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Food Allowance ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtfallow" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fallded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFoterded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Excess Mobile bill  ">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="gvtxtmbill" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mbillded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotermbill" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Special Deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvspecialded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "spcded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFspecialded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FPS Deduction">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlgvotherded" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otherded")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFotherded" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amt.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="loandeduction" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvEmploan" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnPageIndexChanging="gvEmploan_PageIndexChanging"
                                        ShowFooter="True" Width="732px" Style="margin-right: 0px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionlondded" runat="server" Font-Bold="true" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card #">
                                                <%--  <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalLoan" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalLoan_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno1" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName1" runat="server" Height="16px"
                                                        Text='<%#"<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig"))%>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbntUpdateLoan" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntUpdateLoan_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company Loan">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcloan" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cloan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PF Loan">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvpfloan" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfloan")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amt.">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFLToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvltoamt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                        <asp:View ID="Viewarersalary" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvarrear" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvarrear_PageIndexChanging" ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnRowDeleting="gvarrear_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>">
                                                <ControlStyle ForeColor="Red" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpId" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <table>

                                                        <tr>
                                                            <th class="">Section                                                              
                                                            </th>
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <%--<FooterTemplate>
                                                <asp:LinkButton ID="lbtnCalArrear" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="Black" OnClick="lbtnCalArrear_Click"
                                                    Style="text-decaration: none;">Calculation</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionarrear" runat="server" Font-Bold="true" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Card_no">
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalArrear" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalArrear_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name & Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <%--  <FooterTemplate>
                                                <asp:LinkButton ID="lbntUpdateArrear" runat="server" OnClick="lbntUpdateArrear_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear-Salary">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtarrear" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFarrearamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear PF.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPFAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvPFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Arrear Income Tax">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtitaxAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itax")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFitaxAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPFTotal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tapfamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvAPFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemrks" runat="server"
                                                        BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PF" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPercent" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="11px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="ViewOtherEarn" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="gvothearn" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvothearn_PageIndexChanging"
                                        OnRowDeleting="gvothearn_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpIdearn" runat="server" Font-Bold="True" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionearn" runat="server" Font-Bold="true"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalOthEarn" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalOthEarn_Click">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearn" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name &amp; Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpNameearn" runat="server" Height="16px"
                                                        Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                <asp:LinkButton ID="lbntUpdateOthEarn" runat="server" OnClick="lbntUpdateOthEarn_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Food Allow">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvfoodallow" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "foodallow")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFfoodallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TPT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtpallow" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tptallow")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtpallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="KPI">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvkpi" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "kpi")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFkpi" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Per. Bonus">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txgvperbon" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFperbon" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Others">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvotherearn" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othearn")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFotherearn" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtotal" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                        <asp:View ID="ViewAdjustment" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <asp:GridView ID="grvAdjDay" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" Width="572px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="grvAdjDay_PageIndexChanging"
                                        OnRowDeleting="grvAdjDay_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                            <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSectionearn" runat="server" Font-Bold="true"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Card #">
                                                <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalDay" runat="server" OnClick="lbtnTotalDay_Click"
                                                    CssClass="btn btn-primary primaryBtn">Total</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCardnoearn" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name &amp; Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpNameearn" runat="server" Height="16px"
                                                        Text='<%# "<b>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) +"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                <asp:LinkButton ID="btnUpdateDayAdj" runat="server" OnClick="btnUpdateDayAdj_Click"
                                                    CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Late Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDelday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dalday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Approved Days">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnCalCulationSadj" runat="server" OnClick="lbtnCalCulationSadj_Click">Calculation</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtaprday" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Adjust Day">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAdj" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedday")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAdj" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                        <asp:View ID="ViewCarSub" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body">
                                    <div class=" table table-responsive">
                                        <asp:CheckBox ID="chkSubBonustype" runat="server" CssClass="checkbox"
                                            Text="Sub Bonus" />
                                        <asp:GridView ID="gvCarSub" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvCarSub_PageIndexChanging"
                                            ShowFooter="True" Width="685px" OnRowDeleting="gvCarSub_RowDeleting">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNod5cs" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                                <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvEmpIdcs" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                            Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSectioncs" runat="server" Font-Bold="true" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Card #">
                                                    <%--  <FooterTemplate>
                                                    <asp:LinkButton ID="lbtncsAloowance" runat="server" Visible="false" CssClass="btn btn-primary primaryBtn" OnClick="lbtncsAloowance_Click">Total</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCardnocs" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name & Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEmpNamecs" runat="server"
                                                            Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                    <asp:LinkButton ID="lbntcsallow" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbntcsallow_Click">Update</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbglDjoin" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbglgrade" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grdesc")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cost Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgbcostcnt" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "costcnt")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Location" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgbjobdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gross Salary 1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvgsalary1" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gsalary")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFleaveded1" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Gross Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvgsalary" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grsalry1")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFleaveded" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Car Allowance">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvcarallow" runat="server" BackColor="Transparent"
                                                            BorderStyle="Solid" BorderWidth="1" BorderColor="Green" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carallow")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFcarallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Arrear Car Allowance">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvarcallow" runat="server" BackColor="Transparent"
                                                            BorderStyle="Solid" BorderWidth="1" BorderColor="Green" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "arcallow")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFarcallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Subsistance Allowance">
                                                    <ItemTemplate>

                                                        <asp:Label ID="txtgvsuballowance" runat="server" BackColor="Transparent"
                                                            Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suballowance")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                        <asp:Label ID="Label1sub" runat="server" BackColor="Transparent" Visible="false"
                                                            Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suballowance")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFsuballowance" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Subsistance Bonus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvsubbonus" runat="server" BackColor="Transparent"
                                                            Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subbonus")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Arrear Subsistancen Allowance ">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="gvasallow" runat="server" BackColor="Transparent"
                                                            BorderStyle="Solid" BorderWidth="1" BorderColor="Green" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asallow")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFasallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Payment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvtxnetpay" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFonetpay" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
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
                        <asp:View ID="View1" runat="server">

                            <asp:GridView ID="gvMobLst" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False"
                                ShowFooter="True" Width="685px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstSl" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Id Card">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstIdcard" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp. Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstempname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp. Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstDesignation" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesig")) %>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstDepart" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>' Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp. Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstmobileno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobileno")) %>' Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMobLstStatus" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>' Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:GridView>

                        </asp:View>
                        <asp:View ID="ViewEarnLvEnrty" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvEarnLvEntry" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvEarnLvEntry_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpId" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCardno0" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                             <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <th class="">Employee Name</th>      
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export to Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpDesig" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpLine" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvJoinDate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth4" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth5" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth6" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth7" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth8" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth9" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth10" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth11" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth12" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br>Present">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPrsnt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalpresent")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Earn <br>Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "eleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Enjoyed <br>EL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnjELeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "elenjoyed")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payable <br>EL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "payeleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                        <asp:View ID="ViewLvEnCashmnt02" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvLvEnCashmnt02" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvLvEnCashmnt02_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpId" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCardno0" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                             <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <th class="">Employee Name</th>      
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export to Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName0" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpDesig" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpLine" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvJoinDate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jan">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Feb">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mar">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth4" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="May">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth5" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jun">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth6" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Jul">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth7" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Aug">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth8" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sep">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth9" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Oct">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth10" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nov">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth11" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dec">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMonth12" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "mon12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br>Present">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPrsnt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalpresent")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Earn <br>Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "eleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Enjoyed <br>EL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEnjELeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "elenjoyed")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payable <br>EL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "payeleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Encashable <br>EL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "eneleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross <br>Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="One Day <br>Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "onedaysal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Payable <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayEnLeave" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                 <asp:Label ID="lblgvFPayEnLeave" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <!------ modal---------------->
            <div id="myModal" class="modal col-md-8 col-md-offset-2 animated zoomIn" role="dialog">
                <div class="modal-dialog   modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header bg-primary">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="glyphicon glyphicon-hand-right"></span>
                                <asp:Label ID="lbmodalheading" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row-fluid form-horizontal forgotform" id="">
                            </div>
                            <div class="">
                                <asp:GridView ID="mgvbreakdown" runat="server"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="572px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvSlNo8" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="mlgvEmpIdAdj" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day">


                                            <HeaderTemplate>
                                                <table style="width: 30%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="mLabel4" Font-Size="Smaller" runat="server" Font-Bold="True"
                                                                Text="Day" Width="70px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="mhlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="50px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>



                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateday1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Office Out time" Visible="false">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Out time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime1")).ToString("hh:mm tt") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Hour">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvlateday" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ovthour")).ToString() %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="mlgvFDelday" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Minute">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvStdntime" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ovtmin")).ToString() %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="mlgvFovtmin" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                        <div class="modal-footer">

                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

