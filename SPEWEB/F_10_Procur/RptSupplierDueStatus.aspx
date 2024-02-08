<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptSupplierDueStatus.aspx.cs" Inherits="SPEWEB.F_10_Procur.RptSupplierDueStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        // alert("sfsfsfsf");

        //For navigating using left and right arrow of the keyboard
        //function FnSuccess() {
        //   // alert("sfsfsf");
        //    $.toaster({ message: 'Congratulation!! Your Registration Successfully Done', title: '<span class="glyphicon glyphicon-info-sign"></span> Information', priority: 'success' });

        //}

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {

                case "gvPromData":
                    tblData = document.getElementById("<%=gvPromData.ClientID %>");
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

        function FnDanger() {
            $.toaster('Sorry No Data Found of this Supplier', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function CurrentTabSummary() {
            document.getElementById("currentTabNow").value = "summaryTab"
        }

        function CurrentTabDetail() {
            document.getElementById("currentTabNow").value = "detailTab"
        }

        function CurrentTabEmployee() {
            document.getElementById("currentTabNow").value = "emplTab"
        }

        function pageLoaded() {

            <%--var gvPromData = $('#<%=this.gvPromData.ClientID %>');
            gvPromData.gridviewScroll({
                width: 1220,
                height: 410,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });--%>

            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblDate1" runat="server" class="label" for="ToDate">From:</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblDate2" runat="server" class="label" for="ToDate">To:</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblSupl" runat="server" class="label">Supplier</asp:Label>
                                <asp:DropDownList ID="ddlSuplier" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 form-group" style="margin-left: 50px">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" Style="margin-top: 20px" Text="Ok" OnClick="lnkbtnOk_Click" CssClass="btn btn-sm btn-primary"></asp:LinkButton>
                        </div>

                        <div class="col-md-1 form-group" style="margin-left: -60px">
                            <asp:Label ID="lblPage" runat="server" Text="Page Size" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>500</asp:ListItem>
                                <asp:ListItem>900</asp:ListItem>
                                <asp:ListItem>1500</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2 form-group" style="margin-top: 25px">
                            <div class="">
                                <asp:CheckBox ID="ChkSum" runat="server" CssClass="btn-outline-primary" Text="Outstanding Summary" OnCheckedChanged="ChkSum_CheckedChanged" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid" style="min-height: 550px;">
                <div class="card-body row">
                    <asp:MultiView ID="Multiview1" runat="server">

                        <asp:View ID="SupPayHis" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvSupDueStatus" runat="server" AutoGenerateColumns="False" Visible="true" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvSupDueStatus_RowDataBound">
                                    <PagerSettings Position="Top" />

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: left"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier's Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="240px"></asp:HyperLink>
                                                <asp:Label ID="lblgvrescode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="240px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right">Total:</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnam" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFopnam" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Purchase">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcram" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFcram" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Purchase Return">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpurret" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purret")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpurret" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Purchase">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetpur" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpur")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFnetpur" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Adjustment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdram" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFdram" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalamt" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFbalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                                <asp:GridView ID="gvOutstndSum" runat="server" AutoGenerateColumns="False" Visible="false" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Position="Top" />

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: left"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier's Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc1" runat="server" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="240px"></asp:HyperLink>
                                                <asp:Label ID="lblgvrescode1" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="240px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right">Total:</asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Due">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdue" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdue")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvCurDue" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment 1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpm1" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment1")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvpm1tt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment 2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpm2" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment1")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvpm2tt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Payment 3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpm3" runat="server" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment3")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvpm3tt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
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

                        <asp:View ID="ViewPromHistory" runat="server">

                            <div class="col-md-12">
                                <div class="card card-fluid">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:TextBox runat="server" ID="currentTabNow" ClientIDMode="Static" Style="display: none" />

                                                <header class="card-header">
                                                    <ul class="nav nav-tabs card-header-tabs">
                                                        <li class="nav-item">
                                                            <a class="nav-link active show" data-toggle="tab" href="#home" runat="server" id="hometab" onclick="CurrentTabDetail()" clientidmode="static">Details</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" data-toggle="tab" href="#summary" id="summarytab" onclick="CurrentTabSummary()">Item Summary</a>
                                                        </li>
                                                        <li class="nav-item">
                                                            <a class="nav-link" data-toggle="tab" href="#employee" runat="server" id="empltab" onclick="CurrentTabEmployee()" clientidmode="static">Employee Wise</a>
                                                        </li>

                                                        <div class="col-md-3" style="margin-left: auto">
                                                            <asp:Label ID="Label15" runat="server" CssClass="label" Text="Store Name:"></asp:Label>
                                                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-2 ">
                                                            <asp:Label ID="lblEmployee" runat="server" CssClass="label" Text="List:"></asp:Label>
                                                            <asp:DropDownList ID="ddlEmpList" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                                        </div>

                                                    </ul>

                                                </header>

                                                <div class="card-body">
                                                    <div id="myTabContent" class="tab-content">
                                                        <div class="tab-pane fade active show" id="home" runat="server" clientidmode="static">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="gvPromData" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                                            OnPageIndexChanging="gvPromData_PageIndexChanging" CssClass="table-striped  table-hover table-bordered grvContentarea" AllowPaging="true">
                                                                            <RowStyle />

                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Sl">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvSlNo" runat="server" 
                                                                                            Style="text-align: right"
                                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25px" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldate" runat="server" Font-Size="11px"
                                                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "issuedat")).ToString("dd-MMM-yyyy") %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Challan No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueno" runat="server" Font-Size="11px"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Card No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvTeamCard" runat="server" 
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                                            Width="70px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Name of Person">
                                                                                    <HeaderTemplate>
                                                                                        <table style="width: 150px;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblrsirdesc" runat="server" 
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                                            Width="140px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="">
                                                                                    <HeaderTemplate>
                                                                                        <asp:TextBox ID="txtSearchdept" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Department" onkeyup="Search_Gridview(this, 5, 'gvPromData')"></asp:TextBox><br />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvDept" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                                                            Width="110px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Area">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvArea" runat="server" 
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "territory")) %>'
                                                                                            Width="80px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="">
                                                                                    <HeaderTemplate>
                                                                                        <asp:TextBox ID="txtSearchItmNm" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="Item Name" onkeyup="Search_Gridview(this, 7, 'gvPromData')"></asp:TextBox><br />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvrsirdesc" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                                            Width="180px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="">
                                                                                    <HeaderTemplate>
                                                                                        <asp:TextBox ID="txtSearchSpcf" BackColor="Transparent" BorderStyle="None" runat="server" Width="230px" placeholder="Specification" onkeyup="Search_Gridview(this, 8, 'gvPromData')"></asp:TextBox><br />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvspcfdesc" runat="server" Font-Size="11px"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                                            Width="230px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Unit">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvUnit" runat="server" 
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                                            Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueqty" runat="server"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                                                            Width="55px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueqty" runat="server" Font-Bold="True" Font-Size="11px"
                                                                                            ForeColor="#000" Width="55px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblrate" runat="server" 
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="65px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFrate" runat="server" Font-Bold="True" Font-Size="11px"
                                                                                            ForeColor="#000" Width="65px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueamt" runat="server"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="85px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueamt" runat="server" Font-Bold="True" Font-Size="11px"
                                                                                            ForeColor="#000" Width="85px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Remarks">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvnarr" runat="server" Font-Size="10px"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "narr")) %>'
                                                                                            Width="100px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                            </Columns>

                                                                            <FooterStyle CssClass="grvFooter" />
                                                                            <PagerStyle CssClass="gvPagination" />
                                                                            <HeaderStyle CssClass="grvHeader" />
                                                                            <EditRowStyle />
                                                                            <AlternatingRowStyle />

                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="tab-pane fade" id="summary" ClientIDMode="static">
                                                            <div class="row">
                                                                <div class="col-md-9">
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="gvPromSumm" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                                                            <RowStyle />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Sl">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25px" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Item Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvrsirdesc" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                                            Width="250px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Specification">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvspcfdesc" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                                            Width="320px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="left" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Unit">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvUnitSum" runat="server" 
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                                            Width="50px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                    <HeaderStyle VerticalAlign="Top" />
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueqty" runat="server"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                                                            Width="70px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueqty" runat="server" Font-Bold="True" ForeColor="#000" Width="70px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblrate" runat="server" 
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="75px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFrate" runat="server" Font-Bold="True" ForeColor="#000" Width="75px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueamt" runat="server" 
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="85px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueamt" runat="server" Font-Bold="True" ForeColor="#000" Width="85px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <HeaderStyle HorizontalAlign="right" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <FooterStyle BackColor="#F5F5F5" />
                                                                            <EditRowStyle />
                                                                            <AlternatingRowStyle />
                                                                            <PagerStyle CssClass="gvPagination" />
                                                                            <HeaderStyle CssClass="grvHeader" />
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="tab-pane fade" id="employee" ClientIDMode="static">
                                                            <div class="row">
                                                                <div class="col-md-5">
                                                                    <div class="table-responsive">
                                                                        <asp:GridView ID="gvpromhisteam" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                                            CssClass="table-striped table-hover table-bordered grvContentarea">

                                                                            <RowStyle />
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Sl">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right"
                                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="25px" />
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="ID No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblIdCard" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                                            Width="60px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Person Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvrsirdesc" runat="server"
                                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                                            Width="180px"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Issue Qty">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueqty" runat="server" Font-Size="12px" Width="100px"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                            ForeColor="#000" Width="100px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblissueamt" runat="server"
                                                                                            Font-Size="12px"
                                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                            Width="100px" Style="text-align: right"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        <asp:Label ID="lblFissueamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                            ForeColor="#000" Width="100px"></asp:Label>
                                                                                    </FooterTemplate>
                                                                                    <ItemStyle HorizontalAlign="right" />
                                                                                    <HeaderStyle VerticalAlign="Top" Font-Bold="true" />
                                                                                    <FooterStyle HorizontalAlign="right" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                            <FooterStyle BackColor="#F5F5F5" />
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
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:View>

                        <asp:View ID="IQCInspection" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvIQCReport" runat="server" AutoGenerateColumns="False" Visible="true" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" 
                                    AllowPaging="true" OnRowDataBound="gvIQCReport_RowDataBound">
                                    <PagerSettings Position="Top" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: left"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuppnm" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInvno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chalanno")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                       
                                        <asp:TemplateField HeaderText="Arrival Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvArrDt" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmnm" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcf" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInvQty" runat="server" Width="75px"
                                                    Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFInvQty" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Inspected Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvInsQty" runat="server" Width="75px"
                                                    Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFInsQty" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Passed Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPasQty" runat="server" Width="75px"
                                                    Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passqty")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFPasQty" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rejected Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRejQty" runat="server" Width="75px"
                                                    Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejqty")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFRejQty" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rejection %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRejRatio" runat="server" Width="75px"
                                                    Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFRejRatio" runat="server" Font-Bold="True" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRslt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qcstatus")) == "True" ? "Passed" : "Failed" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRsn" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finding")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accepted With Comments">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chckdetails")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRemrks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
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

                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
