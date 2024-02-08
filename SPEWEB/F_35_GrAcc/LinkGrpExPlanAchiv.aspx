<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpExPlanAchiv.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpExPlanAchiv" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
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
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"  Text="Date:"></asp:Label>

                                           
                                             <asp:Label ID="lblDateRange" runat="server" CssClass="inputtextbox"  Text=""></asp:Label>

                                             <asp:Label ID="lblPage" runat="server" CssClass="  lblTxt lblName" Text="Page Size:" ></asp:Label>

                                              <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"  onselectedindexchanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
                                                <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                            </asp:DropDownList>

                                           <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnOk_Click" TabIndex="3"> Ok </asp:LinkButton>   
                                       
                                  
                                    </div>  
                                    
                                </div>
                                </asp:Panel>
                                </div>
                            </fieldset>
                        </div>
                    <div class="table table-responsive">

                        <asp:GridView ID="gvRptExPlnAch" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    ShowFooter="True" Width="523px" 
                    onrowdatabound="gvRptExPlnAch_RowDataBound1">
                   
                    <Columns>
                        <asp:TemplateField HeaderText="Sl.No.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" 
                                    Style="text-align: right" 
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="L/C  Name">
                           <ItemTemplate>
                           <asp:HyperLink ID="hlnkgvlcname" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                            Width="120px">
                               </asp:HyperLink>
                           </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" />
                     
                     
                     
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Shipment No">
                            <FooterTemplate>
                                <asp:Label ID="lblgvTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#000" Text="Total"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvShiPNoat" runat="server" Style="text-align: left" 
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipmentno")) %>' 
                                    Width="85px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="Shipment Date">
                           
                            <ItemTemplate>
                                <asp:Label ID="lgvShipdate" runat="server" Style="text-align: left" 
                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shimentdate")).ToString("dd-MMM-yyyy") %>' 
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        
                        <asp:TemplateField HeaderText="Delay Before Production">
                            <ItemTemplate>
                                <asp:Label ID="lgvdelday" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Shipment Plan">
                            <ItemTemplate>
                                <asp:Label ID="lgvShiPlqtyat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipplnqty")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFShipPlan" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipment Acheived">
                            <ItemTemplate>
                                <asp:Label ID="lgvShiQtyat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                          
                            <FooterTemplate>
                                <asp:Label ID="lgvFShipAchieved" runat="server" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipment %">
                            <ItemTemplate>
                                <asp:Label ID="lgvShiPerat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shippercent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFPeroShipPlan" runat="server" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Plan">
                            <FooterTemplate>
                                <asp:Label ID="lgvFProPlnQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lgvProPlqtyat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proplanqty")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Target">
                            <ItemTemplate>
                                <asp:Label ID="lgvworktargetat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFworktarget" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Achieved">
                            <ItemTemplate>
                                <asp:Label ID="lgvProqtyat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0;(#,##0); ") %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <%--<FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFProqty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFProPar" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>--%>
                            <FooterTemplate>
                                <asp:Label ID="lgvFproachieved" runat="server" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="#000" Width="60px"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production % On Plan">
                            <ItemTemplate>
                                <asp:Label ID="lgvProPerat" runat="server" Style="text-align: right" 
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercent")).ToString("#,##0.00;(#,##0.00); ")  %>' 
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lgvFPeroProPlan" runat="server" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="#000" Width="60px"></asp:Label>
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

                    </div>
                    </div>
                </div>




                       
                          
                    
                        <%--<tr>
                            <td class="style35" style="text-align: left">
                                <asp:Label ID="lblDate" runat="server" CssClass="style16" Font-Bold="True" 
                                    Font-Size="12px" Height="16px" style="text-align: left;" Text="Date:" 
                                    Width="100px"></asp:Label>
                            </td>
                            <td class="style36" style="text-align: left">
                                <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                    BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                            </td>
                            <td class="style44" style="text-align: left">
                                <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="#993300" style="color: #FFFFFF; text-align: left;" Text="Page Size" 
                                    Width="75px"></asp:Label>
                            </td>
                            <td class="style68" style="text-align: left">
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                    BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                    onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style62" style="text-align: left">
                                <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#000066" 
                                    BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" ForeColor="#000" 
                                    Height="20px" onclick="lnkbtnOk_Click" style="text-align: center" Width="60px">Ok</asp:LinkButton>
                            </td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                            <td class="style69" style="text-align: left">
                                &nbsp;</td>
                        </tr>--%>
                   
                
            
                    </ContentTemplate>
                </asp:UpdatePanel>
    
</asp:Content>



