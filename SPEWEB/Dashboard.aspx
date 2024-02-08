<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SPEWEB.Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="<%= this.ResolveUrl("~/Scripts/highcharts.js") %>"></script>
    <script src="<%= this.ResolveUrl("~/Scripts/highchartexporting.js") %>"></script>

    <style>

        .top-panel {
  width: 100%;
  
}

.top-panel::before{
  content: 'Scroll Down \2193';
  color: red;
  position: absolute;
  opacity: 0.7;
  text-shadow: 0 0 6px rgba(0,0,0,0.5);
    font-size: 25px;
    width: 156px;
  height: 40px;
  line-height: 40px;
  text-align: center;
  border-radius: 10px;
  bottom: 20px;
  left: 50%;
  margin-left: -30px;
  animation: bounce 1s ease infinite;
}

@keyframes bounce {
  50% {
    transform: translateY(-50%);
  }
  100% {
    transform: translateY(0);
  }
}



        /*This use for Active project tab  start here --- 6-10-2020*/
        .nav-tab-project {
            background-color: #95a5a6;
            border-radius: .5rem !important;
            /*border-top-right-radius: .5rem !important;
    border-bottom-right-radius: .5rem !important;
     border-top-left-radius: .5rem !important;
    border-bottom-left-radius: .5rem !important;*/
        }

        .nav-fill {
            height: 45px;
        }

        .nav-tabs {
            border-bottom: none !important;
        }

            #tabs .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
                /*color: #336683 !important;*/
                /*background-color: #a4d6ef !important;*/
                /*border-color: transparent transparent #f3f3f3;*/
            }

        #tabs .nav-tabs .nav-link {
            /*border: 1px solid transparent;*/
            /*color: #eee;*/
            font-size: 8px;
            /*background-color: #336683;
    border: 1px solid #a4d6ef;*/
            /*color: #a4d6ef;*/
        }

        .nav-item {
            border-radius: 0 !important;
        }

            .nav-item:last-child {
                border-top-right-radius: .5rem !important;
                border-bottom-right-radius: .5rem !important;
            }

            .nav-item:first-child {
                border-top-left-radius: .5rem !important;
                border-bottom-left-radius: .5rem !important;
            }

        .nav-tabs .nav-link.active {
            /*background-color: #a4d6ef !important;*/
        }

        /*This use for Active project tab  End here -- 6-10-2020*/

        .mb-5 {
            margin-bottom: -1rem !important;
        }

        .metric-row_m {
            margin-bottom: -0.75rem;
        }
    </style>

    <div class="page">
        <!-- .page-inner -->
        <div class="page-inner">
            <!-- .page-title-bar -->
            <header class="page-title-bar">
                <div class="d-flex flex-column flex-md-row">
                    <div class="lead">
                        <span id="UserName" runat="server" class="font-weight-bold"></span>

                    </div>
                    <%--<div class="ml-auto">
                        <!-- .dropdown -->
                        <div class="dropdown">
                            <button class="btn btn-secondary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>My Shortcut</span> <i class="fa fa-fw fa-caret-down"></i></button>
                            <!-- .dropdown-menu -->
                            <div class="dropdown-menu dropdown-menu-right dropdown-menu-md stop-propagation">
                                <div class="dropdown-arrow"></div>
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpToday" name="dpFilter" data-start="2019/03/27" data-end="2019/03/27">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpToday"><span>Today</span> <span class="text-muted">27 Mar</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpYesterday" name="dpFilter" data-start="2019/03/26" data-end="2019/03/26">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpYesterday"><span>Yesterday</span> <span class="text-muted">Mar 26</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpWeek" name="dpFilter" data-start="2019/03/21" data-end="2019/03/27" checked>
                                    <label class="custom-control-label d-flex justify-content-between" for="dpWeek"><span>This Week</span> <span class="text-muted">Mar 21-27</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpMonth" name="dpFilter" data-start="2019/03/01" data-end="2019/03/27">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpMonth"><span>This Month</span> <span class="text-muted">Mar 1-31</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpYear" name="dpFilter" data-start="2019/01/01" data-end="2019/12/31">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpYear"><span>This Year</span> <span class="text-muted">2019</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpCustom" name="dpFilter" data-start="2019/03/27" data-end="2019/03/27">
                                    <label class="custom-control-label" for="dpCustom">Custom</label>
                                    <div class="custom-control-hint my-1">
                                        <!-- datepicker:range -->
                                        <input type="text" class="form-control" id="dpCustomInput" data-toggle="flatpickr" data-mode="range" data-disable-mobile="true" data-date-format="Y-m-d">
                                        <!-- /datepicker:range -->
                                    </div>
                                </div>
                                <!-- /.custom-control -->
                            </div>
                            <!-- /.dropdown-menu -->
                        </div>
                        <!-- /.dropdown -->
                    </div>--%>


                </div>
                               
                <div class="mb-5" id="EventNotice" runat="server">
                    <div class="col-12 py-4 " style="border: 1px solid #D6D8E1;">
                        <div class="row">
                            <!--Breaking box-->
                            <div class="col-md-2 col-lg-2 pr-md-0">
                                <div class="p-2 bg-primary text-white text-center breaking-caret"><span class="font-weight-bold">Notice/Events</span></div>
                            </div>
                            <!--end breaking box-->
                            <!--Breaking content-->
                            <div class="col-md-10 col-lg-10 pl-md-4 py-2">
                                <div class="breaking-box">
                                    <div id="carouselbreaking" class="carousel slide" data-ride="carousel">
                                        <!--breaking news-->
                                        <div class="carousel-inner " id="EventCaro" runat="server">
                                            <!--post-->

                                            <!--post-->
                                            <%--<div class="carousel-item active">
                                    <div class="row">
                                 <div class="col-md-2">
                                         <a  href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/"><span class="position-relative mx-2 badge badge-primary rounded-0">Automotive</span></a> 
                                  </div>
                                     <div class="col-md-9"  >
                                   <a class="label font-size-sm" href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/">যখন উপাসনা শেষ হবে, তোমরা জমিনে ছড়িয়ে পড়বে এবং আল্লাহর দান অনুসন্ধানে ব্যাপৃত থাকবে</a>
                                 </div>
                                        </div>
                                    </div>--%>
                                            <!--post-->
                                            <%--<div class="carousel-item">
                                    <a href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/"><span class="position-relative mx-2 badge badge-primary rounded-0">Football</span></a> <a class="text-white" href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/">World cup 2050 will release in Dubay</a>
                                </div>--%>
                                            <!--post-->
                                            <%--<div class="carousel-item">
                                    <a href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/"><span class="position-relative mx-2 badge badge-primary rounded-0">Beauty</span></a> <a class="text-white" href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/">Lemon make your skin fresh and glowing</a>
                                </div>--%>
                                            <!--post-->
                                            <%--<div class="carousel-item active">
                                    <a href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/"><span class="position-relative mx-2 badge badge-primary rounded-0">Sport</span></a> <a class="text-white" href="https://bootstrap.news/bootstrap-4-template-news-portal-magazine/">5 Takeaways From Elon Musk’s Interview With The Times About Tesla</a>
                                </div>--%>
                                        </div>
                                        <!--end breaking news-->

                                        <!--navigation slider-->
                                        <div class="navigation-box p-2 d-none d-sm-block">
                                            <!--nav left-->
                                            <a class="carousel-control-prev text-primary" href="#carouselbreaking" role="button" data-slide="prev">
                                                <i class="fa fa-angle-left" aria-hidden="true"></i>
                                                <span class="sr-only">Previous</span>
                                            </a>
                                            <!--nav right-->
                                            <a class="carousel-control-next text-primary" href="#carouselbreaking" role="button" data-slide="next">
                                                <i class="fa fa-angle-right" aria-hidden="true"></i>
                                                <span class="sr-only">Next</span>
                                            </a>
                                        </div>
                                        <!--end navigation slider-->
                                    </div>
                                </div>
                            </div>
                            <!--end breaking content-->
                        </div>
                    </div>
                </div>
            </header>
            <!-- /.page-title-bar -->
            <!-- .page-section -->
            <div class="page-section">
                <!-- .section-block -->
                <div class="section-block" id="CounterPanel" runat="server">
                    <!-- metric row -->
                    <div class="metric-row_m metric-row">
                        <div class="col-lg-9">
                            <div class="metric-row metric-flush">
                                <!-- metric column -->
                                <div class="col">
                                    <!-- .metric -->
                                    <a href="" target="_blank" runat="server" id="todaywrk" class="metric metric-bordered align-items-center">
                                        <h2 class="metric-label">Today's Activities </h2>
                                        <%--Teams--%>
                                        <p class="metric-value h3">
                                            <sub><i class="fa fa-tasks"></i></sub> <span class="value" runat="server" id="todaywrkcount">8</span>
                                        </p>
                                    </a>
                                    <!-- /.metric -->
                                </div>
                                <!-- /metric column -->
                                <!-- metric column -->
                                <div class="col">
                                    <!-- .metric -->

                                    <a href="#" class="metric metric-bordered align-items-center" data-toggle="modal" data-target="#exampleModalDrawerRight">

                                        <h2 class="metric-label">User Offline </h2>
                                        <p class="metric-value h3">
                                            <sub><i class="fa fa-user-clock"></i></sub> <span class="value" id="offlineUserCount" runat="server"></span>
                                        </p>
                                    </a>
                                    <!-- /.metric -->
                                </div>
                                <!-- /metric column -->
                                <!-- metric column -->
                                <div class="col">
                                    <!-- .metric -->
                                    <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%>" target="_blank" class="metric metric-bordered align-items-center">
                                        <h2 class="metric-label">Master Order </h2>
                                        <p class="metric-value h3">
                                            <sub><i class="fa fa-cart-arrow-down"></i></sub> <span id="MasterOrder" runat="server" class="value">64</span>
                                        </p>
                                    </a>
                                    <!-- /.metric -->
                                </div>
                                <!-- /metric column -->
                            </div>
                        </div>
                        <!-- metric column -->
                        <div class="col-lg-3">
                            <!-- .metric -->
                            <a href="<%=this.ResolveUrl("~/F_33_Doc/GroupChat")%>" target="_blank" class="metric metric-bordered">
                                <div class="metric-badge">
                                    <span class="badge badge-lg badge-success"><span class="oi oi-media-record pulse mr-1"></span>ONGOING TASKS</span>
                                </div>
                                <p class="metric-value h3">
                                    <sub><i class="oi oi-timer"></i></sub><span class="value" id="TaskRemaining" runat="server">8</span>
                                </p>
                            </a>
                            <!-- /.metric -->
                        </div>
                        <!-- /metric column -->
                    </div>
                    <!-- /metric row -->
                </div>
                <!-- /.section-block -->
                  <div class="row" id="divUserPanel" runat="server">
                  
                    <!-- grid column -->
                    <div class="col-12 col-lg-12 col-xl-8">
                        <!-- .card -->
                        <div class="card card-fluid" style="min-height:380px">
                            <!-- .card-body -->
                            <div class="card-body">
                       <iframe height="600" src="Content/game.html" scrolling="no" frameborder="no" allowtransparency="true" loading="lazy" style="width: 100%;">
