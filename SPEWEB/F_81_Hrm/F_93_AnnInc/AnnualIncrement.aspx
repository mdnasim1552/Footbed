<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AnnualIncrement.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_93_AnnInc.AnnualIncrement" %>

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
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            try {

                //$("input, select").bind("keydown", function (event) {
                //    var k1 = new KeyPress();
                //    k1.textBoxHandler(event);
                //});

                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e)
            {

                alert(e.message);

            }

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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblJobLoc" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLoc" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="81px" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                    <div class="row">
                        <div class="form-group" hidden="hidden">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlCompany1" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompany1_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblCompany" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:LinkButton ID="Lbtn" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group" hidden="hidden">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:TextBox ID="txtSrcDept" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlDept1" runat="server" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblDept" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group" hidden="hidden">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                <asp:TextBox ID="txtSrcSection" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnSectionSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnSectionSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                            </div>
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:DropDownList ID="ddlSection1" runat="server" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:Label ID="lblSection" runat="server" BackColor="White" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Incrment Year</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblIncNo0" runat="server" CssClass="label">Inc. No.</asp:Label>
                                <asp:Label ID="lblCurIncrNo" runat="server" CssClass=" smLbl_to"></asp:Label>
                                <asp:TextBox ID="txtCurIncrNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-inline" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkbdate" runat="server" AutoPostBack="True" CssClass="checkbox" Text="Birth Date" />
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" id="divChkEmp" runat="server" visible="false" style="margin-top: 20px;">
                            <label id="chkbod" runat="server" class="switch" title="Check for Add Emp.">
                                <asp:CheckBox ID="chkAddEmp" runat="server" ClientIDMode="Static" OnCheckedChanged="chkAddEmp_CheckedChanged" AutoPostBack="true" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label ID="lblAddEmp" runat="server" Text="Add Employee" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divPrevList" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblPreVious" runat="server" CssClass="label">Previous List
                                    <asp:LinkButton ID="imgbtnPreList" runat="server" OnClick="imgbtnPreList_Click" ToolTip="Click for Prev. List"><span class="fa fa-search"> </span></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlPrevIncList" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Panel ID="pnlxcel" runat="server">
                                    <asp:Label ID="lblExel" runat="server" CssClass="label">Excel Upload</asp:Label>
                                    <br />
                                    <div class="btn btn-secondary btn-sm fileinput-button">
                                        <i class="fa fa-plus fa-fw"></i>
                                        <span>Add files...</span>
                                        <asp:FileUpload ID="fileuploadExcel" runat="server" Height="26px"
                                            onchange="submitform();" />
                                    </div>
                                    <asp:LinkButton ID="btnexcuplosd" runat="server" CssClass=" btn btn-sm btn-success" Text="ADJUST" OnClick="btnexcuplosd_Click" ToolTip="Click to Adjust Upload Increment"></asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </div>
                         <div class="col-md-1.5 col-sm-1.5 col-lg-1.5">
                            <div class="form-inline" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkOnlyFInc" runat="server" AutoPostBack="True" CssClass="checkbox" Text="Only F Inc." />

                                   <asp:CheckBox ID="chkallemptype" runat="server" AutoPostBack="True" CssClass="checkbox" Text="Emp. Type" />
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
            <div class="card card-fluid" style="min-height: 400px;">
                <div class="card-body">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAnnIncre" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                OnPageIndexChanging="gvAnnIncre_PageIndexChanging" OnRowDataBound="gvAnnIncre_RowDataBound" ShowFooter="True" Width="831px" 
                                CssClass="table-striped table-hover table-bordered grvContentarea"
                                Height="200px" >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkBtnDelIncrmnt" OnClick="lnkBtnDelIncrmnt_Click" Width="20px" ToolTip="Delete Increment">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lgProName" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary btn-sm" ToolTip="Amount Calculation">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnPutSameValue" runat="server" Font-Bold="True" Font-Underline="true"
                                                Font-Size="9px" OnClick="lbtnPutSameValue_Click" CssClass="btn btn-info btn-sm">Put Same Value</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSection" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name &amp; Designation">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkFiUpdate" runat="server" OnClick="lnkFiUpdate_Click" CssClass="btn btn-success btn-sm" ToolTip="Final Update">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvndesig" Font-Size="10px" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+
                                                    "</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Card #">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCardNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Joining Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvjoidat" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="60px" Font-Size="9PX"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnRound" runat="server" OnClick="lbtnRound_Click" Style="float: right;" CssClass="btn btn-primary btn-sm">Round</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gross <br> Salary" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpreamgst" runat="server" Style="text-align: right; font-size: 10px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maingrossal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Car  allow" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprecarsubamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carsubamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub allow" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvprsubamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Increment Prev. <br>Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvincrprevyr" runat="server" Style="text-align: right; font-size: 10px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incamtprevyr")).ToString("#,##0;(#,##0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pre.<br>Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpreamt" runat="server" Style="text-align: right; font-size: 10px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFpresal" runat="server" Font-Bold="True" Font-Size="9px" ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlGrade" CssClass=" form-control form-control-sm" Width="55px" runat="server">
                                                <asp:ListItem Value="A+">A+</asp:ListItem>                                             
                                                <asp:ListItem Value="A">A</asp:ListItem>
                                                <asp:ListItem Value="B">B</asp:ListItem>
                                                <asp:ListItem Value="C">C</asp:ListItem>
                                                <asp:ListItem Value="D">D</asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True">None</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Increment %">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvincpercnt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inpercnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="55px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Increment Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvincamt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="1px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFincamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposed Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvpinamount" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinincamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFPinincamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Signatory">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DdlSignatory" CssClass=" form-control form-control-sm" Width="180px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="HOD Promotion <br> Proposal">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDesignation" CssClass="form-control form-control-sm" Width="150px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="HR Proposal <br> Increment">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvhrpromincamt" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hrpromincamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFhrpromincamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="HR Promotion <br> Proposal">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlDesignation1" CssClass=" form-control form-control-sm" Width="150px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Final Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvfinamount" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="0px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "finincamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFfinincamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtRemarks" runat="server" Font-Size="11px" BackColor="Transparent" BorderColor="#660033"
                                                BorderStyle="Solid" BorderWidth="0px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
