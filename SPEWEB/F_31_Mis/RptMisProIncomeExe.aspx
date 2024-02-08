<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMisProIncomeExe.aspx.cs" Inherits="SPEWEB.F_31_Mis.RptMisProIncomeExe" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="card card-fluid">
                <div class="card-body">
                      <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmDate" runat="server" CssClass="label2" Text="From:" 
                                            ></asp:Label>
                                         <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                                        
                                 <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbltoDate" runat="server" CssClass="label2" Text="To:" 
                                            ></asp:Label>
                                  
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm"  ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto">
                                        </cc1:CalendarExtender>
                                   </div>
                                     </div>
                           <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnShow" runat="server"  CssClass="btn btn-sm btn-primary" onclick="lbtnShow_Click">Ok</asp:LinkButton>
                                    </div>
                               </div>
                 </div>
             </div>
                 </div>
              <div class="card card-fluid">
                <div class="card-body" style="min-height:400px;">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ProjectIncome" runat="server">                              
                                            <asp:GridView ID="gvProIncome" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="662px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings Position="Top" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField FooterText="Total" HeaderText="LC Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvproject" runat="server" Height="16px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" 
                                                            HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revenue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFinamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 style="text-align: right" ></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcost" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 style="text-align: right" ></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Profit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvmargin" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maram")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFmargin" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" % on Revenue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpermarin" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permarin")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="% on Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpermar" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permar")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>                  
                                             
                                                 <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                      
                            </asp:View>
                                <asp:View ID="ProjectExecution" runat="server">
                                    <asp:GridView ID="gvProExecution" runat="server" AutoGenerateColumns="False" 
                                        ShowFooter="True" Width="662px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Position="Top" />
                                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                                        style="text-align: right" 
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterText="Total" HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvproject0" runat="server" Height="16px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                    HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpreamt" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pream")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpreamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcuramt" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFcuramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoamt" runat="server" style="text-align: right" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                           
                                        </Columns>
                                        <FooterStyle BackColor="#333333" />
                                        <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                        <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    </asp:GridView>
                                </asp:View>

                                 <asp:View ID="ConBgdVsExecution" runat="server">
                                     <asp:GridView ID="gvConBgdVsExe" runat="server" AutoGenerateColumns="False" 
                                         ShowFooter="True" Width="662px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                         <PagerSettings Position="Top" />
                                         <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                         <Columns>
                                             <asp:TemplateField HeaderText="Sl">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                                         style="text-align: right" 
                                                         Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                 </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                             </asp:TemplateField>
                                             <asp:TemplateField FooterText="Total" HeaderText="Project Name">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblgvproject1" runat="server" Height="16px" 
                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>' 
                                                         Width="200px"></asp:Label>
                                                 </ItemTemplate>
                                                 <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                     HorizontalAlign="Right" />
                                                 <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                             </asp:TemplateField>
                                             
                                             <asp:TemplateField HeaderText="Budgeted Cost">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lgvBgdCost" runat="server" style="text-align: right" 
                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>' 
                                                         Width="75px"></asp:Label>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFBgdCost" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                 </FooterTemplate>
                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                                 <ItemStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Execution">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lgvExecution" runat="server" style="text-align: right" 
                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeam")).ToString("#,##0;(#,##0); ") %>' 
                                                         Width="75px"></asp:Label>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFEexcution" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="75px"></asp:Label>
                                                 </FooterTemplate>
                                                 <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Execution(%)">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lgvExPer" runat="server" style="text-align: right" 
                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exeper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                         Width="75px"></asp:Label>
                                                 </ItemTemplate>
                                                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                 <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                             </asp:TemplateField>
                                         </Columns>
                                         <FooterStyle BackColor="#333333" />
                                         <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                         <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                             ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                         <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                         <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                     </asp:GridView>
                                 </asp:View>

                             </asp:MultiView>     
                             </div>                         
                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


