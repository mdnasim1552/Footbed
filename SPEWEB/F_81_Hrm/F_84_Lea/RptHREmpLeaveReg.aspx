<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptHREmpLeaveReg.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_84_Lea.RptHREmpLeaveReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
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
            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);


            //});

           <%-- var gridview = $('#<%=this.gvSalAdd.ClientID %>');
            $.keynavigation(gridview);--%>


            $('.chzn-select').chosen({ search_contains: true });

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
                <div class="card-body" >
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
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
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label14" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm " OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblEmp" runat="server" CssClass="label">Emp.  Name:</asp:Label>
                                <asp:TextBox ID="txtSrcEmp" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display: none;"></asp:TextBox>
                                <asp:LinkButton ID="ibtnFindEmp" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Style="display: none;" OnClick="ibtnFindEmp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                <asp:DropDownList ID="ddlPEmpName" OnSelectedIndexChanged="ddlPEmpName_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                <asp:Label ID="lblPEmpName" runat="server" CssClass="form-control " Visible="false"></asp:Label>
                                <!---- for new employee--->
                                <asp:Label ID="lblnewEmp" runat="server" CssClass="lblTxt lblName" Visible="false">New Emp.:</asp:Label>
                                <asp:TextBox ID="txtNSrcEmp" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false" Style="display: none;"></asp:TextBox>
                                <asp:LinkButton ID="ibtnNFindEmp" Style="display: none;" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnNFindEmp_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlNPEmpName" Visible="false" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>

                            </div>

                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group" style="margin-top: 20px;">

                                <asp:LinkButton ID="lnkbtnSerOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5 col-lg-5" style="margin-left: 10px;">
                                <div class="form-group">
                                    <asp:Label ID="lblFrmDate" runat="server" CssClass="label">From</asp:Label>
                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass=" form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFrmDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-5 col-sm-5 col-lg-5 ">
                                
                                    <asp:Label ID="lblToDate" runat="server" CssClass="label">To</asp:Label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass=" form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                                
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                       <div class="col-md-6">
                                       

                                        <asp:GridView ID="gvLeaveRegStatus" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: center; font-family:SutonnyMJ"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvdat")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Cl Leave" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvtype" runat="server"
                                                            Style="text-align: right; font-family:SutonnyMJ"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bcleave")).ToString("#,##0; (#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl Leave">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvAllowAdd" runat="server" BackColor="Transparent" Height="20px"
                                                            Style="text-align: right; font-family:SutonnyMJ"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsleave")).ToString("#,##0; (#,##0); ") %>'
                                                            Width="70px" BorderStyle="None"></asp:TextBox>
                                                    </ItemTemplate>
                                                   
                                                    <ItemStyle HorizontalAlign="right" />
                                                   
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