</iframe>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                      <div class="col-12 col-lg-6 col-xl-4">
                        <!-- .card -->
                        <div class="card card-fluid" style="min-height:640px">
                            <div class="card-body">
                                <h3 class="card-title">My ShortCut Link</h3>
                                 <ul class="menu" id="ShorCut" runat="server">
                                    </ul>
                            </div>
                        </div>
                    </div>
                      </div>
                <!-- grid row -->
                <div class="row" id="DashboardSummary" runat="server">
                  
                    <!-- grid column -->
                    <div class="col-12 col-lg-12 col-xl-4">
                        <!-- .card -->
                        <div class="card card-fluid" style="min-height:380px">
                            <!-- .card-body -->
                            <div class="card-body">
                                <h3 class="card-title mb-4">New Joiner Employee-Last 30 Days </h3>

                                <div class="chartjs" style="height: 250px">
                                    <%--292--%>
                                  
                                    <canvas id="completion-tasks1"></canvas>
                                </div>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                    <!-- grid column -->
                    <div class="col-12 col-lg-6 col-xl-4">
                        <!-- .card -->
                        <div class="card card-fluid" style="min-height:380px">
                            <!-- .card-body -->
                            <div class="card-body">
                                <h3 class="card-title">Order Performance-Last 30 Days </h3>
                                <%--Tasks Performance--%>
                                <!-- easy-pie-chart -->
                                <%--<div class="list-group list-group-flush list-group-divider" id="Empinfo" runat="server">

                                </div>--%>
                                <div class="list-group list-group-flush list-group-divider" id="TopOrderSt" runat="server">
                                </div>
                                <%-- <div class="text-center pt-3"> Coming Soon.........</div>--%>
                                <%-- <div class="chart-inline-group" style="height:214px">
                            <div class="easypiechart" data-toggle="easypiechart" data-percent="60" data-size="214" data-bar-color="#346CB0" data-track-color="false" data-scale-color="false" data-rotate="225"></div>
                            <div class="easypiechart" data-toggle="easypiechart" data-percent="50" data-size="174" data-bar-color="#00A28A" data-track-color="false" data-scale-color="false" data-rotate="225"></div>
                            <div class="easypiechart" data-toggle="easypiechart" data-percent="75" data-size="134" data-bar-color="#5F4B8B" data-track-color="false" data-scale-color="false" data-rotate="225"></div>
                          </div>
                                --%><!-- /easy-pie-chart -->
                                     <div class="top-panel"></div>
                            </div>

                            <!-- /.card-body -->
                            <!-- .card-footer -->
                            <%-- <div class="card-footer">
                        <div class="card-footer-item">
                          <i class="fa fa-fw fa-circle text-indigo"></i> 100% <div class="text-muted small"> Assigned </div>
                        </div>
                        <div class="card-footer-item">
                          <i class="fa fa-fw fa-circle text-purple"></i> 75% <div class="text-muted small"> Completed </div>
                        </div>
                        <div class="card-footer-item">
                          <i class="fa fa-fw fa-circle text-teal"></i> 60% <div class="text-muted small"> Active </div>
                        </div>
                      </div><!-- /.card-footer -->--%>
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                    <!-- grid column -->
                    <div class="col-12 col-lg-6 col-xl-4">
                        <!-- .card -->
                        <div class="card card-fluid" style="min-height:380px">
                            <div class="card-body">
                                <h3 class="card-title">Top Active User Today</h3>
                                <div class="list-group list-group-flush list-group-divider" id="TopActivity" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /grid column -->
                </div>
                <!-- /grid row -->
                <!-- card-deck-xl -->
                <div class="card-deck-xl" id="Grapharea" runat="server">
                    <!-- .card -->
                    <div class="card card-fluid pb-3">
                        <!--This use for Active project tab  start here --6-10-2020-->
                        <div class="card-header">At a Glance </div>
                        <!-- .lits-group -->
                        <div class="lits-group list-group-flush">
                            <!--This use for Active project tab  start here problem solved necessary-->
                            <div class="row">
                                <div>
                                    <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" Style="display: none;"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <!--This use for Active project tab  end here problem solved necessary-->
                            <div class="row">
                                <div class="col-xs-12 col-md-12 col-sm-12">

                                    <div class="tab-content py-3 px-3 px-sm-0" id="nav-tabContent" style="margin-left: 5px; margin-right: 5px;">

                                        <div style="border: 1px solid #D8D8D8" class="tab-pane fade show active" id="nav-Order" role="tabpanel" aria-labelledby="nav-Order-tab">
                                            <div id="SalesChart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-Shipment" role="tabpanel" aria-labelledby="nav-Shipment-tab" style="border: 1px solid #D8D8D8">
                                            <div id="CollChart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-Procurment" role="tabpanel" aria-labelledby="nav-Procurment-tab" style="border: 1px solid #D8D8D8">
                                            <div id="purchart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-Production" role="tabpanel" aria-labelledby="nav-Production-tab" style="border: 1px solid #D8D8D8">
                                            <div id="prodchart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-Accounts" role="tabpanel" aria-labelledby="nav-Accounts-tab" style="border: 1px solid #D8D8D8">
                                            <div id="accchart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                        </div>
                                        <%-- <div class="tab-pane fade" id="nav-Balance" role="tabpanel" aria-labelledby="nav-Balance-tab" style="border:1px solid #D8D8D8">
                               <div id="balsheetchart" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                </div> --%>
                                    </div>
                                    <nav class="nav-tab-project">
                                        <div class="nav nav-tabs nav-fill" id="nav-tab" role="tablist">
                                            <a class="nav-item nav-link active" id="nav-Order-tab" data-toggle="tab" href="#nav-Order" role="tab" aria-controls="nav-Order" aria-selected="true" title="Merchantdising">Merchandise</a>
                                            <a class="nav-item nav-link" id="nav-Shipment-tab" data-toggle="tab" href="#nav-Shipment" role="tab" aria-controls="nav-Shipment" aria-selected="false" title="Shipment">Shipment</a>
                                            <a class="nav-item nav-link" id="nav-Procurment-tab" data-toggle="tab" href="#nav-Procurment" role="tab" aria-controls="nav-Procurment" aria-selected="false" title="Procurement">Procurement</a>
                                            <a class="nav-item nav-link" id="nav-Production-tab" data-toggle="tab" href="#nav-Production" role="tab" aria-controls="nav-Production" aria-selected="false" title="Production">Production</a>
                                            <a class="nav-item nav-link" id="nav-Accounts-tab" data-toggle="tab" href="#nav-Accounts" role="tab" aria-controls="nav-Accounts" aria-selected="false" title="Accounts">Accounts</a>
                                            <%--<a class="nav-item nav-link" id="nav-Balance-tab" data-toggle="tab" href="#nav-Balance" role="tab" aria-controls="nav-Balance" aria-selected="false">Bal</a>--%>
                                        </div>
                                    </nav>
                                </div>
                            </div>


                            <%-- <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-purple">LT</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/team4.jpg" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">Looper Admin Theme</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 74% - Last update 1d </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-purple" role="progressbar" aria-valuenow="2181" aria-valuemin="0" aria-valuemax="100" style="width: 74%">
                              <span class="sr-only">74% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->
                      <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-indigo">SP</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/team1.jpg" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">Smart Paper</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 22% - Last update 2h </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-indigo" role="progressbar" aria-valuenow="867" aria-valuemin="0" aria-valuemax="100" style="width: 22%">
                              <span class="sr-only">22% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->
                      <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-yellow">OS</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/team2.png" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">Online Store</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 99% - Last update 2d </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-yellow" role="progressbar" aria-valuenow="6683" aria-valuemin="0" aria-valuemax="100" style="width: 99%">
                              <span class="sr-only">99% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->
                      <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-blue">BA</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/bootstrap.svg" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">Booking App</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 35% - Last update 4h </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-blue" role="progressbar" aria-valuenow="112" aria-valuemin="0" aria-valuemax="100" style="width: 35%">
                              <span class="sr-only">35% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->
                      <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-teal">SB</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/sketch.svg" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">SVG Icon Bundle</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 32% - Last update 1d </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-teal" role="progressbar" aria-valuenow="461" aria-valuemin="0" aria-valuemax="100" style="width: 32%">
                              <span class="sr-only">32% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->
                      <!-- .lits-group-item -->
                      <div class="list-group-item">
                        <!-- .lits-group-item-figure -->
                        <div class="list-group-item-figure">
                          <div class="has-badge">
                            <a href="page-project.html" class="tile tile-md bg-pink">SP</a> <a href="#team" class="user-avatar user-avatar-xs"><img src="assets/images/avatars/team4.jpg" alt=""></a>
                          </div>
                        </div><!-- .lits-group-item-figure -->
                        <!-- .lits-group-item-body -->
                        <div class="list-group-item-body">
                          <h5 class="card-title">
                            <a href="page-project.html">Syrena Project</a>
                          </h5>
                          <p class="card-subtitle text-muted mb-1"> Progress in 93% - Last update 8h </p><!-- .progress -->
                          <div class="progress progress-xs bg-transparent">
                            <div class="progress-bar bg-pink" role="progressbar" aria-valuenow="3981" aria-valuemin="0" aria-valuemax="100" style="width: 93%">
                              <span class="sr-only">93% Complete</span>
                            </div>
                          </div><!-- /.progress -->
                        </div><!-- .lits-group-item-body -->
                      </div><!-- /.lits-group-item -->--%>
                        </div>
                        <!-- /.lits-group -->
                    </div>
                    <!--This use for Active project tab  End here ----6-10-2020-->
                    <!-- /.card -->
                    <!-- .card -->
                    <div class="card card-fluid">
                        <div class="card-header">Overall Position </div>
                        <!-- .card-body -->
                        <div class="card-body">
                            <!--This use for Active Tasks: To-Dos tab  start here ---- 7-10-2020-->
                            <div class="row">
                                <div class="col-xs-12 col-md-12 col-sm-12">

                                    <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
                                        <div class="carousel-inner">
                                            <div class="carousel-item active">
                                                <%--    <img src="..." class="d-block w-100" alt="...">--%>
                                                <div id="SalesChart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                            </div>
                                            <div class="carousel-item">
                                                <%--<img src="..." class="d-block w-100" alt="...">--%>
                                                <div id="CollChart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                            </div>
                                            <div class="carousel-item">
                                                <div id="purchart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                                <%-- <img src="..." class="d-block w-100" alt="...">--%>
                                            </div>
                                            <div class="carousel-item">
                                                <div id="prodchart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                                <%-- <img src="..." class="d-block w-100" alt="...">--%>
                                            </div>
                                            <div class="carousel-item">
                                                <div id="accchart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                                <%-- <img src="..." class="d-block w-100" alt="...">--%>
                                            </div>
                                            <div class="carousel-item">
                                                <div id="balsheetchart1" style="width: 500px; height: 250px; margin: 0 auto"></div>
                                                <%-- <img src="..." class="d-block w-100" alt="...">--%>
                                            </div>
                                        </div>

                                        <a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">
                                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                            <span class="sr-only">Previous</span>
                                        </a>
                                        <a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">
                                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                            <span class="sr-only">Next</span>
                                        </a>
                                    </div>

                                </div>
                            </div>

                            <!--This use for Active Tasks: To-Dos tab  end here --- 7-10-2020-->






                            <%--   <!-- .todo-list -->
                      <div class="todo-list">
                        <!-- .todo-header -->
                        <div class="todo-header"> Looper Admin Theme (1/3) </div><!-- /.todo-header -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo1"> <label class="custom-control-label" for="todo1">Eat corn on the cob</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo2" checked> <label class="custom-control-label" for="todo2">Mix up a pitcher of sangria</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo3"> <label class="custom-control-label" for="todo3">Have a barbecue</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo4"> <label class="custom-control-label" for="todo4">Ride a roller coaster — <span class="text-red small">Overdue in 3 days</span></label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo-header -->
                        <div class="todo-header"> Smart Paper (0/2) </div><!-- /.todo-header -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo5"> <label class="custom-control-label" for="todo5">Bring a blanket and lie on the grass at an outdoor concert</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo6"> <label class="custom-control-label" for="todo6">Collect seashells at the beach</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo7"> <label class="custom-control-label" for="todo7">Swim in a lake</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                        <!-- .todo -->
                        <div class="todo">
                          <!-- .custom-control -->
                          <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="todo8"> <label class="custom-control-label" for="todo8">Get enough sleep!</label>
                          </div><!-- /.custom-control -->
                        </div><!-- /.todo -->
                      </div><!-- /.todo-list -->--%>
                        </div>
                        <!-- /.card-body -->
                        <!-- .card-footer -->
                        <div class="card-footer">
                            <a href="#" class="card-footer-item">View all <i class="fa fa-fw fa-angle-right"></i></a>
                        </div>
                        <!-- /.card-footer -->
                    </div>
                    <!-- /.card -->
                </div>
                <!-- /card-deck-xl -->
            </div>
            <!-- /.page-section -->
        </div>
        <!-- /.page-inner -->
        <!----Draw right ---->
        <div class="modal modal-drawer fade has-shown" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
            <!-- .modal-dialog -->
            <div class="modal-dialog modal-drawer-right" role="document">
                <!-- .modal-content -->
                <div class="modal-content">
                    <!-- .modal-header -->
                    <div class="modal-header modal-body-scrolled">
                        <h5 id="exampleModalDrawerRightLabel" class="modal-title">Offline User List </h5>
                    </div>
                    <!-- /.modal-header -->
                    <!-- .modal-body -->
                    <div class="modal-body">
                        <div class="section-block" id="OfflineUsers" runat="server">
                            <!-- grid row -->
                            <%--    <div class="row mb-4">
                    <!-- .col -->
                  <%--  <div class="col">
                      <!-- .has-clearable -->
                      <div class="has-clearable">
                        <button type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button> <input type="text" class="form-control" placeholder="Search">
                      </div><!-- /.has-clearable -->
                    </div>--%><!-- /.col -->
                            <!-- .col-auto -->
                            <%--<div class="col-auto">
                      <!-- invite members -->
                      <div class="dropdown">
                        <button class="btn btn-primary" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false"><i class="fas fa-user-plus mr-1"></i> Invite</button> <!-- .dropdown-menu -->
                        <div class="dropdown-menu dropdown-menu-right dropdown-menu-rich stop-propagation">
                          <div class="dropdown-arrow"></div>
                          <div class="dropdown-header"> Add members </div>
                          <div class="form-group px-3 py-2 m-0">
                            <input type="text" class="form-control" placeholder="e.g. @bent10" data-toggle="tribute" data-remote="assets/data/tribute.json" data-menu-container="#people-list" data-item-template="true" data-autofocus="true" data-tribute="true"> <small class="form-text text-muted">Search people by username or email address to invite them.</small>
                          </div>
                          <div id="people-list" class="tribute-inline stop-propagation"></div><a href="#" class="dropdown-footer">Invite member by link <i class="far fa-clone"></i></a>
                        </div><!-- /.dropdown-menu -->
                      </div><!-- /invite members -->
                    </div><!-- /.col-auto -->
                  </div>--%><!-- /grid row -->
                            <!-- .card -->
                            <!-- /.card -->
                            <!-- .card -->

                        </div>
                    </div>
                    <!-- /.modal-body -->
                    <!-- .modal-footer -->
                    <div class="modal-footer modal-body-scrolled">
                        <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                    </div>
                    <!-- /.modal-footer -->
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <!-- /.page -->
    <script language="javascript" type="text/javascript">
        function GetData() {
         //   alert("ddf");
            $.ajax({
                type: "POST",
                url: "<%= ResolveUrl("~/Dashboard.aspx/GetAllData") %>",
              // crossDomain: true,
            //    url: "Dashboard.aspx/GetAllData",
               data: '{dates: "'+ $('#<%=this.txtDate.ClientID%>').val() +'" }',
              //  data: '{dates: "25-May-2021" }',
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
                success: function (response) {
                    //alert(sfsfs);
       console.log(JSON.parse(response.d));
        var data = JSON.parse(response.d);
        // alert(data['sales'][0]['collamt']);
                    funMonthlyGraph(data);
                    GenerateGraph(data["elist"]);
    },
    failure: function (response) {
        //  alert(response);
        alert("f");
    }
            });
            }
        $(document).ready(function () {
            var roleid = <%=this.GetRoleId()%>;
            if (roleid != "01") {
                GetData();
            }
   
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
    });
