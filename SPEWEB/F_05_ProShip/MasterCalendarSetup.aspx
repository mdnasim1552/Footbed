<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MasterCalendarSetup.aspx.cs" Inherits="SPEWEB.F_05_ProShip.MasterCalendarSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#linesModal').modal('hide');
        }

        $(document).ready(function () {

            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);
            //});
            $('.chzn-select').chosen({ search_contains: true });
            // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);  

            //$("#lblgvLine").on("click", function () {
            //    console.log("Clicked");
            //})

        });

        function PopulateLinesModal(strKey) {
            let linename = document.getElementById("linename");            
            var linecodedata = $(strKey).attr("data-linecode");
            //console.log(linecodedata);
            linename.innerHTML = strKey.innerText;
            
            document.getElementById("TxtLineCode").value = linecodedata;
            document.getElementById("TxtLineCode").text = linecodedata;

        }

        function pageLoaded() {
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblProcess" Visible="false" runat="server" CssClass="label">Process</asp:Label>
                                <asp:DropDownList ID="ddlProcess" Visible="false" runat="server" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="LblHour" Visible="false" runat="server" CssClass="label">Hours</asp:Label>
                                <asp:TextBox ID="TxtHours" Visible="false" runat="server" CssClass="form-control form-control-sm" TabIndex="2"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="LbtnPopulate" runat="server" OnClick="LbtnPopulate_Click" Visible="false" Text="Populate" CssClass="btn btn-warning btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="card card-fluid">

                <div class="card-body">
                    <div class="row" style="min-height: 400px;">

                        <div class="col-md-12">
                            <asp:MultiView ID="Multiview1" runat="server">
                                <asp:View ID="Mastercalendar" runat="server">
                                    <div class="row">
                                        <div class="col-md-4">

                                            <asp:GridView ID="gvMasterCalendar" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvMasterCalendar_RowDataBound"
                                                Width="260px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                        HeaderText="" ItemStyle-Font-Size="12px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                                Width="15px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="70px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDays" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdate1")) %>'
                                                                Width="50px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day Type">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DdlgvDayType" runat="server"
                                                                Width="120px" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="WRK">Working Day</asp:ListItem>
                                                                <asp:ListItem Value="WKD">Weekend</asp:ListItem>
                                                                <asp:ListItem Value="HOL">Holiday</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblgvRemarks" runat="server" CssClass="form-control form-control-sm" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                Width="150px"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-8">
                                            <embed type="text/html" src="yearcalendar.html" style="width: 100%; height: 100%; overflow: hidden !important; border: none;">
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="ViewPlanCalendar" runat="server">
                                    <div class="row">
                                        <div class="col-md-6">


                                            <asp:GridView ID="gvplan" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvplan_RowDataBound"
                                                Width="260px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                        HeaderText="" ItemStyle-Font-Size="12px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvslplan" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                                Width="15px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblgvDate" OnClick="lblgvDate_Click" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDay" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdate1")) %>'
                                                                Width="60px"></asp:Label>
                                                            <asp:Label ID="Lbldayid" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Day Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDayType" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dstatusdet")) %>'
                                                                Width="100px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Alternate Day Type">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="DdlgvAltDaytype" runat="server"
                                                                Width="130px" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="WRK">Working Day</asp:ListItem>
                                                                <asp:ListItem Value="WKD">Weekend</asp:ListItem>
                                                                <asp:ListItem Value="HOL">Holiday</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allocated</br>Hours">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblgvAllocatedHours" runat="server" OnClick="lblgvAllocatedHours_Click" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlhours")).ToString("#,##0.00;") %>'
                                                                Width="60px"></asp:LinkButton>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TxtNotes" runat="server" CssClass Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes")) %>'
                                                                Width="100px"></asp:TextBox>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>

                                        <div class="col-md-6">
                                            <asp:GridView ID="gvlines" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CssClass="table-striped table-hover table-bordered grvContentarea"
                                                Width="260px">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                        HeaderText="" ItemStyle-Font-Size="12px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvslLin" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                                Width="15px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Line">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lblgvLine" ClientIDMode="Static" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' data-toggle="modal" data-linecode='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>' data-target="#linesModal" OnClientClick="PopulateLinesModal(this)">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" Width="130px" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hours">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblgvHours" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkhours")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                        <ItemStyle Font-Size="10px" />
                                                        <FooterStyle HorizontalAlign="right" />
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

                        </div>

                    </div>
                </div>
            </div>

            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Daily Plan Calendar Details</h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDayWiseDeails" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Day">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDay" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProcess" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "processdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvLine" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wrk Hours">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvWrkHours" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkhours")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
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
                                    <asp:GridView ID="gvDaywisePlan" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProcess" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "processdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvLinedesc" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Article">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvArticle" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                        Width="210px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order No">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvOrderNo" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot#">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvLot" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotdesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Style">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvStyledesc" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvColordesc" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: left;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvQtydesc" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

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
                            </div>

                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnPlnModfied" OnClick="lbtnPlnModfied_Click" runat="server" OnClientClick="return confirm('Do you want to save?')" CssClass="btn btn-primary">Update</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="linesModal" tabindex="-1" role="dialog" aria-labelledby="linesModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="linesModalLabel">Line Setup</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        
                        <div class="modal-body mb-4">

                            <div class="d-flex alert alert-secondary justify-content-center">
                                <label id="linename" class="text-center font-weight-bold"></label>
                                 <asp:TextBox ID="TxtLineCode" ClientIDMode="Static" runat="server" class="d-none"></asp:TextBox>
                            </div>
                            
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <asp:Label ID="lblFromDt2" runat="server" CssClass="label">From</asp:Label>
                                    <asp:TextBox ID="txtFromDt2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFromDt2"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <asp:Label ID="lblToDt2" runat="server" CssClass="label">To</asp:Label>
                                    <asp:TextBox ID="txtToDt2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtToDt2"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <asp:Label ID="lblHour2" runat="server" CssClass="label">Hour</asp:Label>
                                    <asp:TextBox ID="txtHourDt2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="SaveLineDateRange" OnClientClick="CLoseMOdal()" OnClick="SaveLineDateRange_Click" runat="server" CssClass="btn btn-sm btn-primary">Save changes</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

