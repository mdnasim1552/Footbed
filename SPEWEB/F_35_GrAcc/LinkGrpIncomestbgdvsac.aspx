<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpIncomestbgdvsac.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpIncomestbgdvsac" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
   <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvcostvsex.ClientID %>');
           gv.Scrollable();
          
       }
       </script>
    
    
   

                
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
                                                 <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName"  Text=" MASTER LC :"></asp:Label>

                                                 <asp:TextBox ID="txtpmlcsrch" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                                <asp:LinkButton ID="imgbtnFindPMlc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindPMlc_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                              
                                                 <asp:DropDownList ID="ddlMLc" runat="server"  CssClass="ddlPage" Width="320px"> </asp:DropDownList>

                                                   <asp:LinkButton ID="lbtnShow" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnShow_Click" TabIndex="2">Show</asp:LinkButton>

                                                </div>
                                            
                                           </div>

                                   <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName"  Text="As on:"></asp:Label>
                                                   <asp:Label ID="lblDate" runat="server" CssClass="inputtextbox"></asp:Label>
 
                                                </div>
                                            
                                           </div>

                                      <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size:" Visible="False"></asp:Label>
                                                  
                                         <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"   onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" CssClass=" ddlPage">
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

                          <asp:GridView ID="gvcostvsex" runat="server" AllowPaging="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" onpageindexchanging="gvcostvsex_PageIndexChanging" 
                            ShowFooter="True" style="text-align: left" 
                            onrowdatabound="gvcostvsex_RowDataBound" Width="548px">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                
                            
                                 
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server" 
                                          Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                    %>'   Width="250px"></asp:Label>
                                    
                                    
                                    
                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Amount (FC)">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBudgetedFC" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                             
                               
                                
                                 <asp:TemplateField HeaderText="Actual Amount (FC) ">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                      <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Variance">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalAmt" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                      <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Variance %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvariance" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "variance")).ToString("#,##0.00;(#,##0.00); ")+"%" %>' 
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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

