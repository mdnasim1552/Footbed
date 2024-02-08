<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptBBLCPayStatus.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptBBLCPayStatus" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">

                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName"   Text="Date From:"></asp:Label>
                                         

                                        <asp:TextBox ID="txtDatefrom" runat="server" 
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDatefrom" >
                                        </cc1:CalendarExtender>

                                         <asp:RadioButtonList ID="rbtnList1" runat="server"  CssClass="rbtnList1"
                                            RepeatColumns="6" RepeatDirection="Horizontal" Width="300px" 
                                            AutoPostBack="True" onselectedindexchanged="rbtnList1_SelectedIndexChanged">
                                            <asp:ListItem>Order Wise</asp:ListItem>
                                            <asp:ListItem>Supplier Wise</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-7 pading5px asitCol7">
                                              <asp:Label ID="lblName" runat="server" CssClass="lblTxt lblName"  Text="Order No:"></asp:Label>

                                           <asp:TextBox ID="txtSrcOrder" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                         
                                        <asp:LinkButton ID="imgbtnFindOrder" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindOrder_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                                
                                         <asp:DropDownList ID="ddlOrderList" runat="server"  CssClass="ddlPage"   Width="370px">   </asp:DropDownList>

                                        <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnOk_Click"   TabIndex="8">Ok</asp:LinkButton>
                                              

                                    </div>
                                </div>

                                  <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                              <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"  Text="Page Size" Visible="False"></asp:Label>

                                           <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Visible="False" TabIndex="18">
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
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">

                                <asp:GridView ID="gvRptBBLCPay" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvRptBBLCPay_PageIndexChanging" ShowFooter="True">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrder" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBLC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBBLC" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blcdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSupName" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                            Width="135px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supply Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSupDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "suppldat")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Settlement Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSettDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "settldat")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Not Yet </Br> Due Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvFcAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grossamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Dues Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDueAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPaidAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPaidAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPayDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydat")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />                      

                        </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
