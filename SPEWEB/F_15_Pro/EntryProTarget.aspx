<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryProTarget.aspx.cs" Inherits="SPEWEB.F_15_Pro.EntryProTarget" %>
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
                                    <asp:Panel ID="Panel1" runat="server">

                                  <div class="form-group">
                                     <div class="col-md-4  pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Planing No:"></asp:Label>

                                         <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox"  Text="WEN00-"></asp:Label>

                                         <asp:Label ID="lblCurNo2" runat="server" CssClass="inputtextbox"></asp:Label>

                                         <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to"  Text="Date:"></asp:Label>

                                           <asp:TextBox ID="txtCurDate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                         <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"> </cc1:CalendarExtender>
                                                                                  
                                           <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2"> Ok </asp:LinkButton>

                                      
                                       </div>
                                      <div class="col-md-4">
                                              <asp:Label ID="lblmsg01" runat="server" CssClass="btn btn-xs btn-danger"></asp:Label>
                                      </div>
                                       <div class="col-md-4  pading5px">
                                       
                                           <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="btn btn-primary primaryBtn lblName" OnClick="lbtnPrevList_Click" TabIndex="8"> Prev. List: </asp:LinkButton>

                                           <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="ddlPage"   TabIndex="18" Width="260px"> </asp:DropDownList>
                                                                                    
                                       </div>
                                        </div>


                                       <div class="form-group">
                                      <div class="col-md-8 asitCol8 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"  Text="Size :"></asp:Label>

                                           <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"   onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>150</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>

                                           <asp:Label ID="Label9" runat="server" CssClass="lblName lblTxt" Text="Ref No:"></asp:Label>


                                           <asp:TextBox ID="txtrefno" runat="server"  CssClass="inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                                                                    
                                       </div>
                                     </div>

                                         
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">

                            <asp:GridView ID="gvprotar" runat="server" AllowPaging="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" onpageindexchanging="gvprotar_PageIndexChanging" 
                                            ShowFooter="True" style="text-align: left" 
                                            onrowcancelingedit="gvprotar_RowCancelingEdit" onrowediting="gvprotar_RowEditing" 
                                            onrowupdating="gvprotar_RowUpdating">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Line">
                                             <ItemTemplate>
                                                 <asp:Label ID="lblgvline" runat="server" AutoCompleteType="Disabled" 
                                                     BackColor="Transparent" BorderStyle="None" Font-Size="11px" 
                                                   
                                                     
                                                      Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "linedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")).Trim(): "") 
                                                                         
                                                                    %>'    Width="80px">
                                                     
                                                     
                                                     </asp:Label>
                                             
                                             
                                             
                                             
                                             </ItemTemplate>
                                             <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                             <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                         </asp:TemplateField>

                                        
                                        
                                       
                                               

                                                  <asp:CommandField ShowEditButton="True" CancelText="Can" UpdateText="Up" />


                                                   <asp:TemplateField HeaderText="Add">
                                                
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnadd" runat="server" CommandArgument="lbtnadd" 
                                                        onclick="lbtnadd_Click" style="text-align: left; " 
                                                        Text="Add"
                                                        Width="40px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            



                                                 <asp:TemplateField HeaderText="Order Name">
                                     <EditItemTemplate>
                                         <asp:Panel ID="Paneorder" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                             BorderWidth="1px">
                                             <table style="width: 100%;">
                                                 <tr>
                                                     <td class="style58">
                                                         <asp:TextBox ID="txtsrchorder" runat="server" BorderStyle="Solid" 
                                                             BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                     </td>
                                                     <td class="style59">
                                                         <asp:ImageButton ID="ibtnSrchorder" runat="server" Height="16px" 
                                                             ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrchorder_Click" TabIndex="5" 
                                                             Width="16px" />
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="ddlOrder" runat="server" Width="140px" TabIndex="6" 
                                                             AutoPostBack="True" onselectedindexchanged="ddlOrder_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </EditItemTemplate>
                                                     <FooterTemplate>
                                                         <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn"  OnClick="lbtnTotal_Click" > Total </asp:LinkButton>
                                                     </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvorder" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                     </asp:TemplateField>




                                      <asp:TemplateField HeaderText="Style Name">
                                     <EditItemTemplate>
                                         <asp:Panel ID="Pnlstyle" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                             BorderWidth="1px">
                                             <table style="width: 100%;">
                                                 <tr>
                                                     <td class="style58">
                                                         <asp:TextBox ID="txtsrchstyle" runat="server" BorderStyle="Solid" 
                                                             BorderWidth="1px" Height="18px" TabIndex="4" Width="50px"></asp:TextBox>
                                                     </td>
                                                     <td class="style59">
                                                         <asp:ImageButton ID="ibtnSrchstele" runat="server" Height="16px" 
                                                             ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrchstele_Click" TabIndex="5" 
                                                             Width="16px" />
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="ddlstyle" runat="server" Width="140px" TabIndex="6">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </EditItemTemplate>
                                          <FooterTemplate>
                                              <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click" 
                                                 > Update </asp:LinkButton>
                                          </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvstyle" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>' 
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                     </asp:TemplateField>



                                      <asp:TemplateField HeaderText="Capacity">
                                                    
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblgvFcapacity" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="#000"></asp:Label>
                                                     </FooterTemplate>
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgcapacity" runat="server"  BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="60px" BorderStyle="None"  Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Total Qty">
                                                    
                                                     <FooterTemplate>
                                                         <asp:Label ID="lblgvFTarqty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                             ForeColor="#000"></asp:Label>
                                                     </FooterTemplate>
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvtqty" runat="server"  BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="60px" BorderStyle="None"  Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                       <asp:TemplateField HeaderText="01">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFQty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty1" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="02">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty2" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center"  />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="03">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty3" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center"  />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="04">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty4" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center"  />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="05">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty5" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center"  />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="06">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty6" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="07">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvqty7" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:TextBox></ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFqty7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="50px"></asp:Label></FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>





                                                


                                                <asp:TemplateField HeaderText="Lay OF M/C">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFmachine" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmachine" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "macno")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                               
                                                 <asp:TemplateField HeaderText="Linecode" visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvlinecode" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linecode")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>   

                                     <asp:TemplateField HeaderText="ordrcode" visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmlccode" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="stylecode" visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvstylecode" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stylecode")) %>' 
                                                            Width="80px"></asp:Label>
                                                           <asp:Label ID="lblgvcolorid" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>' 
                                                            Width="80px"></asp:Label>
                                                           <asp:Label ID="lblgvsizeid" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
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

                                







                                            <%--<tr>
                                                <td class="style39">
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="text-align: left; color: #FFFFFF;" Text="Planing No:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style66">
                                                    <asp:Label ID="lblCurNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                                                        Text="WEN00-" Width="50px"></asp:Label>
                                                </td>
                                                <td class="style65">
                                                    <asp:Label ID="lblCurNo2" runat="server" BorderStyle="Solid" 
                                                        BorderWidth="1px" Font-Bold="True" Font-Size="12px" BackColor="#000" 
                                                        Width="45px">00000</asp:Label>
                                                </td>
                                                <td class="style39">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="text-align: left; color: #FFFFFF;" Text="Date:" Width="80px"></asp:Label>
                                                </td>
                                                <td align="left" class="style56">
                                                    <asp:TextBox ID="txtCurDate" runat="server" AutoCompleteType="Disabled" 
                                                        BorderStyle="None" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" 
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td align="left" class="style67">
                                                    <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                                        BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="#000" onclick="lbtnOk_Click" 
                                                        style="text-align: center; height: 17px;" Width="50px">Ok</asp:LinkButton>
                                                </td>
                                                <td class="style68" align="left">
                                                    <asp:Label ID="lblmsg01" runat="server" BackColor="Red" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="#000"></asp:Label>
                                                </td>
                                                <td align="left" class="style55">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style62">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                   </td>
                                                <td class="style23">
                                                    </td>
                                                <td class="style23">
                                                    </td>
                                            </tr>--%>
                                            <%--<tr>
                                                <td class="style39">
                                                    <asp:Label ID="lblPage" runat="server" CssClass="style27" Font-Bold="True" 
                                                        Font-Size="12px" Font-Underline="False" ForeColor="#000" Height="16px" 
                                                        style="font-weight: 700; text-align: right" Text="Size :" Width="32px"></asp:Label>
                                                </td>
                                                <td class="style64" colspan="2">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="120px">
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>15</asp:ListItem>
                                                        <asp:ListItem>20</asp:ListItem>
                                                        <asp:ListItem>30</asp:ListItem>
                                                        <asp:ListItem>50</asp:ListItem>
                                                        <asp:ListItem>100</asp:ListItem>
                                                        <asp:ListItem>150</asp:ListItem>
                                                        <asp:ListItem>200</asp:ListItem>
                                                        <asp:ListItem>300</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style39">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="text-align: left; color: #FFFFFF;" Text="Ref No:" Width="80px"></asp:Label>
                                                </td>
                                                <td align="left" class="style56">
                                                    <asp:TextBox ID="txtrefno" runat="server" AutoCompleteType="Disabled" 
                                                        BorderStyle="None" Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="left" class="style67">
                                                    &nbsp;</td>
                                                <td align="left" class="style68">
                                                    &nbsp;</td>
                                                <td align="left" class="style55">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style62">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                            </tr>--%>
                                            <%--<tr>
                                                <td class="style39">
                                                    <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="style17" 
                                                        Font-Bold="True" Font-Size="12px" ForeColor="#000" 
                                                        onclick="lbtnPrevList_Click" style="text-align: left; " Width="90px">Prev. List:</asp:LinkButton>
                                                </td>
                                                <td class="style64" colspan="4">
                                                    <asp:DropDownList ID="ddlPrevList" runat="server" Font-Size="12px" 
                                                        Width="295px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="left" class="style67">
                                                    &nbsp;</td>
                                                <td align="left" class="style68">
                                                    &nbsp;</td>
                                                <td align="left" class="style55">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style62">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                                <td class="style23">
                                                    &nbsp;</td>
                                            </tr>--%>
                                     
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>





