<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpProtarVsAchieve.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpProtarVsAchieve" %>

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
                                         <div class="col-md-4 asitCol4 pading5px">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName"  Text="Date:"></asp:Label>

                                              <asp:Label ID="lblAsDate" runat="server" CssClass=" inputtextbox"  Text="A. Sales" Width="185px"></asp:Label>

                                        <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click"   TabIndex="8">Show</asp:LinkButton>
                                  
                                    </div>  
                                        <div class="col-md-1 asitCol1 pading5px">
                                        <asp:CheckBox ID="chkGraph" runat="server"  CssClass="checkbox" Text="Graph" Visible="False" Width="80px" Style="margin-left:25px; margin-top:-10px;" />
                                            </div>
                                    </div>
                                </asp:Panel>
                                </div>
                            </fieldset>
                        </div>

                    <div class="table table-responsive">
                          <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvtvsach" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvtvsach_RowDataBound" ShowFooter="True" Width="501px">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "linedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="100px">
                                                </asp:Label></ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Buyer">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbuyer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer"))%>'
                                                    Width="120px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvorderno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc"))%>'
                                                    Width="120px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstyle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc"))%>'
                                                    Width="120px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Capacity">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcapacity" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Works Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvwhour" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whour")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtarget" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "protqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Production">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproduction" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proacqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short/Excess">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsorexcess" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soexqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hourly Production">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvhproduction" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hproqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lay of M/C">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmacno" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "macno")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <asp:Panel ID="Panel2" runat="server">


                       <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                 <asp:Panel ID="Panel3" runat="server">
                                    <div class="form-group">
                                         <div class="col-md-8 asitCol8 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"  Text=" Order Noe :"></asp:Label>

                                              <asp:TextBox ID="txtpmlcsrch" runat="server" AutoCompleteType="Disabled"  CssClass="inputtextbox"></asp:TextBox>
                                                                                       
                                        <asp:LinkButton ID="imgbtnFindPMlc" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="imgbtnFindPMlc_Click"   TabIndex="8"><span class="glyphicon glyphicon-search asitGlyp"> </asp:LinkButton>
                                  
                                             <asp:DropDownList ID="ddlMLc" runat="server" AutoPostBack="True"  CssClass="ddlPage" Width="371px"> </asp:DropDownList>
                                    </div>  
                                       
                                    </div>
                                </asp:Panel>
                                </div>
                            </fieldset>


                                    
                                                        <%--<tr>
                                                            <td class="style50">
                                                                </td>
                                                            <td class="style51">
                                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                    ForeColor="#000" style="text-align: right" Text=" Order Noe :" Width="100px"></asp:Label>
                                                            </td>
                                                            <td class="style51">
                                                                <asp:TextBox ID="txtpmlcsrch" runat="server" AutoCompleteType="Disabled" 
                                                                    BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td class="style52">
                                                                <asp:ImageButton ID="imgbtnFindPMlc" runat="server" Height="17px" 
                                                                    ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindPMlc_Click" 
                                                                    Width="16px" />
                                                            </td>
                                                            <td class="style53">
                                                                <asp:DropDownList ID="ddlMLc" runat="server" AutoPostBack="True" 
                                                                    Font-Bold="True" Font-Size="12px" 
                                                                    Width="320px">
                                                                </asp:DropDownList>
                                                                <cc1:ListSearchExtender ID="lsePMLc" runat="server" QueryPattern="Contains" 
                                                                    TargetControlID="ddlMLc">
                                                                </cc1:ListSearchExtender>
                                                            </td>
                                                            <td class="style54">
                                                                &nbsp;</td>
                                                            <td class="style50">
                                                                
                                                                </td>
                                                            <td class="style50">
                                                                &nbsp;</td>
                                                            <td class="style50">
                                                                </td>
                                                            <td class="style50">
                                                                </td>
                                                                <td class="style50">
                                                                &nbsp;</td>
                                                            <td class="style50">
                                                                </td>
                                                            <td class="style50">
                                                                </td>
                                                            
                                                        </tr>--%>
                                                  
                                               <asp:GridView ID="RptIndPro" runat="server" AllowPaging="false"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    AutoGenerateColumns="False" 
                                                    ShowFooter="True" Width="315px">
                                                  
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo36" runat="server" Font-Bold="True" 
                                                                    Style="text-align: right" 
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvdate36" runat="server"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqtyTTL" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="40px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        

                                                         <asp:TemplateField HeaderText="Orginal Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgvortarget" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate><asp:Label ID="lblgvFortarget" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                    </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Working Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvcapacity37" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate><asp:Label ID="lblgvFProqty2" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                    </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproqty36" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqty1" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement in % on capacity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproCapPersentAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroncap")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lgvFoproCapPersent" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement in %  on Target">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproTarPersent" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontar")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lgvFoproTarPersentAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Cum Orginal Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvcumortarget" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comppqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum Working Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproduction36" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum Achievement">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvtarget36" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comproqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                                                                                
                                                    </Columns>
                                                   <FooterStyle CssClass="grvFooter"/>
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />

                                                </asp:GridView>
                                                </td>
                                                <td valign="top">
                                                <asp:Chart ID="Chart1" runat="server" Height="264px" Width="663px">
                                                 <Series>

                                                  <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="yellow" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Orginal Target" MarkerSize="4">
                                                     </asp:Series>

                                                     <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Working Target" MarkerSize="4">
                                                     </asp:Series>
                                                     <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Achievement" MarkerSize="4">
                                                     </asp:Series>
                                                 </Series>
                                                 <ChartAreas>
                                                     <asp:ChartArea Name="ChartArea1">
                                                         <AxisY Title="Amount">
                                                        </AxisY>
                                                                
                                                     </asp:ChartArea>
                                                 </ChartAreas>
                                                 <Titles>
                                                     <asp:Title Font="Time New Romans, 16px" Name="Title1" 
                                                         Text="Producion Status (Order Wise)">
                                                     </asp:Title>
                                                 </Titles>
                                                 <Legends>  
                                                <asp:Legend   
                                                    Name="Legend1"  
                                                    BackColor="AliceBlue"  
                                                    ForeColor="CadetBlue"  
                                                    BorderColor="LightBlue"  
                                                    Docking="Bottom"
                                                     Alignment="Center"
                                                    >  
                                                </asp:Legend>  
                                            </Legends>  
                                             </asp:Chart>
                                                </td>
                                        </tr>
                                        
                                        
                                    </table>
                                </asp:Panel>
                            </asp:View>

                            <asp:View ID="View3" runat="server">
                                <asp:Panel ID="Panel4" runat="server">
                                    <table style="width:100%;">
                                        
                                        <tr>
                                            <td valign="top">
                                               <asp:GridView ID="RptAllPro" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    AutoGenerateColumns="False" 
                                                    ShowFooter="True" Width="315px">
                                                   
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoAll" runat="server" Font-Bold="True" 
                                                                    Style="text-align: right" 
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvdateAll" runat="server"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lblgvFProqtyTTLAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="40px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvCapacity1" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate><asp:Label ID="lblgvCapacityAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                    </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Target Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvTargetQunAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate><asp:Label ID="lgvFoTargetQunAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                    </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproqtyAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lblgvFoProqtyAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement in % on capacity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproCapPersentAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroncap")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lgvFoproCapPersentAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Achievement in %  on Target">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvproTarPersentAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontar")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            <asp:Label ID="lgvFoproTarPersentAll" runat="server" BackColor="Transparent" 
                                                                            BorderStyle="None" Font-Size="12px" ForeColor="#000" style="text-align: right" 
                                                                            Width="50px"></asp:Label>
                                                                     </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum Capacity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvCumCap" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comcap")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvCumproductionAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cum Production">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvCumProdAll" runat="server" Style="text-align: right" 
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comproqty")).ToString("#,##0;(#,##0); ") %>' 
                                                                    ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>                                                        
                                                        
                                                    </Columns>
                                                <FooterStyle CssClass="grvFooter"/>
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />

                                                </asp:GridView>
                                                </td>
                                                <td valign="top">
                                                <asp:Chart ID="Chart2" runat="server" Height="264px" Width="663px" >
                                                 <Series>
                                                   <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="green" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Capacity" MarkerSize="4">
                                                     </asp:Series>

                                                    <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="red" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Production Quantity" MarkerSize="4">
                                                     </asp:Series>                                                     

                                                     <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue" 
                                                         MarkerColor="black" MarkerStyle="Circle" Name="Cum Quantity" MarkerSize="4">
                                                     </asp:Series>
                                                 </Series>
                                                 <Titles>
                                                     <asp:Title Font="Time New Romans, 16px" Name="Title1" 
                                                         Text="All Producion Status (Order Wise)">
                                                     </asp:Title>

                                                 </Titles>
                                                 <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                        <AxisY Title="Amount">
                                                        </AxisY>
                                                        <InnerPlotPosition X="10" Y="0" Height="90" Width="90" />
                                                      <AxisY LineColor="#CACACA">
                                                            
                                                            <MajorGrid Enabled="true" LineColor="#CACACA" />
                                                            <MajorTickMark Enabled="true" />
                                                            <MinorGrid LineWidth="1" Enabled="true" LineColor="#CACACA" />
                                                      </AxisY>
                                                      <AxisX LineColor="#CACACA">
                                                            
                                                            <MinorGrid LineWidth="1" Enabled="true" LineColor="#CACACA" />
                                                            <MajorGrid Enabled="true" LineColor="#CACACA" />
                                                            <MajorTickMark Enabled="true" LineColor="#CACACA" />
                                                      </AxisX>                                                           
                                                    </asp:ChartArea>                                                         
                                                 </ChartAreas>
                                                 
                                                 <Legends>  
                                                    <asp:Legend   
                                                        Name="Legend1"  
                                                        BackColor="AliceBlue"  
                                                        ForeColor="CadetBlue"  
                                                        BorderColor="LightBlue"  
                                                        Docking="Bottom"                                                        
                                                        >  
                                                    </asp:Legend>  
                                                </Legends>  
                                             </asp:Chart>
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                                   
                                         </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="DetailsBgd" runat="server">
                            </asp:View>
                        </asp:MultiView>
                    </div>
                    </div>
                </div>
                              
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
