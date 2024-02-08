<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="KpiTargetEntry.aspx.cs" Inherits="SPEWEB.F_47_Kpi.KpiTargetEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="~/Scripts/ScrollableGridPluginNew.js"></script>
    <script type="text/javascript" src="~/Scripts/ScrollableTablePlugin.js"></script>
    <script type="text/javascript" src="~/Scripts/gridviewScrollHaVer.js"></script>
    <link href="~/Content/GridViewScrooling.css" rel="stylesheet" type="text/css" />

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


        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
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




    </script>

    <style>
        .table-striped > tbody > tr td {
            height: 26px;
        }

        .table-striped > tbody > tr > th {
            font-weight: normal;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">

                <div class="card-body" style="min-height: 850px;">
                    <div class="card">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-lg-12 ">
                             
                            <div class="form-group form-inline">
                               
                                <div class="col-md-3 col-sm-3 col-lg-3 ">

                                    <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="custom-select chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">

                                    <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="custom-select chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">

                                    <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="custom-select chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">


                                    <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="custom-select chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-12 col-sm-12 col-lg-12 ">

                            <div class="form-group form-inline">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                   
                                        <asp:Label ID="lblEmp" runat="server" CssClass="lblTxt lblName">Employee List:</asp:Label>

                                        <div class="form-inline">
                                            <asp:TextBox ID="txtEmpSrc" Style="display: none" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary btn-sm" OnClick="ibtnEmpList_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlEmpName" Width="170" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select chzn-select form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    
                                </div>
                            
                                <div class="col-md-3 col-sm-3 col-lg-3 ">


                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Width="68">Month</asp:Label>
                                    <div class="form-inline">

                                        <%--  <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputDateBox " AutoCompleteType="Disabled"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm " Width="190" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                    </div>


                                </div>
                           
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <asp:Label ID="lbluser" runat="server" CssClass="lblTxt lblName">User List:</asp:Label>

                                    <div class="form-inline">
                                        <asp:TextBox ID="txtuser" Style="display: none" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddluserName" Width="200" runat="server" CssClass="custom-select chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group form-inline">
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <asp:Label ID="lblworkGroup" runat="server" Visible="false" CssClass="lblTxt lblName">KPI Work Group</asp:Label>
                            <div class="form-inline">
                                <asp:DropDownList ID="ddlworkGroup" runat="server" Width="200" OnSelectedIndexChanged="ddlworkGroup_SelectedIndexChanged" Visible="false" CssClass="custom-select chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <asp:Label ID="lblworklst" runat="server" Visible="false" CssClass="lblTxt lblName">KPI Work List</asp:Label>
                            <div class="form-inline">
                                <asp:DropDownList ID="ddlworklst" runat="server" Width="190" CssClass="custom-select chzn-select form-control form-control-sm" Visible="false" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                <asp:LinkButton ID="lnkbtnAdd" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnAdd_Click">Add</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                   </div>
               <div class="card">
                    <div class="form-inline" style="margin-left:20px">
                          <asp:GridView ID="gvkpibgd" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive" OnRowDeleting="gvkpibgd_RowDeleting"
                        AutoGenerateColumns="False"  AllowSorting="true"
                        ShowFooter="True">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Bottom"
                            Mode="NumericFirstLast" />
                        <RowStyle />
                        <Columns>


                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                     <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvEmpName" runat="server" Width="250" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empdesc")) %>'></asp:Label>
                                </ItemTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvEmpCode" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KPI Work">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvworklst" runat="server"  Width="170" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpidesc")) %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KPI Work" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvworklstcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpicode")) %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtkpiqty" runat="server" Width="50" CssClass=" form-control form-control-sm" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgdqty")) %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                               <ItemStyle  HorizontalAlign="Right" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Mark">
                                <ItemTemplate>
                                    <asp:Label ID="lblMark" runat="server" Width="50"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0.00") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                               <ItemStyle  HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                        <div class="col-md-1">
                                    
                                        <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn-danger btn-sm btn" Visible="false"></asp:Label>
                                </div>
                    </div>
                  </div>
                </div>

             </div>
           

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

