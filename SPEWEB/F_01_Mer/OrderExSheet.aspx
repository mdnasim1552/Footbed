<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="OrderExSheet.aspx.cs" Inherits="SPEWEB.F_01_Mer.OrderExSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>


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

            <%--var gvOrderExSheet = $('#<%=this.gvOrderExSheet.ClientID %>');
            gvOrderExSheet.Scrollable();--%>

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server" CssClass="control-label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
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
                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvOrderExSheet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" HeaderStyle-VerticalAlign="Middle">
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

                                    <asp:TemplateField HeaderText="BOM No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBomno" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>

                                            <table>

                                                <tr>
                                                    <th class="">PO Number                                                             
                                                    </th>
                                                    <th class="pull-right">
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                            Font-Bold="True"
                                                            ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                    </th>
                                                </tr>

                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPONumber" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="70px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cust. Ref No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCustRefNo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Last Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCustRefNo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lforma")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Art. Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvArtName" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Picture">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("imgpath").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("imgpath").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQuantity" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrqty")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCustomer" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Brand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBrand" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branddesc")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Entry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEntryDate" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dateval")).ToString("dd-MMM-yyyy") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Upper">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUpper" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "uppercom")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lining">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLining" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lining")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Socks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSocks" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "socks")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Outsole">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOutsole" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outsole")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Merchantdiser">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMerchantdiser" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Agent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAgent" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size Range">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSizeRange" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category Ladies/Gents">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCategory" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendata")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="P. Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvType" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "constuctiondesc")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Oder Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOderStatus" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderstatus")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="1st SMPL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv1stSMPL" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2nd SMPL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv2ndSMPL" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="3rd SMPL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv3rdSMPL" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approval Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvApprovalDate" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdate")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Packing Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPackingApproval" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BOM SCM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBOMSCM" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PI Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPIStatus" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="BOM PROD">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBOMPROD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="LC Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLCStatus" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Production Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProdStatus" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="1st Inspection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv1stInspection" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="2nd Inspection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv2ndInspection" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="3rd Inspection">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgv3rdInspection" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ex Factory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvExFactory" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Week of Ex Factory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvWeekExFactory" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Actual Ex Factory">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvActualExFactory" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ETD">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvETD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Week Of ETD">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvWeekETD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvInvoiceNo" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Test">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTest" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Price(Euro)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvEuro" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Price(USD)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUSD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Price(Euro)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotalEuro" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Price(USD)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotalUSD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotalUSD" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text=''
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

