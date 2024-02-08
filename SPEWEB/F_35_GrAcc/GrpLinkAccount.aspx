<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="GrpLinkAccount.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.GrpLinkAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                 <div class="contentPart">

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="ViewBankConfirmation" runat="server">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel2" runat="server">
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName"  Text="From:"></asp:Label>

                                         <asp:Label ID="lblfrmdate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                          <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName"  Text="To:"></asp:Label>

                                         <asp:Label ID="lbltodate" runat="server" CssClass=" inputtextbox"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                  </asp:Panel>
                                 </div>
                            </fieldset>





                                   

                                       <asp:GridView ID="gvCABankBal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                      BackColor="#FFECEC" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="3px" 
                                                      ShowFooter="True" onrowdatabound="gvCABankBal_RowDataBound" 
                                             Width="270px">
                                                      <Columns>
                                                          <asp:TemplateField HeaderText="Sl.No.">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblserialnoid4" runat="server" style="text-align: right" 
                                                                      Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                              </ItemTemplate>
                                                              <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                              <ItemStyle Font-Size="12px" />
                                                          </asp:TemplateField>
                                                         
                                                          <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                                              <HeaderTemplate>
                                                                  <table style="width:100%;">
                                                                      <tr>
                                                                          <td class="style58" style="width:auto">
                                                                              <asp:Label ID="Label8" runat="server" Font-Bold="True" 
                                                                                  Text="Description of Accounts"></asp:Label>
                                                                          </td>
                                                                          <td>
                                                                              &nbsp;</td>
                                                                      </tr>
                                                                  </table>
                                                              </HeaderTemplate>
                                                              <ItemTemplate>
                                                                  <asp:HyperLink ID="HLgvDescbankcb" runat="server" __designer:wfdid="w38" 
                                                                      CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank" 
                                                                     
                                                                       Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'   Width="250px">
                                                                      
                                                                      
                                                                      
                                                                      </asp:HyperLink>
                                                              </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Left" />
                                                              <ItemStyle HorizontalAlign="left" />
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                          </asp:TemplateField>

                                                              <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Change" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvnetbalcb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>

                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Opening" 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvopnamcb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                              <ItemStyle HorizontalAlign="Right" />
                                                          </asp:TemplateField>

                                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                              FooterStyle-HorizontalAlign="Right" HeaderText="Closing " 
                                                              ItemStyle-HorizontalAlign="Right">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblgvclosamcb" runat="server" CssClass="GridLebel" 
                                                                      Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>' 
                                                                      Width="90px"></asp:Label>
                                                              </ItemTemplate>
                                                              <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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

                <asp:View ID="ViewSales" runat="server">



                      <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel1" runat="server">
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"  Text="From:"></asp:Label>

                                         <asp:Label ID="sfrDate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                          <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName"  Text="To:"></asp:Label>

                                         <asp:Label ID="stDate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                         <asp:Label ID="ctl46" runat="server" CssClass="lblTxt lblName"  Text="Project Name:"></asp:Label>

                                          <asp:Label ID="lblPrijDesc" runat="server" CssClass="lblTxt lblName"  Text="Project Name:"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                  </asp:Panel>
                                 </div>
                            </fieldset>
   
                                <%--<asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style51">
                                                
                                            </td>
                                            <td class="style52">
                                                
                                            </td>
                                            <td class="style53">
                                                <asp:Label ID="Label1" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="sfrDate" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style55">
                                                <asp:Label ID="Label3" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Text="To:"></asp:Label>
                                            </td>
                                            <td class="style64">
                                                <asp:Label ID="stDate" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Height="16px" Style="text-align: left" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style66">
                                                <asp:Label ID="ctl46" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Height="16px" Style="text-align: left" Width="100px">Project Name:</asp:Label>
                                            </td>
                                            <td class="style55">
                                                <asp:Label ID="lblPrijDesc" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Height="16px" Style="text-align: left" Width="300px">Project Name:</asp:Label>
                                            </td>
                                            <td class="style55">
                                                &nbsp;</td>
                                            <td class="style55">
                                                &nbsp;</td>
                                            <td class="style55">
                                        
                                            </td>
                                            <td class="style55">
                                           
                                            </td>
                                            <td class="style55">
                                        
                                            </td>
                                            <td class="style55">
                                            
                                            </td>
                                            <td class="style55">
                                             
                                            </td>
                                            <td class="style61">
                                           
                                            </td>
                                            <td class="style63">
                                             
                                            </td>
                                            <td>
                                            
                                            </td>
                                            <td>
                                           
                                            </td>
                                            <td>
                                             
                                            </td>
                                            <td>
                                               
                                            </td>
                                            <td>
                                              
                                            </td>
                                            <td>
                                              
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                       
                                <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="770px" AllowPaging="True" OnPageIndexChanging="gvDayWSale_PageIndexChanging">
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name"><ItemTemplate><asp:Label ID="lblgvDPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px" Font-Bold="true"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name"><ItemTemplate><asp:Label ID="lgvDcuname" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                    Width="150px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item"><ItemTemplate><asp:Label ID="lgvDResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="120px"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                                    Font-Size="12px" ForeColor="White" Style="text-align: right" Width="150px"></asp:Label></FooterTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit"><ItemTemplate><asp:Label ID="lgUnit" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>' Width="35px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size"><ItemTemplate><asp:Label ID="lgvUSize" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price per SFT"><ItemTemplate><asp:Label ID="lgvUpsft" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px" Style="text-align: right"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Team"><ItemTemplate><asp:Label ID="lgDCper" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>' Width="120px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted Amt."><ItemTemplate><asp:Label ID="lgvDTAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label></FooterTemplate><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Amt"><ItemTemplate><asp:Label ID="lgvDSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label></FooterTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Date"><ItemTemplate><asp:Label ID="lgvDSchdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px" Style="text-align: left"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel Date"><ItemTemplate><asp:Label ID="lgvDCandate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cudate")) %>'
                                                    Width="65px" Style="text-align: left"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /><ItemStyle HorizontalAlign="left" /></asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDDisAmt" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>' 
                                                        Width="70px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                     <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                 </FooterTemplate>
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDvDisPer" runat="server" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                        Width="60px" style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                 
                                                  <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
  
                </asp:View>

                <asp:View ID="ViewSalDet" runat="server">

                       <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel3" runat="server">
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName"  Text="Net Sales Details:"></asp:Label>

                                         <asp:Label ID="Label2" runat="server" CssClass=" inputtextbox" Text="Form:"></asp:Label>

                                          <asp:Label ID="lblSFrmDate" runat="server" CssClass="lblTxt lblName"  Text=""></asp:Label>

                                         <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName"  Text="To:"></asp:Label>

                                         <asp:Label ID="lblSTrmDate" runat="server" CssClass="inputtextbox"></asp:Label>

                                          <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"  TText="Page Size:"></asp:Label>

                                       <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  CssClass="ddlPage" onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
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

                                                                                          
                                         </div>
                                      </div>
                                  </asp:Panel>
                                 </div>
                            </fieldset>

                 
                                <%--<asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style51">
                                                &nbsp;
                                            </td>
                                            <td class="style52">
                                                <asp:Label ID="Label11" runat="server" BackColor="#003399" CssClass="style50" 
                                                    Font-Bold="True" Font-Size="12px" Height="16px" Style="text-align: left" 
                                                    Width="300px">Net Sales Details</asp:Label>
