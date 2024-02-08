<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AllEmpIDCard.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.AllEmpIDCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript">
          $(document).ready(function () {
              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          });
          function pageLoaded() {
              $('.chzn-select').chosen({ search_contains: true });
          }
    </script>--%>

    <style>
        .multiselect.dropdown-toggle.btn.btn-default.nsl {
            width: 200px !important;
        }

        #charts {
            width: 600px;
            height: 400px;
            position: relative;
            margin: 0 auto;
            font-size: 12px;
        }

        .chartdiv {
            width: 600px;
            height: 400px;
            position: absolute;
            top: 10px;
            left: 0;
        }

        .legnd {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 500;
            background: #ffffff;
        }

            .legnd span:nth-child(1) {
                color: #0FAD5E;
            }

            .legnd span:nth-child(2) {
                color: #F15922;
            }

            .legnd span:nth-child(3) {
                color: #C3DA57;
            }

            .legnd span:nth-child(4) {
                color: #88C760;
            }

            .legnd span:nth-child(5) {
                color: #F59AB8;
            }

            .legnd span:nth-child(6) {
                color: #E56B8D;
            }

            .legnd span:nth-child(7) {
                color: #336699;
            }


        .lblrangeup {
            background: rgba(0, 0, 0, 0) url("../../Images/ArrowUp.png") no-repeat scroll center top;
            float: right;
            height: 60px;
            width: 27px;
        }

        .lblrangedwn {
            background: rgba(0, 0, 0, 0) url("../../Images/ArrowDown.png") no-repeat scroll center bottom;
            float: right;
            height: 60px;
            width: 27px;
        }
    </style>
    <style>
        .well h4 {
            margin: 0;
            font-size: 15px;
            font-weight: bold;
        }

        .well p {
            margin: 0;
        }

        .multiselect {
            width: 420px !important;
            border: 1px solid;
            height: 29px;
            border-color: silver
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
            border-color: silver
        }
    </style>



    <script type="text/javascript">
        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });


            $('[id*=ddlPEmpName]').multiselect({
                includeSelectAllOption: true,
                searchable: true,
                enableFiltering: true,
                maxHeight: 200

                //includeSelectAllOption: true,
                //maxHeight: 200,
                //enableCaseInsensitiveFiltering: true,

            });
        };

    </script>


    <div class="nahidProgressbar">

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
            <ProgressTemplate>

                <%--  <div class="loader"></div> --%>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body" style="height: 150px">
                    <div class="row">

                        <div class="form-group form-inline">

                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="200" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm  pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 pading5px form-inline">
                                <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" Width="120" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm  pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                <asp:DropDownList ID="ddlSection" runat="server" Width="150" CssClass="chzn-select form-control form-control-sm pull-left" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>

                            <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                <asp:Label ID="lblLIne" runat="server" CssClass="smLbl_to">Line</asp:Label>
                                <asp:DropDownList ID="ddlLine" runat="server" Width="100" CssClass="chzn-select form-control form-control-sm pull-left" TabIndex="2" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>

                            <div class="clearfix"></div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="form-inline">

                            <div class="col-2 pading5px ">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" Width="200" CssClass="form-control form-control-sm pull-left "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                            </div>
                            <div class="col-md-2 pading5px " style="margin-left: 60px">
                                <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control form-control-sm pull-left "></asp:TextBox>
                                <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>

                            </div>
                            <div class="col-md-1" style="margin-top: 20px">
                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">W.Date</asp:Label>
                                <asp:CheckBox ID="chkwithoutdate" runat="server" Style="margin-left: 2px;" />
                            </div>
                            <div class="col-md-3 pading5px form-inline">
                                <asp:Label ID="lblEmpSearch" runat="server">Employee                                    
                                <asp:LinkButton ID="ibtnFindEmp" runat="server" CssClass="lblTxt lblName" OnClick="ibtnFindEmp_Click" 
                                    ToolTip="Search" Style="color: blue;" Font-Underline="false"><i class="fas fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox ID="ddlPEmpName" runat="server" CssClass="form-control form-control-sm" Style="min-width: 100% !important; border: solid" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-md-2 pading5px form-inline ">
                                <asp:Label ID="lblEmp" runat="server" CssClass="lblTxt lblName">Format</asp:Label>
                                <asp:DropDownList ID="DdlIdFormat" Width="120" runat="server" CssClass="chzn-select form-control inputTxt">
                                    <asp:ListItem>Executive</asp:ListItem>
                                    <asp:ListItem>Factory Staff</asp:ListItem>
                                    <asp:ListItem Selected="True">Worker</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title" id="myModalLabel">Staff Graph</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-lg-12">
                                                <asp:Panel ID="onDayGraph" runat="server">
                                                    <script src="../../Scripts/Mchart.js"></script>
                                                    <script src="../../Scripts/Mchartpie.js"></script>
                                                    <%--<script src="http://www.amcharts.com/lib/3/themes/light.js"></script>--%>



                                                    <div id="charts">
                                                        <div class="btn btn-primary btn-sm legnd" style="background: #fff; width: 50%; border: none !important; height: 31px;">

                                                            <span></span>
                                                            <span></span>
                                                            <span></span>
                                                            <span></span>
                                                            <span></span>
                                                            <span></span>
                                                            <span></span>

                                                        </div>
                                                        <div id="chart1" class="chartdiv"></div>
                                                        <div id="chart2" class="chartdiv"></div>

                                                    </div>

                                                </asp:Panel>
                                                <div>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                    </div>
                    <div class="row">
                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="col-xs-12 col-sm-6 col-md-3" style="padding: 0 5px;">
                                    <div id="EmpAll" runat="server">
                                        <%--<a href="#"><i class="fa fa-archive"></i>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("desig")%>'></asp:Label></a>--%>
                                        <div class="well well-sm" style="height: 100px; margin-bottom: 2px;">
                                            <div class="row">
                                                <div class="col-sm-6 col-md-4">
                                                    <a href="<%# "../F_82_App/RptMyInterface.aspx?empid=" + Eval("empid") %>" target="_blank">

                                                        <%--<asp:Image ID="ImgID" runat="server"  ImageUrl="~/GetImage.aspx?ImgID=HRIndEmp&&empid=''<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>''" Height="70" Width="80" CssClass="img-thumbnail img-responsive" />--%></p>
                                                                        <%--<img src="http://placehold.it/380x500" alt="" class="img-rounded img-responsive" />--%>

                                                        <asp:Image ID="ImgID" runat="server" ImageUrl='<%# "~/GetImage.aspx?ImgID=HRIndEmp&&empid=" + Eval("empid") %>' Height="70" Width="80" CssClass="img-thumbnail img-responsive" />

                                                    </a>
                                                </div>
                                                <div class="col-sm-6 col-md-8 pading5px">
                                                    <h4>
                                                        <asp:Label ID="lblempname" runat="server" Text='<% #Bind("empname")%>'></asp:Label></h4>
                                                    <p>
                                                        <asp:Label ID="Label2" runat="server" Text='<% #Bind("desig")%>'></asp:Label>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="Label4" runat="server" Text='<% #Bind("section")%>'></asp:Label>
                                                    </p>
                                                    <p>
                                                        <i class="glyphicon glyphicon-calendar"></i>
                                                        <asp:Label ID="Label5" runat="server" Text='<% #Bind("joindate")%>'></asp:Label>
                                                    </p>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                    <div style="display: none">

                        <%-- <asp:TextBox ID="txtpresent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtlate" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtearlylev" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtonleave" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtabsent" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txttostaff" runat="server"></asp:TextBox>--%>
                        <asp:TextBox ID="txtTtlStaff" runat="server"></asp:TextBox>

                        <asp:TextBox ID="txtdpt1" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt2" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt3" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt4" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt5" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt6" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt7" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt8" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt9" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt10" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt11" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt12" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt13" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt14" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt15" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt16" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt17" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt18" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt19" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt20" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt21" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt22" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt23" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt24" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt25" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt26" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt27" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt28" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt29" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt30" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt31" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt32" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt33" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt34" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt35" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt36" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt37" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt38" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt39" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt40" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt41" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt42" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt43" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt44" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt45" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt46" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt47" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt48" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt49" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt50" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt51" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt52" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt53" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt54" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt55" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt56" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt57" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt58" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt59" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtdpt60" runat="server"></asp:TextBox>

                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox35" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox36" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox37" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox38" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox39" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox40" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox41" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox42" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox43" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox44" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox45" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox46" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox47" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox48" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox49" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox50" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox51" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox52" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox53" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox54" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox55" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox56" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox57" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox58" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox59" runat="server"></asp:TextBox>
                        <asp:TextBox ID="TextBox60" runat="server"></asp:TextBox>





                    </div>

                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="height: 800px">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        /**
* Plugin: Manipulate z-index of the chart
*/
        AmCharts.addInitHandler(function (chart) {
            // init holder for nested charts
            if (AmCharts.nestedChartHolder === undefined)
                AmCharts.nestedChartHolder = {};

            if (chart.bringToFront === true) {
                chart.addListener("init", function (event) {
                    // chart inited
                    var chart = event.chart;
                    var div = chart.div;
                    var parent = div.parentNode;

                    // add to holder
                    if (AmCharts.nestedChartHolder[parent] === undefined)
                        AmCharts.nestedChartHolder[parent] = [];
                    AmCharts.nestedChartHolder[parent].push(chart);

                    // add mouse mouve event
                    chart.div.addEventListener('mousemove', function () {

                        // calculate current radius
                        var x = Math.abs(chart.mouseX - (chart.realWidth / 2));
                        var y = Math.abs(chart.mouseY - (chart.realHeight / 2));
                        var r = Math.sqrt(x * x + y * y);

                        // check which chart smallest chart still matches this radius
                        var smallChart;
                        var smallRadius;
                        for (var i = 0; i < AmCharts.nestedChartHolder[parent].length; i++) {
                            var checkChart = AmCharts.nestedChartHolder[parent][i];

                            if ((checkChart.radiusReal < r) || (smallRadius < checkChart.radiusReal)) {
                                checkChart.div.style.zIndex = 1;
                            }
                            else {
                                if (smallChart !== undefined)
                                    smallChart.div.style.zIndex = 1;
                                checkChart.div.style.zIndex = 2;
                                smallChart = checkChart;
                                smallRadius = checkChart.radiusReal;
                            }

                        }
                    }, false);
                });
            }

        }, ["pie"]);


    <%--    var pres = this.parseFloat($("#<%=this.txtpresent.ClientID %>").val());
        var el = this.parseFloat($("#<%=this.txtearlylev.ClientID %>").val());
        var ol = this.parseFloat($("#<%=this.txtonleave.ClientID %>").val());
        var late = this.parseFloat($("#<%=this.txtlate.ClientID %>").val());
        var abs = this.parseFloat($("#<%=this.txtabsent.ClientID %>").val());--%>


        var dpt1 = this.parseFloat($("#<%=this.txtdpt1.ClientID %>").val());
        var dpt2 = this.parseFloat($("#<%=this.txtdpt2.ClientID %>").val());
        var dpt3 = this.parseFloat($("#<%=this.txtdpt3.ClientID %>").val());
        var dpt4 = this.parseFloat($("#<%=this.txtdpt4.ClientID %>").val());
        var dpt5 = this.parseFloat($("#<%=this.txtdpt5.ClientID %>").val());
        var dpt6 = this.parseFloat($("#<%=this.txtdpt6.ClientID %>").val());
        var dpt7 = this.parseFloat($("#<%=this.txtdpt7.ClientID %>").val());
        var dpt8 = this.parseFloat($("#<%=this.txtdpt8.ClientID %>").val());
        var dpt9 = this.parseFloat($("#<%=this.txtdpt9.ClientID %>").val());
        var dpt10 = this.parseFloat($("#<%=this.txtdpt10.ClientID %>").val());


        //Titel
        var dptname1 = this.String($("#<%=this.TextBox1.ClientID %>").val());
        var dptname2 = this.String($("#<%=this.TextBox2.ClientID %>").val());
        var dptname3 = this.String($("#<%=this.TextBox3.ClientID %>").val());
        var dptname4 = this.String($("#<%=this.TextBox4.ClientID %>").val());
        var dptname5 = this.String($("#<%=this.TextBox5.ClientID %>").val());
        var dptname6 = this.String($("#<%=this.TextBox6.ClientID %>").val());
        var dptname7 = this.String($("#<%=this.TextBox7.ClientID %>").val());
        var dptname8 = this.String($("#<%=this.TextBox8.ClientID %>").val());
        var dptname9 = this.String($("#<%=this.TextBox9.ClientID %>").val());
        var dptname10 = this.String($("#<%=this.TextBox10.ClientID %>").val());


        /// var ttlstaff = dpt1 + dpt2 + dpt3 + dpt4 + dpt5 + dpt6 + dpt7 + dpt8 + dpt9 + dpt10;


        var ttlstaff = this.parseFloat($("#<%=this.txtTtlStaff.ClientID %>").val());
        //var abs = 4;

        /**
         * Create the charts
         */
        AmCharts.makeChart("chart1", {

            "type": "pie",
            "theme": "light",
            "bringToFront": true,
            "dataProvider": [{
                "title": 'Staff' + '(' + ttlstaff + ')',
                "value": ttlstaff,
                "color": "#fff"
            }],
            "startDuration": 0,
            "pullOutRadius": 0,
            "color": "#000",
            "fontSize": 10,
            "titleField": "title",
            "valueField": "value",
            "colorField": "color",
            "labelRadius": -50,
            "labelColor": "#fff",
            "radius": 50,
            "innerRadius": 0,
            "labelText": "[[title]]",
            "balloonText": "[[title]]: [[value]]"
        });

        AmCharts.makeChart("chart2", {
            "type": "pie",
            "theme": "light",
            "bringToFront": true,
            "dataProvider": [{
                "title": dptname1,
                "value": dpt1,
                "color": "#3366CC"
            }, {
                "title": dptname2,
                "value": dpt2,
                "color": "#DC3912"
            },
            {
                "title": dptname3,
                "value": dpt3,
                "color": "#FF9900"
            },
            {
                "title": dptname4,
                "value": dpt4,
                "color": "#109618"
            },
            {
                "title": dptname5,
                "value": dpt5,
                "color": "#990099"
            },
            {
                "title": dptname6,
                "value": dpt6,
                "color": "#0099C6"
            },
            {
                "title": dptname7,
                "value": dpt7,
                "color": "#DD4477"
            },
            {
                "title": dptname8,
                "value": dpt8,
                "color": "#66AA00"
            },
            {
                "title": dptname9,
                "value": dpt9,
                "color": "#B82E2E"
            },
            {
                "title": dptname10,
                "value": dpt10,
                "color": "#316395"
            }],
            "startDuration": 1,
            "pullOutRadius": 0,
            "color": "#fff",
            "fontSize": 8,
            "titleField": "title",
            "valueField": "value",
            "colorField": "color",
            "labelRadius": -70,
            "labelColor": "#fff",
            "radius": 150,
            "innerRadius": 40,
            "outlineAlpha": 1,
            "outlineThickness": 1,
            "labelText": "[[title]]",
            "balloonText": "[[title]]: [[value]]",
            //});

            //AmCharts.makeChart("chart4", {
            //    "type": "pie",
            //    "bringToFront": true,
            //    "dataProvider": [{
            //        "title": "Present",
            //        "value": 6,
            //        "color": "#BA3233"
            //    },   {
            //        "title": "Absent",
            //        "value": 4,
            //        "color": "#6179C0"
            //    }],
            //    "startDuration": 1,
            //    "pullOutRadius": 0,
            //    "color": "#fff",
            //    "fontSize": 8,
            //    "titleField": "title",
            //    "valueField": "value",
            //    "colorField": "color",
            //    "labelRadius": -27,
            //    "labelColor": "#fff",
            //    "radius": 190,
            //    "innerRadius": 137,
            //    "outlineAlpha": 1,
            //    "outlineThickness": 4,
            //    "labelText": "[[title]]",
            //    "balloonText": "[[title]]: [[value]]",

            "allLabels": [{
                "text": "Company Total Staff",

                "bold": true,
                "size": 20,
                "color": "#404040",
                "x": 0,

                "align": "center",
                "y": 20

            }]
        });
    </script>
</asp:Content>

