<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpIncomestprobasis.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpIncomestprobasis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                <asp:Panel ID="panel11" runat="server" >

                                <div class="form-group">

                                       <div class="col-md-7 asitCol7 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName"  Text="From:"></asp:Label>

                                              <asp:Label ID="lblfrmDate" runat="server" CssClass=" inputtextbox"  Text=""></asp:Label>

                                             <asp:Label ID="Label11" runat="server" CssClass="smLbl_to"  Text="To:"></asp:Label>

                                             <asp:Label ID="lbltoDate" runat="server" CssClass=" inputtextbox" Text=""></asp:Label>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click"   TabIndex="8">Ok</asp:LinkButton>
                                  
                                    </div>  
                                </div>
                                     </asp:Panel>
                                </div>
                               
                            </fieldset>
                        </div>
                    <div class="table table-responsive">
                            <asp:GridView ID="gvinstment" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvinstment_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" Width="387px">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="150px">
                                                                    
                                                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Amount (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfcamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "famount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount (TK)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvamount" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                              
                               
                                <asp:TemplateField HeaderText=" %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpercntage" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcntage")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcntage"))==0?"":"%" ) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
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




            
                <%--<table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style34">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: left" Text="From:" Width="80px"></asp:Label>
                        </td>
                        <td class="style29">
                            <asp:Label ID="lblfrmDate" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Width="90px"></asp:Label>
                        </td>
                        <td class="style31">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                Style="text-align: left" Text="To:" Width="60px"></asp:Label>
                        </td>
                        <td class="style33">
                            <asp:Label ID="lbltoDate" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Width="90px"></asp:Label>
                        </td>
                        <td class="style32">
                            <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="#000"
                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="16px" ForeColor="#000"
                                Height="20px" OnClick="lbtnOk_Click" Style="text-align: center;" 
                                Width="52px">Ok</asp:LinkButton>
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>--%>
           
           
                    
                    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
