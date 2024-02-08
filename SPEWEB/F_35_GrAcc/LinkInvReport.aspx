<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkInvReport.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkInvReport" %>

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
                              <asp:Panel ID="Panel1" runat="server">
                                       <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName"  Text="Date From:"></asp:Label>

                                                 <asp:Label ID="txtDatefrom" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                  <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to"  Text="To:" ></asp:Label>

                                                 <asp:Label ID="txtDateto" runat="server" CssClass="inputtextbox" ></asp:Label>
                                                </div>
                                           
                                           </div>

                                       <div class="form-group">
                                              <div class="col-md-8 asitCol8 pading5px">
                                                 <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Order Name:"></asp:Label>

                                                 <asp:Label ID="txtSearch" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                  <asp:LinkButton ID="ImgbtnFindProj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProj_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                              
                                                 <asp:DropDownList ID="ddlAccProject" runat="server" AutoPostBack="True"  CssClass="ddlPage"   OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="270px"> </asp:DropDownList>

                                                   <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>
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
                                 

                                    </asp:Panel>

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
                                    <asp:TemplateField HeaderText="Materials Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMatdescryption" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Amt.">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvReQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOpAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTrQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtrnsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIssQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Amt.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIssAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matisamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "strate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="center" VerticalAlign="top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stcamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
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
                                            <asp:Label ID="lgvABFOpnAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                            <asp:Label ID="lgvABFRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                            <asp:Label ID="lgvABFtrnsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                            <asp:Label ID="lgvABFIssAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                            <asp:Label ID="lgvABFStkAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
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
                                <asp:View ID="View2" runat="server">
                               
                                            <asp:GridView ID="gvproprocess" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" style="text-align: left" Width="368px" 
                                                onrowdatabound="gvproprocess_RowDataBound">
                                                <PagerSettings Position="Top" />
                                              
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                        <ItemStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Description">
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblgvStyleDesr" runat="server" 
                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "styledesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")).Trim(): "") %>' 
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvUnit" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdate" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdate")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Received Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrecQty" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transfer Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTdate" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdate")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Transfer Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvtrnsQty" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnsqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBalQty" runat="server" BackColor="Transparent" 
                                                                BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                Width="50px"></asp:Label>
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
                                            BackColor="#000"></asp:Label>
                                    </td>
                                    <td class="style63">
                                        <asp:Label ID="lbldateto" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="To" Width="20px"></asp:Label>
                                    </td>
                                    <td class="style75" colspan="2">
                                        <asp:Label ID="txtDateto" runat="server" Width="100px" BorderStyle="None" 
                                            BackColor="#000"></asp:Label>
                                    </td>
                                    <td class="style97">
                                        &nbsp;
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
                                        &nbsp;</td>
                                </tr>--%>
                                <%--<tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td class="style88">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                            Style="text-align: right; color: #FFFFFF;" Text="Order Name:" 
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td class="style89">
                                        <asp:TextBox ID="txtSearch" runat="server" Style="border-style: solid; border-width: 1px"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style90">
                                        <asp:ImageButton ID="ImgbtnFindProj" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="ImgbtnFindProj_Click" Width="21px" />
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlAccProject" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="350px">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="4">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                            Style="text-align: center" Width="52px" BackColor="#000066" BorderStyle="Solid"
                                            BorderWidth="1px" ForeColor="#000">Ok</asp:LinkButton>
                                    </td>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                    <td colspan="11">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                                        &nbsp;
                                    </td>
                                    <td class="style98">
                                        &nbsp;
                                    </td>
                                    <td class="style93">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: right;
                                            color: #FFFFFF;" Text="Size:" Width="65px"></asp:Label>
                                    </td>
                                    <td class="style97">
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
