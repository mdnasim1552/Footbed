<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpMonthlyAtten.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HREmpMonthlyAtten" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            $('.chzn-select').chosen({ search_contains: true });
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
        /*
        .headcardcontent {
            padding: 1px 5px;
        }*/
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
            <div class="card card-fluid headrcard">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select  form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select  form-control" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lbljoblocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJob" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Emp. Name</asp:Label>
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" OnClick="imgbtnEmployee_Click" TabIndex="2" CssClass="label"><i class="fa fa-search"  aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlEmpName" AutoPostBack="True" runat="server" CssClass="form-control chzn-select inputTxt" TabIndex="3" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass=" form-control chzn-select " TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-6 col-lg-4" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click" ToolTip="Show Attendance">Show</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnPrev" runat="server" CssClass="btn btn-sm btn-warning" OnClick="lnkbtnPrev_Click" ToolTip="Previous Employee"><span class="fa fa-backward"></span></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnNext" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkbtnNext_Click" ToolTip="Next Employee"><span class="fa fa-forward"></span></asp:LinkButton>
                                <asp:CheckBox ID="ChckResign" runat="server" OnCheckedChanged="ChckResign_OnCheckedChanged" AutoPostBack="True" Text="Resign" />
                                <asp:CheckBox ID="checkFullMonth" runat="server" OnCheckedChanged="checkFullMonth_CheckedChanged" AutoPostBack="True" Text="F. Mon." />
                                <asp:Label ID="lblJoinDate" runat="server" Text="" ForeColor="#339933" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="lblhdncdate" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row mb-5">
                        <div class="col-md-10">
                            <asp:GridView ID="gvMonthlyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkgvDate" runat="server" Style="color: black;" OnClick="lnkgvDate_Click" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy ddd") %>'
                                                Width="110px"></asp:LinkButton>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblshiftname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftname")) %>'
                                                Width="140px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvoffIntime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("HH:mm") %>'
                                                Width="50px"></asp:Label>
                                             <asp:Label ID="lblgvoffInDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")) %>'
                                                Width="50px" Visible="false"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shift Out">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvoffouttime" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("HH:mm") %>'
                                                Width="50px"></asp:Label>
                                             <asp:Label ID="lblgvoffOutDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")) %>'
                                                Width="50px" Visible="false"></asp:Label>
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
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="ADH" 
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="W" %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out time">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtOutDategv" runat="server" BorderStyle="none" BackColor="Transparent"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("dd-MMM-yyyy") %>'
                                                Width="75px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leav")).Trim())!="L"
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
                                                     &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="ADH" 
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
                                                    &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "hday")).Trim())!="ADH" 
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
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="label">In Time</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Out Time</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txttodate_TextChanged"></asp:TextBox>
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

