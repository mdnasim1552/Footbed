<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptOrderStatus.aspx.cs" Inherits="SPEWEB.F_05_ProShip.RptOrderStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        function AlertConfirm() {
            if (confirm('Do you want to save?')) {
                closeModal();
            } else {
                return false;
            }
        }

        function closeModal() {
            $('#exampleModalDrawerRight').modal('hide');
        }

        function OpenModal() {
            $('#SizeModal').modal('show');
        }

        function OpenRecModal() {
            $('#RecDetailsModal').modal('show');
        }

        function Search_Gridview(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvOrderstatus.ClientID %>");

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

        function Search_gvMatMasterDetails(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvMatMasterDetails.ClientID %>");
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

        function Search_gvMatMasterSummary(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvMatMasterSummary.ClientID %>");
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

        function Search_Pipeline(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvBomlistPan":
                    tblData = document.getElementById("<%=gvBomlistPan.ClientID %>");
                    break;

                case "gvBomlist":
                    tblData = document.getElementById("<%=gvBomlist.ClientID %>");
                    break;

                case "gvArchived":
                    tblData = document.getElementById("<%=gvArchived.ClientID %>");
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
        }

        function CurrentTabDetail() {
            document.getElementById("currentTabNow").value = "detailTab"
        }

        function CurrentTabSummary() {
            document.getElementById("currentTabNow").value = "summaryTab"
        }



        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            document.getElementById("currentTabNow").value = "detailTab"
        });

        //function pageLoaded() {
        //    $("input, select").bind("keydown", function (event) {
        //        var k1 = new KeyPress();
        //        k1.textBoxHandler(event);
        //    });
        //}

        function pageLoaded() {

            let options = { ScrollHeight: 600 };
            let gv1 = $('#<%=this.gvOrderstatus.ClientID %>');
            gv1.Scrollable(options);

<%--            let gv2 = $('#<%=this.gvMatStatus.ClientID %>');
            gv2.Scrollable(options);

            let gv3 = $('#<%=this.gvProdRpt.ClientID %>');
            gv3.Scrollable(options); --%>

            $('.chzn-select').chosen({ search_contains: true });

            //gv2.gridviewScroll({

            //});
            $('[id*=ddlBomList]').multiselect({
                includeSelectAllOption: true,
                searchable: true,
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                maxHeight: 250

            })

            $(".Multidropdown button").addClass("multiselect dropdown-toggle btn btn-default btn-sm");

        }

        function SelectAllCheckboxes(chk) {
            $('#<%=gvBomlistPan.ClientID %>').find("input:checkbox").each(function () {
                // console.log(tblData1.rows[i].style.display);
                if ((this).disabled == false) {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }

            });
        }

        function SelectAllCheckboxesforArchive(chk) {
            $('#<%=gvBomlist.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false) {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }

            });
        }
    </script>

    <style>
        #RecDetailsModal .modal-dialog {
            max-width: 100% !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        #SizeModal .modal-dialog {
            max-width: 100% !important;
        }

        .multiselect-native-select .btn-group {
            width: 100% !important;
            border: 1px solid #dad5d5;
        }
        
        .multiselect-native-select .btn-group btn {
            border: 1px solid #808080;
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


        /*
        table .grvContentarea{
            width: 100% !important;
        }*/

        /*.OrderStatusreport .grvContentarea {
            width: 2040px !important;
        }*/
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
                        <div class="col-md-1 col-sm-1 col-lg-1 " runat="server" id="plnDateF">
                            <div class="form-group">
                                <asp:Label ID="lblDate1" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateto_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="DdlSeason">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblAgent" runat="server" class="label" for="ddlAgent">Agent</asp:Label>
                                <asp:DropDownList ID="ddlAgent" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblBuyer" runat="server" class="label" for="ddlBuyer">Buyer</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div runat="server" id="FieldCompNameList" class="col-md-2" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lblComapnies" runat="server" class="label" for="ToDate">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompanyName" class="form-control form-conrol-sm chzn-select" runat="server" AutoPostBack="True" TabIndex="1"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" Text="Page Size" CssClass=""></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18" AutoPostBack="true">
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="900">900</asp:ListItem>
                                    <asp:ListItem Value="1500">1500</asp:ListItem>
                                    <asp:ListItem Value="2500">2500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3" style="margin-top: 20px;">
                            <div class="row">
                                <div class="col-6">
                                    <div class="input-group input-group-alt">
                                        <asp:TextBox runat="server" ID="txtSearch" Width="100px" CssClass="form-control form-control-sm" placeholder="Search Order"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton runat="server" ID="lnkbtnSearch" CssClass="input-group-text" OnClick="lnkbtnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <asp:HyperLink runat="server" ID="lnkbtnExcl" CssClass="btn btn-sm btn-primary text-light" Visible="false">
                                <i class="fa fa-file-excel mr-1"></i> Download Excel
                                    </asp:HyperLink>
                                </div>
                            </div>

                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblmatgrp" Visible="false" runat="server" class="label" for="ToDate">Group</asp:Label>
                                <asp:DropDownList ID="DdlMatGroup" Visible="false" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group Multidropdown">
                                <asp:Label ID="lblBom" runat="server" class="label" for="ToDate">BOM</asp:Label>
                                <asp:ListBox ID="ddlBomList" SelectionMode="Multiple" CssClass="form-control form-control-sm multiselect-search" runat="server"></asp:ListBox>
                                <%--<asp:DropDownList ID="ddlBomList" OnSelectedIndexChanged="ddlBomList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>--%>
                            </div>
                        </div>

                        <div class="col-md-1 px-0" id="Pipeline" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px">
                                <a href="#" class="btn btn-sm btn-danger small" data-toggle="modal" data-target="#exampleModalDrawerRight">BM Pipeline <i class="fa fa-database"></i>
                                </a>
                            </div>
                        </div>

                        <div class="col-md-1" id="BomType" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" class="label" for="ToDate">Bom Type</asp:Label>
                                <asp:DropDownList ID="DdlBomType" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="DdlBomType_SelectedIndexChanged" runat="server">
                                    <asp:ListItem Value="0">Selected</asp:ListItem>
                                    <asp:ListItem Value="1">Archive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="log-divider" id="lblHeadCost" runat="server" visible="False">
                            <span>
                                <i class="fa fa-fw fa-dollar-sign"></i>Cost
                            </span>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 350px">
                <%--<div class="card-body">--%>

                <asp:MultiView ID="Multiview" runat="server">
                    <asp:View ID="Ordrstatus" runat="server">

                        <%--<div class="row">
                                <div class="col-lg-12">--%>
                        <!-- .card -->
                        <%--<section class="card OrderStatusreport">--%>
                        <!-- .card-header -->
                        <%--<header class="card-header">
                                    <ul class="nav nav-tabs card-header-tabs">
                                        <li class="nav-item">
                                            <a class="nav-link active show" data-toggle="tab" href="#ordr-info">Order Information</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link " data-toggle="tab" href="#mat-status">Material Status</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link " data-toggle="tab" href="#prod-report">Production Report</a>
                                        </li>
                                    </ul>
                                </header>--%>
                        <!-- /.card-header -->

                        <!-- .card-body -->
                        <div class="card-body">
                            <!-- .tab-content -->
                            <%--<div id="myTabCard" class="tab-content">--%>
                            <%--<div class="tab-pane fade active show" id="ordr-info">--%>
                            <div class="table-responsive">

                                <asp:GridView ID="gvOrderstatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="1000px" AllowPaging="true" OnPageIndexChanging="gvOrderstatus_PageIndexChanging" RowStyle-Font-Size="11px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderstatusl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="30px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="30px" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyer" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'></asp:Label>
                                                <asp:Label ID="lblmlccod" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>
                                                <asp:Label ID="lblstyle" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'></asp:Label>
                                                <asp:Label ID="lbldayid" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agent">
                                            <HeaderTemplate>
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">
                                                            <asp:Label ID="gvOrderstatus_Label4" runat="server"
                                                                Text="Agent"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="gvOrderstatus_hlbtntbCdataExel" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAgent" runat="server" Width="60px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="60px" ForeColor="#333333" />
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Order No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink CssClass="text-flickr font-weight-bold" ID="lblgvCustomOrder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customorder")) %>'
                                                    Width="70px" NavigateUrl='<%# this.ResolveUrl("~/F_01_Mer/OrderDetails?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))) %>' Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="70px" ForeColor="#333333" />
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Images">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="60" />
                                            <FooterStyle Width="60" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Article Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvArticleName" runat="server" Width="100px" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="100px" ForeColor="#333333" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColor" runat="server" Width="80px" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlOrdr" CssClass="font-weight-bold" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblgvOrdQty1" Width="50px" CssClass="text-twitter font-weight-bold" OnClick="lblgvOrdQty_Click1" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,#0") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlOrdrQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ex. Factory Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblcolorid" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "colorid") %>'></asp:Label>
                                                <asp:Label ID="lblgvExpDate" Visible="false" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px"></asp:Label>
                                                <asp:LinkButton ID="LbtnDetailsExfactdate" OnClick="LbtnDetailsExfactdate_Click" CssClass="text-flickr font-weight-bold text-center" Width="65px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdat")).ToString("dd-MMM-yyyy") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="65px" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Week">
                                             <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="35px" placeholder="Week" onkeyup="Search_Gridview(this,9)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvWeek" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "weeknumber")) %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="35px" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="35px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receive Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvRecDate" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "odrdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="65px" ForeColor="#333333" />
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lead Time">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvLeadTime" runat="server" Style="text-align: center;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0; ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="30px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Shoe Type" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvShoeType" runat="server"
                                                    Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetypdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Construction">
                                            <ItemTemplate>
                                                <asp:Label ID="LblgvConstru" runat="server" Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "constdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Outsole Source">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOutSoleSource" runat="server"
                                                    Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsolesource")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <%--false--%>
                                        <asp:TemplateField HeaderText="Booking<br> Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBookQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookqty")).ToString("#,#0") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Booking <br> Bal Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBookbalQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookbal")).ToString("#,#0") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Confirm<br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvosCnfrmDt" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <%--16--%>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="BOM No" onkeyup="Search_Gridview(this,16)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink Target="_blank" NavigateUrl='<%# this.ResolveUrl("~/F_01_Mer/MerChanPrint?Type=BOMPrint&" +
                                                                                    "mlccod="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))+"&Ptype=import&dayid="+Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid"))+
                                                                                    "&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+"&format=PDF&Dept=Planning") %>'
                                                    ID="gvmshlnkBomid" runat="server" Font-Size="10px"
                                                    Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pipeline">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmsLblPipeline" runat="server"
                                                    Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pipeline")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Knife">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmsLblKnife" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "knif")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="40px" HorizontalAlign="Right" />
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="40px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PPT">
                                            <ItemTemplate>
                                                <asp:Label ID="gvmsLblPPT" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate"))).ToString("dd-MMM-yyyy") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="40px" HorizontalAlign="Right" />
                                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="40px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Leather">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvmslnkbtnLeatherpercnt" OnClick="lgvLeatherpercnt_Click" runat="server" CssClass="text-danger font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leatherpct")).ToString("#,##0;(#,##0);")+" %" %>'
                                                    Width="50px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Width="50px" HorizontalAlign="Right" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Textile<br>Synthetic">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvmslnkbtnSynthetic" OnClick="lgvSynthetic_Click" runat="server" CssClass="text-flickr font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "synthtpc")).ToString("#,##0;(#,##0);")+" %" %>'
                                                    Width="55px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Width="55px" HorizontalAlign="Right" />
                                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="55px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ornament">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvmslnkbtnORNAMENT" OnClick="lgvORNAMENT_Click" runat="server" CssClass="text-twitter font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ornament")).ToString("#,##0;(#,##0);")+" %" %>'
                                                    Width="60px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Width="60px" HorizontalAlign="Right" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="60px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Thread">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvmslnkbtnThread" OnClick="lgvTHREAD_Click" runat="server" CssClass="text-gray-dark font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "threadpct")).ToString("#,##0;(#,##0);")+" %" %>'
                                                    Width="50px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Width="50px" HorizontalAlign="Right" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Outsole">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvmsLnkBtnOutsole" OnClick="lgvOUTSOLE_Click" runat="server" CssClass="text-blue font-weight-bold"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outsole")).ToString("#,##0;(#,##0);")+" %" %>'
                                                    Width="50px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Width="50px" HorizontalAlign="Right" />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" Font-Bold="True" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutting<br>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblCuttingQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cutdone")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlCutQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutting<br>Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblCuttingBal" runat="server"
                                                    Text='<%# ((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty"))) - (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cutdone")))).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlCutBal" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutting<br>Start">
                                            <ItemTemplate>
                                                <asp:Label ID="gvpoLblCutStart" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cutstart")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cutstart")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutting<br>End">
                                            <ItemTemplate>
                                                <asp:Label ID="gvpoLblCutEnd" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cutend")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cutend")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sewing<br>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblSewingQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sewdone")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlSngQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sewing<br>Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblSewingBal" runat="server"
                                                    Text='<%# ((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty"))) - (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sewdone")))).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlSngBal" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sewing<br>Start">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblSewingStart" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sewstart")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sewstart")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sewing<br>End">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblSewingEnd" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sewend")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sewend")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fitting<br>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblFittingQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fitdone")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlFitQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fitting<br>Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblFittingBal" runat="server"
                                                    Text='<%# ((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty"))) - (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fitdone")))).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlFitBal" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fitting<br>Start">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblFittingStart" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fitstart")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fitstart")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fitting<br>End">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblFittingEnd" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fitend")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fitend")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lasting<br>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblLastingQty" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lasdone")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlLastQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lasting<br>Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblLastingBal" runat="server"
                                                    Text='<%# ((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty"))) - (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lasdone")))).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlLastBal" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lasting<br>Start">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblLastingStart" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lasstart")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lasstart")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lasting<br>End">
                                            <ItemTemplate>
                                                <asp:Label ID="gvprLblLastingEnd" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lasend")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lasend")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipped <br> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvShippedQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipedqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlShipQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Width="70px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Width="70px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>

                            <!-- /.tab-content -->
                        </div>
                        <!-- /.card-body -->
                        <%--</section>--%>
                        <!-- /.card -->

                        <%--</div>
                            </div>--%>
                    </asp:View>

                    <asp:View ID="MatMaster" runat="server">
                        <asp:TextBox runat="server" ID="currentTabNow" ClientIDMode="Static" style="display:none"/>
                        <div class="card card-fluid">
                            <header class="card-header">
                                <!-- .nav-tabs -->
                                <ul class="nav nav-tabs card-header-tabs">
                                    <li class="nav-item">
                                        <a class="nav-link active show" data-toggle="tab" href="#details" onclick="CurrentTabDetail()">Details</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#summary" onclick="CurrentTabSummary()">Summary</a>
                                    </li>
                                </ul>
                                <!-- /.nav-tabs -->
                            </header>
                            <div class="card-body">
                                <div class="tab-content">
                                    <div class="tab-pane fade active show" id="details">
                                        <div class="row table-responsive">
                                            <asp:GridView ID="gvMatMasterDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvMatMasterDetails_RowDataBound" AllowPaging="true"
                                            OnPageIndexChanging="gvMatMasterDetails_PageIndexChanging"
                                            AllowSorting="true" OnSorting="gvMatMasterDetails_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterDetailsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                            Width="20px" Style="text-align: left"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BOM No" SortExpression="bomid" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBOM" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                        
                                                <asp:TemplateField HeaderText="Order No.">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Order No." onkeyup="Search_gvMatMasterDetails(this, 2)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOrdrNo" runat="server" CssClass="text-left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Group">

                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Group" onkeyup="Search_gvMatMasterDetails(this, 3)"></asp:TextBox>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:HyperLink ID="gvMatMasterDetails_hlbtntbCdataExel" runat="server" CssClass="text-primary"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                </td>--%>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatGroup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matgrpdesc")) %>'
                                                            Width="100px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" SortExpression="itmdesc">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Material Name" onkeyup="Search_gvMatMasterDetails(this, 4)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMaterialName" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc"))%>'
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Specifications" onkeyup="Search_gvMatMasterDetails(this, 5)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterDetailsSpecifications" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc"))%>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="color" onkeyup="Search_gvMatMasterDetails(this, 6)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterDetailsColor" runat="server" CssClass="text-left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color"))%>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnit" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit"))%>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                            
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrUnit" runat="server" CssClass="text-center font-weight-bold" Width="50px" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                            
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvStkQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BOM Qty" SortExpression="itmqty" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBOmqty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrBOmqtyD" runat="server" CssClass="text-center font-weight-bold" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Size="11px" />
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctIssueQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomisuqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue Bal">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctIssueBalQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomisubal")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                            
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrIsuBalD" runat="server" CssClass="text-center font-weight-bold" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Size="11px" />
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual Stock" SortExpression="stockqty" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvActualStockQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project<br>Stock" SortExpression="bomstock" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctStockQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomstock")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
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
                                    <div class="tab-pane fade" id="summary">
                                        <div class="row">
                                            <asp:GridView ID="gvMatMasterSummary" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvMatMasterSummary_RowDataBound" AllowPaging="true"
                                            OnPageIndexChanging="gvMatMasterSummary_PageIndexChanging"
                                            AllowSorting="true" OnSorting="gvMatMasterSummary_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterSummaryl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                            Width="20px" Style="text-align: left"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Group">

                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Group" onkeyup="Search_gvMatMasterSummary(this, 1)"></asp:TextBox>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:HyperLink ID="gvMatMasterDetails_hlbtntbCdataExel" runat="server" CssClass="text-primary"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                </td>--%>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatGroupS" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matgrpdesc")) %>'
                                                            Width="100px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" SortExpression="itmdesc">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Material Name" onkeyup="Search_gvMatMasterSummary(this, 2)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMaterialNameS" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc"))%>'
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Specifications" onkeyup="Search_gvMatMasterSummary(this, 3)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterDetailsSpecificationsS" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc"))%>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="color" onkeyup="Search_gvMatMasterSummary(this, 4)"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatMasterDetailsColorS" runat="server" CssClass="text-left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color"))%>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnitS" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit"))%>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                            
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrUnitS" runat="server" CssClass="text-center font-weight-bold" Width="50px" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                            
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvStkQtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BOM Qty" SortExpression="itmqty" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBOmqtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                            
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrBOmqtyS" runat="server" CssClass="text-center font-weight-bold"></asp:Label>
                                                    </FooterTemplate>
                                            
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctIssueQtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomisuqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue Bal">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctIssueBalQtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomisubal")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                            
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtrIsuBalS" runat="server" CssClass="text-center font-weight-bold" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                            
                                                    <HeaderStyle Font-Bold="True" ForeColor="#333333" />
                                                    <ItemStyle />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual Stock" SortExpression="stockqty" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvActualStockQtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project<br>Stock" SortExpression="bomstock" HeaderStyle-CssClass="text-twitter font-italic">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPrjoctStockQtyS" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomstock")).ToString("#,#0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <ItemStyle />
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
                    </asp:View>

                    <asp:View ID="SMVsheet" runat="server">
                        <div class="card-body">
                            <div class="table-responsive" style="min-height: 250px">
                                <asp:GridView ID="gvSMVsheet" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="1000px" AllowPaging="true" OnPageIndexChanging="gvOrderstatus_PageIndexChanging" RowStyle-Font-Size="11px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVSL" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="30px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="30px" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVBuyer" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'></asp:Label>
                                                <asp:Label ID="lblgvSMVmlccod" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>
                                                <asp:Label ID="lblgvSMVstyle" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'></asp:Label>
                                                <asp:Label ID="lblgvSMVdayid" Visible="false" runat="server" Width="80px" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Agent">

                                            <HeaderTemplate>
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">
                                                            <asp:Label ID="gvSMVOrderstatus_Label4" runat="server"
                                                                Text="Agent"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="gvSMV_hlbtntbCdataExel" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVAgent" runat="server" Width="60px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentdesc")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="60px" ForeColor="#333333" />
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Order No" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink CssClass="text-flickr font-weight-bold" ID="lblgvSMVCustomOrder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customorder")) %>'
                                                    Width="70px" NavigateUrl='<%# this.ResolveUrl("~/F_01_Mer/OrderDetails?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))) %>' Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="70px" ForeColor="#333333" />
                                            <ItemStyle Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Images">

                                            <ItemTemplate>

                                                <asp:HyperLink ID="gvSMVhyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblgvSMVImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <HeaderStyle Width="60" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="60" />
                                            <FooterStyle Width="60" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Article Name" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVArticleName" runat="server" Width="100px" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="100px" ForeColor="#333333" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVColor" runat="server" Width="80px" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblgvSMVOrdQty1" Width="50px" CssClass="text-twitter font-weight-bold" OnClick="lblgvOrdQty_Click1" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,#0") %>'></asp:LinkButton>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ex. Factory Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVExpDate" Visible="false" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px"></asp:Label>
                                                <asp:LinkButton ID="LbtngvSMVDetailsExfactdate" OnClick="LbtnDetailsExfactdate_Click" CssClass="text-flickr font-weight-bold" runat="server">Details</asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="65px" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Week">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVWeek" runat="server" Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "weeknumber")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" Font-Bold="True" ForeColor="#333333" />
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receive Date">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvSMVRecDate" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "odrdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="65px" ForeColor="#333333" />
                                            <ItemStyle Width="65px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lead Time">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvSMVLeadTime" runat="server" Style="text-align: center;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0; ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="30px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Shoe Type" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVShoeType" runat="server"
                                                    Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetypdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Construction">
                                            <ItemTemplate>
                                                <asp:Label ID="LblgvSMVConstru" runat="server" Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "constdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Width="80px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Outsole Source">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVOutSoleSource" runat="server"
                                                    Style="text-align: center;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsolesource")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <%--false--%>
                                        <asp:TemplateField HeaderText="Booking<br> Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVBookQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookqty")).ToString("#,#0") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Booking <br> Bal Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSMVBookbalQty" runat="server" Style="text-align: center" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bookbal")).ToString("#,#0") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Width="50px" ForeColor="#333333" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Confirm<br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSMVosCnfrmDt" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).Year.ToString() == "1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <%--16--%>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="BOM No" onkeyup="Search_Gridview(this,16)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink Target="_blank" NavigateUrl='<%# this.ResolveUrl("~/F_01_Mer/MerChanPrint?Type=BOMPrint&" +
                                                                                    "mlccod="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))+"&Ptype=import&dayid="+Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid"))+
                                                                                    "&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+"&format=PDF&Dept=Planning") %>'
                                                    ID="gvSMVmshlnkBomid" runat="server" Font-Size="10px"
                                                    Style="text-align: center" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle Width="70px" />
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

                <%--</div>--%>
            </div>


            <div id="SizeModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog UpdateMOdel modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header" style="display: inline">
                            <div class="row">
                                <div class="col-6">
                                    <h4 class="modal-title">
                                        <span class="fa fa-info-circle"></span>Article Wise Size Breakdown/Packing information
                                    </h4>
                                </div>
                                <div class="col-6 text-right">
                                    <asp:HyperLink Target="_blank" ID="hyprModalPrint" Visible="false" runat="server" CssClass="btn btn-sm btn-primary">Print</asp:HyperLink>
                                </div>
                            </div>

                        </div>
                        <div class="modal-body form-horizontal table-responsive">

                            <asp:GridView ID="gv1pack" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>

                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sl" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvSlnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category and <br> Article Number">

                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color">

                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                Width="91px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Order <span class='text-red'>*</span>">
                                        <ItemTemplate>
                                            <asp:Label ToolTip="Please Don't use space" ID="TxtCustOrder" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custordno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ref No">
                                        <ItemTemplate>
                                            <asp:Label ID="TxtRefno" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custrefno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Packing" Visible="false">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DdlPacklist" Width="100px" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Num of. CTN">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Ptxtcarton" runat="server" CssClass="bg-twitter " BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cartoon")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <br />
                                            <br />
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF1" Style="text-align: right !important;" CssClass="text-danger" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF1" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF2" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF2" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF3" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF3" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF4" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF4" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF5" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF5" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF6" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF6" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF7" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF7" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF8" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF8" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF9" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF9" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF10" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF10" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF11" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF11" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF12" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF12" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF13" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF13" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF14" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF14" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF15" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF15" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF16" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF16" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF17" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF17" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF18" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF18" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF19" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF19" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF20" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF20" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF21" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF21" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF22" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p22")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF22" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF23" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p23")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF23" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF24" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p24")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF24" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF25" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p25")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF25" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF26" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p26")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF26" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF27" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p27")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF27" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF28" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p28")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF28" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF29" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p29")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF29" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF30" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p30")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF30" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF31" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p31")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF31" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF32" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p32")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF32" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF33" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p33")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF33" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF34" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p34")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF34" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF35" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p35")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF35" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF36" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p36")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF36" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF37" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p37")).ToString("###0;(###0);") %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF37" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF38" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p38")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF38" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF39" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p39")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF39" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="PtxtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                            <asp:Label ID="PlblgvF40" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p40")).ToString("###0;(###0);") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="flblgvF40" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ")+" /CTN" %>'
                                                Width="60px"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" Style="font-size: 11px; text-align: right" CssClass="text-danger"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "psum")).ToString("#,##0;(#,##0); ")+" PRS" %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="PFLblgvTotal" runat="server"></asp:Label><br />
                                            <asp:Label ID="PFLblgvTotalPair" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ex.Factory Date">
                                        <ItemTemplate>
                                            <asp:Label ID="PlblgvExfactDate1" runat="server"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfactorydate")).AddDays(-10).ToString("dd-MMM-yyyy") %>'
                                                Width="90px"></asp:Label>

                                        </ItemTemplate>
                                        <%--  <FooterTemplate>
                                            <asp:Label ID="PFLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
                                        </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Article Name">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color">
                                        <%-- <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                                </FooterTemplate>--%>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                Width="91px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
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
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-sm btn-default" style="border: solid; border-width: thin;" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="RecDetailsModal" data-backdrop="static" class="modal" role="dialog">
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-info-circle"></span>BOM Wise Purchase Receive Details
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal table-responsive">

                            <asp:GridView ID="gvRecDetails" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>
                                    <asp:TemplateField HeaderText="BOM NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBomID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOM User">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBomUser" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomuser")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rec No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRecID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rec Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPoDate" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrrdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMaterialGrp" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matgrpdesc")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Material">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMaterial" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specifications">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpecifications" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="MR Ref">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMRfRef" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrref")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budget Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBUdgetQty" runat="server" CssClass="text-flickr" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budget")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rcv Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQty" runat="server" CssClass="text-twitter" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStore" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rec BY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUserName" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uername")) %>'
                                                Width="80px"></asp:Label>
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
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal modal-drawer fade has-shown" data-backdrop="static" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-drawer-right" role="document" style="max-width: 800px !important;">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header modal-body-scrolled">
                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">BOM Pipeline List
                                <span class="text-flickr small">(** Select Season For better Opearation)</span></h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="card card-fluid">
                                <div class="card-header">
                                    <!-- .nav-tabs -->
                                    <ul class="nav nav-tabs card-header-tabs">

                                        <li class="nav-item">
                                            <a class="nav-link active " data-toggle="tab" href="#tbUnmarked">Pending <span class="badge badge-info" id="PendingCounter" runat="server"></span></a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#tbMarked">Selected  <span class="badge badge-success" id="SelectedCounter" runat="server"></span></a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#Archive">Archive  <span class="badge badge-danger" id="ArchiveCounter" runat="server"></span></a>
                                        </li>
                                    </ul>
                                    <!-- /.nav-tabs -->
                                </div>
                                <div class="card-body ">
                                    <div id="myTabContent" class="tab-content">
                                        <div class="tab-pane fade " id="tbMarked">

                                            <asp:GridView ID="gvBomlist" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="gvArcChkCol" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "planarchive")) %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>

                                                            <%--<asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />--%>
                                                            <asp:CheckBox ID="gvArcchkhead" onclick="javascript:SelectAllCheckboxesforArchive(this);" ClientIDMode="Static" runat="server" />


                                                        </HeaderTemplate>
                                                        <ItemStyle Width="40" />
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" Width="40" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BOM No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvlBOmNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="550px" placeholder="BOM No " onkeyup="Search_Pipeline(this,1, 'gvBomlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblBOmid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvBomNo" CssClass='<%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "pipeline"))==true)?"text-twitter":"text-youtube" %>' runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomdesc")) %>'
                                                                Width="550px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>

                                        </div>

                                        <div class="tab-pane fade active show" id="tbUnmarked">

                                            <asp:GridView ID="gvBomlistPan" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea dataTables_scrollBody" Font-Size="11px">

                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="gvPenChkCol" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "pipeline")) %>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>

                                                            <%--<asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />--%>
                                                            <asp:CheckBox ID="gvPenchkhead" onclick="javascript:SelectAllCheckboxes(this);" ClientIDMode="Static" runat="server" />


                                                        </HeaderTemplate>
                                                        <ItemStyle Width="40" />
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" Width="40" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BOM No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvlPenBOmNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="500px" placeholder="BOM No " onkeyup="Search_Pipeline(this,1, 'gvBomlistPan')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblgvPenBOmid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvPenBomNo" CssClass='<%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "pipeline"))==true)?"text-success":"text-github" %>' runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomdesc")) %>'
                                                                Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>

                                        </div>

                                        <div class="tab-pane fade" id="Archive">

                                            <asp:GridView ID="gvArchived" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="BOM No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvlArcBOmNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="550px" placeholder="BOM No " onkeyup="Search_Pipeline(this,0, 'gvArchived')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblBOmid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvBomNo" CssClass='text-danger' runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomdesc")) %>'
                                                                Width="550px"></asp:Label>
                                                        </ItemTemplate>
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
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->

                            <!-- /.modal-footer -->
                        </div>
                        <div class="modal-footer modal-body-scrolled">
                            <asp:LinkButton ID="LbtnUpdatePipeline" OnClientClick=" return AlertConfirm();" OnClick="LbtnUpdatePipeline_Click" runat="server" CssClass="btn btn-success"><span class="fa fa-save"></span> Selected Item Push To Pipeline/Archive</asp:LinkButton>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>

























        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

