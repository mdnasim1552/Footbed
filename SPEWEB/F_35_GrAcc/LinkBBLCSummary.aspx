<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkBBLCSummary.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkBBLCSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <div class="container moduleItemWrpper">
                 <div class="contentPart">
                        <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                               <asp:Panel ID="Panel1" runat="server">
                                       <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lbltxtDateRange" runat="server" CssClass="lblTxt lblName"  Text="Date:"></asp:Label>

                                                 <asp:Label ID="lblDaterange" runat="server" CssClass=" inputtextbox" Width="190px"></asp:Label>

                                                  <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to"  Text="Page Size:"></asp:Label>

                                                

                                              
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100">100</asp:ListItem>
                                                        <asp:ListItem Value="150">150</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                    </asp:DropDownList>

                                              
                                           </div>
                              </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                     <div class="table table-responsive">
                         <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="OrderWiseSupplier" runat="server">
                                    <asp:GridView ID="gvOrderStatus" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvOrderStatus_PageIndexChanging" ShowFooter="True" Width="501px">
                            
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job #">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFGrandTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px">Grand Total</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvJobNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobno")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBuyer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvorder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Export Master LC">
                                            <ItemTemplate>
                                                <asp:Label ID="lgMasterLc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mainmlcdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LC Amount(FC)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlcamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BBLC Amount(FC) ">
                                            <FooterTemplate>
                                                <asp:HyperLink ID="hlnkgvFbblcamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Target="_blank" Style="text-align: right" Width="100px"></asp:HyperLink>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbblcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFperonlc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperonlc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peronlc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewSupplierWise" runat="server">
                                <asp:GridView ID="gvSupplierWise" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvSupplierWise_PageIndexChanging" ShowFooter="True" Width="501px"
                                    OnRowDataBound="gvSupplierWise_RowDataBound">
                               
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFGrandTotals" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Width="70px">Grand Total</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvssupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsJobNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobno")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsBuyer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsorder" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Export Master LC">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsMasterLc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mainmlcdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LC Amount(FC)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvslcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BBLC #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsbblcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BBLC Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsbblcdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BBLC Amount(FC) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsbblcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFbblcamts" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                        <FooterStyle CssClass="grvFooter"/>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>

                     </div>

                </div>
             </div>

                            <%--<table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltxtDateRange" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="color: #FFFFFF;" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style20">
                                        <asp:Label ID="lblDaterange" runat="server" BackColor="#000066" BorderColor="Yellow"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                                            Width="240px"></asp:Label>
                                    </td>
                                    <td class="style22">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;"
                                            Text="Page Size:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style23">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="80px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>--%>
              
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
