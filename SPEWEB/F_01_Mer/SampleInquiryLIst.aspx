<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SampleInquiryLIst.aspx.cs" Inherits="SPEWEB.F_01_Mer.SampleInquiryLIst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvSmpleinqlist":
                    tblData = document.getElementById("<%=gvSmpleinqlist.ClientID %>");
                    break;
                case "gvCostingSummary":
                    tblData = document.getElementById("<%=gvCostingSummary.ClientID %>");
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

        function SetTarget(type) {

            var baseUrl = "<%= ResolveUrl("~/RDLCViewerWin.aspx?PrintOpt=PDF") %>";
            window.open(baseUrl);
            /*window.open('/RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');*/
            <%-- var baseUrl = "<%= ResolveUrl("~/") %>";--%>
        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            var gvCostingSummary = $('#<%=this.gvCostingSummary.ClientID %>');
            gvCostingSummary.Scrollable();
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server" CssClass="control-label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" Text="Season" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="LblAgent" runat="server" Text="Agent Name" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlAgent" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="LblCustomer" runat="server" Text="Customer" CssClass="control-label"></asp:Label>
                                <asp:DropDownList ID="DdlCustomer" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="LblCategory" runat="server" Text="Category" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlCategory" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LblSampleType" runat="server" Text="Sample Type" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlSampType" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px">
                    <div class="table-responsive">

                        <asp:MultiView ID="Multiview" runat="server">
                            <asp:View ID="SampleInqList" runat="server">

                                <asp:GridView ID="gvSmpleinqlist" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvMarchQutation_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Buyer" onkeyup="Search_Gridview(this,1, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Category" onkeyup="Search_Gridview(this,2, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Image">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Possible Size Range">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Possible Size Range" onkeyup="Search_Gridview(this,5, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample Size">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,6, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Consumption Size">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Consumption Size" onkeyup="Search_Gridview(this,7, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                    Width="80px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attachment </br>with Information">
                                            <ItemTemplate>
                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,9, 'gvSmpleinqlist')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkEdit" Target="_blank" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkIndPrint" OnClick="lnkIndPrint_Click" ToolTip="Individual Inquiry Print" runat="server"><span class="fa fa-print"></span></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Con.">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbtnCons" Target="_blank" ToolTip="Consumption" runat="server"><span class="fa fa-th-list"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbtnCost" Target="_blank" ToolTip="Costing" runat="server"><span class="fa fa-money-bill"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbtnOrder" Target="_blank" ToolTip="Order" runat="server"><span class="fa fa-file-invoice"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>


                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="CostingSummary" runat="server">
                                <asp:Panel ID="pCostingSummary" runat="server">


                                    <asp:GridView ID="gvCostingSummary" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" CssClass="text-center" runat="server" Width="120px" placeholder="Article" onkeyup="Search_Gridview(this,1 , 'gvCostingSummary')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLast" runat="server"
                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "llast")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("imgurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imgurl") %>' Target="_blank">
                                                        <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("imgurl").ToString()=="")?"~/images/no_img_preview.png":Eval("imgurl") %>' class="img-responsive"></asp:Image>
                                                    </asp:HyperLink>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                    Width="80px"></asp:Label>



                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Upper">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvUpper" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uppercom")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lininig">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvlining" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lining")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Socks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvSocks" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "socks")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Outsole">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvOutsole" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsole")) %>'
                                                        Width="270px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Knife">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvKnife" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: center" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "knife")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delivery<br/>Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvKnife" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: center" Font-Size="11px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deliverydate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sample Confirm">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvKnife" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sampleconfirm")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sampleconfirm")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvcustDesc" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Materials <br> Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvMatCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamtc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Outsole <br> OutSourcing">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvMatCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outsdning")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job Work Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvJobWorkCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamtd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Materials <br> Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvMatCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamte")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="LO">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvLoCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "locost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mold <br> Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvMoldCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "moldcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Moccasin">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvMoccasinCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "moccasin")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test & Inspection">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvTestCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "testfee")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net <br> Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvNetCost" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designing <br> Charge">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvDesignComm" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dsigncomm")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No <br> Claim">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvNoClaim" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noclaim")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comission">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvOtherComm" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othercomm")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total In <br> USD">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvTotalUsd" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total In <br>EURO">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvTotalEuro" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamteuro")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Offer <br> Price <br> (FOB)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvOfferPrice" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "offprice")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="P/L">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvProfitLoss" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "profitloss")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% of <br> P/L">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvProfitLossPercnt" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prftlssprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>


                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>






