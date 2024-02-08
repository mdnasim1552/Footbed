<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMonthlyProFund.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_90_PF.RptMonthlyProFund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
            $(function () {
                $('[id*=ddlWstation]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            <%--var gvpf = $('#<%=this.gvProFund.ClientID %>');
            gvpf.Scrollable();--%>
            $(function () {
                $('[id*=ddlWstation]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });
            $('.chzn-select').chosen({ search_contains: true });
        }

        function openUserModal() {
            $('#modalAddUser').modal('toggle');
        }
        function closeUserModal() {
            $('#modalAddUser').modal('hide');
        }

    </script>
    
    <style>
        .table-striped > tbody > tr td {
            height: 26px;
        }

        .table-striped > tbody > tr > th {
            font-weight: normal;
        }

        .mt-20 {
            margin-top: 20px !important;
        }


         .dropdown-toggle {
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: flex;
            vertical-align: middle;
        }

        .multiselect {
            width: 100% !important;
            border: 1px solid;
            height: 29px;
            line-height: 17px;
            border-color: #cfd1d4;
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }
        .caret {
            display: none !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px;
            line-height: 30px;
        }

        .chzn-container-single .chzn-drop {
            width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

    </style>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                 <asp:LinkButton ID="lnkEmptype" runat="server" OnClick="ddlWstation_SelectedIndexChanged"><i class="fa fa-search"></i></asp:LinkButton>
                                 <asp:ListBox ID="ddlWstation" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;"></asp:ListBox>
                               <%-- <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>--%>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDivision" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblMonth" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblEmpSearch" runat="server" Text="Card"></asp:Label>
                                <asp:TextBox ID="txtEmpSearch" runat="server" CssClass="form-control form-control-sm" placeholder="Card: 10001" AutoPostBack="true" OnTextChanged="txtEmpSearch_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPageSize" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>                                   
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <asp:MultiView ID="Multiview1" runat="server">
                        <asp:View ID="viewMonthlyPF" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMonthlyPF" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvMonthlyPF_PageIndexChanging"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Month ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmonthidid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Ref No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrefnoid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Comcod" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcomcodid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <table style="width: 170px">
                                                    <tr>
                                                        <td class="style225">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px"
                                                                Text=" Employee Name " Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="style237">
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="80px" ToolTip="Export to Excel">Export Exel</asp:HyperLink>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Grade" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrade" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grade")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnam" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joinindat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt1" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Basic Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamt2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PF">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lgvamt3" runat="server" Style="text-align: right" OnClick="lnkEditUser_Click"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0);0") %>'
                                                    Width="60px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="viewMonthlyPFPay" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvProFund" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvProFund_PageIndexChanging"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lgsection" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptment")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcardno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <table style="width: 170px">
                                                    <tr>
                                                        <td class="style225">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Height="16px"
                                                                Text=" Employee Name " Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="style237">
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ToolTip="Export To Excel"
                                                                ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDesignation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Joining Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnam" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joinindat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Service <br>Length">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvservlength" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "servlength")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening<br> PF">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvopnpfamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PF">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpfamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pfund")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt2" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company <br>Con. PF">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcompanypf" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comconpfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt3" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payable <br>PF Amout">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnetpf" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payablepfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFamt4" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>


              <div id="modalAddUser" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><span class="fa fa-table"></span>&nbsp;Employee Information</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" aria-label="Close"><i class="fas fa-times-circle"></i></button>

                        </div>
                        <div class="modal-body form-horizontal">
                             <asp:Label ID="lblcomcodId" runat="server" Visible="false"></asp:Label>
                             <asp:Label ID="lblEmployeeId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblMonthId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblRefno" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblRowIndex" runat="server" Visible="false"></asp:Label>
                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Employee ID </label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtEmpIdcard" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Name</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmShortName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>Designation</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmDesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>              
                             <div class="row mb-1">
                                <div class="col-md-3">
                                    <label>PF</label>
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtmPF" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>       
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnSaveUser" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="closeUserModal();" OnClick="lbtnSaveUser_Click"><i class="fas fa-badge-check"></i>&nbsp;Update </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

