<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="ReportMLCProduction.aspx.cs" Inherits="SPEWEB.F_15_Pro.ReportMLCProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <div class="container moduleItemWrpper">
                    <div class="contentPart">

                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 asitCol6 pading5px">
                                            <asp:Label ID="lfd" runat="server" CssClass="lblTxt lblName"
                                                Text="Master L/C:"></asp:Label>


                                            <asp:DropDownList ID="ddlMLc" runat="server" AutoPostBack="True" CssClass=" ddlPage" OnSelectedIndexChanged="ddlMLc_SelectedIndexChanged"
                                                Width="310px"
                                                TabIndex="2">
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click"
                                                TabIndex="8">Show</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-6 asitCol6 pading5px">
                                            <asp:Label ID="lfd0" runat="server" CssClass="lblTxt lblName"
                                                Text="Order No:"></asp:Label>


                                            <asp:DropDownList ID="ddlOrder" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                                Width="310px"
                                                TabIndex="2">
                                            </asp:DropDownList>

                                           
                                        </div>
                                        <div class="col-md-2">
                                             <asp:CheckBox ID="chkAllOrder" runat="server" CssClass="btn btn-primary checkbox" Text="ALL Order"  Height="15px" margin_left="-20px" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-6 asitCol6 pading5px">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                                Text="Page Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" TabIndex="18">
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

                                    </div>
                            </fieldset>
                        </div>

                        <div class="table table-responsive">
                             <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                    
                                <asp:GridView ID="gvProStatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False" OnPageIndexChanging="gvProStatus_PageIndexChanging"
                                                ShowFooter="True" Style="text-align: left" Height="255px" Width="261px">
                                                <PagerSettings Position="Top" />
                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProdDate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "PRODUCDAT")) %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production No.">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProNo" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodno")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Description">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleDes")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color Description">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvColor" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Text="Total: " Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColor1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvunit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit1")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty1" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProQty1" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f7201001")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="M">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty2" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProQty2" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201002")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty3" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProQty3" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201003")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="XL">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty4" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProQty4" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201004")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="XXL">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty5" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProQty5" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201005")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFTptal" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" ForeColor="#000" Style="text-align: right"
                                                                Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTotal" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
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

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td>
        </tr>
    </table>
</asp:Content>


