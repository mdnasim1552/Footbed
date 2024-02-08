<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpOverTimeSalary03.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_89_Pay.EmpOverTimeSalary03" %>


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

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" id="divLine" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblline" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Width="68">Date</asp:Label>
                                <div class="form-inline">
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm small" Width="45%" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                    <asp:Label ID="lbltodate" runat="server" CssClass=" label">To</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small" Width="45%" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divPayType" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lbltAtype2" runat="server" CssClass="label">Payment Type</asp:Label>
                                <asp:RadioButtonList ID="rbtPaymentType" runat="server" AutoPostBack="True"
                                    CssClass="rbtnList1 chkBoxControl margin5px"
                                    Font-Size="10px" ForeColor="Black" Width="25%"
                                    RepeatColumns="6" RepeatDirection="Horizontal" TabIndex="23">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Bank</asp:ListItem>
                                    <asp:ListItem>All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divEmpStatus" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblEmpStatus" runat="server" CssClass="label">Emp. Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Resign</asp:ListItem>
                                    <asp:ListItem Value="2">Hold</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-half col-sm-half col-lg-half ml-2">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label ">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                    Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divChkEmp" runat="server" visible="false" style="margin-top: 20px;">
                            <label id="chkbod" runat="server" class="switch" title="Check for Add Emp.">
                                <asp:CheckBox ID="chkAddEmp" runat="server" ClientIDMode="Static" OnCheckedChanged="chkAddEmp_CheckedChanged" AutoPostBack="true" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label ID="lblAddEmp" runat="server" Text="Add Employee" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                        </div>
                    </div>
                    <div class="row mb-2" id="divAddEmp" runat="server" visible="false">
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <asp:Label ID="lblEmp" runat="server" CssClass="label">Employee</asp:Label>
                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                        </div>
                        <br />
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <asp:LinkButton ID="lnkbtnAddEmp" runat="server" Style="margin-top: 20px;" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnAddEmp_Click">Add</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvovsal02" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvovsal02_OnRowDataBound"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" Width="420px" PageSize="50" OnPageIndexChanging="gvovsal02_PageIndexChanging">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNos2" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchArt" BorderStyle="None" runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this,1, 'gvovsal02')"></asp:TextBox><br />
                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcardnoos" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="White" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <HeaderTemplate>
                                                    <table style="width: 200px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <label runat="server" backcolor="#000066"
                                                                    bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                                    forecolor="White" style="text-align: center" width="120px">
                                                                    Employee Name</label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export to Excel" Width="30px" 
                                                                    CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    <%--<asp:LinkButton ID="lnkFiUpdateoSalary" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdateoSalary_Click">Update</asp:LinkButton>--%>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcompanyandempos" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))    %>'
                                                        Width="150px">   </asp:Label>

                                                    <asp:Label ID="LblEmpid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))    %>'
                                                        Width="150px">   </asp:Label>
                                                    <asp:Label ID="lblxmlcol1" runat="server" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "xmlcol1")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdesignationos" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDeptnameos" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Section">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsectionos" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Floor Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdfline" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvjoindateos" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Basic </br>Salary">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBasic" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBasicss" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gross </br>Salary">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFGross" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGross" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal1")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EOT(HR)">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothour" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>

                                                    <asp:LinkButton OnClick="lblgvdeptandemployeeemp_Click" ID="lblgvdeptandemployeeemp" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:LinkButton>

                                                    <asp:Label ID="lgvothour" runat="server" Style="text-align: right" Visible="false"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ohour")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="OT Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvotoffrate" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EOT Amount">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFotamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvotamount" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otamount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payable Amount" Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnetamtos" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnetamtos" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                        </EmptyDataTemplate>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="grvIndOvrSum" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" Width="420px" PageSize="50" OnPageIndexChanging="gvovsal02_PageIndexChanging">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNos2IndOvrSum" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchArtIndOvrSum" BorderStyle="None" runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this,1, 'gvovsal02')"></asp:TextBox><br />
                                            </HeaderTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcardnoosIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="White" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">

                                            <FooterTemplate>
                                                <%--<asp:LinkButton ID="lnkFiUpdateoSalary" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFiUpdateoSalary_Click">Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcompanyandemposIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))    %>'
                                                    Width="150px">   </asp:Label>

                                                <asp:Label ID="LblEmpidIndOvrSum" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))    %>'
                                                    Width="150px">   </asp:Label>
                                                <asp:Label ID="lblxmlcol1" runat="server" Visible="False"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "xmlcol1")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationosIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDeptnameosIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Section">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsectionosIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Floor Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdflineIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindateosIndOvrSum" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic </br>Salary" Visible="false">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBasicIndOvrSum" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBasicssIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross </br>Salary" Visible="false">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFGrossIndOvrSum" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGrossIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total OT">
                                            <ItemTemplate>

                                                <asp:LinkButton OnClick="lblgvdeptandemployeeemp_Click" ID="lblgvdeptandemployeeemp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otoffday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:LinkButton>

                                                <asp:Label ID="lgvotofdayIndOvrSum" runat="server" Style="text-align: right" Visible="false"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otoffday")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="OT Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvotoffrateIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otoffrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>







                                        <asp:TemplateField HeaderText="EOT Amount">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFotofdayamtIndOvrSum" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvotoffamountIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "otoffamount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Pressent Days">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFnetamtosIndOvrSum" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetamtosIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presnday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetamtosIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlabsnt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Holiday">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetamtosIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "offdaycnt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetamtosIndOvrSum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlleave")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>




                                    </Columns>

                                    <EmptyDataTemplate>
                                        <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <%--<PagerSettings PageButtonCount="50" />--%>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>


                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:GridView ID="gvdailyOTSum" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" Width="420px" PageSize="50">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNos2IndOvrSum" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Day">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDay" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "daysname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Staff (Min)">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvStaffC" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvStaffEx" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvStaffT" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstaffTotal" runat="server" Style="text-align: right"
                                                    Text='<%# "C-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcomplaincehr")).ToString("#,##0;") %>'
                                                    Width="80px"></asp:Label><br />
                                                <asp:Label ID="Label10" runat="server" Style="text-align: right"
                                                    Text='<%# "E-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stextrahr")).ToString("#,##0;") %>'
                                                    Width="80px"></asp:Label><br />
                                                <asp:Label ID="Label11" runat="server" Style="text-align: right" ForeColor="Blue"
                                                    Text='<%# "T-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stovthr")).ToString("#,##0;") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Worker (Min)">

                                            <FooterTemplate>
                                                <asp:Label ID="lgvWorkerC" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvWorkerE" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvWorkertotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Style="text-align: right"
                                                    Text='<%# "C-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wkcomplaincehr")).ToString("#,##0;") %>'
                                                    Width="100px"></asp:Label><br />
                                                <asp:Label ID="Label1" runat="server" Style="text-align: right"
                                                    Text='<%# "E-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wkextrahr")).ToString("#,##0;") %>'
                                                    Width="100px"></asp:Label><br />
                                                <asp:Label ID="lgvWrkTtal" runat="server" Style="text-align: right" ForeColor="Blue"
                                                    Text='<%# "T-"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wkovthr")).ToString("#,##0;") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvTTC" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvTTE" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                <asp:Label ID="lgvftotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttcomplaincehr")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="80px"></asp:Label><br />
                                                <asp:Label ID="Label6" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttextrahr")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="80px"></asp:Label><br />
                                                <asp:Label ID="lgTotalOT" runat="server" Style="text-align: right" ForeColor="Blue"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttovthr")).ToString("#,##0.00;(#,##0.00);") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                    </Columns>

                                    <EmptyDataTemplate>
                                        <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>


                            </asp:View>
                            <asp:View ID="View3" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvMonOTSumSecWise" runat="server" AllowPaging="True" OnPageIndexChanging="gvMonOTSumSecWise_PageIndexChanging"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNos2IndOvrSum" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day">
                                                <HeaderTemplate>
                                                    <table style="width: 10%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                    ForeColor="White" Style="text-align: center" Width="120px" ToolTip="Export To Excel">Day</asp:HyperLink>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDay" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "daysname")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S1">                                                 
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m1"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS1" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m2"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS2" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m3"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS3" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m4"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS4" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m5"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS5" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m6"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS6" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m7"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS7" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S8">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m8"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS8" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S9">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m9"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS9" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m10"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS10" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S11">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m11"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS11" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m12"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS12" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S13">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m13"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS13" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S14">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m14"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS14" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S15">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m15"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS15" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S16">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m16"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS16" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S17">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m17"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS17" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S18">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m18"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS18" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S19">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m19"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS19" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m20"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS20" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S21">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m21"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS21" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S22">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m22"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS22" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S23">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m23"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS23" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S24">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m24"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS24" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S25">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m25"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS25" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S26">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m26"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS26" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S27">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m27"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS27" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S28">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m28"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS28" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S29">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM29" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m29"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS9" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m30"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS30" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S31">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m31"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS31" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S32">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM32" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m32"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS32" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM32" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS32" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S33">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM33" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m33"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS33" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM33" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS33" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S34">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM34" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m34"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS34" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM34" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS34" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S35">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM35" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m35"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS35" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM35" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS35" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S36">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM36" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m36"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS36" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM36" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS36" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S37">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM37" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m37"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS37" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM37" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS37" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S38">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM38" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m38"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS38" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM38" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS38" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S39">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM39" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m39"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS39" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM39" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS39" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM40" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m40"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS40" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM40" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS40" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S41">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM41" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m41"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS41" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s41")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM41" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS41" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S42">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM42" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m42"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS42" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s42")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM42" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS42" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S43">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM43" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m43"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS3" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s43")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM43" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS43" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S44">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM44" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m44"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS44" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s44")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM44" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS44" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S45">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM45" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m45"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS45" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s45")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM45" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS45" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="S46">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvM46" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "m46"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvS46" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s46")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFM46" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFS46" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total OT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotalMan" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalmanpower"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvTotalOT" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalovt")).ToString("#,##0;") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                     <asp:Label ID="lgvTotalMan" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvTotalFOT" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                        </EmptyDataTemplate>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
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
                                                <asp:Label ID="mlblgvintime" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Office Out time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Out time">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblouttime1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actul Hour">

                                            <ItemTemplate>
                                                <asp:Label ID="mlblgvovthour" runat="server" BackColor="Transparent"
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
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
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
