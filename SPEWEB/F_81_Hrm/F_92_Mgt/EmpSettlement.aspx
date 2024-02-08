<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpSettlement.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.EmpSettlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .table td, .table th {
            padding: 0.10rem !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        };

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
            <div class="card card-fluid" style="min-height: 600px;">
                <div class="card-body pt-1">
                    <div class="row pt-0 pb-0">
                        <div class="col-sm-1 col-md-1 col-lg-1" id="divRefNo" runat="server" visible="false">
                            <div class="form-group mb-2" style="margin-top: 20px;">
                                <asp:TextBox ID="lblEmpSettStatus" runat="server" CssClass="form-control form-control-sm" ForeColor="White" BackColor="Red">Edited</asp:TextBox>
                                <asp:Label ID="txtrefno" runat="server" CssClass="label" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group mb-2">
                                <asp:Label ID="Label6" runat="server" CssClass=" label"> Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group mb-2">
                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee List</asp:Label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group mb-2" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group mb-2" style="margin-top: 20px;">
                                <asp:RadioButtonList ID="rbtnstatement" runat="server" AutoPostBack="True"
                                    CssClass="checkbox"
                                    OnSelectedIndexChanged="rbtnstatement_OnSelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">English</asp:ListItem>
                                    <asp:ListItem>বাংলা</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row pt-0">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="EmpSettGeneral" runat="server">
                                <div class="form-horizontal">
                                    <div class="col-md-4" runat="server" id="engst" visible="False">
                                        <table class="table table-bordered table-responsive table-condensed table-hover">
                                            <tr class="bg-success">
                                                <td>Name</td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Designation</td>
                                                <td>
                                                    <asp:Label ID="lbldesig" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>Id Card No</td>
                                                <td>
                                                    <asp:Label ID="lblidcard" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Section/Department</td>
                                                <td>
                                                    <asp:Label ID="lblsection" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>Job Seperation Type</td>
                                                <td>
                                                    <asp:Label ID="lblseptype" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Joining Date</td>
                                                <td>
                                                    <asp:Label ID="lbljoin" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>Seperation Date</td>
                                                <td>
                                                    <asp:Label ID="lblsep" runat="server"></asp:Label>
                                                </td>
                                                <tr>
                                                    <td>Service Length</td>
                                                    <td>
                                                        <asp:Label ID="lblservlen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="col-md-4" runat="server" id="bngst" visible="false">
                                        <table class="table table-bordered table-responsive table-condensed table-hover">
                                            <tr>
                                                <td>তারিখ</td>
                                                <td>
                                                    <asp:Label ID="lbldate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>নাম</td>
                                                <td>
                                                    <asp:Label ID="lblnam" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>পদবী</td>
                                                <td>
                                                    <asp:Label ID="lbldesg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>আই ডি নং</td>
                                                <td>
                                                    <asp:Label ID="lblid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>সেকশন/বিভাগ</td>
                                                <td>
                                                    <asp:Label ID="lblsec" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>চাকুরী পৃথকীকরনের ধরন </td>
                                                <td>
                                                    <asp:Label ID="lbljobtype" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>চাকুরীতে যোগদানের তারিখ</td>
                                                <td>
                                                    <asp:Label ID="lbljdate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bg-success">
                                                <td>পৃথকীকরনের তারিখ</td>
                                                <td>
                                                    <asp:Label ID="lblsepdate" runat="server"></asp:Label>
                                                </td>
                                                <tr>
                                                    <td>চাকুরীর মেয়াদকাল</td>
                                                    <td>
                                                        <asp:Label ID="lbljonlen" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="col-md-8">
                                        <span class="label label-success"><big>Salary Information</big></span>
                                        <asp:GridView ID="gvsettlemntcredit" OnRowDataBound="gvsettlemntcredit_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Information">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                        <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>

                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day/Hour">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtnumofday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>

                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount (Day/Hour)">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtperday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftotal" runat="server">Total Amount</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TtlAmout" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfttlamt" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                        <span class="label label-success "><big>Deduction Information</big></span>
                                        <asp:GridView ID="gvsttlededuct" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduct Information">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                            Width="220px"></asp:Label>
                                                        <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Day/Hour">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtnumofday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>

                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount (Day/Hour)">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtperday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftotal" runat="server">Total Deduction Amount</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TtlAmout" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblfttlamt" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                        <table class="table-striped table-hover table-bordered">
                                            <tr class="bg-danger">
                                                <td class="text-right" style="width: 580px; color: black; font-weight: bolder; font-size: 13px;">Net Payable Amount</td>
                                                <td style="width: 130px" class="text-right">
                                                    <asp:Label ID="NetAmount" runat="server" Font-Bold="true" Style="color: black; font-weight: bolder; font-size: 13px;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </asp:View>
                            <asp:View ID="EmpSettFB" runat="server">
                                <div class="col-lg-3">
                                    <div class="card card-fluid mb-2">
                                        <div class="card-header pt-1 pb-0">Employee Information</div>
                                        <!-- .card-body -->
                                        <div class="card-body pt-0">
                                            <div class="row mb-0 mt-0 pt-0 pb-0">
                                                <div class="col-md-12 emptble text-center" style="padding-top: 0px !important" runat="server" id="settEmpInfo" visible="False">
                                                    <a href="<%= this.ResolveUrl("/F_81_Hrm/F_82_App/RptMyInterface?Type=Empid&empid=")+this.ddlEmpName.SelectedValue.ToString() %>" class="user-avatar user-avatar-xxl my-3" target="_blank">
                                                        <asp:Image ID="EmpImg" runat="server" />
                                                    </a>
                                                    <h3 class="card-title text-truncate">
                                                        <a href="<%= this.ResolveUrl("/F_81_Hrm/F_82_App/RptMyInterface?Type=Empid&empid=")+this.ddlEmpName.SelectedValue.ToString() %>" target="_blank">
                                                            <asp:Label ID="settempname" runat="server"></asp:Label>, 
                                                            <asp:Label ID="settempid" runat="server"></asp:Label></a>
                                                    </h3>
                                                    <br />
                                                    <h6 class="card-subtitle text-muted mb-3">
                                                        <asp:Label ID="settempdesig" runat="server"></asp:Label>
                                                        @  
                                                        <asp:Label ID="settempdept" runat="server"></asp:Label></h6>

                                                    <table class="table-striped table-hover table table-bordered grvContentarea">
                                                        <tr>
                                                            <td>Section</td>
                                                            <td>
                                                                <asp:Label ID="settempsection" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Joining Date</td>
                                                            <td>
                                                                <asp:Label ID="settempjoindate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr class="bg-danger">
                                                            <td>Date of Resign</td>
                                                            <td>
                                                                <asp:Label ID="settempsepdate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Effective date of Resign</td>
                                                            <td>
                                                                <asp:Label ID="settempeffdate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Notice Period</td>
                                                            <td>
                                                                <asp:Label ID="settempnotperiod" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Service Length</td>
                                                            <td>
                                                                <asp:Label ID="settempserlen" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Days of concern month</td>
                                                            <td>
                                                                <asp:Label ID="settempdaycon" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.card-body -->
                                    </div>
                                    <div class="card card-fluid">
                                        <div class="card-header pt-1 pb-0">Salary Information</div>
                                        <div class="card-body pt-2">
                                            <div class="row mb-2 pl-2">
                                                <asp:GridView ID="gvSettWages" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo00" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvwagesinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                                <asp:Label ID="lblgvwageshrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvwagescrinfof" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvwagesamt" class="text-right" runat="server" Width="100px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvwagesfttl" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="" />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="card card-fluid mb-2">
                                        <div class="card-header pt-1 pb-0">BENEFITS & DUES</div>
                                        <div class="card-body pt-2">
                                            <div class="row mb-2 pl-2">
                                                <asp:GridView ID="gvSettEarn" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo01" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvearninfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                                    Width="280px"></asp:Label>
                                                                <asp:Label ID="lblgvearnhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                                    Width="280px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbenduedesc" runat="server" Width="120px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partdesc")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Days">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvearnnumofday" Style="text-align: right" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvearnperday" Style="text-align: right" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvearnperdayf" runat="server">Total Amount</asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvearntotal" runat="server" Style="text-align: right" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvearnfttl" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvearnremarks" Style="text-align: left" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle CssClass="" />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card card-fluid mt-2">
                                        <div class="card-header pt-1 pb-0">DEDUCTION</div>
                                        <div class="card-body pt-2">
                                            <div class="row mb-2 pl-2">
                                                <asp:GridView ID="gvSettDeduct" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo02" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvdedinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                                    Width="280px"></asp:Label>
                                                                <asp:Label ID="lblgvdedhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                                    Width="280px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvdeducdesc" runat="server" Width="120px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partdesc")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Days">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvdednumofday" Style="text-align: right" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvdedperday" Style="text-align: right" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvdedperdayf" runat="server">Total Amount</asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvdedtotal" Style="text-align: right" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvdedfttl" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvdeducremarks" Style="text-align: left" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <FooterStyle CssClass="" />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="gvHeader" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                </asp:GridView>
                                                <table class="table-striped table-hover table-bordered">
                                                    <tr class="bg-success">
                                                        <td class="text-right" style="width: 604px; color: black; font-weight: bolder; font-size: 13px;">Net Payable Amount</td>
                                                        <td style="width: 83px" class="text-right">
                                                            <asp:Label ID="lblnetamt" runat="server" Font-Bold="true" Style="color: black; font-weight: bolder; font-size: 13px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