</td>
                                            <td class="style53">
                                                <asp:Label ID="Label2" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Text="From:" Width="80px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSFrmDate" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style55">
                                                <asp:Label ID="Label5" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: left" Text="To:"></asp:Label>
                                            </td>
                                            <td class="style64">
                                                <asp:Label ID="lblSTrmDate" runat="server" CssClass="style50" Font-Bold="True" Font-Size="12px"
                                                    Height="16px" Style="text-align: left" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style66">
                                                &nbsp;<asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Height="16px" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                                    Width="70px"></asp:Label>
&nbsp;</td>
                                            <td class="style55">
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                    BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                    onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                                    style="margin-left: 0px" TabIndex="2" Width="85px">
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
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style55">
                                                &nbsp;
                                            </td>
                                            <td class="style61">
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
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                        
                                <asp:GridView ID="grvSalDet" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AllowPaging="True" OnPageIndexChanging="grvSalDet_PageIndexChanging">
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name"><ItemTemplate><asp:Label ID="lgvDcuname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px" Style="text-align: left"></asp:Label></ItemTemplate><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Amount"><ItemTemplate><asp:Label ID="lgvsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label></FooterTemplate><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Agency Commsion"><ItemTemplate><asp:Label ID="lgvaAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "agamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFaAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label></FooterTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Sales"><ItemTemplate><asp:Label ID="lgvNAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label></ItemTemplate><FooterTemplate><asp:Label ID="lgvFNAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Style="text-align: right" Width="75px"></asp:Label></FooterTemplate><ItemStyle HorizontalAlign="Right" /><FooterStyle Font-Bold="True" HorizontalAlign="right" /><HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /></asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                           
                </asp:View>

                <asp:View ID="ViewDetailsBal" runat="server">
                      <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                          BackColor="#FFECEC" BorderColor="#66CCFF" BorderStyle="Solid" 
                          onrowdatabound="gvDetails_RowDataBound" PageSize="20" ShowFooter="True" 
                          Width="185px">
                          <Columns>
                              <asp:TemplateField HeaderText="Sl.No.">
                                  <ItemTemplate>
                                      <asp:Label ID="lblserialnoid0" runat="server" style="text-align: right" 
                                          Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                  </ItemTemplate>
                                  <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                  <ItemStyle Font-Size="12px" />
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Code">
                                  <ItemTemplate>
                                      <asp:Label ID="lblgvcoded" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>' Width="120px">
                                                                          
                                                                           </asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                  FooterStyle-HorizontalAlign="Right" HeaderText="Description">
                                  <HeaderTemplate>
                                      <table style="width:100%;">
                                          <tr>
                                              <td class="style58">
                                                  <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                              </td>
                                              <td class="style59">
                                                  &nbsp;</td>
                                              <td>
                                                  <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066" 
                                                      BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                      ForeColor="White">Export Exel</asp:HyperLink>
                                              </td>
                                          </tr>
                                      </table>
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lblgvdescriptiond" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>' width="350px">
                                                             
                                                             
                                                             </asp:Label>
                                  </ItemTemplate>
                                  <FooterTemplate>
                                      <asp:Label ID="lblfopDes" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                  </FooterTemplate>
                                  <HeaderStyle HorizontalAlign="Left" />
                                  <ItemStyle HorizontalAlign="left" />
                                  <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                              </asp:TemplateField>

                              <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                  FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount" 
                                  ItemStyle-HorizontalAlign="Right">
                                  <FooterTemplate>
                                      <asp:Label ID="lblfcloamtd" runat="server" CssClass="GridLebel" 
                                          Font-Bold="True"></asp:Label>
                                  </FooterTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lblgvclobald" runat="server" CssClass="GridLebel" 
                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>' 
                                          Width="90px"></asp:Label>
                                  </ItemTemplate>
                                  <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:TemplateField>


                              <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                  FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt" 
                                  ItemStyle-HorizontalAlign="Right">
                                  <FooterTemplate>
                                      <asp:Label ID="lblfopnamtd" runat="server" CssClass="GridLebel" 
                                          Font-Bold="True"></asp:Label>
                                  </FooterTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lblgvopnamtd" runat="server" CssClass="GridLebel" 
                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>' 
                                          Width="90px"></asp:Label>
                                  </ItemTemplate>
                                  <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                  <ItemStyle HorizontalAlign="Right" />
                              </asp:TemplateField>


                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Changes During the Period" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel" Font-Size="10px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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

               <asp:View ID="ViewDetailsofInSt" runat="server">
                          
                                       <asp:GridView ID="gvInDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                           BackColor="#FFECEC" BorderColor="#66CCFF" BorderStyle="Solid" 
                                           onrowdatabound="gvInDetails_RowDataBound" PageSize="20" ShowFooter="True" 
                                           Width="185px">
                                           <Columns>
                                               <asp:TemplateField HeaderText="Sl.No.">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblserialnoid5" runat="server" style="text-align: right" 
                                                           Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                   </ItemTemplate>
                                                   <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                   <ItemStyle Font-Size="12px" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Code">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvcoded0" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>' Width="120px">
                                                                          
                                                                           </asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                   FooterStyle-HorizontalAlign="Right" HeaderText="Description">
                                                   <HeaderTemplate>
                                                       <table style="width:100%;">
                                                           <tr>
                                                               <td class="style58">
                                                                   <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                                               </td>
                                                               <td class="style59">
                                                                   &nbsp;</td>
                                                               <td>
                                                                   <asp:HyperLink ID="hlbtnCdataExel0" runat="server" BackColor="#000066" 
                                                                       BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                       ForeColor="White">Export Exel</asp:HyperLink>
                                                               </td>
                                                           </tr>
                                                       </table>
                                                   </HeaderTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvdescriptionind" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>' width="350px">
                                                             
                                                             
                                                             </asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lblfopDes0" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                                   </FooterTemplate>
                                                   <HeaderStyle HorizontalAlign="Left" />
                                                   <ItemStyle HorizontalAlign="left" />
                                                   <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                               </asp:TemplateField>
                                               <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                   FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount" 
                                                   ItemStyle-HorizontalAlign="Right">
                                                   <FooterTemplate>
                                                       <asp:Label ID="lblfcloamtind" runat="server" CssClass="GridLebel" 
                                                           Font-Bold="True"></asp:Label>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvclobalind" runat="server" CssClass="GridLebel" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="90px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="Right" />
                                               </asp:TemplateField>
                                               <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                   FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt" 
                                                   ItemStyle-HorizontalAlign="Right">
                                                   <FooterTemplate>
                                                       <asp:Label ID="lblfopnamtind" runat="server" CssClass="GridLebel" 
                                                           Font-Bold="True"></asp:Label>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvopnamtind" runat="server" CssClass="GridLebel" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="90px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="Right" />
                                               </asp:TemplateField>
                                               <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                   FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period" 
                                                   ItemStyle-HorizontalAlign="Right">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvcuamtind" runat="server" CssClass="GridLebel" 
                                                           Font-Size="10px" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                           Width="100px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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

                <asp:View ID="ViewReqStatus" runat="server">
                  

                     <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel4" runat="server">
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName"  Text="Vessel Name:" ></asp:Label>

                                         <asp:Label ID="txtCompanySearch" runat="server" CssClass=" inputtextbox"></asp:Label>

                                       <asp:LinkButton ID="ImgbtnFindCompany" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindCompany_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        
                                         <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True"   onselectedindexchanged="ddlCompany_SelectedIndexChanged"  TabIndex="2" Width="300px"> </asp:DropDownList>  
                                                                         
                                         </div>
                                      </div>

                                   <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName"  Text="Date:" ></asp:Label>

                                         <asp:Label ID="lblAsDate" runat="server" CssClass=" inputtextbox" Width="300px"></asp:Label>
                                                                                                                                                  
                                         </div>
                                      </div>
                                  </asp:Panel>
                                 </div>
                            </fieldset>

                
                                             <%--<asp:Panel ID="Panel5" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                                 BorderWidth="1px">
                                                 <table style="width: 100%;">
                                                     <tr>
                                                         <td class="style70">
                                                             <asp:Label ID="Label13" runat="server" CssClass="style15" Font-Bold="True" 
                                                                 Font-Size="12px" Style="text-align: left" Text="Vessel Name:" Width="83px"></asp:Label>
                                                         </td>
                                                         <td class="style73">
                                                             <asp:TextBox ID="txtCompanySearch" runat="server" BorderStyle="None" 
                                                                 Height="18px" Style="margin-left: 0px" Width="80px"></asp:TextBox>
                                                         </td>
                                                         <td class="style72">
                                                             <asp:ImageButton ID="ImgbtnFindCompany" runat="server" Height="19px" 
                                                                 ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindCompany_Click" 
                                                                 TabIndex="1" Width="16px" />
                                                         </td>
                                                         <td>
                                                             <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" 
                                                                 onselectedindexchanged="ddlCompany_SelectedIndexChanged" 
                                                                 Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid; border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1" 
                                                                 TabIndex="2" Width="300px">
                                                             </asp:DropDownList>
                                                  
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td class="style70">
                                                             <asp:Label ID="Label14" runat="server" CssClass="style15" Font-Bold="True" 
                                                                 Font-Size="12px" Style="text-align: left" Text="Date:" Width="83px"></asp:Label>
                                                         </td>
                                                         <td class="style71" colspan="3">
                                                             <asp:Label ID="lblAsDate" runat="server" BackColor="#000066" 
                                                                 BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="Yellow" Text="A. Sales" Width="300px"></asp:Label>
                                                         </td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                         <td>
                                                             &nbsp;</td>
                                                     </tr>
                                                 </table>
                                             </asp:Panel>--%>
                               
                                             <asp:GridView ID="gvReqStatus" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                 OnRowDataBound="gvReqStatus_RowDataBound" ShowFooter="True" 
                                                 Style="margin-right: 0px" Width="396px">
                                                 <PagerSettings Position="Top" />
                                                
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl.No.">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Req. No">
                                                         <ItemTemplate>
                                                             <asp:HyperLink ID="hlnkgvreqno" runat="server" BorderColor="#99CCFF" 
                                                                 BorderStyle="none" Font-Size="11px" Font-Underline="false" 
                                                                 Style="text-align: left; background-color: Transparent; color: Black;" 
                                                                 Target="_blank" 
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))  %>' 
                                                                 Width="70px">                                             
                                            
                                                </asp:HyperLink>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Req. Date">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvreqdat" runat="server" 
                                                                 Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Ship Supply Date">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvshipsupdat" runat="server" 
                                                                 Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipsupdat")).ToString("dd-MMM-yyyy") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Actual Supply Date">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvacsupdat" runat="server" 
                                                                 Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "deldat"))%>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Ship">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvship" runat="server" 
                                                                 Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                 Width="130px"></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Requisition Amount&lt;br/&gt;(3)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvreqamt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFreqamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Sales Order Amount &lt;br/&gt; (4)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvsalordamt" runat="server" BorderStyle="None" 
                                                                 Font-Size="12px" Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salordamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFsalordamt" runat="server" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Order  Received in % &lt;br/&gt; (5)=4/3*100">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvperoreqaamt" runat="server" BorderStyle="None" 
                                                                 Font-Size="12px" Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroreq")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Purchase Order &lt;br/&gt; (6)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvordramt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFordramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Received Amount &lt;br/&gt; (7)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvrcvamt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFrcvmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delivery Amount  &lt;br/&gt; (8)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvdelamt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFdelamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Short Delivery Amount &lt;br/&gt;(9)=7-8">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvstoreamt" runat="server" BorderStyle="None" 
                                                                 Font-Size="12px" Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "storeamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFstoreamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Invoice Amount  &lt;br/&gt; (10)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvinvamt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFinvamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Recovery Amount &lt;br/&gt; (11)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvcollamt" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFcollamt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Payment Amount &lt;br/&gt; (12)">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvpayment" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acpaidamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFpayment" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Fund &lt;br/&gt;Blocked&lt;br/&gt;(13)=11&lt;12">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvlfhoff" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lfhoff")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFlfhoff" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Fund &lt;br/&gt; Generated&lt;br/&gt;(14)=11&gt;12">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvltohoff" runat="server" BorderStyle="None" Font-Size="12px" 
                                                                 Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltohoff")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFltohoff" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Overhead &amp; Risk&lt;br/&gt;(15)=10-8">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvoheadarisk" runat="server" BorderStyle="None" 
                                                                 Font-Size="12px" Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oheadarisk")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                 Width="65px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lblgvFoheadarisk" runat="server" Font-Bold="True" 
                                                                 Font-Size="12px" ForeColor="White"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle HorizontalAlign="Right" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Mark Up %&lt;br/&gt;(16)=15/8*100">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvperodelamt" runat="server" BorderStyle="None" 
                                                                 Font-Size="12px" Style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perooheadar")).ToString("#,##0.00;(#,##0.00); ")+"%" %>' 
                                                                 Width="60px"></asp:Label>
                                                         </ItemTemplate>
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
                                  
                    </asp:View>
                
                 <asp:View ID="ViewCashFlow" runat="server">
           
                        <asp:GridView ID="grvCashFlow" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Style="text-align: left; margin-right: 0px;" OnRowDataBound="grvCashFlow_RowDataBound" 
                            Width="326px">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnactDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="280px" Font-Underline="False" Style="color: Black"
                                            OnClick="lbtnactDesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                              
                              
                               <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcf" runat="server" Font-Size="10px" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                 
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcf" runat="server" Font-Size="10px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcf" runat="server" Font-Size="10px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                         <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                               <asp:Panel ID="PanelNote" runat="server">
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                  
                                         <asp:Label ID="lblBankstatus" runat="server" CssClass="lblTxt lblName"  Text="Bank Status:"></asp:Label>

                                         </div>
                                      </div>

                                     <asp:GridView ID="gvbankbal" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="258px" OnRowDataBound="gvbankbal_RowDataBound">
                                            
                                            <Columns>
                                             <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNcf" runat="server" Font-Bold="True" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>' Width="280px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Change">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgbalambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Opening">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Closing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvclosambb" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                               
                                               
                                                
                                            </Columns>
                                          <FooterStyle CssClass="grvFooter"/>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                  </asp:Panel>
                                 </div>
                            </fieldset>

                
                      <%--  BorderColor="Maroon" BorderStyle="Solid"
                            BorderWidth="1px" Visible="False">
                            <table style="width: 100%;">
                                <%--<tr>
                                    <td class="style91">
                                        <asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Bank Status:"
                                            Width="120px"></asp:Label>
                                    </td>
                                   
                                </tr>--%>
                                
               
        </asp:View>

            </asp:MultiView>
        
</div>
</div>
           </ContentTemplate>
</asp:UpdatePanel>
    </asp:Content>