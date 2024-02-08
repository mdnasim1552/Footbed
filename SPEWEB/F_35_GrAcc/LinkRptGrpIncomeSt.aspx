<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptGrpIncomeSt.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkRptGrpIncomeSt" %>
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
                              <asp:Panel ID="Panel1" runat="server">
                                     <div class="form-group">
                                              <div class="col-md-8 asitCol8 pading5px">
                                                 <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName"  Text="LC Name:"></asp:Label>

                                                 <asp:TextBox ID="txtSrcPro" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                                <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                              
                                                 <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="ddlPage"  Width="300px"> </asp:DropDownList>

                                                   <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lnkbtnSerOk_Click" TabIndex="2">Ok</asp:LinkButton>

                                                </div>
                                            
                                           </div>

                                   <div class="form-group">
                                              <div class="col-md-8 asitCol8 pading5px">
                                                 <asp:Label ID="lblSymbol" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                                  
                                                </div>
                                            
                                           </div>

                              </asp:Panel>

                                 <div class="form-group">
                                              <div class="col-md-8 asitCol8 pading5px">
                                                 <asp:Label ID="lblHeadStyle" runat="server" CssClass="lblTxt lblName" Text="Style:" Visible="False"></asp:Label>
                                                  
                                                </div>
                                            
                                           </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                          <asp:GridView ID="gvFeaPrj" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea" 
                        ShowFooter="True">
                       
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                        style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCod" runat="server" Height="16px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvItemdesc" runat="server" AutoCompleteType="Disabled" 
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                        Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgUnit" runat="server" AutoCompleteType="Disabled" 
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"  
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                
                                
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Height="18px" style="text-align: right" Font-Size="11px" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate(FC)">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvrate" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Height="18px" style="text-align: right"  Font-Size="11px" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" style="text-align: right" Font-Size="11px"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvFPerM" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" style="text-align: right" Font-Size="11px"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFPerM" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                           
                        </Columns>
                      <FooterStyle CssClass="grvFooter"/>
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                          <div class="form-group">
                                              <div class="col-md-8 asitCol8 pading5px">
                                                 <asp:Label ID="lblHeadCost" runat="server" CssClass="lblTxt lblName" Text="Cost:" Visible="False"></asp:Label>
                                                  
                                                </div>
                                            
                                           </div>

                         <asp:GridView ID="gvFeaPrjC" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                      
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" 
                                        style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCodc" runat="server" Height="16px" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled" 
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                        Width="200px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Supplier">
                                
                                <FooterTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFGTotalT" runat="server" Font-Bold="True" Font-Size="12px" Text="Grand Total Cost"
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFTotalCMT" runat="server" Font-Bold="True" Font-Size="12px" Text="Total CM"
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFCMDZT" runat="server" Font-Bold="True" Font-Size="12px" Text="CM/DZ"
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFCMPCST" runat="server" Font-Bold="True" Font-Size="12px" Text="CM/PCS"
                                        ForeColor="#000" style="text-align: right"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                    
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled" 
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supplier")) %>' 
                                        Width="120px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgUnitc" runat="server" AutoCompleteType="Disabled" 
                                        BackColor="Transparent" BorderStyle="None"  Font-Size="11px" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvQtyc" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Height="18px" style="text-align: right" Font-Size="11px"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                   
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvratec" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" Height="18px" style="text-align: right" Font-Size="11px"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmtc" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" style="text-align: right"  Font-Size="11px" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFAmtc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFTotalCM" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFCMDZ" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFCMPCS" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="90px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                    
                                </FooterTemplate>
                              
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="%">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPer" runat="server" BackColor="Transparent" 
                                        BorderStyle="None" style="text-align: right"  Font-Size="11px" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntge")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFPer" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" style="text-align: right" Width="40px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lgvFTotalCMPer" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="40px"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="40px"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" style="text-align: right" Width="40px"></asp:Label>
                                        </td>
                                    </tr>
                                   
                                </table>
                                    
                                </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="right" />
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

