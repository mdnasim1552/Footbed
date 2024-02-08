<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpJobCard.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptEmpJobCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

          

            $('[id*=ddlEmpName]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true,

            });

        }

    </script>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label ">Date</asp:Label>
                                <div class="form-inline">

                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm small" Style="width: 45%"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                    <asp:Label ID="lbltodate" runat="server" CssClass="label">To</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small" Style="width: 45%"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="listProject" runat="server" CssClass="chzn-select form-control form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1-half col-sm-1-half col-lg-1-half">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass=" label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged" CssClass="chzn-select form-control form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblEmpName" runat="server" CssClass="label">Employee  Name                                    
                                <asp:LinkButton ID="imgbtnEmpName" runat="server" CssClass="label" OnClick="imgbtnEmpName_Click" ToolTip="Get Employee"><i class="fas fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox ID="ddlEmpName" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-sm-half col-md-half col-lg-half">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-half col-md-half col-lg-half ml-2">
                            <div class="form-group" style="margin-top: 27px;">
                                <asp:CheckBox ID="BtnChckResign" runat="server" AutoPostBack="true" OnCheckedChanged="BtnChckResign_CheckedChanged" CssClass="chkBoxControl" Text="Resign" />
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblOTHours" runat="server" Text="Max OT Hours"></asp:Label>
                                <asp:TextBox ID="txtMaxOTHours" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                    <div class="row">
                        <div class="col-lg-4 col-md-4" runat="server" id="settEmpInfo" visible="False">
                            <!-- .card -->
                            <section class="card">
                                <header class="card-header">Employee Information</header>
                                <!-- .card-body -->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12 emptble text-center" style="padding-top: 0px !important">
                                            <a href="<%= this.ResolveUrl("/F_81_Hrm/F_82_App/RptMyInterface?Type=Empid&empid=")+this.ddlEmpName.SelectedValue.ToString() %>" class="user-avatar user-avatar-xxl my-3">
                                                <asp:Image ID="EmpImg" runat="server" />
                                            </a>
                                            <h3 class="card-title text-truncate">
                                                <a href="<%= this.ResolveUrl("/F_81_Hrm/F_82_App/RptMyInterface?Type=Empid&empid=")+this.ddlEmpName.SelectedValue.ToString() %>">
                                                    <asp:Label ID="lblname" runat="server"></asp:Label>, 
                                                    <asp:Label ID="lblcard" runat="server"></asp:Label></a>
                                            </h3>
                                            <br />
                                            <h6 class="card-subtitle text-muted mb-3">
                                                <asp:Label ID="lbldesg" runat="server"></asp:Label>
                                                @  
                                                <asp:Label ID="lbldpt" runat="server"></asp:Label></h6>

                                            <table class="table-striped table-hover table table-bordered grvContentarea">


                                                <tr>
                                                    <td>In Time</td>
                                                    <td>
                                                        <asp:Label ID="lblIntime" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Out Time</td>
                                                    <td>
                                                        <asp:Label ID="lblout" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="bg-facebook">
                                                    <td>Total Working Day</td>
                                                    <td>
                                                        <asp:Label ID="lblwork" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Late Day</td>
                                                    <td>
                                                        <asp:Label ID="lblLate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Leave Day</td>
                                                    <td>
                                                        <asp:Label ID="lblLeave" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Absent Day</td>
                                                    <td>
                                                        <asp:Label ID="lblAbsent" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Holiday Day</td>
                                                    <td>
                                                        <asp:Label ID="lblHoliday" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="bg-facebook">
                                                    <td>Total OT</td>
                                                    <td>
                                                        <asp:Label ID="lblOvtime" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>Company Name</td>
                                                    <td>
                                                        <asp:Label ID="lblcompname" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                </div>
                                <!-- /.card-body -->
                            </section>
                            <!-- /.card -->
                        </div>
                        <div class="col-md-8 col-lg-8">



                            <asp:Repeater ID="RptMyAttenView" runat="server" OnItemDataBound="RptMyAttenView_ItemDataBound">

                                <HeaderTemplate>
                                    <table class="table-striped table-hover table-bordered grvContentarea " style="width: 100%;">
                                        <tr>
                                            <th>SL. No.</th>
                                            <th>Date</th>
                                            <th>In Time</th>
                                            <th>Out Time</th>
                                            <th>Status</th>
                                            <th style="display: none;">Penalty</th>
                                            <th style="display: none;">Official Hour</th>
                                            <th>Late</th>
                                            <th>E.exit</th>
                                            <th id="thheader" runat="server">OT</th>
                                            <th>Attn. Type</th>
                                            <th>Remarks</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblempdeptid" Visible="False" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")) %>'></asp:Label>
                                            <asp:Label ID="lblacintime" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("dd-MMM-yyyy") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblactualin" runat="server"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="WH" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "leav"))=="L" ||
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="AB" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="FH" ? false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualin")).ToString("hh:mm tt") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblactualout" runat="server"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="WH" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "leav"))=="L" ||
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="AB" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus"))=="FH" ? false:true  %>'
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "actualout")).ToString("hh:mm tt") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatus")).ToString() %>'></asp:Label>

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
                                           
                                            <asp:Label ID="lblOt" runat="server"
                                                 Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")).Substring(0,4)=="9403" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")).Substring(0,4)=="9414" ||
                                                             Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")).Substring(0,4)=="9408" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "empdeptid")).Substring(0,4)=="9416" ? true:false  %>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovtmin")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblAttnType" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"attnstatus")) %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRemarks" runat="server" Width="120px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))  %>'></asp:Label>

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

                        </div>
                    </div>
                </div>
            </div>
   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>

