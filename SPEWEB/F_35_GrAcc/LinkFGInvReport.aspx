<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkFGInvReport.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkFGInvReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                 <div class="contentPart">
                        <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                             

                                       <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName"  Text="Date From:"></asp:Label>

                                                 <asp:Label ID="txtDatefrom" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                  <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to"  Text="To:" ></asp:Label>

                                                 <asp:Label ID="txtDateto" runat="server" CssClass=" inputtextbox" Width="110px"></asp:Label>

                                                   <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2">Ok </span></asp:LinkButton>

                                                </div>
                                           
                                           </div>
                                      <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lblRptGroup" runat="server" CssClass="lblTxt lblName"  Text="Group :"></asp:Label>

                                         <asp:DropDownList ID="ddlRptGroup" runat="server"  CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>


                                                  <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to"  Text="Size:"></asp:Label>
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"   OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage">
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
                                 

                              
                                </div>
                            </fieldset>
                        </div>
                      <div class="table table-responsive">
                          <asp:Panel ID="Panel2" runat="server">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    
                                <asp:View ID="General" runat="server">
                                
                            <asp:GridView ID="gvCenStore" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="400px" OnPageIndexChanging="gvCenStore_PageIndexChanging">
                                <PagerSettings Position="Top" />
                            
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Master L/C Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMLCDesc" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "actdesc").ToString() %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Opening Qty.">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opproqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFOpQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Production Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProdeQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFProdeQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                      <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Shipment Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvShipQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lgvABFShipQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStockQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lgvABFStocQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                            </asp:View>

                                <asp:View ID="QtyBasis" runat="server">
                                 <asp:GridView ID="gvQBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="400px" OnPageIndexChanging="gvQBasis_PageIndexChanging">
                                <PagerSettings Position="Top" />
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqbSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Received Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBReQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Transfer Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBTrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBIssQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvQBStQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>

                                   
                                </Columns>
                            <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                                </asp:View>
                                 <asp:View ID="AmtBasis" runat="server">
                                 <asp:GridView ID="gvAmtBasis" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="400px" OnPageIndexChanging="gvAmtBasis_PageIndexChanging">
                                <PagerSettings Position="Top" />
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Opening Amt.">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Received Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Transfer Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABtrnsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFtrnsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Issue Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABIssAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Stock Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvABStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvABFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
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
                          </asp:Panel>

                      </div>
                    </div>
                </div>
                                <%--<tr>
                                    <td class="style76">
                                        &nbsp;
                                    </td>
                                    <td class="style61">
                                        <asp:Label ID="lblDatefrom" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="Date From:" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="txtDatefrom" runat="server" Width="80px" BorderStyle="None" 
                                            BackColor="White"></asp:Label>
                                    </td>
                                    <td class="style63">
                                        <asp:Label ID="lbldateto" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="To" Width="20px"></asp:Label>
                                    </td>
                                    <td class="style75" colspan="2">
                                        <asp:Label ID="txtDateto" runat="server" Width="100px" BorderStyle="None" 
                                            BackColor="White"></asp:Label>
                                    </td>
                                    <td class="style97">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" OnClick="lbtnOk_Click" Style="text-align: center" 
                                            Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style95">
                                        &nbsp;
                                    </td>
                                    <td class="style95">
                                        &nbsp;
                                    </td>
                                    <td class="style92" colspan="3">
                                        &nbsp;
                                    </td>
                                    <td class="style63">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td class="style68">
                                        &nbsp;
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>--%>
                                
                                <%--<tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style88">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                            Style="font-weight: 700; text-align: right" Text="Group :" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style89">
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" Font-Size="12px"
                                            Height="21px" Style="text-transform: capitalize" Width="85px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style90">
            
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                            color: #FFFFFF;" Text="Size:" Width="30px"></asp:Label>
                                    </td>
                                    <td class="style98">
     
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="80px">
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
                                    <td class="style93">
                                        &nbsp;</td>
                                    <td class="style97">
                                        &nbsp;</td>
                                    <td class="style93" colspan="2">
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
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
                                    <td colspan="11">
                                        &nbsp;
                                    </td>
                                </tr>--%>
             
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
