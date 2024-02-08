<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryMatIssue.aspx.cs" Inherits="SPEWEB.F_11_RawInv.EntryMatIssue" %>

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
                               <asp:Panel ID="Panel1" runat="server" >

                                   <div class="form-group">
                                    <div class="col-md-6 pading5px asitCol6">
                                        <asp:LinkButton ID="lbtnPreIsue" CssClass="btn btn-primary srearchBtn lblTxt lblName" runat="server" OnClick="lbtnPreIsue_Click" TabIndex="2"> Previous Issue </asp:LinkButton>

                                         <asp:DropDownList ID="ddlPreIssueNo" runat="server" CssClass=" ddlPage"  Width="395px" TabIndex="2" ></asp:DropDownList>
                                        </div>
                                       </div>

                                   <div class="form-group">
                                      <div class="col-md-8 asitCol8 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Order No. :"></asp:Label>

                                           <asp:TextBox ID="txtOrdsrch" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                           <asp:LinkButton ID="imgbtnFindOrd" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindOrd_Click"  TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                         <asp:DropDownList ID="ddlOrder" runat="server" CssClass=" ddlPage"  Width="300px" TabIndex="2" ></asp:DropDownList>
                                        <asp:Label ID="lblOrder" runat="server" CssClass=" ddlPage"  Width="300px"  Visible="False"></asp:Label>

                                           <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2" Text="Ok"></asp:LinkButton>
                                       </div>
                                     </div>

                                     <div class="form-group">
                                      <div class="col-md-8 asitCol8 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Issue No:"></asp:Label>

                                           <asp:TextBox ID="txtIssueno" runat="server"  CssClass="inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>

                                            <asp:Label ID="Label9" runat="server" CssClass=" smLbl_to" Text="Date:"></asp:Label>

                                          <asp:TextBox ID="txtDate" runat="server"  CssClass="inputtextbox" AutoCompleteType="Disabled"></asp:TextBox>
                                          <cc1:CalendarExtender ID="txtDate_CalendarExtender0" runat="server"  Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate">  </cc1:CalendarExtender>

                                          <asp:Label ID="lblmsg" runat="server" CssClass="btn  btn-danger primaryBtn"></asp:Label>
                                       </div>
                                     </div>


                                     </asp:Panel>

                                 <asp:Panel ID="PnlReslist" Visible="False" runat="server" >

                                     <div class="form-group">
                                      <div class="col-md-8 asitCol8 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Resource List:"></asp:Label>

                                           <asp:TextBox ID="txtRessrch" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                           <asp:LinkButton ID="imgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindRes_Click"  TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                         <asp:DropDownList ID="ddlResList" runat="server" CssClass=" ddlPage"  Width="300px" TabIndex="2" ></asp:DropDownList>
                                        <asp:Label ID="Label2" runat="server" CssClass=" ddlPage"  Width="300px"  Visible="False"></asp:Label>

                                           <asp:LinkButton ID="lbtnSelect" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnSelect_Click" TabIndex="2"> Select </asp:LinkButton>
                                       </div>
                                     </div>


                                 </asp:Panel>

                                </div>
                            </fieldset>
                        </div> 

                    <div class="table table-responsive">

                  <asp:GridView ID="gvMat" runat="server" AllowPaging="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" onpageindexchanging="gvMat_PageIndexChanging" 
                            ShowFooter="True" style="text-align: left" Width="580px">
                          
                            <Columns>
                            <asp:TemplateField HeaderText="Serial No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" onclick="lbtnTotal_Click" >Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                    <ItemStyle Font-Size="12px"  Font-Bold="true"/>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Materials Description ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnUpdate_Click" 
                                             CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMatDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>' 
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField> 
                                
                                <asp:TemplateField HeaderText="Unit">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="60px" BackColor="Transparent" BorderStyle="None" 
                                            style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                                 
                      

                                 <asp:TemplateField HeaderText="Balance Qty">
                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                     <FooterTemplate>
                                         <asp:Label ID="lgvfqty" runat="server" Font-Size="12px" ForeColor="#000" 
                                             style="text-align: right" Width="80px"></asp:Label>
                                     </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                            BorderStyle="Solid" Font-Size="12px" style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
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