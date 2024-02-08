<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SPEWEB.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.chzn-select').chosen({ search_contains: true });
        });


        function doSomething() {
            var cellMailYN = document.getElementById("cellEntryMailPass");
            if (document.getElementById("rdoBtnYes").checked == true) {
                cellMailYN.style.display = "None";
            } else {
                cellMailYN.style.display = "Block";
            }
        }

    </script>

        <style>
    label {
        display: inline-block;
        margin-bottom: 0rem;
        margin-left:0.25rem;
    }

    </style>

    <div class="page">
        <!-- .page-cover -->
        <header class="page-cover">
            <div class="text-center">
                <a href="UserProfile.aspx" class="user-avatar user-avatar-xl">
                    <img src="GetImage.aspx?ImgID=ImgUser" alt=""></a>
                <h2 class="h4 mt-2 mb-0" id="UserName" runat="server">Beni Arisandi </h2>
                <div class="my-1">
                    <i class="fa fa-star text-yellow"></i><i class="fa fa-star text-yellow"></i><i class="fa fa-star text-yellow"></i><i class="fa fa-star text-yellow"></i><i class="far fa-star text-yellow"></i>
                </div>
                <p class="text-muted" id="UDesignation" runat="server">Project Manager @CreativeDivision </p>
                <p>Huge fan of HTML, CSS and Javascript. Web design and open source lover. </p>
            </div>
            <!-- .cover-controls -->
            <div class="cover-controls cover-controls-bottom">
                <a href="MyShortCutLink.aspx?Module=" class="btn btn-light">My Shortcut</a> <a href="#" class="btn btn-light" data-toggle="modal" data-target="#followingModal">Change Profile</a>
            </div>
            <!-- /.cover-controls -->
        </header>
        <!-- /.page-cover -->

        <!-- .modal -->
        <div class="modal fade" id="followingModal" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
            <!-- .modal-dialog -->
            <div class="modal-dialog modal-dialog-scrollable" role="document">
                <!-- .modal-content -->
                <div class="modal-content">
                    <!-- .modal-header -->
                    <div class="modal-header">
                        <h6 id="followingModalLabel" class="modal-title"><span class="fa fa-user-tag"></span>Change Your Profile Picture </h6>
                    </div>
                    <!-- /.modal-header -->
                    <!-- .modal-body -->
                    <div class="modal-body px-0">

                        <div class="card-body">
                            <div id="dropzone" class="fileinput-dropzone">
                                <span>Drop files or click to upload.</span>
                                <!-- The file input field used as target for the file upload widget -->
                                <asp:FileUpload ID="fileuploaddropzone" runat="server"
                                    onchange="submitform();" />

                            </div>
                            <div id="progress" class="progress progress-xs rounded-0 fade">
                                <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>


                        </div>



                    </div>
                    <!-- /.modal-body -->
                    <!-- .modal-footer -->
                    <div class="modal-footer">
                        <label>New Profile Picture Effective After Logout and login</label>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <!-- /.modal-footer -->
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->

        <!-- /Following Modal -->
        <!-- .page-navs -->
        <nav class="page-navs">
            <!-- .nav-scroller -->
            <div class="nav-scroller">
                <!-- .nav -->
                <div class="nav nav-center nav-tabs">
                    <a class="nav-link" href="user-profile.html">Overview</a> <a class="nav-link" href="user-activities.html">Activities <span class="badge">16</span></a> <a class="nav-link active" href="user-teams.html">Teams</a> <a class="nav-link" href="user-projects.html">Projects</a> <a class="nav-link" href="user-tasks.html">Tasks</a> <a class="nav-link" href="user-profile-settings.html">Settings</a>
                </div>
                <!-- /.nav -->
            </div>
            <!-- /.nav-scroller -->
        </nav>
        <!-- /.page-navs -->
        <!-- .page-inner -->
        <div class="page-inner">
            <!-- .page-section -->
            <div class="page-section">
                <!-- .section-block -->
                <div class="section-block">
                    <div class="list-group-item d-flex justify-content-between align-items-center">
                        <span class="text-dark" id="UserName1" runat="server"></span>
                        <!-- .switcher-control -->

                        <label class="switcher-control switcher-control-lg" id="EventSTatus" runat="server">
                        </label>
                        <!-- /.switcher-control -->
                    </div>
                    <!-- metric row -->
                    <div class="metric-row">
                        <!-- metric column -->
                        <div class="col-12 col-sm-6 col-lg-3">
                            <!-- .metric -->
                            <div class="card-metric">
                                <div class="metric">
                                    <label class="label" for="ToDate">Select Your Default Season</label>
                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    <br />
                                    <asp:LinkButton ID="LbtnSaveDefultSeason" runat="server" OnClick="LbtnSaveDefultSeason_Click" CssClass="btn btn-sm btn-success">Set Default</asp:LinkButton>
                                </div>
                            </div>
                            <!-- /.metric -->
                        </div>
                        <!-- /metric column -->
                        <!-- metric column -->
                        <div class="col-12 col-sm-6 col-lg-6">
                            <!-- .metric -->
                            <div class="card-metric" style="min-height: 137px">
                                <div class="metric">
                                    <div>
                                        <b>Do You Want to use your default mail address for your every mail related work <span class="text-red">???</span></b>
                                    </div>
                                    <div class="mt-1">
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton ID="rdoBtnYes" runat="server" Text="Yes" GroupName="gender" onclick="doSomething()" ClientIDMode="Static" />
                                        </div>
                                        <div class="form-check form-check-inline">
                                            <asp:RadioButton ID="rdoBtnNo" runat="server" Text="No" GroupName="gender" onclick="doSomething()" ClientIDMode="Static" />
                                        </div>
                                    </div>

                                    <div class="mt-2 container" id="cellEntryMailPass" style="display: none">
                                        <div class="d-flex">
                                            <div class="form-group col-md-5 col-sm-12 col-lg-5">
                                                <asp:TextBox runat="server" type="email" class="form-control form-control-sm" ID="lblMail" placeholder="Enter email" Required="true" />
                                            </div>
                                            <div class="form-group col-md-5 col-sm-12 col-lg-5">
                                                <asp:TextBox runat="server" type="password" class="form-control form-control-sm" ID="lblPassword" placeholder="Password" Required="true" />
                                            </div>
                                            <div class="col-md-2 col-sm-12 col-lg-2">
                                                <asp:Button runat="server" ID="btnSubmitCngMail" class="btn btn-primary btn-sm" Style="float: right" Text="Submit" OnClick="btnSubmitCngMail_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-12 col-sm-6 col-lg-3">
                            <div class="card-metric" style="min-height: 137px">
                                <div class="metric">
                                    <p class="metric-value h3">
                                        <sub><i class="oi oi-timer"></i></sub><span class="value">8</span>
                                    </p>
                                    <h2 class="metric-label">Ongoing Tasks </h2>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-between align-items-center">
                        <h1 class="section-title mb-0">Achievement </h1>
                        <!-- .dropdown -->
                        <div class="dropdown">
                            <button class="btn btn-secondary" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span>This Month</span> <i class="fa fa-fw fa-caret-down"></i></button>
                            <!-- .dropdown-menu -->
                            <div class="dropdown-menu dropdown-menu-right dropdown-menu-md stop-propagation">
                                <div class="dropdown-arrow"></div>
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpToday" name="dpFilter" value="0">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpToday"><span>Today</span> <span class="text-muted">Mar 27</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpYesterday" name="dpFilter" value="1">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpYesterday"><span>Yesterday</span> <span class="text-muted">Mar 26</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpWeek" name="dpFilter" value="2">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpWeek"><span>This Week</span> <span class="text-muted">Mar 21-27</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpMonth" name="dpFilter" value="3" checked="">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpMonth"><span>This Month</span> <span class="text-muted">Mar 1-31</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpYear" name="dpFilter" value="4">
                                    <label class="custom-control-label d-flex justify-content-between" for="dpYear"><span>This Year</span> <span class="text-muted">2019</span></label>
                                </div>
                                <!-- /.custom-control -->
                                <!-- .custom-control -->
                                <div class="custom-control custom-radio">
                                    <input type="radio" class="custom-control-input" id="dpCustom" name="dpFilter" value="5">
                                    <label class="custom-control-label" for="dpCustom">Custom</label>
                                    <div class="custom-control-hint my-1">
                                        <!-- datepicker:range -->
                                        <input type="text" class="form-control flatpickr-input" data-toggle="flatpickr" data-mode="range" data-date-format="Y-m-d" readonly="readonly">
                                        <!-- /datepicker:range -->
                                    </div>
                                </div>
                                <!-- /.custom-control -->
                            </div>
                            <!-- /.dropdown-menu -->
                        </div>
                        <!-- /.dropdown -->
                    </div>
                </div>
                <!-- /.section-block -->
                <!-- grid row -->
                <div class="row">
                    <!-- grid column -->
                    <div class="col-xl-8">
                        <!-- .card -->
                        <div class="card card-body card-fluid">
                            <!-- legend -->
                            <ul class="list-inline small">
                                <li class="list-inline-item">
                                    <i class="fa fa-fw fa-circle text-teal"></i>Assigned tasks </li>
                                <li class="list-inline-item">
                                    <i class="fa fa-fw fa-circle text-purple"></i>Completed tasks </li>
                            </ul>
                            <!-- /legend -->
                            <div class="chartjs" style="height: 253px">
                                <div class="chartjs-size-monitor">
                                    <div class="chartjs-size-monitor-expand">
                                        <div class=""></div>
                                    </div>
                                    <div class="chartjs-size-monitor-shrink">
                                        <div class=""></div>
                                    </div>
                                </div>
                                <canvas id="canvas-achievement" style="display: block; width: 658px; height: 253px;" width="658" height="253" class="chartjs-render-monitor"></canvas>
                            </div>
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                    <!-- grid column -->
                    <div class="col-xl-4">
                        <!-- .card -->
                        <div class="card card-fluid">
                            <!-- .card-header -->
                            <div class="card-header">Overview </div>
                            <!-- /.card-header -->
                            <!-- .card-body -->
                            <div class="card-body">
                                <dl class="d-flex justify-content-between">
                                    <dt class="text-left">
                                        <span class="mr-2">Revenue</span> <small class="text-success"><i class="fa fa-caret-up"></i>87%</small>
                                    </dt>
                                    <dd class="text-right mb-0">
                                        <strong>17,400</strong> <small class="text-muted">USD</small>
                                    </dd>
                                </dl>
                                <dl class="d-flex justify-content-between mb-0">
                                    <dt class="text-left">
                                        <span class="mr-2">Projects</span> <small class="text-success"><i class="fa fa-caret-up"></i>24%</small>
                                    </dt>
                                    <dd class="text-right mb-0">
                                        <strong>5</strong>
                                    </dd>
                                </dl>
                            </div>
                            <!-- /.card-body -->
                            <!-- .card-body -->
                            <div class="card-body border-top">
                                <dl class="d-flex justify-content-between">
                                    <dt class="text-left">
                                        <span class="mr-2">Expenses</span> <small class="text-danger"><i class="fa fa-caret-down"></i>12%</small>
                                    </dt>
                                    <dd class="text-right mb-0">
                                        <strong>1,600</strong> <small class="text-muted">USD</small>
                                    </dd>
                                </dl>
                            </div>
                            <!-- /.card-body -->
                            <!-- .card-body -->
                            <div class="card-body border-top">
                                <div class="summary">
                                    <p class="text-left">
                                        <strong class="mr-2">Earnings</strong> <small class="text-success"><i class="fa fa-caret-up"></i>24%</small>
                                    </p>
                                    <p class="text-center">
                                        <strong class="h3">15,800</strong> <span class="text-muted">USD</span>
                                    </p>
                                </div>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                </div>
                <!-- /grid row -->
                <!-- grid row -->
                <div class="row">
                    <!-- grid column -->
                    <div class="col-xl-6">
                        <!-- .card -->
                        <div class="card card-fluid">
                            <!-- .card-header -->
                            <div class="card-header border-0">
                                <!-- .d-flex -->
                                <div class="d-flex align-items-center">
                                    <span class="mr-auto">Time Spent: Projects</span>
                                    <!-- .card-header-control -->
                                    <div class="card-header-control">
                                        <!-- .dropdown -->
                                        <div class="dropdown">
                                            <button type="button" class="btn btn-icon btn-light" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-fw fa-ellipsis-v"></i></button>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <div class="dropdown-arrow"></div>
                                                <a href="#" class="dropdown-item">Actions</a> <a href="#" class="dropdown-item">Goes here</a> <a href="#" class="dropdown-item">Remove</a>
                                            </div>
                                        </div>
                                        <!-- /.dropdown -->
                                    </div>
                                    <!-- /.card-header-control -->
                                </div>
                                <!-- /.d-flex -->
                            </div>
                            <!-- /.card-header -->
                            <!-- .table-responsive -->
                            <div class="table-responsive">
                                <!-- .table -->
                                <table class="table">
                                    <!-- thead -->
                                    <thead>
                                        <tr>
                                            <th style="min-width: 260px">Project </th>
                                            <th class="text-center">Progress </th>
                                            <th class="text-right" style="min-width: 142px">Hours Spent </th>
                                        </tr>
                                    </thead>
                                    <!-- /thead -->
                                    <!-- tbody -->
                                    <tbody>
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#" class="tile bg-blue text-white mr-2">BA</a> <a href="#">Booking App</a>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="badge bg-blue text-white">35%</span>
                                            </td>
                                            <td class="align-middle text-center">35:28 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#" class="tile bg-teal text-white mr-2">SB</a> <a href="#">SVG Icon Bundle</a>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="badge bg-teal text-white">32%</span>
                                            </td>
                                            <td class="align-middle text-center">20:39 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#" class="tile bg-pink text-white mr-2">SP</a> <a href="#">Syrena Project</a>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="badge bg-pink text-white">93%</span>
                                            </td>
                                            <td class="align-middle text-center">35:28 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#" class="tile bg-green text-white mr-2">MG</a> <a href="#">Mobile App Gex</a>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="badge bg-green text-white">100%</span>
                                            </td>
                                            <td class="align-middle text-center">35:10 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#" class="tile bg-red text-white mr-2">LB</a> <a href="#">Landing Page Booster</a>
                                            </td>
                                            <td class="align-middle text-center">
                                                <span class="badge bg-red text-white">100%</span>
                                            </td>
                                            <td class="align-middle text-center">21:21 </td>
                                        </tr>
                                        <!-- /tr -->
                                    </tbody>
                                    <!-- /tbody -->
                                </table>
                                <!-- /.table -->
                            </div>
                            <!-- /.table-responsive -->
                            <!-- .card-footer -->
                            <div class="card-footer">
                                <a href="#" class="card-footer-item">View report <i class="fa fa-fw fa-angle-right"></i></a>
                            </div>
                            <!-- /.card-footer -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                    <!-- grid column -->
                    <div class="col-xl-6">
                        <!-- .card -->
                        <div class="card card-fluid">
                            <!-- .card-header -->
                            <div class="card-header border-0">
                                <!-- .d-flex -->
                                <div class="d-flex align-items-center">
                                    <span class="mr-auto">Time Spent: Tasks</span>
                                    <!-- .card-header-control -->
                                    <div class="card-header-control">
                                        <!-- .dropdown -->
                                        <div class="dropdown">
                                            <button type="button" class="btn btn-icon btn-light" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-fw fa-ellipsis-v"></i></button>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <div class="dropdown-arrow"></div>
                                                <a href="#" class="dropdown-item">Actions</a> <a href="#" class="dropdown-item">Goes here</a> <a href="#" class="dropdown-item">Remove</a>
                                            </div>
                                        </div>
                                        <!-- /.dropdown -->
                                    </div>
                                    <!-- /.card-header-control -->
                                </div>
                                <!-- /.d-flex -->
                            </div>
                            <!-- /.card-header -->
                            <!-- .table-responsive -->
                            <div class="table-responsive">
                                <!-- .table -->
                                <table class="table">
                                    <!-- thead -->
                                    <thead>
                                        <tr>
                                            <th style="min-width: 259px">Task </th>
                                            <th>Due Date </th>
                                            <th>Estimate </th>
                                            <th>Worked </th>
                                        </tr>
                                    </thead>
                                    <!-- /thead -->
                                    <!-- tbody -->
                                    <tbody>
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#project" class="tile tile-circle bg-indigo text-white mr-1">SP</a> <a href="#task">Make lemonade from scratch</a>
                                            </td>
                                            <td class="align-middle">Apr 02 </td>
                                            <td class="align-middle">24:00 </td>
                                            <td class="align-middle">16:36 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#project" class="tile tile-circle bg-blue text-white mr-1">LT</a> <a href="#task">Mix up a pitcher of sangria</a>
                                            </td>
                                            <td class="align-middle">In 3 days </td>
                                            <td class="align-middle">04:00 </td>
                                            <td class="align-middle">03:36 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#project" class="tile tile-circle bg-yellow text-white mr-1">OS</a> <a href="#task">Ride a roller coaster</a>
                                            </td>
                                            <td class="align-middle">Tomorrow </td>
                                            <td class="align-middle">48:00 </td>
                                            <td class="align-middle">50:02 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#project" class="tile tile-circle bg-pink text-white mr-1">SP</a> <a href="#task">Dangle your feet off a dock</a>
                                            </td>
                                            <td class="align-middle">Apr 07 </td>
                                            <td class="align-middle">6:00 </td>
                                            <td class="align-middle">03:11 </td>
                                        </tr>
                                        <!-- /tr -->
                                        <!-- tr -->
                                        <tr>
                                            <td class="align-middle text-truncate">
                                                <a href="#project" class="tile tile-circle bg-yellow text-white mr-1">OS</a> <a href="#task">Have a picnic in the park</a>
                                            </td>
                                            <td class="align-middle">In 2 days </td>
                                            <td class="align-middle">12:00 </td>
                                            <td class="align-middle">08:36 </td>
                                        </tr>
                                        <!-- /tr -->
                                    </tbody>
                                    <!-- /tbody -->
                                </table>
                                <!-- /.table -->
                            </div>
                            <!-- /.table-responsive -->
                            <!-- .card-footer -->
                            <div class="card-footer">
                                <a href="#" class="card-footer-item">View report <i class="fa fa-fw fa-angle-right"></i></a>
                            </div>
                            <!-- /.card-footer -->
                        </div>
                        <!-- /.card -->
                    </div>
                    <!-- /grid column -->
                </div>
                <!-- /grid row -->
            </div>
            <!-- /.page-section -->
        </div>
    </div>


    <script>
        $(document).ready(function () {
                        $(".switcher-input").change(function () {
                            $.ajax({
                                type: "POST",
                                url: 'UserProfile.aspx/ChangeEventsStatus',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    //   $("#divResult").html("success");
                                },
                                error: function (e) {
                                    //  $("#divResult").html("Something Wrong.");
                                }
                            });
                        });
                    });


    </script >

</asp:Content>
