<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpEntry01.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.EmpEntry01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../Content/Theme/WebCam.js"></script>
    <style>
        .moduleItemWrpper {
            overflow: auto;
        }

        label {
            margin-bottom: 0rem !important;
        }

        .personal-info .form-group {
            margin-bottom: 0rem !important;
        }

        .personal-info hr {
            margin-top: 0rem !important;
            margin-bottom: 0rem !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {


            $('.chzn-select').chosen({ search_contains: true });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });



                var gridview = $('#<%=this.gvPersonalInfo.ClientID %>');
                $.keynavigation(gridview);

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);


                });
            }

            catch (e) {

                alert(e);
            }

        }

    </script>
    <script type="text/javascript">
        $(function () {

            $("#btnCapture").hide();
            $("#btnUpload").hide();
            $(document).on("click", "#btnCapture", function () {
                Webcam.snap(function (data_uri) {
                    $("#imgCapture")[0].src = data_uri;
                    $("#btnUpload").removeAttr("disabled");
                });
                $("#btnUpload").show();
            });
            $(document).on("click", "#btnUpload", function () {
                $.ajax({
                    type: "POST",
                    url: "EmpEntry01.aspx/SaveCapturedImage",
                    data: "{data: '" + $("#imgCapture")[0].src + "',empid:'" + $("#ddlEmpName").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        // console.log(r);
                        if (r.d == true) {
                            showContent('Updated Successfully')
                        }
                        else {
                            showContentFail('Not Updated Successfully')

                        }
                    }
                });

            });

            $(document).on("click", "#BtnCamera", function () {
                Webcam.set({
                    width: 340,
                    height: 260,
                    image_format: 'jpeg',
                    jpeg_quality: 90
                });
                Webcam.attach('#webcam');
                $("#btnCapture").show();
            });
        });
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
                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee</asp:Label>
                                <asp:CheckBox ID="chkNewEmp" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkNewEmp_CheckedChanged"
                                    Text="New" />
                                <asp:CheckBox ID="InactiveEmp" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="InactiveEmp_CheckedChanged"
                                    Text="Inactive" />
                                <asp:DropDownList ID="ddlEmpName" ClientIDMode="Static" runat="server" CssClass="chzn-select form-control  form-control-sm " AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:Label ID="lblEmpName" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>--%>
                            </div>
                            <div class="col-md-2 pull-right" style="display: none;">
                                <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                <asp:LinkButton ID="lnkNextbtn" runat="server" CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;" OnClick="lnkNextbtn_Click">
                                    <span class="flaticon-add118"></span> Next</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Information</asp:Label>
                                <a href="../F_82_App/EmpEntryForm" class="btn btn-sm btn-success"><span class=" fa fa-plus" aria-hidden="true">Add Employee</span></a>
                                <asp:DropDownList ID="ddlInformation" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlInformation_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lblLastCardNo" runat="server" Visible="false" CssClass=" btn btn-info primaryBtn btn-sm"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 personal-info">
                    <!-- .card -->
                    <section class="card" style="min-height: 420px;">
                        <header class="task-header small" style="padding-top: 5px;">
                            <h3 class="task-title mr-auto">Personal Information<span class="badge">(Basic Information)</span>
                            </h3>
                            <a id="BtnCamera" href="#" class="text-twitter">Photo &nbsp;<span class="fa fa-camera"></span></a> &nbsp; Select Signatory : &nbsp;
                       <asp:DropDownList ID="ddlsign" runat="server" Style="width: 200px" CssClass="custom-select chzn-select"></asp:DropDownList>
                        </header>
                        <hr />

                        <!-- .card-body -->
                        <div class="card-body">
                            <div class="row"
                                style="margin-bottom: 130px !important;">

                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="ViewPersonal" runat="server">
                                        <div class="col-md-9">
                                            <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                OnRowDataBound="gvPersonalInfo_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                      <%--  <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdatPerInfo_Click" OnClientClick="return confirm('Do you want to save?');" ToolTip="Update Personal Info">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="537px" TabIndex="13" AutoCompleteType="Disabled"></asp:TextBox>

                                                            <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" OnTextChanged="txtgvdVal_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="537px" TabIndex="14"></asp:TextBox>

                                                            <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                            <asp:Panel ID="Panegrd" runat="server">

                                                                <div class="form-group">
                                                                    <div class="col-md-12 pading5px">

                                                                        <%--<asp:LinkButton ID="ibtngrdEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtngrdEmpList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>

                                                                        <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass=" ddlPage62 inputTxt chzn-select" Width="350" AutoPostBack="true" TabIndex="12">
                                                                        </asp:DropDownList>



                                                                    </div>
                                                                </div>


                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="বাংলা">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtGdatatbn" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatatbn")) %>'
                                                                Width="200"></asp:TextBox>
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
                                        <div class="col-md-3 text-center ">
                                            <div id="webcam"></div>
                                            <a href="#" id="btnCapture" class="btn btn-sm btn-info">Capture <span class="fa fa-camera"></span></a>
                                            <img id="imgCapture" />



                                            <input type="button" id="btnUpload" class="btn btn-sm btn-success" value="Upload" disabled="disabled" />

                                        </div>
                                    </asp:View>
                                    <asp:View ID="ViewDegree" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvDegree" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="831px" OnRowDeleting="gvDegree_RowDeleting">
                                                <RowStyle />
                                                <Columns>                                                    
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                                    <asp:TemplateField HeaderText="Degree Name">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control chzn-select" Width="150" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Exam/Degree Title">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlAcadegree" runat="server" CssClass="form-control chzn-select" Width="180">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Major Subject">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlMajorSubj" runat="server" CssClass="form-control chzn-select" Width="180">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Institution">                                                       
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "institute")) %>' Width="200px"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlInst" runat="server" OnSelectedIndexChanged="ddlInst_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control chzn-select" Width="200">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>                                                 
                                                        <%-- <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateDegree" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateDegree_Click" ToolTip="Update Academic Record">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Result">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlResult" runat="server" AutoPostBack="True"
                                                                CssClass="form-control chzn-select" Width="150" OnSelectedIndexChanged="ddlResult_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">

                                                        <ItemTemplate>
                                                            <table style="width: 23%; height: 17px;">
                                                                <tr>
                                                                    <td class="style49">
                                                                        <asp:Label ID="lblgvMarks" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Text="Marks(%) :" Width="62px"></asp:Label>
                                                                    </td>
                                                                    <td class="style52">
                                                                        <asp:TextBox ID="txtgvmarkorgrade" runat="server"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "markorgrade")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                            BackColor="Transparent"
                                                                            BorderStyle="None" Width="40px" Font-Size="11px" Style="text-align: right;">                                                                            
                                                                              
                                                                              
                                                                        </asp:TextBox>
                                                                    </td>
                                                                    <td class="style51">
                                                                        <asp:Label ID="lblgvScale" runat="server" Font-Bold="True" Font-Size="12px"
                                                                            Text="Scale :" Width="40px" Style="text-align: center;"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtgvScale" runat="server" BackColor="Transparent"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "scale")).ToString("#,##0;(#,##0); ")  %>'
                                                                            BorderStyle="None" Width="30px" Font-Size="11px" Style="text-align: center;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Passing Year">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlPassingYear" runat="server"
                                                                CssClass="form-control chzn-select" Width="80">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Board">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlBoard" runat="server" CssClass="form-control chzn-select" Width="100">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="gvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="ViewCompany" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvEmpRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="831px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgcvCompany" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>'
                                                                Width="300px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                Width="300px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Duration">
                                                        <%--<FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateEmprecord" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateEmprecord_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvesDuration" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>'
                                                                Width="100px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
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
                                    </asp:View>
                                    <asp:View ID="ViewAssociation" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAssocia" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="701px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Organization Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgcvOrgName" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orgname")) %>'
                                                                Width="300px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Position">
                                                        <%--<FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateEmpAssocia" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateEmpAssocia_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvPostion" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "position")) %>'
                                                                Width="300px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
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
                                    </asp:View>
                                    <asp:View ID="ViewRef" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvRef" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="831px">
                                                <RowStyle />
                                               <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCodeRef" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRefDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRefgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                       <%-- <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateRef" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateRef_Click" ToolTip="Update Reference Info.">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRefVal" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="537px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="বাংলা">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRefGdatabn" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatatbn")) %>'
                                                                Width="250"></asp:TextBox>
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
                                    </asp:View>
                                    <asp:View ID="ViewJobRespo" runat="server">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="500px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode1" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Responsibilities">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgcvJobRes" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="20px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobresp")) %>'
                                                                Width="300px"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <%--<FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateJobRes" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateJobRes_Click" ToolTip="Update Job Responsibilities">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>

                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
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
                                    </asp:View>
                                    <asp:View runat="server" ID="ViewNomineeForm">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvNomineeForm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="831px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvItmCodeNomi" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgval" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>                                                        
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="537px"></asp:TextBox>

                                                            <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" OnTextChanged="txtgvdVal_TextChanged1" AutoPostBack="true"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                Width="537px"></asp:TextBox>

                                                            <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                                            <asp:Panel ID="Panegrd" runat="server">
                                                                <div class="form-group">
                                                                    <div class="col-md-12 pading5px">
                                                                        <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged1" CssClass=" ddlPage62 inputTxt chzn-select" Width="350" AutoPostBack="true" TabIndex="3">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                        <%--<FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateEmpNominee" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateEmpNominee_Click" ToolTip="Update Nominee Info">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="বাংলা" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtGdatatbn" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatatbn")) %>'
                                                                Width="250"></asp:TextBox>
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

                                    </asp:View>
                                    <asp:View runat="server" ID="ViewSpecialInfo">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvSpecialInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Width="10"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvItmCodeNomi" runat="server" Height="16px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                       <%-- <FooterTemplate>
                                                            <asp:LinkButton ID="lUpdateSpecialSkill" runat="server" CssClass="btn btn-success btn-sm" OnClick="lUpdateSpecialSkill_Click" ToolTip="Update Special Info">Final Update</asp:LinkButton>
                                                        </FooterTemplate>--%>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlSpecialVal" runat="server" AutoPostBack="True" Width="81px" CssClass="ddlPage">
                                                                <asp:ListItem Value="3" Text="High"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Average"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Low"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="gvHeader" />
                                            </asp:GridView>
                                        </div>

                                    </asp:View>
                                </asp:MultiView>
                                <div class="clearfix">
                                    <br />
                                </div>
                                <br />
                            </div>
                        </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