function pageLoaded() {
    }

    </script>
    <script>
        function GenerateGraph(grpdata) {
           // console.log(grpdata);
           // grpdata = JSON.parse(grpdata);
            var datasource = [];
            var level = [];
            for (var i = 0; i < grpdata.length; i++) {
                level.push(grpdata[i]["deptname"]);
                datasource.push(grpdata[i]["empcount"]);
            }
            console.log(datasource);
            var data = {
                labels: level,//['21 Mar', '22 Mar', '23 Mar', '24 Mar', '25 Mar', '26 Mar', '27 Mar'],
                datasets: [{
                    backgroundColor: Looper.getColors('brand').indigo,
                    borderColor: Looper.getColors('brand').indigo,
                    data: datasource//[155, 65, 465, 265, 225, 325, 80]
                }] // init chart bar

            };
         //   document.getElementById('completion-tasks1')
            var canvas = $('#completion-tasks1')[0].getContext('2d');
          //  var canvas = document.getElementById('completion-tasks1');//.getContext('2d');

            var chart = new Chart(canvas, {
                type: 'horizontalBar', //bar//horizontalBar
                data: data,
                options: {
                    responsive: true,
                    legend: {
                        display: false
                    },
                    title: {
                        display: false
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: true,
                                drawBorder: false,
                                drawOnChartArea: false
                            },
                            ticks: {
                                maxRotation: 0,
                                maxTicksLimit: 3
                            }
                        }],
                        yAxes: [{
                            gridLines: {
                                display: true,
                                drawBorder: false
                            },
                            ticks: {
                                beginAtZero: true,
                                stepSize: 100
                            }
                        }]
                    }
                }
            });
        }


        //This use for Active project tab  Start here -- 6-10-2020
        /////--------------------------Month Graph-------------------------

        function funMonthlyGraph(data) {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });


            //for Carousel
            $('#purchart1').highcharts({


                chart: {
                    type: 'line'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Procurement TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Purchase',
                    data: [data['pur'][0]['ttlsalamt'], data['pur'][1]['ttlsalamt'], data['pur'][2]['ttlsalamt'], data['pur'][3]['ttlsalamt'], data['pur'][4]['ttlsalamt'], data['pur'][5]['ttlsalamt'], data['pur'][6]['ttlsalamt'], data['pur'][7]['ttlsalamt'], data['pur'][8]['ttlsalamt'], data['pur'][9]['ttlsalamt'], data['pur'][10]['ttlsalamt'], data['pur'][11]['ttlsalamt']],
                    color: '#f4429e'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['pur'][0]['tpayamt'], data['pur'][1]['tpayamt'], data['pur'][2]['tpayamt'], data['pur'][3]['tpayamt'], data['pur'][4]['tpayamt'], data['pur'][5]['tpayamt'], data['pur'][6]['tpayamt'], data['pur'][7]['tpayamt'], data['pur'][8]['tpayamt'], data['pur'][9]['tpayamt'], data['pur'][10]['tpayamt'], data['pur'][11]['tpayamt']],
                    color: '#b24942'
                }]
            });
            $('#purchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Procurement TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Purchase',
                    data: [data['pur'][0]['ttlsalamt'], data['pur'][1]['ttlsalamt'], data['pur'][2]['ttlsalamt'], data['pur'][3]['ttlsalamt'], data['pur'][4]['ttlsalamt'], data['pur'][5]['ttlsalamt'], data['pur'][6]['ttlsalamt'], data['pur'][7]['ttlsalamt'], data['pur'][8]['ttlsalamt'], data['pur'][9]['ttlsalamt'], data['pur'][10]['ttlsalamt'], data['pur'][11]['ttlsalamt']],
                    color: '#f4429e'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['pur'][0]['tpayamt'], data['pur'][1]['tpayamt'], data['pur'][2]['tpayamt'], data['pur'][3]['tpayamt'], data['pur'][4]['tpayamt'], data['pur'][5]['tpayamt'], data['pur'][6]['tpayamt'], data['pur'][7]['tpayamt'], data['pur'][8]['tpayamt'], data['pur'][9]['tpayamt'], data['pur'][10]['tpayamt'], data['pur'][11]['tpayamt']],
                    color: '#b24942'
                }]
            });
            //for Carousel
            $('#SalesChart1').highcharts({


                chart: {
                    type: 'line'
                },
                title: {
                    text: ''

                },

                subtitle: {
                    text: 'Order Vs Shipment FC(K)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                    //,
                    //labels: {
                    //    format: '{value} crore'
                    //}

                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Order',
                    data: [data['sales'][0]['ordramt'], data['sales'][1]['ordramt'], data['sales'][2]['ordramt'], data['sales'][3]['ordramt'], data['sales'][4]['ordramt'], data['sales'][5]['ordramt'], data['sales'][6]['ordramt'], data['sales'][7]['ordramt'], data['sales'][8]['ordramt'], data['sales'][9]['ordramt'], data['sales'][10]['ordramt'], data['sales'][11]['ordramt']],
                    color: '#1581C1'

                }, {

                    name: 'Shipment',
                    //color:red,
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#CA6621'
                }]
            });
            $('#SalesChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''

                },

                subtitle: {
                    text: 'Order Vs Shipment FC(K)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                    //,
                    //labels: {
                    //    format: '{value} crore'
                    //}

                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Order',
                    data: [data['sales'][0]['ordramt'], data['sales'][1]['ordramt'], data['sales'][2]['ordramt'], data['sales'][3]['ordramt'], data['sales'][4]['ordramt'], data['sales'][5]['ordramt'], data['sales'][6]['ordramt'], data['sales'][7]['ordramt'], data['sales'][8]['ordramt'], data['sales'][9]['ordramt'], data['sales'][10]['ordramt'], data['sales'][11]['ordramt']],
                    color: '#1581C1'

                }, {

                    name: 'Shipment',
                    //color:red,
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#CA6621'
                }]
            });
            //for Carousel
            $('#prodchart1').highcharts({


                chart: {
                    type: 'line'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Production TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Target',
                    data: [data['prod'][0]['bgdamt'], data['prod'][1]['bgdamt'], data['prod'][2]['bgdamt'], data['prod'][3]['bgdamt'], data['prod'][4]['bgdamt'], data['prod'][5]['bgdamt'], data['prod'][6]['bgdamt'], data['prod'][7]['bgdamt'], data['prod'][8]['bgdamt'], data['prod'][9]['bgdamt'], data['prod'][10]['bgdamt'], data['prod'][11]['bgdamt']],
                    color: '#96780a'

                }, {

                    name: 'Excution',
                    //color:red,
                    data: [data['prod'][0]['proamt'], data['prod'][1]['proamt'], data['prod'][2]['proamt'], data['prod'][3]['proamt'], data['prod'][4]['proamt'], data['prod'][5]['proamt'], data['prod'][6]['proamt'], data['prod'][7]['proamt'], data['prod'][8]['proamt'], data['prod'][9]['examtcore'], data['prod'][10]['proamt'], data['prod'][11]['proamt']],
                    color: '#990c4b'
                }]
            });
            $('#prodchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Production TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Target',
                    data: [data['prod'][0]['bgdamt'], data['prod'][1]['bgdamt'], data['prod'][2]['bgdamt'], data['prod'][3]['bgdamt'], data['prod'][4]['bgdamt'], data['prod'][5]['bgdamt'], data['prod'][6]['bgdamt'], data['prod'][7]['bgdamt'], data['prod'][8]['bgdamt'], data['prod'][9]['bgdamt'], data['prod'][10]['bgdamt'], data['prod'][11]['bgdamt']],
                    color: '#96780a'

                }, {

                    name: 'Excution',
                    //color:red,
                    data: [data['prod'][0]['proamt'], data['prod'][1]['proamt'], data['prod'][2]['proamt'], data['prod'][3]['proamt'], data['prod'][4]['proamt'], data['prod'][5]['proamt'], data['prod'][6]['proamt'], data['prod'][7]['proamt'], data['prod'][8]['proamt'], data['prod'][9]['examtcore'], data['prod'][10]['proamt'], data['prod'][11]['proamt']],
                    color: '#990c4b'
                }]
            });
            //for Carousel
            $('#accchart1').highcharts({


                chart: {
                    type: 'line'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Recipt',
                    data: [data['acc'][0]['cram'], data['acc'][1]['cram'], data['acc'][2]['cram'], data['acc'][3]['cram'], data['acc'][4]['cram'], data['acc'][5]['cram'], data['acc'][6]['cram'], data['acc'][7]['cram'], data['acc'][8]['cram'], data['acc'][9]['cram'], data['acc'][10]['cram'], data['acc'][11]['cram']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['acc'][0]['dram'], data['acc'][1]['dram'], data['acc'][2]['dram'], data['acc'][3]['dram'], data['acc'][4]['dram'], data['acc'][5]['dram'], data['acc'][6]['dram'], data['acc'][7]['dram'], data['acc'][8]['dram'], data['acc'][9]['dram'], data['acc'][10]['dram'], data['acc'][11]['dram']],
                    color: '#aa1811'
                }]
            });
            $('#accchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Recipt',
                    data: [data['acc'][0]['cram'], data['acc'][1]['cram'], data['acc'][2]['cram'], data['acc'][3]['cram'], data['acc'][4]['cram'], data['acc'][5]['cram'], data['acc'][6]['cram'], data['acc'][7]['cram'], data['acc'][8]['cram'], data['acc'][9]['cram'], data['acc'][10]['cram'], data['acc'][11]['cram']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['acc'][0]['dram'], data['acc'][1]['dram'], data['acc'][2]['dram'], data['acc'][3]['dram'], data['acc'][4]['dram'], data['acc'][5]['dram'], data['acc'][6]['dram'], data['acc'][7]['dram'], data['acc'][8]['dram'], data['acc'][9]['dram'], data['acc'][10]['dram'], data['acc'][11]['dram']],
                    color: '#aa1811'
                }]
            });

            ////Collection Bar chart 
            $('#CollChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Shipment Vs Realization(k)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Shipment',
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#42f47a'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [data['sales'][0]['collamt'], data['sales'][1]['collamt'], data['sales'][2]['collamt'], data['sales'][3]['collamt'], data['sales'][4]['collamt'], data['sales'][5]['collamt'], data['sales'][6]['collamt'], data['sales'][7]['collamt'], data['sales'][8]['collamt'], data['sales'][9]['collamt'], data['sales'][10]['collamt'], data['sales'][11]['collamt']],
                    color: '#454289'
                }]
            });
            //for Carousel
            $('#CollChart1').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Shipment Vs Realization(k)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Shipment',
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#42f47a'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [data['sales'][0]['collamt'], data['sales'][1]['collamt'], data['sales'][2]['collamt'], data['sales'][3]['collamt'], data['sales'][4]['collamt'], data['sales'][5]['collamt'], data['sales'][6]['collamt'], data['sales'][7]['collamt'], data['sales'][8]['collamt'], data['sales'][9]['collamt'], data['sales'][10]['collamt'], data['sales'][11]['collamt']],
                    color: '#454289'
                }]
            });
            //for Carousel
            $('#balsheetchart1').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Balance Sheet (%)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Parcentage'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.1f}%'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Balance Sheet",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Non-Current Asset",
                                "y": data['balshet'][0]['noncuram'],

                            },
                            {
                                "name": "Current Asset",
                                "y": data['balshet'][0]['curam'],

                            },
                            {
                                "name": "Equity",
                                "y": data['balshet'][0]['equityam'],

                            },
                            {
                                "name": "Non-Current Liabilities",
                                "y": data['balshet'][0]['noncurlia'],

                            },
                            {
                                "name": "Current Liabilities",
                                "y": data['balshet'][0]['curlia'],

                            }
                        ]
                    }
                ]
            });
           

        }

        // This use for Active project tab  End here -- 6-10-2020
    </script>
</asp:Content>

