<%@ Page Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptSkilMatrix.aspx.cs" Inherits="SPEWEB.F_05_ProShip.RptSkilMatrix" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CloseMOdal() {
            $('#myModal').modal('hide');
        }
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Section', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
            $(function () {
                $('[id*=empGradeDropDownList]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });
            $('[id*=empDesignationDropDownList]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
            });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            $("#txtCycleTime").focus(function () {
                alert("21212");
            })
        });


        function definecolor() {
            return "bg-red";
        }
        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");

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
            $(function () {
                $('[id*=empGradeDropDownList]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
                $('[id*=empDesignationDropDownList]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true,
                });
            });
        }

    </script>

    <style>
        .table-striped > tbody > tr td {
            height: 26px;
        }

        .skillmatrix .table-striped > tbody > tr > th {
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

        .VerticalColumn {
            font: bold;
            padding-top: 10px;
            vertical-align: middle;
            writing-mode: vertical-rl;
            -ms-transform: rotate(180deg);
            -moz-transform: rotate(180deg);
            -webkit-transform: rotate(180deg);
            -o-transform: rotate(180deg);
            transform: rotate(180deg);
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=1);
        }

        .skillmatrix .table-bordered th {
            height: 135px !important;
        }

        .skilltable td {
            line-height: 0;
        }

        .minWidth {
            min-width: 120px;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="min-height: 1000px">
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
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellEmpType" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellDivi" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellDept" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellSec" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellLine" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblline" runat="server" CssClass="label" Visible="false">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" Visible="false" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="cellJobLoc" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblJob" runat="server" CssClass="label" Visible="false">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJob" runat="server" Visible="false" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="cellListEmpGrd">
                            <div class="form-group">
                                <asp:Label ID="empGrade" runat="server" CssClass="label">Employee Grade</asp:Label>
                                <asp:LinkButton ID="lnkGrade" runat="server" OnClick="ddlGrade_SelectedIndexChanged"><i class="fa fa-search"></i></asp:LinkButton>
                                <asp:ListBox ID="empGradeDropDownList" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="cellListDesig">
                            <div class="form-group">
                                <asp:Label ID="empDesignation" runat="server" CssClass="label">Employee Designation</asp:Label>
                                <asp:ListBox ID="empDesignationDropDownList" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple" Style="min-height: 200px !important;"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="cellListSkil">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Skills</asp:Label>
                                <asp:DropDownList ID="ddlSkill" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="cellListSkilLvl">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Skill Level</asp:Label>
                                <asp:DropDownList ID="ddlSpecialVal" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="3" Text="High"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Average"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Low"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1" id="cellSeason" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-5" id="cellArtNo" runat="server" visible="false">
                            <asp:LinkButton ID="ibtnFindLC" CssClass="lblTxt lblName" runat="server" Text="Article No"> </asp:LinkButton>

                            <asp:DropDownList ID="ddlLCName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                        </div>

                        <div class="col-md-2" id="cellDdlProcess" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="small">Process</asp:Label>
                                <asp:DropDownList ID="DdlProcess" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-half col-sm-half col-lg-half ml-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" OnClick="lnkbtnShow_Click" runat="server" CssClass="btn btn-primary btn-sm pull-left">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
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

                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="cellddlType" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblType" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="1" Text="Operator Wise"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Operation Wise"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-2 ml-4 d-flex">
                            <div>
                                <asp:Label runat="server" CssClass="font-weight-bold"></asp:Label>
                                <br>
                                <asp:LinkButton ID="LinkButtonExportToExcel" runat="server" Style="font-weight: bold; margin-bottom: 5px !important; text-decoration: underline; color: green;" ToolTip="Export To Excel" Visible="true" OnClick="LinkButtonExportToExcel_Click">Export</asp:LinkButton>
                            </div>

                            <div class="ml-5" runat="server" id="cellLvlColorTbl">
                                <table class="table table-bordered table-sm table-striped">
                                    <thead>
                                        <tr>
                                            <th scope="col" style="width: 200px; font-size: 12px">High</th>
                                            <th scope="col" style="width: 200px; font-size: 12px">Medium</th>
                                            <th scope="col" style="width: 200px; font-size: 12px">Low</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="bg-green"></td>
                                            <td class="bg-yellow"></td>
                                            <td class="bg-red"></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <div class="card" style="margin-top: -15px">

                <div class="card-body mb-5" style="min-height: 800px">

                    <div class="row table-responsive  skillmatrix" style="min-height: 800px" runat="server" id="cellGvSkilMat">

                        <asp:GridView ID="gvSkilMatrix" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="false">

                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID CARD">
                                    <ItemTemplate>
                                        <asp:Label ID="lgIdCard10" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgEmpName10" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s1">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s1")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s2">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls2" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s2")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s3">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls3" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s3")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s4">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls4" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s4")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s5">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls5" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s5")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s6">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls6" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s6")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s7">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls7" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s7")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s8">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls8" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s8")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s9">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls9" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s9")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s10">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls10" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s10")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s11">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls11" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s11")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s12">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls12" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s12")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s13">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls13" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s13")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s14">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls14" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s14")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s15">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls15" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s15")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s16">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls16" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s16")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s17">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls17" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s17")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s18">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls18" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s18")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s19">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls19" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s19")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="VerticalColumn" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s20">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls20" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s20")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s21">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls21" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s21")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s22">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls22" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s22")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s23">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls23" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s23")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s24">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls24" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s24")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s25">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls25" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s25")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s26">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls26" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s26")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s27">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls27" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s27")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s28">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls28" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s28")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s29">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls29" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s29")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s30">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls30" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s30")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s31">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls31" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s31")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s32">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls32" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s32")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s33">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls33" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s33")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s34">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls34" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s34")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s35">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls35" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s35")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s36">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls36" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s36")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s37">
                                    <ItemTemplate>
                                        <asp:Label ID="lnl37" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s37")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s38">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls38" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s38")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s39">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls39" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s39")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s40">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls40" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s40")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s41">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls41" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s41")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s42">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls42" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s42")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s43">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls43" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s43")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s44">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls44" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s44")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s45">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls45" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s45")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s46">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls46" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s46")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s47">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls47" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s47")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s48">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls48" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s48")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s49">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls49" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s49")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="s50">
                                    <ItemTemplate>
                                        <asp:Label ID="lnls50" runat="server" CssClass='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "s50")))%>'
                                            Text=''
                                            Width="23px" Height="23px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>

                    <div class="row" style="min-height: 800px" runat="server" id="cellGvWrkWisCapMon">

                        <div class="col-md-6">

                            <asp:GridView ID="gvWrkWisCapMon" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="false"
                                OnPageIndexChanging="gvWrkWisCapMon_PageIndexChanging" OnRowEditing="gvWrkWisCapMon_RowEditing" OnRowCancelingEdit="gvWrkWisCapMon_RowCancelingEdit"
                                OnRowUpdating="gvWrkWisCapMon_RowUpdating" OnRowDataBound="gvWrkWisCapMon_RowDataBound">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:CommandField DeleteText="" HeaderText="" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                        <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                    </asp:CommandField>

                                    <asp:TemplateField HeaderText="EmpId" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Procod" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procod")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OPERATOR NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lgEmpName10" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ID NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lgIdCard10" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OPERATION NAME">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlPProces" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPProces" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AVG. Cycle time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCycleTime" runat="server" BackColor="Transparent" Font-Size="11px" CssClass="form-control form-control-sm"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cycletime")).ToString("#,##0.00") %>'
                                                Width="50px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                        <div class="col-md-6">

                            <asp:GridView ID="gvrWrkCapDate" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="false">

                                <Columns>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblWrkCapDate" OnClick="lblWrkCapDate_Click" runat="server"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dayid"))).ToString("dd-MMM-yyyy") %>'
                                                Width="150px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                    </div>

                    <div class="row" style="min-height: 800px" runat="server" id="cellGrvWWCMOprW">

                        <div class="col-md-6">

                            <asp:GridView ID="grvWWCMOprW" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" 
                                AutoGenerateColumns="false" OnRowDataBound="grvWWCMOprW_RowDataBound">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation Name" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcodOprWis" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                            <asp:Label ID="lblOerationame" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Operation Name">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPProcesByOpNam" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AVG. Cycle time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCycleTimeByOpNam" runat="server" BackColor="Transparent" Font-Size="11px" CssClass="form-control form-control-sm"
                                                Width="50px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                    </div>

                    <div class="row" style="min-height: 800px" runat="server" id="CellRptWrkWisCapMon">

                        <div class="col-md-1">

                            <asp:GridView ID="GVRptWrkMonth" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="false" ShowHeader="False" OnRowDataBound="GVRptWrkMonth_RowDataBound">

                                <Columns>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblWrkCapDateR" runat="server" OnClick="lblWrkCapDateR_Click" CssClass="btn btn-primary"
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dayid"))).ToString("dd-MMM-yyyy") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                        <div class="col-md-9">

                            <asp:Label ID="lblSelectDate" runat="server" Font-Bold="true" Font-Size="15" />

                            <asp:GridView ID="GVRptWrkWisCapMon" runat="server" AllowPaging="true" CssClass="table-striped table-bordered grvContentarea" AutoGenerateColumns="false"
                                OnPageIndexChanging="GVRptWrkWisCapMon_PageIndexChanging">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlR" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="EmpId" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpIdR" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Procod" runat="server" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcodR" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procod")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OPERATOR NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpNameR" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ID NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdNoR" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OPERATION NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOprNameR" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "oprname")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="AVG. Cycle time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvgCyclR" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cycletime")).ToString("#,##0.00") %>'
                                                Width="50px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Machine Type" ItemStyle-CssClass="minWidth">
                                        <ItemTemplate>
                                            <asp:Label ID="lgIdCard10" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machtyp")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px" />
                                    </asp:TemplateField>

                                </Columns>

                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
