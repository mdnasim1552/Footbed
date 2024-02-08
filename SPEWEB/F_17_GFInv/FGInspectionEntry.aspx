<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="FGInspectionEntry.aspx.cs" Inherits="SPEWEB.F_17_GFInv.FGInspectionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
            <%--var gvProdCons = $('#<%=this.gvProdCons.ClientID %>');
            gvProdCons.Scrollable();--%>

            $('.chzn-select').chosen({
                search_contains: true,
            });

            $(function() {
                $('[id*=ddlToProcess]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    maxHeight: 250
                })
            });

            //document.getElementById("txtCritical").value = "";
            //document.getElementById("txtMajor2").value = "";
            //document.getElementById("txtMinor2").value = "";

        }

        <%--function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;
            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvProdCons":
                    tblData = document.getElementById("<%=gvProdCons.ClientID %>");
                    break;
            }

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
        }--%>

        function recalculate() {

            var OrdrQty = document.getElementById("lblOrdQtyDemo").innerHTML;
            OrdrQty = parseFloat(OrdrQty);

            var InspQty = document.getElementById("txtInspQty").value;
            InspQty = InspQty == "" ? 0 : InspQty;
            InspQty = parseFloat(InspQty);

            if (InspQty > OrdrQty) {
                alert("Inspection Qty cannot be greater than Order Qty!")
                document.getElementById("txtInspQty").innerHTML = 0;
                return;
            }

            var fndDftQty1 = document.getElementById("txtCritical").value;
            var fndDftQty2 = document.getElementById("txtMajor2").value;
            var fndDftQty3 = document.getElementById("txtMinor2").value;

            fndDftQty1 = fndDftQty1 == "" ? 0 : fndDftQty1;
            fndDftQty2 = fndDftQty2 == "" ? 0 : fndDftQty2;
            fndDftQty3 = fndDftQty3 == "" ? 0 : fndDftQty3;

            var failedQty = parseFloat(fndDftQty1) + parseFloat(fndDftQty2) + parseFloat(fndDftQty3);

            if (failedQty > InspQty || failedQty < 0) {
                alert("Failed Qty cannot be greater than Inspection Qty or less than 0!")
                document.getElementById("txtInspQty").value = 0;
                return;
            }

            document.getElementById("lblFailQty").value = failedQty;
            document.getElementById("lblPassedQty").innerHTML = InspQty - failedQty;

            var passedQty = InspQty - failedQty;

            document.getElementById("lblFtprRatio").innerHTML = Math.floor((passedQty / InspQty) * 100) + " %";
        }

        function updatefailedqty() {

            document.getElementById("txtCritical").value = "";
            document.getElementById("txtMajor2").value = "";
            document.getElementById("txtMinor2").value = "";

            var InspQty = document.getElementById("txtInspQty").value;
            InspQty = InspQty == "" ? 0 : InspQty;
            InspQty = parseFloat(InspQty);

            var FailedQty = document.getElementById("lblFailQty").value;
            FailedQty = FailedQty == "" ? 0 : FailedQty;
            FailedQty = parseFloat(FailedQty);

            if (FailedQty > InspQty || FailedQty < 0) {
                alert("Failed Qty cannot be greater than Inspection Qty or less than 0!")
                return;
            }

            document.getElementById("lblPassedQty").innerHTML = InspQty - FailedQty;
            var passedQty = InspQty - FailedQty;

            document.getElementById("lblFtprRatio").innerHTML = Math.floor((passedQty / InspQty) * 100) + " %";
        }

        function ddlInspctChange(e) {

            console.log(e.target.value);

            if (e.target.value == "True") {
                e.target.classList.remove("bg-red")
                e.target.classList.add("bg-green")
            } else {
                e.target.classList.remove("bg-green")
                e.target.classList.add("bg-red")
            }

        }

        function AllwDefctQty() {

            var majorDefect = document.getElementById("txtMajor").value;
            majorDefect = majorDefect == "" ? 0 : majorDefect;
            majorDefect = parseInt(majorDefect);

            var minorDefect = document.getElementById("txtMinor").value;
            minorDefect = minorDefect == "" ? 0 : minorDefect;
            minorDefect = parseInt(minorDefect);

            var totaldefect = majorDefect + minorDefect;

            if (totaldefect > 0) {

                if (totaldefect >= 0 && totaldefect <= 100) {

                    var OrdrQty = document.getElementById("lblOrdQtyDemo").innerHTML;
                    OrdrQty = parseFloat(OrdrQty);

                    document.getElementById("txtInspQty").value = OrdrQty * totaldefect / 100;
                    document.getElementById("lblFailQty").value = 0;
                }

            }

        }

    </script>

    <style type="text/css">

        .multiselect {
            width: 530px !important;
            border: 1px solid;
            height: 35px;
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

        .multiselect-container {
            overflow: scroll;
            max-height: 300px !important;
        }

        .custom-failed {
            background-color: red;
            color: white;
        }

        .custom-passed {
            background-color: green;
            color: white;
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
                <div class="card-body">
                    <div class="row">

                        <%--<div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Text="From:"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>--%>

                        <%--<div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="label" Text="To:"></asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>--%>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="smLbl_to text-left">Season</asp:Label>
                                <div class="form-inline">
                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" Width="100%" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 form-group">
                            <asp:Label ID="lblArticle" runat="server">Article List</asp:Label>
                            <asp:DropDownList ID="ddlOrderList" runat="server" SelectionMode="Multiple" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary btn-sm" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">

                    <asp:Panel ID="PnlFGIEntry" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12">

                                <table class="table table-bordered">
                                    <tr>
                                        <th>Order No</th>
                                        <td>
                                            <asp:Label ID="lblOrderNo" runat="server" CssClass="form-control bg-secondary"></asp:Label></td>
                                        <th>Customer</th>
                                        <td>
                                            <asp:Label ID="lblCustomer" runat="server" CssClass="form-control bg-secondary"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>Article</th>
                                        <td>
                                            <asp:Label ID="ArticleName" runat="server" CssClass="form-control bg-secondary"></asp:Label></td>
                                        <th>Color</th>
                                        <td>
                                            <asp:Label ID="lblColor" runat="server" CssClass="form-control bg-secondary"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>Produced Line No</th>
                                        <td>
                                            <asp:ListBox ID="ddlToProcess" runat="server" CssClass="form-control" ClientIDMode="Static" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <th>Inspect By</th>
                                        <td>
                                            <asp:TextBox ID="TxtInspectBy" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Order Qty</th>
                                        <td>
                                            <asp:Label ID="lblOrdQtyDemo" runat="server" CssClass="form-control bg-secondary" ClientIDMode="Static" style="display:none"></asp:Label>
                                            <asp:Label ID="lblOrdQty" runat="server" CssClass="form-control bg-secondary"></asp:Label>
                                        </td>
                                        <th>Inspection Date</th>
                                        <td>
                                            <asp:TextBox ID="txtInspDate" runat="server" CssClass="form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtInspDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtInspDate"></cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Allowed Defect Qty (Ratio)</th>
                                        <td>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtMajor" runat="server" CssClass="form-control" placeholder="Major" ClientIDMode="Static" onchange="AllwDefctQty()" TextMode="Number"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtMinor" runat="server" CssClass="form-control" placeholder="Minor" ClientIDMode="Static" onchange="AllwDefctQty()" TextMode="Number"></asp:TextBox>
                                                </div>
                                            </div>

                                        </td>
                                        <th>Found Defect Qty</th>
                                        <td>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtCritical" runat="server" CssClass="form-control" placeholder="Critical" ClientIDMode="Static" TextMode="Number" onchange="recalculate()" Text="100"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtMajor2" runat="server" CssClass="form-control" placeholder="Major" ClientIDMode="Static" TextMode="Number" onchange="recalculate()"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtMinor2" runat="server" CssClass="form-control" placeholder="Minor" ClientIDMode="Static" TextMode="Number" onchange="recalculate()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <th>Inspection Qty</th>
                                        <td>
                                            <asp:TextBox ID="txtInspQty" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="recalculate()" TextMode="Number"></asp:TextBox></td>
                                        <th rowspan="2" style="vertical-align: middle">FTPR %</th>
                                        <td rowspan="2" style="vertical-align: middle;">
                                            <asp:Label ID="lblFtprRatio" runat="server" Font-Size="40px"  CssClass="text-twitter font-weight-bolder font-size-lg" ClientIDMode="Static"></asp:Label></td>
                                    </tr>

                                    <tr>
                                        <th>Failed Qty</th>
                                        <td>
                                            <asp:TextBox ID="lblFailQty" runat="server" CssClass="form-control bg-secondary" ClientIDMode="Static" onchange="updatefailedqty()"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <th>Passed Qty</th>
                                        <td>
                                            <asp:Label ID="lblPassedQty" runat="server" CssClass="form-control bg-secondary" ClientIDMode="Static"></asp:Label></td>
                                        <th rowspan="2" style="vertical-align: middle">Remarks</th>
                                        <td rowspan="2" style="vertical-align: middle">
                                            <asp:TextBox ID="TxtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    </tr>

                                    <tr>
                                        <th>Inspection Result</th>
                                        <td>
                                            <asp:DropDownList ID="DDLResult" runat="server" CssClass="form-control text-center" Font-Size="17px" onchange="return ddlInspctChange(event)">
                                                <asp:ListItem Value="False" Selected="True" class="font-weight-bold text-white">FAILED</asp:ListItem>
                                                <asp:ListItem Value="True" class="font-weight-bold text-white">PASSED</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                </table>
                            </div>

                        </div>
                    </asp:Panel>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





