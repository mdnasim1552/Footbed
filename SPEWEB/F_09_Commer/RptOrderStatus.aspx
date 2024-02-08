<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptOrderStatus.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptOrderStatus" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                       <div class="form-group">
                                            <div class="col-md-7 asitCol7 pading5px">
                                                <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date:"  ></asp:Label>

                                             <asp:TextBox ID="txtFDate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd-MMM-yyyy" TargetControlID="txtFDate"> </cc1:CalendarExtender>

                                               <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                                 <asp:TextBox ID="txttodate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                                  <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"  Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                                                             
                                             <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnOk_Click" TabIndex="3"> Ok </asp:LinkButton>
                                        
                                              

                                            </div>
                                        </div>

                                      <div class="form-group">
                                            <div class="col-md-7 asitCol7 pading5px">
                                                <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt"  Text="Page Size: "></asp:Label>

                                                   <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                                    </asp:Panel>
                                </div>
                            </fieldset>
                        </div>
                    <div class="table table-responsive">
                         <asp:MultiView ID="MultiView1" runat="server">
                                       <asp:View ID="OrderWiseSupplier" runat="server">
                            
                        <asp:GridView ID="gvOrderStatus" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"

                            AutoGenerateColumns="False" onpageindexchanging="gvOrderStatus_PageIndexChanging" 
                            ShowFooter="True" Width="501px" onrowdatabound="gvOrderStatus_RowDataBound">
                        
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                 

                                <asp:TemplateField HeaderText="Job #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvJobNo" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobno")) %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Buyer">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvBuyer" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvorder" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Export Master LC">
                                    <ItemTemplate>
                                       <asp:Label ID="lgMasterLc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mainmlcdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="LC Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlcamt" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                                                
                                 <asp:TemplateField HeaderText="BBLC #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgbblcdesc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="BBLC Date">
                                    <ItemTemplate>
                                       <asp:Label ID="lgbblcdate" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdat")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsupplier" runat="server"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BBLC Amount(FC) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbblcamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
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
                         <asp:GridView ID="gvSupplierWise" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"

                            AutoGenerateColumns="False" onpageindexchanging="gvSupplierWise_PageIndexChanging" 
                            ShowFooter="True" Width="501px" onrowdatabound="gvSupplierWise_RowDataBound">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsSlNo9" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                 

                                <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvssupplier" runat="server"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Job #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsJobNo" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobno")) %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Buyer">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsBuyer" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsorder" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Export Master LC">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsMasterLc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mainmlcdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="LC Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvslcamt" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                                                
                                 <asp:TemplateField HeaderText="BBLC #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsbblcdesc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="BBLC Date">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsbblcdate" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdat")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                   

                                <asp:TemplateField HeaderText="BBLC Amount(FC) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsbblcamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

