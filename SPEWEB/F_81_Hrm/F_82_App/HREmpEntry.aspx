<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpEntry.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.HREmpEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            Visibility();


        });
        function pageLoaded() {
            $(function () {


                $('[id*=DropCheck1]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true
                })


            });

            $('.chzn-select').chosen({ search_contains: true });
        }

        function showImagePreview(input) {
            document.getElementById("<%=EmpImg.ClientID %>").style.display = "block";
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $("#<%= EmpImg.ClientID %>").attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }

        function showImagePreview1(input) {
            //document.getElementById("EmpSign").style.display = "block";
            document.getElementById("<%=EmpSign.ClientID %>").style.display = "block";
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $("#<%= EmpSign.ClientID %>").attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }


        function Visibility() {
            var comcod = '<%= this.GetCompCode()%>';
            $('#<%=this.lbltEduQua.ClientID%>').css({ "display": "none" });
            $('#<%=this.lblEduQua.ClientID%>').css({ "display": "none" });
            $('#<%=this.ddlEduQua.ClientID%>').css({ "display": "none" });
            $('#<%=this.lbltEduQua.ClientID%>').css({ "display": "none" });
            $('#<%=this.lbltEduPass.ClientID%>').css({ "display": "none" });
            $('#<%=this.txtEduPass.ClientID%>').css({ "display": "none" });
            $('#<%=this.lblholidayrate.ClientID%>').css({ "display": "none" });
            $('#<%=this.rbtholiday.ClientID%>').css({ "display": "none" });
           <%-- $('#<%=this.lbltOverTime.ClientID%>').css({ "display": "none" });
            $('#<%=this.rbtnOverTime.ClientID%>').css({ "display": "none" });
            $('#<%=this.lblfiexedRate.ClientID%>').css({ "display": "none" });
            $('#<%=this.txtfixedRate.ClientID%>').css({ "display": "none" }); --%>
        }

    </script>

    <style>
        .card-header {
            padding: 0.3rem !important;
        }


        .form-group {
            margin-bottom: 0.1rem !important;
        }

        .Multidropdown ul {
            top: -47px !important;
        }

        b.caret {
            display: none !important;
        }

        ul.dropdown-menu {
            min-width: 15rem;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

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
                <div class="card-body" style="min-height: 550px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="Services" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" CssClass="label">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="label">Job Location</asp:Label>
                                        <asp:DropDownList ID="ddlJob" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlJob_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>                                
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmp" runat="server" CssClass="label">Emp.  Name</asp:Label>
                                        <asp:DropDownList ID="ddlPEmpName" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        <!---- for new employee--->
                                        <asp:Label ID="lblnewEmp" runat="server" CssClass="label" Visible="false">New Emp.</asp:Label>
                                        <asp:DropDownList ID="ddlNPEmpName" Visible="false" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>

                                    </div>
                                </div>
                                 <div class="col-md-4 col-sm-4 col-lg-4">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chknewEmp" runat="server" AutoPostBack="True" OnCheckedChanged="chknewEmp_CheckedChanged" TabIndex="13" Text="New" CssClass="checkbox" />
                                        <asp:CheckBox ID="chkEdit" runat="server" AutoPostBack="True" OnCheckedChanged="chkEdit_CheckedChanged" TabIndex="13" Text="Edit Employee" CssClass="checkbox" Visible="false" />

                                        <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                                        <a href="#" class="btn btn-sm btn-warning " onclick="history.go(-1)" title="Previous"><span class="fa fa-backward"></span></a>
                                        <asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn btn-sm btn-success " OnClick="lnkNextbtn_Click" ToolTip="Next"><span class="fa fa-forward"></span></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDeletelink" runat="server" OnClick="lbtnDeletelink_Click" CssClass="btn btn-sm btn-danger" Style="Display:none;"
                                            OnClientClick="return ('Are you sure want to unlink employee')" ToolTip="Unlink Employee"><span class="fa fa-unlink"></span> Unlink</asp:LinkButton>
                                        <a href="../F_82_App/EmpEntryForm" class="btn btn-sm btn-success" title="Add New Employee"><span class=" fa fa-plus" aria-hidden="true">Add Employee</span></a>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6 col-lg-6">
                                    <div class="form-group">

                                    </div>
                                </div>                               
                            </div>
                            <asp:Panel ID="pnlGenInfo" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 320px;">
                                            <header class="card-header">Agreement Information</header>
                                            <!-- .card-body -->
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbltDesignation" runat="server" CssClass="label">Designation</asp:Label>
                                                            <asp:Label ID="lblDesgination" runat="server" Visible="false" ReadOnly="true" CssClass="form-control form-control-sm"></asp:Label>

                                                            <asp:DropDownList ID="ddlDesignation" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="15">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group" style="display: none;">
                                                            <asp:Label ID="lbltEduQua" runat="server" CssClass="label">Last Degree</asp:Label>
                                                            <asp:Label ID="lblEduQua" runat="server" CssClass="text-danger"></asp:Label>

                                                            <asp:DropDownList ID="ddlEduQua" AutoPostBack="true" OnSelectedIndexChanged="ddlEduQua_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" TabIndex="21">
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbltAtype" runat="server" CssClass="label">Agrmt. Type</asp:Label>

                                                            <asp:TextBox ID="lblAtype" runat="server" ReadOnly="true" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlAggrement" OnSelectedIndexChanged="ddlProQua_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="22"></asp:DropDownList>
                                                        </div>

                                                        <div class="form-group" style="display: none;">
                                                            <asp:Label ID="lbltEduPass" runat="server" CssClass="label">YOP</asp:Label>
                                                            <asp:TextBox ID="txtEduPass" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </div>


                                                    </div>

                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblpfstdate" runat="server" CssClass="label">PF Starting Date</asp:Label>
                                                            <asp:TextBox ID="txtPf" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                            <cc1:CalendarExtender ID="txtPf_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtPf"></cc1:CalendarExtender>


                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">

                                                            <asp:Label ID="lblpfenddate" runat="server" CssClass="label">End Date</asp:Label>
                                                            <asp:TextBox ID="txtpfend" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>

                                                            <cc1:CalendarExtender ID="txtpfend_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtpfend"></cc1:CalendarExtender>

                                                        </div>

                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbltOverTime" runat="server" CssClass="label">Over Time</asp:Label>
                                                            <asp:RadioButtonList ID="rbtnOverTime" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="rbtnOverTime_SelectedIndexChanged" RepeatColumns="6"
                                                                RepeatDirection="Horizontal" TabIndex="30">
                                                                <asp:ListItem>Fixed</asp:ListItem>
                                                                <asp:ListItem>Fixed(H)</asp:ListItem>
                                                                <asp:ListItem Selected="True">For.(H)</asp:ListItem>
                                                                <asp:ListItem>Ceilling</asp:ListItem>
                                                            </asp:RadioButtonList>

                                                            <div class="clearfix"></div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="PnlMultiply" runat="server" Visible="false">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbldivided" runat="server" CssClass="label">Divided</asp:Label>
                                                                <asp:TextBox ID="txtdevided" runat="server" ReadOnly="false" CssClass="form-control form-control-sm" Text="208"></asp:TextBox>
                                                            </div>
                                                        </asp:Panel>

                                                        <div class="form-group">
                                                            <asp:Label ID="lblfiexedRate" runat="server" CssClass="label">H. Rate</asp:Label>
                                                            <asp:TextBox ID="txtfixedRate" runat="server" TabIndex="32" CssClass="form-control form-control-sm"></asp:TextBox>

                                                            <asp:Label ID="lblhourlyRate" runat="server" CssClass="label">H.Rate</asp:Label>
                                                            <asp:TextBox ID="txthourlyRate" runat="server" TabIndex="32" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCeilingRate1" runat="server" CssClass="label small">(7PM-10PM)</asp:Label>
                                                            <asp:TextBox ID="txtceilingRate1" runat="server" TabIndex="33" CssClass="form-control form-control-sm"></asp:TextBox>



                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCeilingRate2" runat="server" CssClass="label small">(10:1PM-2AM)</asp:Label>
                                                            <asp:TextBox ID="txtceilingRate2" runat="server" TabIndex="34" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCeilingRate3" runat="server" CssClass="label small">(2:1AM-6PM)</asp:Label>
                                                            <asp:TextBox ID="txtceilingRate3" runat="server" TabIndex="35" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <!-- /.card-body -->
                                        </section>
                                        <!-- /.card -->
                                    </div>

                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 320px;">
                                            <header class="card-header">Office/Lunch/Leave</header>
                                            <!-- .card-body -->
                                            <div class="card-body row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbltOfftime" runat="server" CssClass="label">InTime</asp:Label>
                                                        <asp:Label ID="lbloffintime" runat="server" ReadOnly="true" CssClass="text-danger small"></asp:Label>
                                                        <asp:DropDownList ID="ddlOffintime" AutoPostBack="true" OnSelectedIndexChanged="ddlOffintime_SelectedIndexChanged" runat="server" CssClass="form-control  form-control-sm" TabIndex="16"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="lbltLantime" runat="server" CssClass="label">Launch In</asp:Label>
                                                        <asp:Label ID="lbllanintime" runat="server" ReadOnly="true" CssClass="text-danger small"></asp:Label>
                                                        <asp:DropDownList ID="ddlLanintime" AutoPostBack="true" OnSelectedIndexChanged="ddlLanintime_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" TabIndex="17"></asp:DropDownList>

                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label8" runat="server" CssClass="label">Earn Leave</asp:Label>
                                                        <asp:TextBox ID="txternleave" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label9" runat="server" CssClass="label">Casual Leave</asp:Label>
                                                        <asp:TextBox ID="txtcsleave" runat="server" CssClass="form-control form-control-sm" Text="10"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbltOfftime0" runat="server" CssClass="label">OutTime</asp:Label>
                                                        <asp:Label ID="lbloffouttime" runat="server" ReadOnly="true" CssClass="text-danger small"></asp:Label>
                                                        <asp:DropDownList ID="ddlOffouttime" AutoPostBack="true" OnSelectedIndexChanged="ddlOffouttime_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" TabIndex="17"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="lbltLantime0" runat="server" CssClass="label">Launch Out</asp:Label>
                                                        <asp:Label ID="lbllanouttime" runat="server" ReadOnly="true" CssClass="text-danger small"></asp:Label>
                                                        <asp:DropDownList ID="ddlLanouttime" AutoPostBack="true" OnSelectedIndexChanged="ddlLanouttime_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm" TabIndex="18"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label10" runat="server" CssClass="label">Sick Leave</asp:Label>
                                                        <asp:TextBox ID="txtskleave" runat="server" CssClass="form-control form-control-sm" Text="14"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="lblholidayallowance" runat="server" Visible="False" CssClass="label">H.R Amount</asp:Label>
                                                        <asp:TextBox ID="txtholidayallowance" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <!-- /.card-body -->
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="lblholidayrate" runat="server" CssClass="label">Holiday Rate</asp:Label>

                                                    <asp:RadioButtonList ID="rbtholiday" runat="server"
                                                        CssClass="" RepeatColumns="6"
                                                        RepeatDirection="Horizontal" ForeColor="Black"
                                                        AutoPostBack="True"
                                                        OnSelectedIndexChanged="rbtholiday_SelectedIndexChanged" TabIndex="28">
                                                        <asp:ListItem>N/A</asp:ListItem>
                                                        <asp:ListItem>Scaled Based</asp:ListItem>
                                                        <asp:ListItem>Fixed Allowance</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <!-- /.card-footer -->
                                        </section>
                                        <!-- /.card -->
                                    </div>

                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 320px;">
                                            <header class="card-header">Payment Information</header>
                                            <!-- .card-body -->
                                            <div class="card-body row">
                                                <div class="col-md-6">
                                                    <div class="form-group ">
                                                        <asp:Label ID="lbltAtype2" runat="server" CssClass="label">Payment Type</asp:Label>

                                                        <asp:RadioButtonList ID="rbtPaymentType" runat="server" AutoPostBack="True"
                                                            CssClass=""
                                                            Font-Size="12px" ForeColor="Black"
                                                            OnSelectedIndexChanged="rbtPaymentType_SelectedIndexChanged"
                                                            RepeatColumns="6" RepeatDirection="Horizontal" TabIndex="23">
                                                            <asp:ListItem>Cash</asp:ListItem>
                                                            <asp:ListItem>Bank</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </div>


                                                    <asp:Panel ID="pnlPaymenttypeB" runat="server" Visible="false">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblAcNo1" runat="server" CssClass="label">A/C No 01</asp:Label>
                                                            <asp:TextBox ID="txtAcNo1" runat="server" TabIndex="25" CssClass="form-control form-control-sm"></asp:TextBox>


                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblAcNo2" runat="server" CssClass="label">A/C No 02</asp:Label>
                                                            <asp:TextBox ID="txtAcNo2" runat="server" TabIndex="27" CssClass="form-control form-control-sm"></asp:TextBox>



                                                        </div>
                                                    </asp:Panel>
                                                    <div class="form-group">
                                                        <asp:Label ID="lblhSalary" runat="server" Visible="False" CssClass="label" ToolTip="Salary Mandatory">Salary<span style=color:red;> (*) <span></asp:Label>

                                                        <asp:TextBox ID="txtgrossal" runat="server" Visible="False" BackColor="Yellow" CssClass="form-control form-control-sm" TabIndex="38"></asp:TextBox>
                                                    </div>
                                                    <asp:Panel runat="server" ID="pnlcarsub2" Visible="False">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblcar" runat="server" CssClass="label">Car Allow</asp:Label>
                                                            <asp:TextBox ID="txtcar" runat="server" CssClass="form-control form-control-sm" Text="0.00" BackColor="#f18787"></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>

                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Panel ID="pnlleavRule" runat="server">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCahsamt" runat="server" CssClass="label">Cash</asp:Label>
                                                            <asp:TextBox ID="txtCashAmt" runat="server" TabIndex="32" CssClass="form-control form-control-sm"></asp:TextBox>

                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlPaymenttype" runat="server" Visible="false">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbltBankName1" runat="server" CssClass="label">Bank 01</asp:Label>
                                                            <asp:DropDownList ID="ddlBankName1" runat="server" CssClass="form-control form-control-sm" TabIndex="24"></asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlPaymenttypeA" runat="server" Visible="false">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbltBankName2" runat="server" CssClass="smLbl">Bank 02</asp:Label>
                                                            <asp:DropDownList ID="ddlBankName2" runat="server" CssClass="form-control form-control-sm" TabIndex="26"></asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>

                                                    <asp:Panel runat="server" ID="pnlcarsub" Visible="False">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblgrosssal" runat="server" CssClass="label">Gross Sallary</asp:Label>
                                                            <asp:TextBox ID="txtgsallary" runat="server" CssClass="form-control form-control-sm" Text="0.00" BackColor="#f18787"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblsub" runat="server" CssClass="label">Sub Allow</asp:Label>
                                                            s<asp:TextBox ID="txtSuballow" runat="server" CssClass="form-control form-control-sm" Text="0.00" BackColor="#f18787"></asp:TextBox>
                                                        </div>

                                                    </asp:Panel>
                                                </div>

                                            </div>

                                        </section>
                                    </div>
                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 320px;">
                                            <header class="card-header">
                                                Image/Signature Upload                               
                                            </header>
                                            <!-- .card-body -->
                                            <div class="card-body row">
                                                <asp:Panel ID="pnlImage" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="row" style="display: none;">
                                                            <div class="col-md-4">
                                                                <label>Url</label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtImgUrl"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label11" runat="server" CssClass="label">Employee Image</asp:Label>
                                                                <div class="btn btn-secondary btn-sm fileinput-button">
                                                                    <i class="fa fa-plus fa-fw"></i>
                                                                    <span>Add files...</span>
                                                                    <!-- The file input field used as target for the file upload widget -->
                                                                    <asp:FileUpload ID="imgFileUpload" ToolTip="Employee Image" runat="server" Height="26px" AllowMultiple="true" onchange="showImagePreview(this)" />
                                                                </div>
                                                                <%--<img id="EmpImg" runat="server" alt="Preview Image" height="100" width="150" style="display: none;" />--%>
                                                                <asp:Image ID="EmpImg" runat="server" AlternateText="Preview Image" Height="100" Width="150" />
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label12" CssClass=" smLbl_to" runat="server">Employee Signature</asp:Label>
                                                                <div class="btn btn-secondary btn-sm fileinput-button">
                                                                    <i class="fa fa-plus fa-fw"></i>
                                                                    <span>Add files...</span>
                                                                    <asp:FileUpload ID="imgSigFileUpload" runat="server" Height="26px" AllowMultiple="true" onchange="showImagePreview1(this)" ToolTip="Employee Signature" />
                                                                </div>
                                                                <%--<img id="EmpSign" runat="server" alt="Preview Sign" height="100" width="130" style="display: none;" />--%>
                                                                <asp:Image ID="EmpSign" runat="server" AlternateText="Preview Sign" Height="100" Width="130" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <!-- /.card-body -->
                                            <!-- .card-footer -->
                                            <footer class="card-footer">
                                                <a href="<%= ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid=")+this.ddlPEmpName.SelectedValue.ToString() %>" class="card-footer-item card-footer-item-bordered">Basic Info </a>
                                                <a href="<%= ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave?Type=LeaveRule") %>" class="card-footer-item card-footer-item-bordered">Leave Rule</a>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="card-footer-item card-footer-item-bordered" OnClick="lbtnDelete_Click"><span class="fa fa-image"></span> Remove</asp:LinkButton>


                                            </footer>
                                            <!-- /.card-footer -->
                                        </section>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 400px;">
                                            <header class="card-header">Salary Details Information</header>
                                            <!-- .card-body -->
                                            <div class="card-body row">
                                                <asp:RadioButtonList ID="rbtGross" runat="server" Font-Bold="True" CssClass="rbtnList1 chkBoxControl margin5px flotLeft"
                                                    Font-Size="14px" Height="14px" RepeatColumns="6" RepeatDirection="Horizontal"
                                                    Visible="False" TabIndex="37" Width="60%">
                                                    <asp:ListItem>Gross1</asp:ListItem>
                                                    <asp:ListItem>Gross2</asp:ListItem>
                                                    <asp:ListItem>Gross3</asp:ListItem>
                                                    <asp:ListItem>Basic</asp:ListItem>
                                                    <asp:ListItem>Edison</asp:ListItem>
                                                    <asp:ListItem>Basic(FB)</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:GridView ID="gvSalAdd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo1SalAdd" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodesaladd" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                    Width="35px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcResDesc2" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvgperadd" runat="server" BackColor="Transparent"
                                                                    Height="20px" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                                    Width="35px" BorderStyle="None"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgtype" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvSaladd" runat="server" BackColor="Transparent"
                                                                    Height="20px" BorderStyle="None" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gval")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFSalAdd" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:GridView>
                                            </div>

                                            <!-- /.card-body -->
                                            <!-- .card-footer -->
                                            <footer class="card-footer">
                                                <asp:LinkButton ID="lbtnCalculation" runat="server" CssClass="card-footer-item card-footer-item-bordered"
                                                    OnClick="lbtnCalculation_Click">Distribute</asp:LinkButton>
                                                <asp:LinkButton ID="lbtnTSalAdd" runat="server" CssClass="card-footer-item card-footer-item-bordered" OnClick="lbtnTSalAdd_Click">Total</asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnFinalSWUpdate" runat="server" CssClass="card-footer-item card-footer-item-bordered"
                                                    OnClick="lnkbtnFinalSWUpdate_Click"
                                                    Visible="False" TabIndex="39">Update</asp:LinkButton>
                                            </footer>
                                            <!-- /.card-footer -->
                                        </section>
                                        <!-- /.card -->
                                    </div>
                                    <div class="col-lg-3">
                                        <!-- .card -->
                                        <section class="card" style="height: 400px;">
                                            <header class="card-header">Deduction Details Information</header>
                                            <div class="card-body row">
                                                <asp:GridView ID="gvSalSub" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodesalsub" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcResDesc3" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgtype0" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvgpersub" runat="server" BackColor="Transparent"
                                                                    Height="20px" BorderStyle="None" Font-Size="11px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00; (#,##0.00); ") %>' Width="35px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvSalSub" runat="server" BackColor="Transparent"
                                                                    Height="20px" BorderStyle="None"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gval")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFSalSub" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                </asp:GridView>
                                            </div>
                                            <footer class="card-footer">
                                                <asp:Label ID="lbltxtTotalSal" runat="server" CssClass="card-footer-item card-footer-item-bordered small text-danger" Visible="false">Net Salary</asp:Label>
                                                <asp:Label ID="lbltotalsal" runat="server" CssClass="card-footer-item card-footer-item-bordered"></asp:Label>
                                                <asp:LinkButton ID="lbtnTSalSub" CssClass="card-footer-item card-footer-item-bordered" runat="server" OnClick="lbtnTSalSub_Click">Recalculate</asp:LinkButton>
                                            </footer>
                                        </section>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row">
                                <div class="col-md-6 ">

                                    <asp:Panel ID="TeamSetup" runat="server" Visible="false">

                                        <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary primaryBtn" Style="position: absolute; left: 25px;" OnClick="btnAdd_Click">Add</asp:LinkButton>


                                        <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="450px" OnRowDeleting="gvLocation_RowDeleting">
                                            <RowStyle />
                                            <Columns>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Circle">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlcircle" runat="server" CssClass="form-control chzn-select" Width="100">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Region">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlregion" runat="server" CssClass="form-control chzn-select" Width="100">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Area">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlarea" runat="server" CssClass="form-control chzn-select" Width="100">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="territory">
                                                    <FooterTemplate>
                                                        <%--<asp:LinkButton ID="lUpdateLocation" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdateLocation_Click">Update</asp:LinkButton>--%>
                                                    </FooterTemplate>

                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlterritory" runat="server" CssClass="form-control chzn-select" Width="100">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Catagory">


                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlposeccode" runat="server" CssClass="form-control chzn-select" Width="100">
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


                                    </asp:Panel>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblhAllowAdd" runat="server" Font-Bold="true" Text="Addition" Visible="False"></asp:Label>

                                        <asp:GridView ID="gvAllowAdd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
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
                                                        <asp:Label ID="lgcResDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTAllowAdd" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnTAllowAdd_Click"
                                                            Style="text-decaration: none;">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUnit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvtype" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvAllowAdd" runat="server" BackColor="Transparent" Height="20px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px" BorderStyle="None"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAllowAdd" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-5 pull-right">
                                        <asp:Label ID="lblhAllowDed" runat="server" Font-Bold="true" Text="Deduction" Visible="False"></asp:Label>

                                        <asp:GridView ID="gvAllowSub" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="400px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode2" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc4" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnTAllowSub" runat="server" Font-Bold="True"
                                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnTAllowSub_Click"
                                                            Style="text-decaration: none;">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvUnit0" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvtype2" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtype")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvAllowSub" runat="server" BackColor="Transparent" Height="20px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px" BorderStyle="None"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAllowSub" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </asp:View>
                        <asp:View ID="EmpOfftimeSetup" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation1" runat="server" OnSelectedIndexChanged="ddlWstation1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="label">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision1" runat="server" OnSelectedIndexChanged="ddlDivision1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" CssClass="label">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept1" runat="server" OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" CssClass="label">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection1" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblJobLoc" runat="server" CssClass="label">Job Location</asp:Label>
                                        <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlJobLocation_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click" Style="margin-top: 20px;"
                                            TabIndex="47">Ok</asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <br />
                                <asp:Label ID="Label1" runat="server" CssClass="alert alert-info" Text="Office Time Setup"></asp:Label>
                                <div class="clearfix">
                                    <br />
                                </div>
                            </div>
                            <asp:Panel ID="pnlShift" runat="server" Visible="false">
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label17" runat="server" CssClass="label">Shift Name</asp:Label>

                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" TabIndex="2">
                                            </asp:DropDownList>

                                            <asp:Label ID="lblsinTime" runat="server"></asp:Label>
                                            <asp:Label ID="lblsOutTime" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlOfftime" runat="server" Visible="False">
                                <div class="row">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Visible="false">Date</asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm" Visible="false"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lbltodate" runat="server" CssClass="label" Visible="false">To</asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        </div>



                                    </div>


                                </div>

                                <div class="row" style="min-height: 350px;">

                                    <div class="col-md-2  ">
                                        <div class="form-group">
                                            <asp:Label ID="lblempnameoff" runat="server" CssClass="label">Employee Search</asp:Label>

                                            <div class="input-group input-group-alt">
                                                <asp:TextBox ID="txtsrchempoff" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <div class="input-group-append">


                                                    <asp:LinkButton ID="lbtnsrchEmployeeoff" runat="server" CssClass="input-group-text" OnClick="lbtnsrchEmployeeoff_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Select Employee</asp:Label>
                                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; height: 30px;">
                                                <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>


                                            </div>
                                        </div>
                                    </div>




                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:HiddenField ID="Hiddshiftid" runat="server" />
                                            <asp:HiddenField ID="Hiddabstime" runat="server" />
                                            <asp:HiddenField ID="Hiddlatemarg" runat="server" />
                                            <asp:HiddenField ID="hiddmacstarttime" runat="server" />

                                            <asp:Label ID="lbltOfftime1" runat="server" CssClass="label">Office InTime</asp:Label>
                                            <asp:DropDownList ID="ddlOffintimedw" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="lbltOfftime2" runat="server" CssClass="label">Office OutTime</asp:Label>
                                            <asp:DropDownList ID="ddlOffouttimedw" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkbtnUpdateOfftime" runat="server" CssClass="btn btn-danger btn-sm" Style="margin-top: 20px;"
                                                OnClick="lnkbtnUpdateOfftime_Click"
                                                TabIndex="52">Update</asp:LinkButton>
                                        </div>
                                    </div>






                                    <div class="form-group" hidden="hidden">
                                        <div class="col-md-3">
                                            <asp:Label ID="lbltLantime1" runat="server" CssClass="lblTxt lblName">Launch InTime</asp:Label>
                                            <asp:DropDownList ID="ddlLanintimedw" runat="server" CssClass=" ddlistPull inputTxt" Width="85px" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3" hidden="hidden">
                                            <asp:Label ID="lbltLantime2" runat="server" CssClass="lblTxt lblName">Launch OutTime</asp:Label>
                                            <asp:DropDownList ID="ddlLanouttimedw" runat="server" CssClass=" ddlistPull inputTxt" Width="85px" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>



                                    </div>

                                </div>

                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnFinalSWUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

