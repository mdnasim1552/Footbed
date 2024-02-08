<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPdBook.aspx.cs" Inherits="SPEWEB.F_04_Sampling.RptPdBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">    

        let itmArr = [];
        let inch = 0;

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function ShowWindow(url) {

            window.open(url, '_blank');
        }

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

            var gv = $('#<%=this.gvpdbook.ClientID %>');
            gv.Scrollable();

            var gv = $('#<%=this.gvKnChkListInfoEntry.ClientID %>');
            gv.Scrollable();

            var gv = $('#<%=this.gvPdBookInfoEntry.ClientID %>');
            gv.Scrollable();

            var gv = $('#<%=this.gvSamReport.ClientID %>');
            gv.Scrollable();
        }

        function openModal() {
            $('#myModal').modal('toggle');
        }

        function openModal2() {
            $('#myKnifeModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#myKnifeModal').modal('hide');
            $('#SizeModal').modal('hide');
            $('#ProductionModal').modal('hide');
            $('#PDBookModal').modal('hide');
            $('#exampleModalCode').modal('hide');

            $('#ProReqModal').modal('hide');
        }

        function Rerunmodal(inqno) {

            $("#TxtSdino").val(inqno);
        }

        function Search_Gridview(strKey, cellNr, gvName) {
            var tblData;
            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvSamReport":
                    tblData = document.getElementById("<%=gvSamReport.ClientID %>");
                    break;

                case "gvPdBookInfoEntry":
                    tblData = document.getElementById("<%=gvPdBookInfoEntry.ClientID %>");
                    break;

                case "gvKnChkListInfoEntry":
                    tblData = document.getElementById("<%=gvKnChkListInfoEntry.ClientID %>");
                    break;

                case "gvpdbook":
                    tblData = document.getElementById("<%=gvpdbook.ClientID %>");
                    break;
            }

            var rowData;
            for (var i = 0; i < tblData.rows.length; i++) {
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


        function bindSdiToModal(lnkbtn) {
            $("#<%= txtbxSdiNo.ClientID %>").val(lnkbtn.dataset.inqno);
        }


        function SelectAllCheckboxes(gridName, chk) {


            switch (gridName) {

                case "gvSamReport":

                    $('#<%=gvSamReport.ClientID %>').find("input:checkbox").each(function () {

                        if ($(this).closest('tr').attr('class') == "grvRows") {

                            if ($(this).closest('tr').css('display') != "none") {

                                if ((this).disabled == false) {
                                    if (this != chk) {
                                        this.checked = chk.checked;
                                    }
                                }
                            }
                        }
                    });

                    break;

            }

        }

        function SelectAllCheckboxes(chk) {
            itmArr = [];
            $("#lstSelectedItems").empty();
            var tblData1 = document.getElementById("<%=gvknifentry.ClientID %>");
            var i = 0;
            var totalInch = 0;

            $('#<%=gvknifentry.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;

                        let itmName = this.parentElement.parentElement.cells[3].querySelector(".itm-name").innerText;
                        let itmInch = parseFloat(this.parentElement.parentElement.cells[7].querySelector(".inch").value.replaceAll(",", ""))
                        itmInch = isNaN(itmInch) ? 0 : itmInch;

                        totalInch += itmInch;

                        let itmObj = { name: "", inch: 0 };
                        itmObj.name = itmName;
                        itmObj.inch = itmInch;

                        let isExist = false;
                        let index = 0;

                        itmArr.forEach(item => {
                            if (item.name == itmObj.name) {
                                index = itmArr.indexOf(item);
                                isExist = true;
                            }
                        });

                        if (chk.checked) {
                            if (isExist) {
                                itmArr[index].inch += itmObj.inch;
                            } else {
                                itmArr.push(itmObj);
                            }
                        } else {
                            itmArr = [];
                            $("#lstSelectedItems").empty();
                        }
                    }
                    i = i + 1;
                }
            });

            let ttlInchNode = document.querySelector("#selectedTotal");
            if (chk.checked) {
                ttlInchNode.innerHTML = totalInch;
            } else {
                ttlInchNode.innerHTML = 0;
            }

            let itmList = "";

            itmArr.forEach(item => {
                itmList += `
            <li class="list-group-item d-flex justify-content-between align-items-center bg-twitter text-light border border-bottom border-light py-2 small">${item.name}
                <span class="badge badge-light badge-pill text-dark itm-qty">${item.inch}</span>
            </li>
        `;
            });

            $("#lstSelectedItems").append(itmList);
        }




        function CountSelectedInch(chk) {
            let itmName = chk.parentElement.parentElement.cells[3].querySelector(".itm-name").innerText;
            let itmInch = parseFloat(chk.parentElement.parentElement.cells[7].querySelector(".inch").value.replaceAll(",", ""));
            itmInch = isNaN(itmInch) ? 0 : itmInch;

            let itmObj = { name: "", inch: 0 };

            $("#lstSelectedItems").empty();

            if (chk.checked) {
                itmObj.name = itmName;
                itmObj.inch = itmInch;

                itmArr.push(itmObj);
            } else {
                itmObj.name = itmName;
                itmObj.inch = itmInch;

                itmArr.forEach(item => {
                    if (item.name == itmObj.name) {
                        let index = itmArr.indexOf(item);
                        item.inch -= itmInch;

                        if (itmArr[index].inch <= 0) {
                            itmArr.splice(index, 1);
                        }
                    }
                });
            }

            let totalInch = 0;

            itmArr.forEach(item => {
                totalInch += item.inch;
            });

            let ttlInchNode = document.querySelector("#selectedTotal");
            ttlInchNode.innerHTML = totalInch;

            let itmList = "";

            itmArr.forEach(item => {
                itmList += `
            <li class="list-group-item d-flex justify-content-between align-items-center bg-twitter text-light border border-bottom border-light py-2 small">${item.name}
                <span class="badge badge-light badge-pill text-dark itm-qty">${item.inch}</span>
            </li>
        `;
            });

            $("#lstSelectedItems").append(itmList);
        }


        function CheckNotice() {
            var r = confirm("Do you want to Make New Requistion?");
            console.log(r);
            if (r == true) {
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

        function ShowWindow(url) {
            window.open(url, '_blank');
        }


    </script>
    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-1 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-6 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 pading5px">
                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to text-left">Season</asp:Label>
                            <div class="form-inline">
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2 col-lg-2" runat="server" id="divDdlSmpltype">
                            <asp:Label runat="server" ID="lblSmplType" class="">Sample Type</asp:Label>
                            <asp:DropDownList ID="ddlSmpltype" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                        </div>

                        <div class="row col-md-4 col-sm-4 col-lg-4 pading5px" id="datewise" runat="server" visible="false">
                            <div class="col-md-6 col-sm-6 col-lg-6 pading5px">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Agent Name</asp:Label>
                                <div class="form-group">

                                    <asp:DropDownList ID="DdlAgent" runat="server" OnSelectedIndexChanged="DdlAgent_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6 col-lg-6 ">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-2 col-md-2" runat="server" id="divDdlCategory" visible="false">
                            <div class="form-group">
                                <asp:Label ID="LblCategory" runat="server" Text="Category Name" CssClass="control-label"></asp:Label>
                                <asp:DropDownList ID="DdlCategory" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-2 col-md-2" runat="server" id="divDdlShoetype" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Shoe Type" CssClass="control-label"></asp:Label>
                                <asp:DropDownList ID="DdlShoType" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" id="divSrcArt">
                            <div class="form-group">
                                <asp:CheckBox ID="CbReqOnly" Style="margin-bottom: 0px !important" runat="server" CssClass="" Text="REQ Completed Only" AutoPostBack="true" OnCheckedChanged="CbReqOnly_CheckedChanged" />

                                <div class="input-group input-group-alt">
                                    <asp:TextBox runat="server" ID="txtSrcArt" Width="100px" CssClass="form-control form-control-sm" placeholder="Search Article"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lnkbtnSearch" CssClass="input-group-text" OnClick="lnkbtnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1" id="ReqBtn" runat="server" visible="false" style="margin-top: 20px;">
                            <div class="form-group">
                                <a href="#" class="btn btn-sm btn-warning" data-toggle="modal" data-target="#exampleModalDrawerRight" style="font-size: smaller">New Req? <%--<i class="fa fa-file-alt">--%></i></a>
                            </div>
                        </div>


                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body mb-3" style="min-height: 600px">

                    <div class="row table-responsive">
                        <asp:GridView ID="gvpdbook" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea">

                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Season">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSeason" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "seasonnam")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpdLast" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Art. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvartno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCustomer" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size Range">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSizeRange" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSampleSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize"))  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Consumption Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvConsumptionSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pattern Designer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPatternDesigner" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dsgnrnam"))  %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pattern Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPatternLocation" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pattloc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pattern Grading">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpbPatternGrading" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pattgrad")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upper Knife">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUpperKnife" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uppknif")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lining Knife">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLiningKnife" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linknif")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bottom Knife">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpbBottomKnife" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "botknif")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outsole">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpbOutsole" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsole")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpbRmrks" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="120px"></asp:Label>
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

                    <div class="row table-responsive">

                        <asp:GridView ID="gvSamReport" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvSamReport_RowDataBound">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inq.No">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvSearchinqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Search Inq.No" onkeyup="Search_Gridview(this, 1, 'gvSamReport')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinqno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Batch No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatchNo" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hyprrrcom" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                            <asp:Image ID="lblImageUrlcom" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Samp Type Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrSampType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptype")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Samp Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrSampTypeDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inq. Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvartno" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delivery date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCustomer" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldate")).ToString("dd-MMM-yyyy") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLiningKnife" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Article">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvSearchArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Search Article" onkeyup="Search_Gridview(this, 7, 'gvSamReport')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSizeRange" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSampleSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "categorydesc"))  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Agent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvConsumptionSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentdesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Buyer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPatternDesigner" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc"))  %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrPatternGrading" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shoe Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrShoeType" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetypdesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Brand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUpperKnife" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brandesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Season">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrSeason" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "seasondesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Constuction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrConstruction" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "constuctiondesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sample Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrSampSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size Range">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrSizeRnge" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FG Rcv. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrFgRcvQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fgrcvqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shipment Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrShpmntQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrApprv" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvSearchStatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Search Status" onkeyup="Search_Gridview(this, 20, 'gvSamReport')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrStatus" runat="server" CssClass="text-left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstatus")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pro. Req." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsrProReq" runat="server" CssClass="text-left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proreq")) %>'
                                            Width=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <div class="dropdown">
                                            <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                Action                                                                             
                                            </button>
                                            <ul class="dropdown-menu dropdown-menu-right px-2" style="width: 230px;">
                                                <%--<li>
                                                    <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Sheet</asp:HyperLink>
                                                </li>--%>
                                                <li>
                                                    <asp:HyperLink ID="HypPdGuidPrint" runat="server" Target="_blank" Font-Underline="false">
                                                        <span class="fa fa-print mr-2"></span>PD Guide
                                                    </asp:HyperLink>
                                                </li>

                                                <li>
                                                    <a data-toggle="modal" id="Retunbtn" data-inqno='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>' class="link text-primary" onclick="Rerunmodal('<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno"))%>')" data-target="#exampleModalSm">
                                                        <span class="fa fa-reply mr-2"></span>Re-Run
                                                    </a>
                                                </li>

                                                <li>
                                                    <asp:LinkButton ID="LbtnFinal" OnClick="LbtnFinal_Click" runat="server">
                                                        <span class="fa fa-check-circle mr-2"></span>Final Sample
                                                    </asp:LinkButton>
                                                </li>

                                                <li>
                                                    <asp:LinkButton ID="LbtnForward" OnClick="LbtnForward_Click" OnClientClick="return confirm('Do You want Forward This Item?');" runat="server">
                                                        <span class="fa fa-forward mr-2"></span>Forward To Concern Company
                                                    </asp:LinkButton>
                                                </li>

                                                <li>
                                                    <asp:HyperLink ID="hlnkCnsumpSheet" runat="server" Target="_blank" Visible="false">
                                                        <i class="fa fa-link mr-2"></i>Consumption Sheet
                                                    </asp:HyperLink>
                                                </li>

                                                <li>
                                                    <asp:LinkButton ID="lnkbtnWHReq" runat="server" data-inqno='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>' OnClientClick="bindSdiToModal(this)" data-toggle="modal" data-target="#ProReqModal">
                                                        <i class="fa fa-hand-point-up mr-2"></i> Make WH Requisition
                                                    </asp:LinkButton>
                                                </li>

                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbltransstatus" Target="_blank" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transstatus")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="Lbliscomplite" Target="_blank" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "iscomplite")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">

                                    <HeaderTemplate>
                                        <div class="d-flex justify-content-center">
                                            <div class="mr-3">
                                                <asp:LinkButton runat="server" ID="LbtnIssueMulti" OnClick="LbtnIssueMulti_Click" ToolTip="Selected Multiple REQ Issue" CssClass="btn btn-sm btn-success small mr-2">
                                                                <i class="fa fa-check"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton runat="server" ID="lnkbtnPrintCombined" CssClass="btn btn-sm btn-primary" ToolTip="Selected Multiple Req Print" OnClick="lnkbtnPrintCombined_Click">
                                                    <i class="fa fa-print"></i>
                                                </asp:LinkButton>
                                            </div>
                                            <div>
                                                <asp:CheckBox ID="chkhead" CssClass="mt-2" onclick="javascript:SelectAllCheckboxes('gvSamReport', this);" ClientIDMode="Static" runat="server" />
                                            </div>
                                        </div>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkPrintCombined" CssClass="mx-2" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <RowStyle CssClass="grvRows" />
                        </asp:GridView>

                    </div>

                    <div runat="server" id="pnlProReqNote" visible="false" class="mt-3">
                        <span class="py-2 px-3 mr-2" style="background-color: mediumspringgreen;"></span>
                        <label class="mt-1 font-weight-bold">Product Requisition Completed </label>
                    </div>


                    <div class="row">
                        <div class="col-4">
                            <asp:GridView ID="gvKnChkListInfoEntry" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                CssClass="table-striped table-hover table-bordered grvContentarea">

                                <PagerSettings Visible="False" />
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPDIESlNo0" runat="server" Height="16px"
                                                Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Article">
                                        <HeaderTemplate>
                                            <asp:TextBox ID="txtgvPDIESearchArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Search Article" onkeyup="Search_Gridview(this,1, 'gvKnChkListInfoEntry')"></asp:TextBox><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPDIEarticle" runat="server" Style="display: inline-block; width: 150px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSize" runat="server" Style="display: inline-block; width: 85px;" ForeColor="White" BackColor="#44bcd8" CssClass="btn btn-primary btn-sm" Text="Size Add" OnClick="btnSize_Click" CommandName="SizeCommand" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnKnifeEntry" runat="server" Style="display: inline-block; width: 85px;" ForeColor="White" BackColor="#9925be" CssClass="btn btn-primary btn-sm" OnClick="btnKnifeEntry_Click" Text="Knife Entry" CommandName="KnifeEntryCommand" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnImport" runat="server" Style="display: inline-block; width: 85px;" ForeColor="White" BackColor="#548732" CssClass="btn btn-success btn-sm" OnClientClick="return confirm('Do you want to import this article?');" OnClick="btnImport_Click" Text="Import" CommandName="ImportCommand" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                        <div class="col-8">
                            <asp:Panel ID="PanelKnifeEntry" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="LblKnifeEntryHead" Font-Size="Medium" runat="server" CssClass="control-label" Text="Knife Entry for Article- "></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-9" style="margin-left: -75px;">
                                        <div class="form-group">
                                            <asp:Label ID="LblKnifeEntryTxt" Font-Bold="true" Font-Size="Medium" runat="server" CssClass="control-label"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LbtnComponent" CssClass="label" runat="server" OnClick="LbtnComponent_Click" Text="COMPONENT NAME"></asp:LinkButton>

                                            <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control form-control-sm chzn-select" Width=""></asp:DropDownList>

                                        </div>
                                    </div>


                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LbtnMaterial" runat="server" OnClick="LbtnMaterial_Click" CssClass="label" Text="MATERIALS NAME"></asp:LinkButton>
                                            <asp:DropDownList ID="DdlMatecode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlMatecode_SelectedIndexChanged" CssClass="form-control chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                        <div class="form-group">
                                            <asp:Label ID="LblSpecfications" runat="server" CssClass="label" Text="Specifications"></asp:Label>
                                            <asp:DropDownList ID="DdlSpcfcod" runat="server" CssClass="form-control chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-lg-1 col-md-1 col-sm-1">
                                        <div class="form-group" style="margin-top: 20px">
                                            <asp:LinkButton ID="lnkAddKnifeEntry" runat="server" Text="Add Table" OnClick="lnkAddKnifeEntry_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                        </div>
                                    </div>


                                    <div class="col-md-12">

                                        <asp:GridView ID="gvknifentry" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvknifentry_RowDeleting" ClientIDMode="Static"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Font-Size="11px">
                                            <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconsizeSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                            Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="18px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                                <asp:TemplateField HeaderText="Component">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvComponent" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Component Code" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvComponentCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Material">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMaterial" runat="server" CssClass="itm-name"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Material Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMaterialCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpecf" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpecfCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty" runat="server" CssClass="GridItmTextBoxRight" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Con. Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvConQty" runat="server" CssClass="GridItmTextBoxRight" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inch">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvInch" runat="server" CssClass="inch" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inch")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblgvRmks" runat="server" CssClass="GridItmTextBoxRight" BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"
                                                            Font-Size="10px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCol" runat="server" onclick="javascript:CountSelectedInch(this);" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
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

                            </asp:Panel>
                        </div>
                    </div>


                    <div class="row table-responsive">
                        <asp:GridView ID="gvPdBookInfoEntry" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowEditing="gvPdBookInfoEntry_RowEditing"
                            OnRowUpdating="gvPdBookInfoEntry_RowUpdating"
                            OnRowCancelingEdit="gvPdBookInfoEntry_RowCancelingEdit"
                            OnRowDataBound="gvPdBookInfoEntry_RowDataBound">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIESlNo0" runat="server" Height="16px"
                                            Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />

                                <asp:TemplateField HeaderText="Article">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvPDIESearchArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Search Article" onkeyup="Search_Gridview(this,2, 'gvPdBookInfoEntry')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEarticle" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lbleditgvPDIEdesigner" runat="server" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'></asp:Label>
                                    </EditItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designer">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEdesigner" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designerdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:Label ID="lblforddlgvPDIEdesigner" runat="server" Visible="false"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designer")) %>'
                                            Width="60px"></asp:Label>
                                        <asp:DropDownList ID="ddlgvPDIEdesigner" runat="server" CssClass="form-control form-control-sm" Style="height: 24px" Font-Size="X-Small"
                                            BackColor="Transparent" BorderStyle="Solid" BorderColor="Highlight" BorderWidth="2px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Outsole">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEoutsole" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsole")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvPDIEoutsole" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderColor="Highlight" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsole")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PD Book Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEpdboknotes" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pdboknotes")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvPDIEpdboknotes" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderColor="Highlight" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pdboknotes")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Knif">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEknif" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "knif")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "knif")).ToString("dd-MMM-yyyy") %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvPDIEknif" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderColor="Highlight" Width="120px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "knif")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "knif")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="calgvPDIEknif" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvPDIEknif"></cc1:CalendarExtender>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PPT Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEpptdate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvPDIEpptdate" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderColor="Highlight" Width="120px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pptdate")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="calgvPDIEpptdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvPDIEpptdate"></cc1:CalendarExtender>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pattern Grading">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEpattgrad" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pattgrad")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pattgrad")).ToString("dd-MMM-yyyy") %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvPDIEpattgrad" runat="server" BackColor="Transparent"
                                            BorderStyle="Solid" BorderColor="Highlight" Width="120px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pattgrad")).Year.ToString() == "1900" ?
                                                "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pattgrad")).ToString("dd-MMM-yyyy") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="calgvPDIEpattgrad" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvPDIEpattgrad"></cc1:CalendarExtender>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Show Board">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEshowboard" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "showboard")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:Label ID="forddlgvPDIEshowboard" runat="server" Visible="false"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "showboard")) %>'
                                            Width="120px"></asp:Label>
                                        <asp:DropDownList runat="server" BackColor="Transparent" BorderWidth="2px" Font-Size="X-Small"
                                            BorderStyle="Solid" BorderColor="Highlight" ID="ddlgvPDIEshowboard"
                                            CssClass="form-control form-control-sm" Style="height: 23px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MARPATERN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDIEmarpatern" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "marpatern")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:Label ID="forddlgvPDIEmarpatern" runat="server" Visible="false"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "marpatern")) %>'
                                            Width="120px"></asp:Label>
                                        <asp:DropDownList runat="server" BackColor="Transparent" BorderWidth="2px" Font-Size="X-Small"
                                            BorderStyle="Solid" BorderColor="Highlight" ID="ddlgvPDIEmarpatern"
                                            CssClass="form-control form-control-sm" Style="height: 23px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
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


            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Sample Final Submit
                            </h4>
                        </div>

                        <div class="modal-body">
                            <asp:Label ID="ModalSdino" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="LblAlrtMsg" runat="server"></asp:Label><br />
                            <div class="log-divider" runat="server" id="Divider" visible="false">
                                OR                                   
                                
                            </div>
                            <asp:CheckBox ID="MakeNew" runat="server" Checked="true" Text="Make a New Inquiry without merge?" />
                            <div class="alert alert-primary has-icon" role="alert">
                                <div class="alert-icon">
                                    <span class="fa fa-info"></span>
                                </div>
                                This is inform you that after final submit it will appear on Order Accept/Reject Segment in Merchandising Interfaces. Thank you
                            </div>
                        </div>

                        <div class="modal-footer ">
                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="fa fa-save"></span>Final Update </asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


            <div id="exampleModalSm" class="modal fade " tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-sm" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h5 class="modal-title">Sample Re Run </h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="TxtSdino" Style="display: none" Visible="true" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:Label ID="LblCustomer" runat="server" Text="Customer Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="DdlCustomer" CssClass="form-control from-control-sm" runat="server"></asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <label class="label">Select Sample type</label>
                                <asp:DropDownList ID="DdlSamType" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <asp:LinkButton ID="LbtnReRunUpdate" runat="server" CssClass="btn btn-primary" OnClick="LbtnReRunUpdate_Click">Update</asp:LinkButton>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>





            <div class="modal fade" id="ProReqModal" tabindex="-1" aria-labelledby="ProReqModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Pro. Requisition</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row my-3">
                                <div class="col-8 d-none">
                                    <asp:TextBox runat="server" ID="txtbxSdiNo" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-8">
                                    <asp:Label runat="server" ID="lblProReqQty" Text="Pro. Req. Qty."></asp:Label>
                                    <asp:TextBox runat="server" ID="txtProReqQty" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:LinkButton runat="server" ID="lnkbtnSave" class="btn btn-primary" OnClientClick="CLoseMOdal()" OnClick="lnkbtnWHReq_Click">Save changes</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>




            <div class="container-fluid">

                <div id="myKnifeModal" class="modal animated slideInLeft sizecolor" role="dialog">
                    <div class="modal-dialog ">
                        <div class="modal-content  " style="width: 60%">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Add Size</h4>
                            </div>
                            <div class="modal-body">


                                <div class="label label-success"><big>Select Size</big></div>
                                <asp:GridView ID="gvSize" runat="server" AutoGenerateColumns="False" Height="90px"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                    Font-Size="11px">
                                    <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyleSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSizeID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                    Width="51px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="gvChkSize1" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeselect"))=="Y" %>'
                                                    ForeColor="Blue" Style="font-size: 11px" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">

                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSizeDesc" runat="server" Style="border-top-width: 1px; border-left-width: 1px; font-size: 11px; border-bottom-width: 1px; text-align: left; border-right-width: 1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")).Trim() %>'
                                                    Width="40px"></asp:TextBox>
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
                            <div class="modal-footer ">
                                <asp:Label ID="lblStylecode" runat="server" Visible="false"></asp:Label>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="AddSizeButton_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                            </div>
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

                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Select Supplier"></asp:Label>

                                        <asp:DropDownList ID="ddlSupplier" runat="server" Style="width: 650px" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Select Store"></asp:Label>
                                        <asp:DropDownList ID="DDlStore" runat="server" CssClass=" form-control form-control-sm" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Department"></asp:Label>
                                        <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control form-control-sm " TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="MRF No"></asp:Label>
                                        <asp:TextBox ID="TxtMrfno" runat="server" CssClass="form-control form-control-sm" TabIndex="3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Purchase Type"></asp:Label>
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
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Remarks"></asp:Label>
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
                                    inch
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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



