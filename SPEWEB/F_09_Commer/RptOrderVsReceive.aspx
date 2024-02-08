<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptOrderVsReceive.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptOrderVsReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        let itmArr = [];
        let qty = 0;

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.ddlCurrency.ClientID %>').change(function () {
                // alert(this.value);

                $.ajax({
                    type: "POST",
                    url: "RptOrderVsReceive.aspx/GetCurRate",
                    data: "{'curcode':'" + this.value + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(JSON.parse(response.d));

                        $('#<%=this.lblConRate.ClientID %>').val(JSON.parse(response.d));// = JSON.parse(response.d);

                    },
                    failure: function (response) {
                        //  alert(response);
                        alert("Sorry!");
                    }
                });
            });


        });

        <%--function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvBMvRev.ClientID %>");

            var i = 0
            $('#<%=gvBMvRev.ClientID %>').find("input:checkbox").each(function () {
                // console.log(tblData1.rows[i].style.display);
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
        }--%>

        function Search_Gridview(srcTxt, cellNr) {

            //alert(cellNr);

            var tblData;
            var sumtype = $("#ddlSummary").val();

            if (cellNr == 3) {
                if (sumtype == 1) {
                    cellNr = 1;
                }
                else if (sumtype == 2) {
                    cellNr = 1;
                }
            }

            else if (cellNr == 15) {
                if (sumtype == 1) {
                    cellNr = 11;
                }
                else if (sumtype == 2) {
                    cellNr = 12;
                }
            }

            var strData = srcTxt.value.toLowerCase().split(" ");

            tblData = document.getElementById("<%=gvBMvRev.ClientID %>");


            var rowData;

            for (var i = 0; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';

                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0) {
                        styleDisplay = '';
                    }
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }

                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function pageLoaded() {

            $(function () {
                $('[id*=ddlBuyer]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 250

                })
                $('[id*=ddlGroup]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 250

                })
                $('[id*=DdlCustomer2]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 250

                })
                $(".Multidropdown button").addClass("multiselect dropdown-toggle btn btn-default btn-sm");
            });

            var gv1 = $('#<%=this.gvOrderStatus.ClientID %>');
            gv1.Scrollable({
                ScrollHeight: 500,
            });
            var gv2 = $('#<%=this.gvBMvRev.ClientID %>');
            gv2.Scrollable({
                ScrollHeight: 500,
            });

            $('.chzn-select').chosen({ search_contains: true });

            //    var startv = 0;
            //    var endv = 100;
            //    var selector = document.getElementById('keypress');
            //    var input0 = document.getElementById('LowerValue');
            //    var input1 = document.getElementById('UperValue');
            //    var ExitLower = document.getElementById('ExitLower').value;
            //    if (ExitLower != "" && ExitLower > 0) {
            //        input0 = document.getElementById('ExitLower');
            //        startv = ExitLower;
            //    }
            //    var ExitUper = document.getElementById('ExitUper').value;
            //    if (ExitUper != "" && ExitUper > 0) {
            //        input1 = document.getElementById('ExitUper');
            //        endv = ExitUper;
            //    }
            //    console.log(input0);

            //    var inputs = [input0, input1];

            //    // Initializing the slider and linking the input
            //    noUiSlider.create(selector, {
            //        start: [startv, endv],
            //        connect: true,
            //        direction: 'ltr',
            //        tooltips: [true, wNumb({ decimals: 1 })],
            //        range: {
            //            'min': [0],
            //            '10%': 10,
            //            '50%': 50,
            //            '80%': 80,
            //            'max': 100
            //        }
            //    });

            //    selector.noUiSlider.on('update', function (values, handle) {
            //        inputs[handle].value = values[handle];
            //    });

            //    // Listen to keypress on the input
            //    var setSliderHandle = function setSliderHandle(i, value) {
            //        var r = [null, null];
            //        r[i] = value;
            //        selector.noUiSlider.set(r);
            //    };

            //    // Listen to keydown events on the input field.
            //    inputs.forEach(function (input, handle) {
            //        input.addEventListener('change', function () {
            //            setSliderHandle(handle, this.value);
            //        });

            //        input.addEventListener('keydown', function (e) {
            //            var values = selector.noUiSlider.get();
            //            var value = Number(values[handle]);
            //            // [[handle0_down, handle0_up], [handle1_down, handle1_up]]
            //            var steps = selector.noUiSlider.steps();
            //            // [down, up]
            //            var step = steps[handle];
            //            var position = void 0;
            //            // 13 is enter,
            //            // 38 is key up,
            //            // 40 is key down.
            //            switch (e.which) {
            //                case 13:
            //                    setSliderHandle(handle, this.value);
            //                    break;
            //                case 38:
            //                    // Get step to go increase slider value (up)
            //                    position = step[1];
            //                    // false = no step is set
            //                    if (position === false) {
            //                        position = 1;
            //                    }
            //                    // null = edge of slider
            //                    if (position !== null) {
            //                        setSliderHandle(handle, value + position);
            //                    }
            //                    break;
            //                case 40:
            //                    position = step[0];
            //                    if (position === false) {
            //                        position = 1;
            //                    }
            //                    if (position !== null) {
            //                        setSliderHandle(handle, value - position);
            //                    }
            //                    break;
            //            }
            //        });
            //    });

        }

        function CheckNotice() {
            var r = confirm("Do you want to Make New Requistion?");
            console.log(r);
            if (r == true) {
                /// $(this).attr('disabled', 'disabled');
                $('#exampleModalDrawerRight').modal('hide');
                return r;
                console.log("fired");
            }
            else {
                $('#exampleModalDrawerRight').modal('hide');
                return r;
                console.log("not fired");

            }
            return r;
        }
        //function ShowModal() {
        //    $('#exampleModalDrawerRight').modal('toggle');
        //}

        function OpenModal(stockqty) {

            if (stockqty) {
                BindCurrentStockQty(stockqty);
            }

            $('#PODetailsModal').modal('show');
        }
        function OpenModalReq(stockqty) {



            $('#REQDetailsModal').modal('show');
        }

        function BindCurrentStockQty(stockqty) {
            document.getElementById("btnCurrentStk").innerText = stockqty.toLocaleString(undefined, { minimumFractionDigits: 2 });
        }

        function closeModal() {

            $('#PODetailsModal').modal('hide');
        }

        function closeModalREQ() {

            $('#REQDetailsModal').modal('hide');
        }


        function SelectAllCheckboxes(chk) {


            itmArr = [];
            $("#lstSelectedItems").empty();
            var tblData1 = document.getElementById("<%=gvBMvRev.ClientID %>");
            var i = 0;

            $('#<%=gvBMvRev.ClientID %>').find("input:checkbox").each(function () {
                // console.log(tblData1.rows[i].style.display);

                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {

                    if (this != chk) {
                        this.checked = chk.checked;

                        let itmName = this.parentElement.parentElement.cells[3].querySelector(".itm-name").innerText;
                        let itmQty = parseFloat(this.parentElement.parentElement.cells[9].querySelector(".qty").innerText.replaceAll(",", ""))
                        itmQty = isNaN(itmQty) ? 0 : itmQty;

                        qty += itmQty;

                        let itmObj = { name: "", qty: 0 };
                        itmObj.name = itmName;
                        itmObj.qty = itmQty;

                        let isExist = false;
                        let index = 0;

                        itmArr.forEach(item => {
                            if (item.name == itmObj.name) {
                                index = itmArr.indexOf(item);
                                isExist = true;
                            }
                        });

                        if (chk.checked == true) {
                            if (isExist) {
                                itmArr[index].qty += itmObj.qty;
                            } else {
                                itmArr.push(itmObj);
                            }
                        } else {
                            itmArr = [];
                            $("#lstSelectedItems").empty();
                        }
                    }
                }
                i = i + 1;
            });

            let ttlQtyNode = document.querySelector("#selectedTotal");
            if (chk.checked) {
                //const sum = itmArr.reduce((accumulator, object) => {
                //    //console.log(accumulator, object);
                //    return accumulator + object.qty;
                //}, 0);
                ttlQtyNode.innerHTML = qty;
            }
            else {
                qty = 0;
                ttlQtyNode.innerHTML = qty;
            }

            let itmList = "";

            itmArr.forEach(item => {
                itmList += `
                    <li class="list-group-item d-flex justify-content-between align-items-center bg-twitter text-light border border-bottom border-light py-2 small">${item.name}
                        <span class="badge badge-light badge-pill text-dark itm-qty">${item.qty}</span>
                    </li>
                `
            });

            $("#lstSelectedItems").append(itmList);

        }

        function CountSelectedItemQty(chk) {

            let itmName = chk.parentElement.parentElement.cells[3].querySelector(".itm-name").innerText;
            let itmQty = parseFloat(chk.parentElement.parentElement.cells[9].querySelector(".qty").innerText.replaceAll(",", ""))
            itmQty = isNaN(itmQty) ? 0 : itmQty;

            let itmObj = { name: "", qty: 0 };

            $("#lstSelectedItems").empty();

            if (chk.checked) {
                qty += itmQty;

                itmObj.name = itmName;
                itmObj.qty = itmQty;

                let isExist = false;
                let index = 0;

                itmArr.forEach(item => {
                    if (item.name == itmObj.name) {
                        index = itmArr.indexOf(item);
                        isExist = true;
                    }
                });

                if (isExist) {
                    itmArr[index].qty += itmObj.qty;
                } else {
                    itmArr.push(itmObj);
                }

            } else {
                qty -= itmQty;

                itmObj.name = itmName;
                itmObj.qty = itmQty;

                itmArr.forEach(item => {
                    if (item.name == itmObj.name) {
                        let index = itmArr.indexOf(item);
                        item.qty -= itmQty;

                        if (itmArr[index].qty <= 0) {
                            itmArr.splice(index, 1);
                        }
                    }
                });
            }

            let ttlQtyNode = document.querySelector("#selectedTotal");
            ttlQtyNode.innerHTML = qty;

            let itmList = "";

            itmArr.forEach(item => {
                itmList += `
                    <li class="list-group-item d-flex justify-content-between align-items-center bg-twitter text-light border border-bottom border-light py-2 small">${item.name}
                        <span class="badge badge-light badge-pill text-dark itm-qty">${item.qty}</span>
                    </li>
                `
            });

            $("#lstSelectedItems").append(itmList);
        }


        function ResetSelectedItems() {
            itmArr = [];
            qty = 0;
        }

    </script>

    <style>
        #PODetailsModal .modal-dialog {
            max-width: 100% !important;
        }

        .Multidropdown ul {
            top: -47px !important;
        }

        .Multidropdown b.caret {
            display: none !important;
        }

        .Multidropdown ul.dropdown-menu {
            min-width: 21rem;
        }

        .Multidropdown .multiselect-container > li > a > label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 3px 2px 3px 2px;
            font-size: 12px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chk-summary label {
            margin-left: 4px;
        }

        .progress-bar {
            background-color: #f73535;
        }

        .font-size-11 {
            font-size: 11px !important;
        }
    </style>

    <style type="text/css">
        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }

        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background: #fff;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }



        .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        /* for interface*/

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 130px;
            font-size: 11px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 4px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 8px;
            padding-bottom: 6px;
            border-radius: 0px 15px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: uppercase;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #34495E;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }

        .orange {
            background-color: #F39C12;
        }

        .red {
            background-color: #E74C3C;
        }

        .purple {
            background-color: #8E44AD;
        }

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            color: rgba(255, 255, 255, 0.7);
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

            <div class="card card-fluid mb-1">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-1" runat="server" id="divfDate">
                            <div class="form-group">
                                <label class="label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divtDate">
                            <div class="form-group">
                                <label class="label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divddlSummary">
                            <div class="form-group">
                                <label class="label" for="ToDate">Summary</label>
                                <asp:DropDownList ID="ddlSummary" CssClass="form-control form-control-sm" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlSummary_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Default</asp:ListItem>
                                    <asp:ListItem Value="1">Materials</asp:ListItem>
                                    <asp:ListItem Value="2">Specification</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="col-md-1" runat="server" id="lblplnSeason">
                            <div class="form-group">
                                <label class="label" for="ToDate">Season</label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="Div1" visible="false">
                            <div class="form-group">
                                <label class="label" for="Customer">Customer</label>
                                <asp:DropDownList ID="DdlCustomer" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <label runat="server" id="Label7">Customer(Multiple)</label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                <asp:ListBox ID="DdlCustomer2" SelectionMode="Multiple" CssClass="form-control multiselect-search" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" ></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="pnlDays" visible="false">
                            <div class="form-group">
                                <label class="label" for="ddlDays">Days</label>
                                <asp:DropDownList ID="ddlDays" CssClass="form-control form-control-sm chzn-select" runat="server">
                                    <asp:ListItem Value="" Text="Select Day"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="">
                            <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn-sm btn btn-primary" Style="margin-top: 29px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <label id="lblPage" runat="server" class="label" for="ddlUserName">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="900">900</asp:ListItem>
                                    <asp:ListItem Value="1500">1500</asp:ListItem>
                                    <asp:ListItem Value="5000">5000</asp:ListItem>
                                    <asp:ListItem Value="10000">10000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div runat="server" id="pnlBuyer" class="col-md-2">
                            <div class="form-group" runat="server" id="lblplntype">
                                <div class="row">
                                    <div class="col-4" style="flex: 0 0 50%; max-width: 50%;">
                                        <label class="label" for="ToBuyername" runat="server" id="lblType">Buyer Name</label>
                                    </div>
                                    <div class="col-6">
                                        <asp:CheckBox runat="server" ID="CheckPObal" AutoPostBack="true" OnCheckedChanged="CheckPObal_CheckedChanged" Text="PO Bal." />
                                    </div>
                                </div>

                                <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                    <asp:ListBox ID="ddlBuyer" SelectionMode="Multiple" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-1" id="ReqBtn" runat="server" visible="false">
                            <div class="form-group">
                                <asp:CheckBox ID="CheckReqbal" runat="server" AutoPostBack="true" OnCheckedChanged="CheckReqbal_CheckedChanged" Text="Req. Bal." />
                                <a href="#" class="btn btn-sm btn-warning" data-toggle="modal" data-target="#exampleModalDrawerRight" style="font-size: smaller">New Req? <%--<i class="fa fa-file-alt">--%></i></a>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" id="divddlCodeBook">
                            <label class="label" for="MatGroup">Mat. Group</label>
                            <asp:DropDownList runat="server" ID="ddlCodeBook" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1" runat="server" id="pnlSubGroup">
                            <label runat="server" id="lblSubGroup">Sub Group</label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                <asp:ListBox ID="ddlGroup" SelectionMode="Multiple" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>
                            </div>
                        </div>

                    </div>

                </div>
            </div>


            <div class="card card-fluid mt-0" style="min-height: 250px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewReqStatus" runat="server">

                            <div class="row">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="tbMenuWrp nav nav-tabs">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0"><div class='circle-tile'><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'><span class="fa fa-calendar"></span> Date Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="1"><div class='circle-tile'><div class='circle-tile-content green'><div class='circle-tile-description text-faded'><span class="fa fa-file-excel"></span> BOM Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="2"><div class='circle-tile'><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'><span class="fa fa-cart-plus"></span> Order Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="3"><div class='circle-tile'><div class='circle-tile-content green'><div class='circle-tile-description text-faded'><span class="fa fa-user"></span> Buyer Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="4"><div class='circle-tile'><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'><span class="fa fa-file"></span> Requision Type Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="5"><div class='circle-tile'><div class='circle-tile-content green'><div class='circle-tile-description text-faded'><span class="fa fa-magnet"></span> Material Type Wise</div></div></div></asp:ListItem>
                                                <asp:ListItem Value="6"><div class='circle-tile'><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'><span class="fa fa-cart-plus"></span> Store Wise</div></div></div></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                            </div>

                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderStatus" runat="server" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvOrderStatus_PageIndexChanging" ShowFooter="True"
                                        Width="510px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order No" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmlcdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbomid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDept" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqdat" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Material Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmatdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="240px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvspcfdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="240px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="Flgvreqqty" runat="server" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrecqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="Flgvrecqty" runat="server" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remaining Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvremqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="Flgvremqty" runat="server" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgreqtype" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Buyer Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgbuyername" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>



                                        </Columns>

                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />

                                    </asp:GridView>

                                </div>

                            </div>

                        </asp:View>

                        <asp:View ID="ViewOrderStatus" runat="server">
                            <div class="row">

                                <asp:GridView ID="gvBMvRev" runat="server" ClientIDMode="Static"
                                    onscroll="SetDivPosition()" AllowPaging="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea bomvspotable"
                                    AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvBMvRev_PageIndexChanging" ShowFooter="True">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdSlNo9" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdmlcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                                            <ItemStyle HorizontalAlign="Left" Width="110px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdbomid" runat="server" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                <asp:Label ID="LblRsircode" Visible="false" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                                <asp:Label ID="LblSpcfCod" Visible="false" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                                <asp:Label ID="LblRate" Visible="false" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rate")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdreqdat" runat="server" Font-Size="9px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtMatSrc" BackColor="Transparent" BorderStyle="None" runat="server" Width="160px" placeholder="Material Desc" onkeyup="Search_Gridview(this, 3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdmatdesc" runat="server" Font-Size="10px" CssClass="itm-name"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                                            <ItemStyle HorizontalAlign="Left" Width="160px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spcf Desc.">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSpeHeaddesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="160px" placeholder="Spcf Desc" onkeyup="Search_Gridview(this, 4)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdspcfdesc" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Stock Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvStockqty" runat="server" Font-Size="9px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvStockqty" runat="server" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="right" Width="50px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM Qty.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lgvBomqty" runat="server" Font-Size="9px" OnClick="lgvBomqty_Click" CssClass="text-flickr font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvbomqty" runat="server" Style="text-align: right;" Font-Size="10px" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req Qty.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lgvOrdreqqty" runat="server" Font-Size="9px" OnClick="lgvOrdreqqty_Click"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvOrdreqqty" runat="server" Style="text-align: right;" Font-Size="10px" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" />
                                            <ItemStyle HorizontalAlign="right" Width="55px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqBalqty" runat="server" Font-Size="9px" CssClass="qty"
                                                    Text='<%# ( Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomqty"))-Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty"))).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Flgvreqbalqty" runat="server" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="right" Width="50px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Projection Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPrjctionOrd" runat="server" Font-Size="9px" CssClass="qty"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "projorder")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="right" Width="50px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Qty.">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lgvPOqty" Font-Size="9px" OnClick="lgvPOqty_Click" runat="server" CssClass="text-twitter font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purordqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvPoqty" runat="server" Font-Size="10px" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                            <ItemStyle HorizontalAlign="right" Width="60px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTotalOrder" Font-Size="9px" runat="server" CssClass="font-weight-bold"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "projorder")) + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purordqty"))).ToString("#,##0.00;(#,##0.00); ") %>'>
                                                </asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPOBalqty" Font-Size="9px" runat="server"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty"))-(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "projorder")) + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purordqty")))).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvPoBalqty" runat="server" Font-Size="9px" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            <ItemStyle HorizontalAlign="right" Width="50px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rcv<br> Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdrecqty" runat="server" Font-Size="9px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvOrdrecqty" runat="server" Font-Size="10px" Style="text-align: right;" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="55px" />
                                            <ItemStyle HorizontalAlign="Right" Width="55px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rcv <br> Bal Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdremqty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="FlgvOrdremqty" runat="server" Style="text-align: right;" Font-Size="10px" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Buyer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdbuyername" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                            <ItemStyle HorizontalAlign="Left" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtDefaultSupplier" runat="server" ClientIDMode="Static" BackColor="Transparent" BorderStyle="None" Width="140px" placeholder="Default Supplier" onkeyup="Search_Gridview(this, 15)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgSuplname" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "defaultsupname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                            <ItemStyle HorizontalAlign="Left" Width="140px" Font-Size="10px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCol" runat="server" onclick="javascript:CountSelectedItemQty(this);" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <%--<asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />--%>
                                                <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />

                                </asp:GridView>



                            </div>

                        </asp:View>

                        <asp:View ID="ViewBomWiseMatSummary" runat="server">
                            <div class="row">
                                <asp:Label runat="server" ID="lblFGQty" CssClass="alert alert-success font-weight-bold" Visible="false"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvBomWise" runat="server" AllowPaging="True"
                                        CssClass="table-condensed table-hover table-bordered grvContentarea bomvspotable"
                                        AutoGenerateColumns="False" ShowFooter="True" AllowSorting="true" OnSorting="gvBomWise_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBWOrdSlNo" runat="server" Font-Bold="True"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                <ItemStyle Width="30px" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Name ⥮" SortExpression="matdesc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWmatdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Left" Width="150px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification ⥮" SortExpression="spcfdesc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWspcfdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvLblTtlFooter" CssClass="font-weight-bold" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Left" Width="300px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmatunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matunit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Center" Width="50px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BOM Qty ⥮" SortExpression="itmqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWitmqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlBomQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stock Qty ⥮" SortExpression="stockqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWitmqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlStkQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order Qty ⥮" SortExpression="ordqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWordqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlOrdQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Projection Order ⥮" SortExpression="prjectionord">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvProjOrdr" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prjectionord")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlProjOrd" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Order ⥮" SortExpression="ttlorder">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTtlOrdr" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlorder")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlOrd" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Shipment qty ⥮" SortExpression="shipqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvshipqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlShpQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Shipment bal. qty ⥮" SortExpression="shipbalqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvshipbalqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlSBalQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Receive Qty ⥮" SortExpression="rcvqty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWrcvqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlRcvQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Issue Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBWisuqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlIssQty" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BOM Bal.(Rec) ⥮" SortExpression="bombalrcv">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbombalrcv" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bombalrcv")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlBomRcv" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOM Bal.(PO) ⥮" SortExpression="bombalpo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbombalpo" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bombalpo")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlBomPO" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rec. Bal.(Issue)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbombalissue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bombalissue")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvTtlRcvBal" CssClass="font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Size="X-Small" />
                                                <ItemStyle HorizontalAlign="Right" Width="80px" Font-Size="X-Small" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />

                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:View>

                        <asp:View ID="ViewSesonWiseBOM" runat="server">
                            <div class="masonry-item col-lg-6 d-none">
                                <!-- .card -->
                                <section class="card card-fluid">
                                    <!-- .card-body -->
                                    <div class="card-body">
                                        <h3 class="card-title">Changing the slider by keypress </h3>
                                        <div class="nouislider-wrapper">
                                            <div id="keypress"></div>
                                        </div>
                                        <!-- grid row -->
                                        <div class="form-row">
                                            <!-- grid column -->
                                            <div class="col">
                                                <asp:TextBox ID="LowerValue" Style="" OnTextChanged="LowerValue_TextChanged" AutoPostBack="true" runat="server" ClientIDMode="Static"></asp:TextBox>

                                                <asp:TextBox ID="UperValue" Style="" AutoPostBack="true" OnTextChanged="UperValue_TextChanged" runat="server" ClientIDMode="Static"></asp:TextBox>





                                                <!-- /grid column -->
                                            </div>
                                            <asp:TextBox ID="ExitLower" Style="display: none;" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="ExitUper" Style="display: none;" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            <!-- /grid row -->
                                        </div>
                                        <!-- /.card-body -->
                                </section>
                                <!-- /.card -->
                            </div>
                            <!-- /.masonry-item -->
                            <!-- .masonry-item -->

                            <div class="row">
                                <asp:GridView ID="gvSesonWiseBom" runat="server" AllowPaging="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea bomvspotable"
                                    AutoGenerateColumns="False" ShowFooter="True"
                                    OnPageIndexChanging="gvSesonWiseBom_PageIndexChanging"
                                    AllowSorting="true" OnSorting="gvSesonWiseBom_Sorting">

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBWOrdSlNo" runat="server" Font-Bold="True"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM ID">
                                            <ItemTemplate>
                                                <asp:HyperLink Target="_blank" NavigateUrl='<%# ResolveUrl("~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))
                        +"&Ptype=import&dayid="+Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid"))+"&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+"&format=PDF") %>'
                                                    ID="lgvBomId" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                            <ItemStyle HorizontalAlign="Left" Width="90px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBomId" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bomdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMlcDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mldesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" />
                                            <ItemStyle HorizontalAlign="Left" Width="250px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="text-right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOrdrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="text-right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO (%)" SortExpression="popercnt">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPoPercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "popercnt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Font-Size="X-Small" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Upto PO Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUptoPoQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptodateqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Font-Size="X-Small" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Upto PO (%)" SortExpression="uptopercnt">
                                            <ItemTemplate>
                                                <asp:Label CssClass='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt"))>90)?"text-success":(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt"))<50)?"text-youtube":"" %>' ID="lgvUptoPoPrcnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" Font-Size="X-Small" />
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Up To Day" SortExpression="updaycount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUptoDayCoun" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "updaycount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Font-Size="X-Small" />
                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Progress">
                                            <ItemTemplate>
                                                <asp:Label ID="LblProgress" runat="server" Width="150px">
                                                    <div class="progress">
                                                        <div class='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt"))>90)?"bg-success text-white":(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt"))<50)?"bg-youtube":"bg-facebook text-white" %>' role="progressbar" style='<%# "width:"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt")).ToString("###0;(###0); ")+"%" %>' aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">
                                                            <span><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uptopercnt")).ToString("###0;(###0); ") %>%</span>
                                                        </div>
                                                    </div>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />

                                </asp:GridView>
                            </div>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>


            <div class="modal fade bd-example-modal-lg" id="PODetailsModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 id="exampleModalLabel" class="modal-title font-weight-light">
                                <span class="fa fa-info-circle mr-2"></span>BOM Wise Material Details
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">


                                    <asp:GridView ID="gvPoDetails" runat="server" AutoGenerateColumns="False" PageSize="15"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="BOM NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBomID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOM User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBomUser" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomuser")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREqID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqdate" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMaterial" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specifications">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpecifications" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSupplier" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPon" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPoDate" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "podate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPoRef" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poref")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvQty" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRate" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00000;(#,##0.00000); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selection">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSelection" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "selection")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Exp Delivery">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvExpDeDate" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUserName" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uername")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-9">

                                    <asp:GridView ID="gvMaterialDetails" runat="server" AutoGenerateColumns="False" PageSize="15"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="BOM NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatBomID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Article">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvArticle" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrdernO" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                                <ItemStyle Width="110px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOM User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatBomUser" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomuser")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOM Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatBomDat" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bomdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Componet">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatMaterial" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "component")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatMaterial" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle Width="250px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specifications">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatSpec" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFTotal" runat="server" Text="Total"
                                                        Width="250px" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="250px" />
                                                <ItemStyle Width="250px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatQty" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFQty" runat="server"
                                                        Width="70px" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle Width="70px" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatRate" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrat")).ToString("#,##0.00000;(#,##0.00000); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-3">
                                    <div class="list-group list-group-bordered mb-3" id="MaterialSuma" visible="false" runat="server">
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-success">
                                                    <i class="oi oi-chat"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Total Requistion </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="TotalReq" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-danger">
                                                    <i class="oi oi-data-transfer-upload"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Projection Requistion </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="Prjtionreq" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-warning">
                                                    <i class="oi oi-tags"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Requisition with Bom </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="ReqWithBom" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-info">
                                                    <i class="oi oi-cart"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Total Purchase Order </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="TotalPO" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-twitter">
                                                    <i class="oi oi-cart"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Projection Order </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="PrjctionORder" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-youtube">
                                                    <i class="oi oi-cart"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Order With BOM</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="OrderWithBOm" runat="server">
                                                </button>
                                            </div>
                                        </div>
                                        <div class="list-group-item">
                                            <div class="list-group-item-figure">
                                                <a href="#" class="tile tile-circle bg-amazon">
                                                    <i class="oi oi-cart"></i>
                                                </a>
                                            </div>
                                            <div class="list-group-item-body">Current Stock</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light" id="btnCurrentStk">
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:HyperLink runat="server" ID="lnkbtnExclDnld" CssClass="btn btn-sm btn-success text-light">
                                <i class="fa fa-file-excel mr-1"></i> Download Excel
                            </asp:HyperLink>
                            <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal">
                                Close
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade bd-example-modal-lg" id="REQDetailsModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 id="exampleModalLabelreq" class="modal-title font-weight-light">
                                <span class="fa fa-info-circle mr-2"></span>BOM Wise Material Details
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:GridView ID="gvREQDetails" runat="server" AutoGenerateColumns="False" PageSize="15"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="BOM NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQBomID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQREqID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQReqdate" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQMaterial" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specifications">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQSpecifications" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQQty" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQRate" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00000;(#,##0.00000); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQUserName" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uername")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                                <ItemStyle Width="100px" />
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
                        <div class="modal-footer">
                            <button type="button" class="btn btn-sm btn-danger" data-dismiss="modal">
                                Close
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal modal-drawer fade has-shown" data-backdrop="static" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-drawer-right" role="document" style="max-width: 700px !important;">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header modal-body-scrolled">
                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">Create Requistion</h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group">

                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Select Supplier"></asp:Label>

                                        <asp:DropDownList ID="ddlSupplier" runat="server" Style="width: 650px" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Select Store"></asp:Label>
                                        <asp:DropDownList ID="DDlStore" runat="server" CssClass=" form-control form-control-sm" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Department"></asp:Label>
                                        <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control form-control-sm " TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="MRF No"></asp:Label>
                                        <asp:TextBox ID="TxtMrfno" runat="server" CssClass="form-control form-control-sm" TabIndex="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Purchase Type"></asp:Label>
                                        <asp:DropDownList ID="ddlPurType" runat="server" CssClass=" form-control form-control-sm" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" Text="Currency:"></asp:LinkButton>
                                        <div class="input-group input-group-sm input-group-alt">
                                            <asp:DropDownList ID="ddlCurrency" ClientIDMode="Static" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            <div class="input-group-append">
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="input-group-text text-success" ToolTip="Create List" Target="_blank"
                                                    NavigateUrl="~/F_34_Mgt/AccConversion"><span class="fa fa-plus"></span></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" CssClass="label" Text="Rate:"></asp:Label>
                                        <asp:TextBox ID="lblConRate" ClientIDMode="Static" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                        <asp:TextBox ID="TxtRemarks" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm" TabIndex="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>--%>


                            <div class="col-md-6 px-0">

                                <p class="alert alert-secondary text-center font-weight-bold px-2">
                                    You have selected 
                                    <span style="font-size: 15px;" class="badge badge-primary px-2 py-1 mx-1" id="selectedTotal">0</span>
                                    quantity
                                </p>

                                <ul class="list-group " id="lstSelectedItems">
                                </ul>

                            </div>

                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer modal-body-scrolled">
                            <asp:LinkButton ID="LbtnCreateReq" ClientIDMode="Static" OnClientClick="return CheckNotice();" OnClick="LbtnCreateReq_Click" runat="server" CssClass="btn btn-sm btn-primary" TabIndex="3">Update</asp:LinkButton>

                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <script>               

</script>
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

