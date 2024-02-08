<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRGeneralDayAdjustment.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HRGeneralDayAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            try {
                $('.chzn-select').chosen({ search_contains: true });

                var gv = $('#<%=this.gvMonthlyAttn.ClientID%>');
                $.keynavigation(gv);
            }

            catch (e) {

                alert(e);
            }


        }

    </script>
    <style>
        .card-body {
            padding: 5px 5px 0px 5px !important;
        }

        .card {
            margin-bottom: 2px !important;
        }

        .headrcard .form-group {
            margin-bottom: 0.5px !important;
        }

        table tr td {
            margin: 0 !important;
            padding: 0 !important
        }

        .headcardcontent .form-control {
            height: 25px;
        }

        .headcardcontent .form-group {
            margin-bottom: 0.5px !important;
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select  form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDivision" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select  form-control" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJob" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblFrmDate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblToDate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblGenDate" runat="server" CssClass="label">Gen. Date</asp:Label>
                                <asp:TextBox ID="txtGenDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtGenDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtGenDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPStatus" runat="server" CssClass="label">P. Status</asp:Label>
                                <asp:DropDownList ID="ddlPrsntStatus" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3">
                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                    <asp:ListItem Value="1">Present</asp:ListItem>
                                    <asp:ListItem Value="2">Absent</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 20px;">
                            <div class="form-group">
                                 <asp:CheckBox ID="chkJoinDate" runat="server" Text="Join Date" CssClass="checkbox" />
                                <asp:CheckBox ID="ChckResign" runat="server" Text="Resign" />
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click" ToolTip="Show Attendance">Show</asp:LinkButton>                             
                                <asp:HiddenField ID="lblhdncdate" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 25px;">
                            <div class="form-group">  
                                <asp:CheckBox ID="chkAddEmp" runat="server" ClientIDMode="Static" OnCheckedChanged="chkAddEmp_CheckedChanged" AutoPostBack="true" Text="Add Emp." />
                            </div>
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
                <div class="card-body" style="min-height: 450px;">
                    <div class="row mb-5">
                        <div class="col-md-10 table-responsive">
                            <asp:GridView ID="gvMonthlyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                                CssClass="table-striped table-hover table-bordered grvContentarea"
                                AllowPaging="false" Width="460px"
                                Font-Size="10px" OnRowDataBound="gvMonthlyAttn_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="All">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="checkdate" runat="server" Style="color: black;" AutoPostBack="True" OnCheckedChanged="checkdate_CheckedChanged"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkdate"))=="True" %>' />
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkgvDate" runat="server" Style="color: black;" OnClick="lnkgvDate_Click" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy ddd") %>'
                                                Width="100px"></asp:LinkButton>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCard" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DOJ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvJoinDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblshiftname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvoffIntime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("HH:mm") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvoffouttime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("HH:mm") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="none" Style="text-align: center;"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("HH:mm") %>'
                                                Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="L" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="EL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="CL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="SL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="ML"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="WPL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="LFT"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="PL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="HL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="AL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="FH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="SPH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="W" %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out time">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtOutDategv" runat="server" BorderStyle="none" BackColor="Transparent"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="L"
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="EL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="CL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="SL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="ML"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="WPL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="LFT"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="PL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="HL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="AL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="FH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="SPH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="W" %>'></asp:TextBox>
                                            <cc1:CalendarExtender ID="csesfdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtOutDategv"></cc1:CalendarExtender>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="none" Style="text-align: center; display: inline;"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("HH:mm ") %>'
                                                Width="50px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="L" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="EL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="CL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="SL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="ML"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="WPL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="LFT"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="PL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="HL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="AL"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A"  
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="FH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="SPH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="W" %>'></asp:TextBox>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Late">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLateIn" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lateinmin")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="50px" Style="text-align: center;"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlnintime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("HH:mm") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlnouttime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("HH:mm") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLeave" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Absent">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAbsent" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")) %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Holiday">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvholiday" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")) %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="R OT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrtohour" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9403" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9414" ||
                                                             Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9408" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9416" ? true:false  %>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tohour")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtohour" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: center"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="C OT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtwohour" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Visible='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9403" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9414" ||
                                                             Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9408" ||  Convert.ToString(DataBinder.Eval(Container.DataItem, "company")).Substring(0,4)=="9416" ? true:false  %>'
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "twohour")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtwohour" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: center"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Work Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvworkhour" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "achour")).ToString("#,##0.00;(#,##0.00);") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtoworkhour" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: center"></asp:Label>
                                        </FooterTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Posting Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvattnstatus" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attnstatus")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" BorderStyle="none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="80px"></asp:TextBox>
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
                        <div class="col-md-2  headcardcontent">
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">Comments</asp:Label>
                                        <asp:TextBox ID="txtComments" runat="server" CssClass="form-control "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="label">OT H</asp:Label>
                                        <asp:TextBox ID="txtothour" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblOTAdjDate" runat="server" CssClass="label">OT Adj. Date</asp:Label>
                                        <asp:TextBox ID="txtOTAdjDate" runat="server" CssClass="form-control form-control-sm small" OnTextChanged="txtOTAdjDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtOTAdjDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtOTAdjDate"></cc1:CalendarExtender>
                                    </div>
                                </div>   
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12" visible="false" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="label">In Time</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 col-sm-12 col-lg-12" visible="false" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Out Time</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <h6>Attendance Summary</h6>
                                        <asp:GridView ID="gvMonAttnsum" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AllowPaging="false" Font-Size="14px">
                                            <RowStyle />
                                            <Columns>

                                                <asp:TemplateField HeaderText="TP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtotalprsnt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "totalprsnt")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Green" ForeColor="White" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="Green" ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtotalabsnt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "totalabsnt")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Red" ForeColor="White" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="Red" ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtotalleav" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "totalleave")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Blue" ForeColor="White" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="Blue" ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TH">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtotalholiday" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "totalholiday")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Yellow" ForeColor="Black" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="Yellow" ForeColor="Black" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="" />
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
