<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPfiInvList.aspx.cs" Inherits="SPEWEB.F_01_Mer.RptPfiInvList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

        function RidirectToLcinfo(url) {
            window.open(url, target = '_blank');
        }
    </script>

    <style type="text/css">
        .chzn-single{
            height: 29px !important;
            border-radius: 4px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row pb-3">

                        <div class="col-md-1">
                            <asp:Label ID="lblFromDate" runat="server" CssClass="">From Date</asp:Label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblToDate" runat="server" CssClass="">To Date</asp:Label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label runat="server" ID="lblSeason"> Season </asp:Label>
                            <asp:DropDownList runat="server" ID="ddlSeason" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>
                        
                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblBuyer"> Buyer </asp:Label>
                            <asp:DropDownList runat="server" ID="ddlBuyer" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" OnClick="lnkbtnok_Click">Ok</asp:LinkButton>
                        </div>
                        
                        <div class="col-md-1">
                            <asp:HyperLink ID="hyplnkCrtNew" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" Target="_blank" >Create New</asp:HyperLink>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">
                        <asp:GridView ShowFooter="true" ID="gvPfiList" runat="server" AutoGenerateColumns="False" 
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPfiList_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblSl" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PFI No." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblPfiNo" runat="server" BorderColor="#99CCFF" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pfino")) %>' Width="80px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="PFI No.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblPfiNo2" runat="server" BorderColor="#99CCFF" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pfino2")) %>' Width="80px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>                                   
                                <asp:TemplateField HeaderText="Article">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblArtclDesc" runat="server" BorderColor="#99CCFF" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "articledesc")) %>' Width="350px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" Width="350px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Buyer ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblBuyerId" runat="server" BorderColor="#99CCFF" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customerid")) %>' Width="120px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" Width="250px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Buyer Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblBuyerDesc" runat="server" BorderColor="#99CCFF" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "customerdesc")) %>' Width="120px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" Width="80px" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Invocie Date">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblInvDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ref. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblRefNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" Width="120px"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inv Qty.">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblOrdrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString() %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Created By">
                                    <ItemTemplate>
                                        <asp:Label ID="gvplLblCreator" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="gvplHypLnkEdit" runat="server" CssClass="btn btn-sm btn-success" Target="_blank">
                                            <i class="fa fa-edit"></i>
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="gvplLnkBtnDelt" runat="server" CssClass="btn btn-sm btn-danger text-light" OnClick="gvplLnkBtnDelt_Click" OnClientClick="return alert('Do you want to delete this Proforma Invoice?')">
                                            <i class="fa fa-trash-alt"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblPrintIcn" runat="server">Invoice <i class="fa fa-print"></i></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="gvplHypLnkPrint" runat="server" CssClass="btn btn-sm btn-info" Target="_blank">Proforma</asp:HyperLink>
                                        <asp:HyperLink ID="gvplHypLnkPrintCCC" runat="server" CssClass="btn btn-sm btn-outline-info" ForeColor="Black" Target="_blank">Pre-payment</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                
                            </Columns>
                            <AlternatingRowStyle />
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
