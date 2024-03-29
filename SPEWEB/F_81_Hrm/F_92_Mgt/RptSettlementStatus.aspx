﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptSettlementStatus.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.RptSettlementStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function SetTarget(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }
    </script>
    <script type="text/javascript" language="javascript">
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
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblToDate" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateto_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
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
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <asp:GridView ID="gvSettInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnRowDataBound="gvSettInfo_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID Card">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidno" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="80px"></asp:Label>
                                        <asp:Label ID="lblgvempname" runat="server" Height="16px" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Settlement </br>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbilldate" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesignation" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designation")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtgvSdupplier" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='Total'></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptname" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining </br>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjoindat" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seperation</br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvretdat" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payable </br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttlamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFttlamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Font-Bold="true" Width="70px"></asp:Label>

                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Service Length">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvservleng" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "servleng")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>

                                        <asp:Label ID="lgvLink" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvstatus")).ToString() %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ToolTip="Edit And Approve" ID="lnkEdit" Target="_blank" runat="server"><span class="fa fa-check"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="HypRDDoPrint" runat="server" OnClick="HypRDDoPrint_Click" ToolTip="Print Employee Settlement"><span class="fa fa-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

