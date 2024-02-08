<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="InvWiseStockChecker.aspx.cs" Inherits="SPEWEB.F_19_EXP.InvWiseStockChecker" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
           
        }


    </script>

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
                <div class="card-body" style="min-height: 100px;">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblprodate" runat="server" CssClass=" smLbl_to"
                                EnableViewState="False" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"
                                Width="94px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtrcvdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>  
                        <div class="col-md-1" style="margin-top:19px">
                            <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary btn-sm" runat="server" OnClick="lbtnOk_Click" TabIndex="2"> Ok </asp:LinkButton>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group" style="margin-top:20px">
                            <asp:CheckBox ID="CheckBoxStockNill" AutoPostBack="true" Checked="true" OnCheckedChanged="CheckBoxStockNill_CheckedChanged" runat="server" Text="Only Stock Not Available" />
                            </div>
                        </div>
                       <div class="col-md-5">
                           <div class="form-group" style="margin-top:20px">
                           <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="Totalinvqty" runat="server"></asp:LinkButton>
                           <asp:LinkButton CssClass="btn btn-sm btn-info" ID="MissingQty" runat="server"></asp:LinkButton>
                               </div>
                           </div>
               
                    </div>
                </div>
                </div>
            </div>

            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 300px;">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvInvStocks"  AutoGenerateColumns="False" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Article" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExelE" CssClass="btn btn-xs btn-danger" runat="server"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcInvRefno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="10px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcOrderType" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                            Width="260px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Style">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcBuyerDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcStorDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    
                                                </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcSizeDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                            Width="50px" style="text-align:center;"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcbatchDesc" runat="server" style="text-align:right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcStockQty" runat="server" style="text-align:right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Missing Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcMissingQty" runat="server" style="text-align:right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unavqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="In Process <br> Except It">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcInProcessQty" runat="server" style="text-align:right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inprqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Availabe Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcAvailbeQty" runat="server" style="text-align:right;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "availbeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
               
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





