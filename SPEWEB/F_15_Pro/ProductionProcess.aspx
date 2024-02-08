<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionProcess.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CheckQty() {
            let recqty = document.getElementById("TextBoxRecQty").value;
            let passqty = document.getElementById("TxtPassQty").value;
            let repqty = document.getElementById("TextBoxRepQty").value;
            let reqqty = document.getElementById("TextBoxRejQty").value;
            let totalqty = (Number(passqty) + Number(repqty) + Number(reqqty));
            if (recqty != totalqty) {
                showContentFail('Sorry! You have Given WRONG Qty');
            }

        }

        function RejClean() {
            var passQty = parseInt(document.getElementById("TxtPassQty").value);
            var rejQty = parseInt(document.getElementById("TextBoxRejQty").value);

            passQty = isNaN(passQty) ? "0" : passQty;
            rejQty = isNaN(rejQty) ? "0" : rejQty;

            document.getElementById("TxtPassQty").value = passQty + rejQty;
            document.getElementById("TextBoxRejQty").value = 0;
        }

        function RepClean() {
            var passQty = parseInt(document.getElementById("TxtPassQty").value);
            var repQty = parseInt(document.getElementById("TextBoxRepQty").value);

            passQty = isNaN(passQty) ? "0" : passQty;
            repQty = isNaN(repQty) ? "0" : repQty;

            document.getElementById("TxtPassQty").value = passQty + repQty;
            document.getElementById("TextBoxRepQty").value = 0;
        }

        const compArr = [];
        const reasonArr = [];

        var tt = "";
        var nn = 0;

        var flag = true;

        var compCode = "";
        var reasonCode = "";

        function RejReasonNew() {

            event.preventDefault();

            RejReasonNew22();

            RejClean()

            tt = "";

            nn = 0;

            compArr.length = 0;

            var listBox = document.getElementById("ddlReason");

            $('#ddlReason').prop('selected', false);

            document.getElementById("qcmodalinf").innerHTML = tt;

            if (nn > 0) {
                document.getElementById("cellReasonList").style.display = "block";
            }
            else {
                document.getElementById("cellReasonList").style.display = "none";
            }

            document.getElementById("txtReasonCode").value = "";
            document.getElementById("txtComponent").value = "";
            document.getElementById("txtRejReasonQty").value = "";

            reasonCode = "";
            compCode = "";

        }

        function ddlReason_SelectedIndexChanged() {

            event.preventDefault();

            RejClean();

            var x = document.getElementById("ddlReason");
            var comp = document.getElementById("ddlComponent");

            for (var j = 0; j < x.options.length; j++) {

                compPlusReson = comp.options[comp.selectedIndex].innerHTML + "-" + x.options[j].text

                if (x.options[j].selected == true && !compArr.includes(compPlusReson)) {

                    compArr.push(compPlusReson);

                    nn++;

                    tt += "<tr><th>" + (nn) + "</th >"
                    tt += "<td>" + comp.options[comp.selectedIndex].innerHTML + "</td>"
                    tt += "<td>" + x.options[j].text + "</td>"
                    tt += "<td><input type='number' Class='form-control form-control-sm' id='rqty" + (nn) + "' style='width:150px;height:20px' onchange='UpdateReqQty()' min=" + 0 + "></td></tr>"

                    compCode += comp.options[comp.selectedIndex].value + ",";
                    reasonCode += x.options[j].value + ",";
                }
            }

            document.getElementById("txtReasonCode").value = reasonCode;
            document.getElementById("txtComponent").value = compCode;

            if (nn > 0) {
                document.getElementById("cellReasonList").style.display = "block";
            }
            else {
                document.getElementById("cellReasonList").style.display = "none";
            }

            document.getElementById("qcmodalinf").innerHTML = tt;

        }

        var totalReqQtyGlobal = 0;

        var totalRepQtyGlobal = 0;

        function UpdateReqQty() {

            var totalqtyval = 0;

            var rejReasson = "";

            for (var i = 0; i < nn; i++) {
                var id = "rqty" + (i + 1);

                var tblqty = parseFloat(document.getElementById(id).value);

                tblqty = isNaN(tblqty) ? "0" : tblqty;

                if (tblqty < 0) {
                    alert("Less than zero(0) is not allowed.");
                    return;
                }

                if (tblqty % 0.5 != 0) {
                    document.getElementById(id).value = 0;
                    return;
                }

                if (tblqty % 1 == 0.5 && tblqty > 1) {
                    document.getElementById(id).value = 0;
                    return;
                }

                rejReasson += String(tblqty) + ",";

                totalqtyval += parseFloat(tblqty);
            }

            rejReasson = rejReasson.substring(0, rejReasson.length - 1);

            var recqty = parseInt(document.getElementById("TextBoxRecQty").value);
            recqty = isNaN(recqty) ? 0 : recqty;

            var rejqty = parseInt(document.getElementById("TextBoxRejQty").value);
            rejqty = isNaN(rejqty) ? 0 : rejqty;

            if (recqty >= totalqtyval) {
                document.getElementById("TextBoxRejQty").value = totalqtyval;

                document.getElementById("TxtPassQty").value = recqty - (totalqtyval + totalRepQtyGlobal);

                totalReqQtyGlobal = totalqtyval

                document.getElementById("txtRejReasonQty").value = rejReasson;
            }
            else {
                alert("Total quantity have to be less than or equal to Rec Qty");
            }

        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////


        const compArr22 = [];
        const reasonArr22 = [];

        var tt22 = "";
        var nn22 = 0;

        var flag22 = true;

        var compCode22 = "";
        var reasonCode22 = "";


        function RejReasonNew22() {

            event.preventDefault();

            RepClean()

            tt22 = "";

            nn22 = 0;

            compArr22.length = 0;

            var listBox = document.getElementById("ddlReason22");

            $('#ddlReason22').prop('selected', false);

            document.getElementById("qcmodalinf22").innerHTML = tt22;

            if (nn22 > 0) {
                document.getElementById("cellReasonList22").style.display = "block";
            }
            else {
                document.getElementById("cellReasonList22").style.display = "none";
            }

            document.getElementById("txtReasonCode22").value = "";
            document.getElementById("txtComponent22").value = "";
            document.getElementById("txtRejReasonQty22").value = "";

            reasonCode22 = "";
            compCode22 = "";

        }

        function ddlReason_SelectedIndexChanged22() {

            event.preventDefault();

            RepClean()

            var x = document.getElementById("ddlReason22");
            var comp = document.getElementById("ddlComponent");

            for (var j = 0; j < x.options.length; j++) {

                compPlusReson22 = comp.options[comp.selectedIndex].innerHTML + "-" + x.options[j].text

                if (x.options[j].selected == true && !compArr22.includes(compPlusReson22)) {

                    compArr22.push(compPlusReson22);

                    nn22++;

                    tt22 += "<tr><th>" + (nn22) + "</th >"
                    tt22 += "<td>" + comp.options[comp.selectedIndex].innerHTML + "</td>"
                    tt22 += "<td>" + x.options[j].text + "</td>"
                    tt22 += "<td><input type='number' Class='form-control form-control-sm' id='rqty22" + (nn22) + "' style='width:150px;height:20px' onchange='UpdateReqQty22()' min=" + 0 + "></td></tr>"

                    compCode22 += comp.options[comp.selectedIndex].value + ",";
                    reasonCode22 += x.options[j].value + ",";
                }
            }

            document.getElementById("txtReasonCode22").value = reasonCode22;
            document.getElementById("txtComponent22").value = compCode22;

            if (nn22 > 0) {
                document.getElementById("cellReasonList22").style.display = "block";
            }
            else {
                document.getElementById("cellReasonList22").style.display = "none";
            }

            document.getElementById("qcmodalinf22").innerHTML = tt22;

        }



        function UpdateReqQty22() {

            var totalqtyval = 0;

            var rejReasson = "";

            for (var i = 0; i < nn22; i++) {
                var id = "rqty22" + (i + 1);

                var tblqty = parseFloat(document.getElementById(id).value);

                tblqty = isNaN(tblqty) ? "0" : tblqty;

                tblqty = isNaN(tblqty) ? "0" : tblqty;

                if (tblqty < 0) {
                    alert("Less than zero(0) is not allowed.");
                    return;
                }

                if (tblqty % 0.5 != 0) {
                    document.getElementById(id).value = 0;
                    return;
                }

                if (tblqty % 1 == 0.5 && tblqty > 1) {
                    document.getElementById(id).value = 0;
                    return;
                }

                rejReasson += String(tblqty) + ",";

                totalqtyval += parseFloat(tblqty);
            }

            rejReasson = rejReasson.substring(0, rejReasson.length - 1);

            var recqty = parseInt(document.getElementById("TextBoxRecQty").value);
            recqty = isNaN(recqty) ? 0 : recqty;

            var rejqty = parseInt(document.getElementById("TextBoxRejQty").value);
            rejqty = isNaN(rejqty) ? 0 : rejqty;

            if (recqty >= totalqtyval) {
                document.getElementById("TextBoxRepQty").value = totalqtyval;

                document.getElementById("TxtPassQty").value = recqty - (totalqtyval + totalReqQtyGlobal);

                totalRepQtyGlobal = totalqtyval;

                document.getElementById("txtRejReasonQty22").value = rejReasson;
            }
            else {
                alert("total quantity have to be less than or equal to rec qty");
            }

        }

        function OpenQCModal() {
            $('#qcmodal').modal('show');
        }

        function OpenBulkQCModal() {
            $('#myBulkQc').modal('show');
        }

        function CLoseMOdal() {

            var reasonQty = document.getElementById("txtRejReasonQty").value;
            var reasonQty22 = document.getElementById("txtRejReasonQty22").value;

            var bool = 0;

            if (nn > 0) {
                if (reasonQty.length == 0) {
                    bool = 1;
                }
            }
            if (nn22 > 0) {
                if (reasonQty22.length == 0) {
                    bool = 1;
                }
            }

            if (bool != 1) {
                var myArr = reasonQty.split(",");

                for (var i = 0; i < myArr.length; i++) {
                    if (myArr[i] == "0") {
                        bool = 1;
                        break;
                    }
                }
            }

            if (bool != 1) {
                var myArr22 = reasonQty22.split(",");

                for (var i = 0; i < myArr22.length; i++) {
                    if (myArr22[i] == "0") {
                        bool = 1;
                        break;
                    }
                }
            }

            if (bool == 1) {
                alert("Please fill up all Reason Quantity Correctly \nMust be greater than Zero (0)");
                event.preventDefault();
            } else {
                $('#qcmodal').modal('hide');
            }

        }

        function CLoseBulkQCModal() {
            $('#myBulkQc').modal('hide');
        }

        function SelectAllCheckboxes(chk) {

            var tblData1 = document.getElementById("<%=gvqc.ClientID %>");
            var i = 0

            $('#<%=gvqc.ClientID %>').find("input:checkbox").each(function () {

                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {

                    if (this != chk) {

                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
        }

        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=ddlsize]').multiselect({
                    includeSelectAllOption: true

                })
                $('[id*=ddlReason]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    maxHeight: 250
                })
            });
            $(".chzn-select").chosen();
            $('.chzn-select').chosen({ search_contains: true });
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        };



    </script>

    <style>
        .failreason .multiselect-container > li > a > label {
            padding: 1px;
        }

        .failreason .multiselect {
            width: 230px !important;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 56px;
            height: 30px;
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
                height: 22px;
                width: 22px;
                left: 4px;
                bottom: 4px;
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
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .multiselect {
            width: 170px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }

        .caret {
            display: none !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">P.Pro No</asp:Label>
                                <asp:TextBox ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-5 col-sm-5 col-lg-5">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnFindOrder" runat="server" CssClass="label" OnClick="imgbtnFindOrder_Click">Order No</asp:LinkButton>
                                <asp:DropDownList ID="ddlOrderNo" runat="server" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group " style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left mr-3" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group ">
                                <asp:Label ID="Label16" runat="server" CssClass="label">Req No</asp:Label>
                                <asp:DropDownList ID="ddlReqno" Visible="false" runat="server" CssClass="chzn-select form-control form-control-sm inputTxt" TabIndex="18"></asp:DropDownList>

                            </div>
                        </div>


                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">



                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="View1" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"
                                    Text="Start Date :"></asp:Label>

                                <asp:TextBox ID="txtDate" runat="server"
                                    CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>

                            <asp:GridView ID="gvStartPro" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="368px">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Style Description">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True" class="UpdateButton"
                                                Font-Size="12px" OnClick="lbtnFinalUpdate_Click">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleDes" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvordrQty" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Start Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvproQty" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </asp:View>

                        <asp:View ID="ViewTransfer" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" CssClass="label">Style/Article</asp:Label>
                                        <asp:DropDownList ID="ddlStyle" runat="server" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="label">Color</asp:Label>
                                        <asp:DropDownList ID="ddlcolor" runat="server" OnSelectedIndexChanged="ddlcolor_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" CssClass="label">Size</asp:Label><br />
                                        <asp:ListBox ID="ddlsize" runat="server" CssClass="form-control form-control-sm" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" CssClass="label">From Process</asp:Label>
                                        <asp:DropDownList ID="ddlFromProcess" runat="server" OnSelectedIndexChanged="ddlFromProcess_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="label">To Process</asp:Label>
                                        <asp:DropDownList ID="ddlToProcess" runat="server" OnSelectedIndexChanged="ddlToProcess_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">Manpower</asp:Label>
                                        <asp:TextBox ID="txtManpower" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="label">N.of Mac</asp:Label>
                                        <asp:TextBox ID="TxtMacqty" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="label">Transfer Date</asp:Label>
                                        <asp:TextBox ID="txttrnsDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttrnsDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttrnsDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="label">Hour</asp:Label>
                                        <asp:DropDownList ID="ddHour" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="label">Line</asp:Label>
                                        <asp:DropDownList ID="ddlLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="divMachine">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Machine</asp:Label>
                                        <asp:DropDownList ID="ddlMachine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-danger btn-sm pull-left" OnClick="lbtnShow_Click">Add</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnAddAllReq" runat="server" CssClass="btn btn-success btn-sm pull-left" OnClick="LbtnAddAllReq_Click">All Req</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnReqItemShow" OnClick="LbtnReqItemShow_Click" runat="server" CssClass="btn btn-sm btn-warning" Text="Expand"></asp:LinkButton>
                                    </div>
                                </div>


                            </div>
                            <div class="row">
                                <asp:GridView ID="gvProItem" runat="server" Visible="false"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Style Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcSumproDesc" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="130px" />
                                            <ItemStyle Width="110px" />
                                            <FooterStyle Width="110px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LbtnRecItemCalculate" OnClick="LbtnRecItemCalculate_Click" runat="server" CssClass="btn btn-xs btn-success">Adjust<span class="fa fa-sync-alt"></span></asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSSpecification" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:LinkButton ID="LbtnToClear2" OnClick="LbtnToClear1_Click" runat="server" CssClass="btn btn-xs btn-danger">Clear</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="80px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--      <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSSize" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" Width="80px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Rec Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSReQqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTotalReceived" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Processed">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvStkQty" runat="server" Style="text-align: right"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty"))-Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTotalOrdQty" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSIsuqtyqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTotalBalQty" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pass Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSMRRQty" runat="server" BorderColor="#ef5b5b" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsqty")).ToString("#,##0;(#,#0); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvRSumSMRRQty" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#CC0066" Width="70px"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                <br />
                                <br />

                                <asp:GridView ID="gvTransPro" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="368px">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserial" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Style Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyleDestrns" runat="server" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lblgvStyleid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnittrns" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                    Width="60px"></asp:Label>
                                                <asp:Label ID="lblColorid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSize" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                    Width="50px"></asp:Label>
                                                <asp:Label ID="lblgvSizeid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line/Floor">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvFloor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flordesc")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Machine">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMach" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LbtnToClear" OnClick="LbtnToClear_Click" runat="server" CssClass="btn btn-xs btn-danger">Clear</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Production Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProdhour" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "timedesc")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Process">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvToProcess" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostepdesc")) %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblgvToProcessid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostep")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Target / Recv Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRecQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterTemplate>
                                                <asp:Label ID="gvTargetQty" runat="server" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Processed<Br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvExecQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty"))-Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterTemplate>
                                                <asp:Label ID="gvProcQty" runat="server" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterTemplate>
                                                <asp:Label ID="gvBalQty" runat="server" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pass">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtrnsQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="gvPass" runat="server" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <%--
                                        <asp:TemplateField HeaderText="Rejection">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrejectionqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Repair">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtrepairqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                            </div>

                            </br> </br>
                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvproprocess" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="700px"
                                    OnRowDataBound="gvproprocess_RowDataBound">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyleDesr" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "styledesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")).Trim(): "") %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnsQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejection Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrejectionqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Repair Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrepairqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Floor Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvflordesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flordesc")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Machine Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmachdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Time Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtimedesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "timedesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="ViewQc" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvqc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="368px" OnRowDataBound="gvqc_RowDataBound">
                                        <PagerSettings Position="Top" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserial" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" />
                                                <ItemStyle Font-Size="11px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Style Description">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleDestrns" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                        Width="100px"></asp:Label>
                                                    <asp:Label ID="lblgvStyleid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prod.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPPnno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppnno")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvUnittrns" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColor" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                        Width="90px"></asp:Label>
                                                    <asp:Label ID="lblColorid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSize" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                        Width="60px"></asp:Label>
                                                    <asp:Label ID="lblgvSizeid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Floor" Visible="false">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFloor" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flordesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Machine" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMach" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Production Hour" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdhour" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "timedesc")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cur Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvToProcess" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostepdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                    <asp:Label ID="lblgvToProcessid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostep")) %>'
                                                        Width="90px"></asp:Label>
                                                    <asp:Label ID="lblgvfromProcessid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fprostep")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotal" runat="server" Width="150px" Font-Bold="true" Style="text-align: right" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Balance Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRecQty" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pass">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtrnsQty" runat="server" BackColor="Transparent"
                                                        BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvTotalTransQty" runat="server" Width="80px" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                            <%-- <asp:TemplateField HeaderText="Rejection">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvrejectionqty" runat="server" BackColor="Transparent"
                                                        BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Repair">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvtrepairqty" runat="server" BackColor="Transparent"
                                                        BorderStyle="Solid" Font-Size="11px" Style="text-align: right" BorderWidth="1" BorderColor="#00ffcc"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Qc">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnQcUpdate" OnClick="LbtnQcUpdate_Click" CssClass="" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPass" runat="server" AutoPostBack="true" OnCheckedChanged="chkPass_CheckedChanged" />
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPass" runat="server" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkPassHead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Oder No" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrderNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPID" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "pid")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
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
                        </asp:View>

                    </asp:MultiView>

                    <div id="qcmodal" class="modal bd-example-modal-xl animated slideInLeft" role="dialog">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content ">
                                <div class="modal-header">

                                    <h4 class="modal-title"><span class="fa fa-table"></span>QC Details Information Update </h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="label">Rec Qty</label>

                                                <asp:TextBox ID="TextBoxRecQty" ClientIDMode="Static" Enabled="false" runat="server" CssClass="form-control form-control-sm">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="label">Pass Qty</label>

                                                <asp:TextBox ID="TxtPassQty" ClientIDMode="Static" onchange="CheckQty()" runat="server" CssClass="form-control form-control-sm bg-twitter">
                                                </asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="label">Rej. Qty</label>
                                                <asp:TextBox ID="TextBoxRejQty" ClientIDMode="Static" runat="server" onchange="CheckQty()" CssClass="form-control form-control-sm bg-red">
                                                </asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="label">Rep. Qty</label>
                                                <asp:TextBox ID="TextBoxRepQty" ClientIDMode="Static" runat="server" onchange="CheckQty()" CssClass="form-control form-control-sm bg-warning">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label id="Label5">Man. Power</label>
                                                <asp:TextBox ID="txtmpower" runat="server" value="0" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group ml-5">
                                                <label class="label">Pass/Fail</label><br />
                                                <label class="switch">
                                                    <asp:CheckBox ID="ChckStatus" runat="server" />
                                                    <span class="slider round"></span>
                                                </label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblmaterial" runat="server" CssClass="label">Component Name</asp:Label>
                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control chzn-select" ClientIDMode="Static"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                                <asp:DropDownList ID="ddlLineModal" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblApplDat" runat="server" CssClass="label">Applied Date</asp:Label>
                                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                        </div>

                                        <div class="col-md-6 d-flex">
                                            <div class="form-group failreason">
                                                <label class="label">Rejected Reason</label><br />
                                                <asp:ListBox ID="ddlReason" runat="server" CssClass="form-control" ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                                                <asp:TextBox ID="txtComponent" runat="server" ClientIDMode="Static" placeholder="Component" Style="display: none" />
                                                <asp:TextBox ID="txtReasonCode" runat="server" ClientIDMode="Static" placeholder="Reason" Style="display: none" />
                                                <asp:TextBox ID="txtRejReasonQty" runat="server" ClientIDMode="Static" placeholder="Reason Qty" Style="display: none"/>
                                            </div>

                                            <div class="form-group">
                                                <label class="label">&nbsp; </label>
                                                <br />
                                                <button class="btn btn-info btn-sm ml-2" onclick="ddlReason_SelectedIndexChanged()">
                                                    Show
                                                </button>

                                                <button class="btn btn-danger btn-sm ml-2" onclick="RejReasonNew()">
                                                    Clear
                                                </button>
                                            </div>
                                        </div>

                                        <div class="col-md-6 d-flex">
                                            <div class="form-group failreason">
                                                <label class="label">Repair Reason</label><br />
                                                <asp:ListBox ID="ddlReason22" runat="server" CssClass="form-control" ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                                                <asp:TextBox ID="txtComponent22" runat="server" ClientIDMode="Static" placeholder="Component" Style="display: none" />
                                                <asp:TextBox ID="txtReasonCode22" runat="server" ClientIDMode="Static" placeholder="Repair" Style="display: none" />
                                                <asp:TextBox ID="txtRejReasonQty22" runat="server" ClientIDMode="Static" Style="display: none"/>
                                            </div>

                                            <div class="form-group">
                                                <label class="label">&nbsp; </label>
                                                <br />
                                                <button class="btn btn-info btn-sm ml-2" onclick="ddlReason_SelectedIndexChanged22()">
                                                    Show
                                                </button>

                                                <button class="btn btn-danger btn-sm ml-2" onclick="RejReasonNew22()">
                                                    Clear
                                                </button>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <table class="table table-bordered table-sm" id="cellReasonList" style="display: none">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">#</th>
                                                        <th scope="col">Component</th>
                                                        <th scope="col">Rejected Reason</th>
                                                        <th scope="col">Qty</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="qcmodalinf">
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-md-6">
                                            <table class="table table-bordered table-sm" id="cellReasonList22" style="display: none">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">#</th>
                                                        <th scope="col">Component</th>
                                                        <th scope="col">Repair Reason</th>
                                                        <th scope="col">Qty</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="qcmodalinf22">
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="label">Remarks</label>
                                                <asp:TextBox ID="txtgRemarks" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group float-right">
                                                <label class="label">&nbsp;</label><br />
                                                <asp:LinkButton ID="LbtnUpdateQcDetails" CssClass="btn btn-sm btn-primary" runat="server" OnClick="LbtnUpdateQcDetails_Click" OnClientClick="CLoseMOdal();"><span class="fa fa-save"></span> Update </asp:LinkButton>
                                                <button type="button" class="btn btn-sm btn-dark" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>




                    <div id="myBulkQc" class="modal animated slideInLeft sizecolor" role="dialog">
                        <div class="modal-dialog modal-xl ">
                            <div class="modal-content  ">
                                <div class="modal-header">
                                    <h4 class="modal-title">
                                        <span class="fa fa-table"></span>Bulk QC Update</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">

                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="form-group ml-5">
                                                <label class="labelPF">Pass/Fail</label><br />
                                                <label class="switch">
                                                    <asp:CheckBox ID="CheckBoxPassFail" runat="server" />
                                                    <span class="slider round"></span>
                                                </label>
                                            </div>
                                        </div>

                                        <div class="col-lg-3 col-md-3 col-sm-3">
                                            <div class="form-group">
                                                <asp:Label ID="LabelAppDate" runat="server" CssClass="label">Applied Date</asp:Label>
                                                <asp:TextBox ID="TextBoxAppDate" runat="server" Style="margin-top: 9px;" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender_AppDate" runat="server" Enabled="True"
                                                    Format="dd-MMM-yyyy" TargetControlID="TextBoxAppDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <div class="form-group">
                                                <label class="label">Remarks</label>
                                                <asp:TextBox ID="TextBoxRemarks" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                    </div>



                                </div>

                                <div class="modal-footer ">
                                    <asp:LinkButton ID="upButton" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseBulkQCModal();" OnClick="upButton_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


