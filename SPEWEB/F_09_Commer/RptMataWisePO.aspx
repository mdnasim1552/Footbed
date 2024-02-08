<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMataWisePO.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptMataWisePO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });


            $(function () {

                $('[id*=ddlWeek]').multiselect({
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
                    maxHeight: 250

                })
                //$(".Multidropdown button").addClass("multiselect dropdown-toggle btn btn-default btn-sm");
            });
        }

        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            tblData = document.getElementById("<%=gvPOReport.ClientID %>");




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

    </script>

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        label {
            display: inline-block;
            margin-bottom: 0rem;
        }

        /*.Multidropdown ul {
            top: -47px !important;
        }*/

        .Multidropdown b.caret {
            display: none !important;
        }

        .Multidropdown ul.dropdown-menu {
            min-width: 15rem;
        }

        .Multidropdown .multiselect-container > li > a > label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 3px 2px 3px 2px;
            font-size: 12px;
        }


        .multiselect-container {
            position: absolute;
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow-y: scroll;
            overflow-x: hidden;
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
                        <div class="col-md-1" runat="server" id="divtxtFDate">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="label" ID="FromDate">From Date</asp:Label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divtxtdate">
                            <div class="form-group">
                                <asp:Label runat="server" ID="ToDate">To Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divDdlSeason">
                            <div class="form-group">
                                <asp:Label ID="lblSeason" runat="server" CssClass="label ">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select chzn-single" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divddlYear">
                            <div class="form-group">
                                <asp:Label ID="lblYear" runat="server">Year</asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control form-control-sm chzn-select chzn-single" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:Label runat="server" ID="lblWeek">Week</asp:Label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 3px; height: 30px;">
                                <asp:ListBox ID="ddlWeek" SelectionMode="Multiple" CssClass="form-control form-control-sm" runat="server"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" visible="false">
                           <%-- <label>Week</label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; width: 130px; font-size: x-small">
                                <asp:ListBox ID="lbxWeek" SelectionMode="Multiple" Font-Size="X-Small" CssClass="form-control multiselect-search" runat="server"></asp:ListBox>
                            </div>--%>
                        </div>

                        <div class="col-md-1 text-center">
                            <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn-sm btn btn-primary" Style="margin-top: 20px;" OnClick="lnkbtnok_Click">Ok</asp:LinkButton>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="label" ID="ddlUserName">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
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
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="divddlType">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="lblType" ID="Label1">Type</asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                    Width="85px">
                                    <asp:ListItem Value="details">Details</asp:ListItem>
                                    <asp:ListItem Value="summary">Summary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-3">
                            <div class="row">
                                <div class="col-md-6" runat="server" id="divddlCodeBook">
                                    <label class="label" for="MatGroup">Mat. Group</label>
                                    <asp:DropDownList runat="server" ID="ddlCodeBook" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-6">
                                    <label runat="server" id="lblSubGroup">Sub Group</label>
                                    <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 3px; height: 30px;">
                                        <asp:ListBox ID="ddlGroup" SelectionMode="Multiple" CssClass="form-control form-control-sm multiselect-search" runat="server"></asp:ListBox>
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="col-md-2" style="margin-top: 20px;">
                            <div class="input-group input-group-alt">
                                <asp:TextBox runat="server" ID="txtSrcMat" Width="100px" CssClass="form-control form-control-sm" placeholder="Search Material"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:LinkButton runat="server" ID="lnkbtnSearch" CssClass="input-group-text" OnClick="lnkbtnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid mt-0" style="min-height: 550px;">
                <div class="card-body">

                    <div class="row px-2 justify-content-end mb-1">
                        <%--<asp:HyperLink></asp:HyperLink>--%>
                        <asp:HyperLink runat="server" ID="lnkbtnExptExcel" CssClass="btn btn-sm btn-primary text-light" Visible="false">
                            <i class="fa fa-file-excel mr-1"></i> Download Excel
                        </asp:HyperLink>
                    </div>
                    <div class=" table-responsive">

                        <asp:GridView ID="gvPOReport" runat="server" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvPOReport_PageIndexChanging" ShowFooter="True"
                            Width="501px" OnRowDataBound="gvPOReport_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PO Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqdat" runat="server"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Store Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))  %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchllmatdesc" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Material" onkeyup="Search_Gridview(this,3, 'gvPOReport')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmatdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Thickness/ Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvspcdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcdesc"))  %>'
                                            Width="110px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" HeaderStyle-Width="60px">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtScolordesc" BackColor="Transparent" BorderStyle="None" Width="60px" runat="server" placeholder="Color" onkeyup="Search_Gridview(this,5, 'gvPOReport')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcolordesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc"))  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LxW">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsizedesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc"))  %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvssirdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc"))  %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PO">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcustompon" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon"))  %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Season">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcustompon" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season"))  %>'
                                            Width="45px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtl" CssClass="font-weight-bold" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Qty" HeaderStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordrqty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtlOrdQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordrRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrate")).ToString("#,##0.0000;(#,##0.0000); ")  %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtlOrdAmt" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordamt" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc"))  %>'
                                            Width="60px" CssClass="text-left"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>

                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />

                        </asp:GridView>

                    </div>

                    <div class="table-responsive">

                        <asp:GridView ID="gvWeekWiseMat" runat="server" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvWeekWiseMat_PageIndexChanging" ShowFooter="True">
                            <Columns>

                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right" Font-Size="X-Small"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mat. Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMrsirdesc" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtl" CssClass="font-weight-bold" Text="Total" style="text-align:right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Specification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMspcfdesc" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Consumed qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMconqty" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtlConsQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Stock qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMstockqty" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtlStckQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shipment qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMshipqty" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblShipQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Remaining </br> Receive Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWMonrcvqty" runat="server" Font-Size="X-Small"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onrcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtlRcvQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />

                        </asp:GridView>
                    </div>


                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

