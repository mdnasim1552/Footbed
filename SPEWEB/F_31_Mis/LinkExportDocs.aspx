<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkExportDocs.aspx.cs" Inherits="SPEWEB.F_31_Mis.LinkExportDocs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate>
         <div class="container moduleItemWrpper">
          <div class="contentPart">
	          <div class="row">
                   <fieldset class="scheduler-border fieldset_A">
                      <div class="form-horizontal">
                         <asp:Panel ID="Panel2" runat="server">
			                <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">
                                     <asp:Label ID="Label2" runat="server"   Text="Invoice ID :" CssClass="lblName lblTxt"></asp:Label>
                                     <asp:Label ID="lblInvID" runat="server"  Text="Invoice ID :" CssClass=" inputtextbox"></asp:Label>
                                     <asp:Label ID="Label4" runat="server" Text="Invoice No :" CssClass=" smLbl_to"></asp:Label>
                                     <asp:TextBox ID="txtRefNo" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                     <asp:Label ID="Label1" runat="server"  Text="Inv.Date:" CssClass="smLbl_to" ></asp:Label>
                                     <asp:TextBox ID="txtDate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
			                   </div>
			                </div>
                            <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="lblPrelist" runat="server" Text="Previous List :" CssClass="lblName lblTxt"></asp:Label>
                                     <asp:TextBox ID="txtPrevSearch" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnPrevList" CssClass="btn btn-primary srearchBtn" runat="server" onclick="lbtnPrevList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                      <asp:DropDownList ID="DDLPrevIDList" runat="server"  CssClass="ddlPage" Width="232px"></asp:DropDownList>
                                     <asp:Label ID="lblMessage" runat="server"  CssClass="btn btn-danger primaryBtn"></asp:Label>

			                   </div>
			                </div>
                          </asp:Panel>

                           <asp:Panel id="Panel1" runat="server">
                               <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label40" runat="server" Text="Master LC No :" CssClass="lblName lblTxt"></asp:Label>
                                       <asp:DropDownList ID="DDLMasterLC" CssClass="ddlPage" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="DDLMasterLC_SelectedIndexChanged"  Width="330px"></asp:DropDownList>
                                     <asp:Label ID="lblMLC" runat="server"  CssClass=" inputtextbox" Visible="False" Width="330px"></asp:Label>
                                      <asp:LinkButton ID="lbtbExport" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtbExport_Click" Text="Ok"   TabIndex="8"></asp:LinkButton>
			                   </div>
			                </div>
                               <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label41" runat="server" Text="Order No :"  CssClass="lblName lblTxt"></asp:Label>
                                       <asp:DropDownList ID="DDLOrder" CssClass="ddlPage" runat="server" AutoPostBack="True" onselectedindexchanged="DDLOrder_SelectedIndexChanged"  Width="330px"></asp:DropDownList>
                                     <asp:Label ID="lblOrderNo" runat="server"  CssClass=" inputtextbox" Visible="False" Width="330px"></asp:Label>
                                     
			                   </div>
			                </div>
                               <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label6" runat="server" Text="Shipment :"   CssClass="lblName lblTxt"></asp:Label>
                                       <asp:DropDownList ID="DDLShipment" CssClass="ddlPage" runat="server"  Width="330px"></asp:DropDownList>
                                     <asp:Label ID="lblShipmentName" runat="server"  CssClass=" inputtextbox" Visible="False" Width="330px"></asp:Label>
                                     
			                   </div>
			                </div>
                               <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label3" runat="server" Text="Shipment Type :"   CssClass="lblName lblTxt"></asp:Label>
                                       <asp:DropDownList ID="DDLShipmentType" CssClass="ddlPage" runat="server"  Width="330px"></asp:DropDownList>
                                     <asp:Label ID="lblShipmentType" runat="server"  CssClass=" inputtextbox" Visible="False" Width="330px"></asp:Label>                                     
			                   </div>
			                </div>
                           </asp:Panel>

                              <asp:Panel ID="PanelFLetter" runat="server" Visible="False">
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label38" runat="server"  CssClass="lblName lblTxt"  Text="Document Checklist in Forwarding Letter"  Width="324px"></asp:Label>
                                                                       
			                   </div>
			                </div>
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label11" runat="server"  CssClass="lblName lblTxt"  Text="01."  Width="19px"></asp:Label>
                                      <asp:Label ID="Label20" runat="server"  Text="BILL OF EXCHANGE" CssClass="lblName lblTxt" Width="150px"></asp:Label>     
                                    
                                      <asp:RadioButtonList ID="rblFwl01" runat="server" Height="1px"   RepeatDirection="Horizontal"  Width="102px" CssClass="rbtnList1">
                                            <asp:ListItem Selected="True">None</asp:ListItem>
                                            <asp:ListItem>Original</asp:ListItem>
                                            <asp:ListItem>Copy</asp:ListItem>
                                        </asp:RadioButtonList>    
                                    
                                          <asp:Label ID="Label15" runat="server" Text="06."  CssClass="lblName lblTxt"></asp:Label>     
                                     <asp:Label ID="Label24" runat="server"  Text="EXPORT LICENCE"  CssClass="lblName lblTxt" Width="150px"></asp:Label>
                                            
                                      <asp:RadioButtonList ID="rblFwl06" runat="server" CssClass="rbtnList1"   RepeatDirection="Horizontal"  Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>


                                        
			                   </div>
			                </div>
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label29" runat="server"  CssClass="lblName lblTxt"  Text="02."  Width="19px"></asp:Label>
                                      <asp:Label ID="Label21" runat="server"   Text="COMMERCIAL INVOICE" CssClass="lblName lblTxt" Width="150px"></asp:Label>     
                                    
                                      <asp:RadioButtonList ID="rblFwl02" runat="server" Height="1px"   RepeatDirection="Horizontal"  Width="102px" CssClass="rbtnList1">
                                            <asp:ListItem Selected="True">None</asp:ListItem>
                                            <asp:ListItem>Original</asp:ListItem>
                                            <asp:ListItem>Copy</asp:ListItem>
                                        </asp:RadioButtonList>    
                                    
                                          <asp:Label ID="Label16" runat="server" Text="07."   CssClass="lblName lblTxt"></asp:Label>     
                                     <asp:Label ID="Label25" runat="server"   Text="INSPECTION CERTIFICATE"  CssClass="lblName lblTxt" Width="150px"></asp:Label>
                                            
                                      <asp:RadioButtonList ID="rblFwl07" runat="server" CssClass="rbtnList1"   RepeatDirection="Horizontal"  Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>


                                        
			                   </div>
			                </div>
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label12" runat="server"  CssClass="lblName lblTxt" Text="03."   Width="19px"></asp:Label>
                                      <asp:Label ID="Label22" runat="server"   Text="PACKAGING LIST" CssClass="lblName lblTxt" Width="150px"></asp:Label>     
                                    
                                      <asp:RadioButtonList ID="rblFwl03" runat="server"   RepeatDirection="Horizontal"  Width="102px" CssClass="rbtnList1">
                                            <asp:ListItem Selected="True">None</asp:ListItem>
                                            <asp:ListItem>Original</asp:ListItem>
                                            <asp:ListItem>Copy</asp:ListItem>
                                        </asp:RadioButtonList>    
                                    
                                          <asp:Label ID="Label17" runat="server" Text="08."   CssClass="lblName lblTxt"></asp:Label>     
                                     <asp:Label ID="Label26" runat="server"   Text="BENEFICIARY CERTIFICATE"  CssClass="lblName lblTxt" Width="150px"></asp:Label>
                                            
                                      <asp:RadioButtonList ID="rblFwl08" runat="server" CssClass="rbtnList1"   RepeatDirection="Horizontal"  Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>


                                        
			                   </div>
			                </div>
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label13" runat="server"  CssClass="lblName lblTxt" Text="04."   Width="19px"></asp:Label>
                                      <asp:Label ID="Label30" runat="server"   Text="B/L/HAWB"  CssClass="lblName lblTxt" Width="150px"></asp:Label>     
                                    
                                      <asp:RadioButtonList ID="rblFwl04" runat="server"   RepeatDirection="Horizontal"  Width="102px" CssClass="rbtnList1">
                                            <asp:ListItem Selected="True">None</asp:ListItem>
                                            <asp:ListItem>Original</asp:ListItem>
                                            <asp:ListItem>Copy</asp:ListItem>
                                        </asp:RadioButtonList>    
                                    
                                          <asp:Label ID="Label18" runat="server" Text="09."   CssClass="lblName lblTxt"></asp:Label>     
                                     <asp:Label ID="Label27" runat="server"   Text="EXP"  CssClass="lblName lblTxt" Width="150px"></asp:Label>
                                            
                                      <asp:RadioButtonList ID="rblFwl09" runat="server" CssClass="rbtnList1"   RepeatDirection="Horizontal"  Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>


                                        
			                   </div>
			                </div>
                                  <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:Label ID="Label14" runat="server"  CssClass="lblName lblTxt" Text="05."   Width="19px"></asp:Label>
                                      <asp:Label ID="Label23" runat="server"   Text="GSP/CO"   CssClass="lblName lblTxt" Width="150px"></asp:Label>     
                                    
                                      <asp:RadioButtonList ID="rblFwl05" runat="server"   RepeatDirection="Horizontal"  Width="102px" CssClass="rbtnList1">
                                            <asp:ListItem Selected="True">None</asp:ListItem>
                                            <asp:ListItem>Original</asp:ListItem>
                                            <asp:ListItem>Copy</asp:ListItem>
                                        </asp:RadioButtonList>    
                                    
                                          <asp:Label ID="Label19" runat="server" Text="10."   CssClass="lblName lblTxt"></asp:Label>     
                                     <asp:Label ID="Label28" runat="server"   Text="ETC"  CssClass="lblName lblTxt" Width="150px"></asp:Label>
                                            
                                      <asp:RadioButtonList ID="rblFwl10" runat="server" CssClass="rbtnList1"   RepeatDirection="Horizontal"  Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>


                                        
			                   </div>
			                </div>
                              </asp:Panel>


                       </div>
                   </fieldset>
	          </div>
              <div class="table table-responsive">
                  <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
            ShowFooter="True" style="text-align: left" Width="755px">
            <Columns>
                <asp:TemplateField HeaderText="Style ID" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblgvStyleID" runat="server" 
                            style="FONT-SIZE: 10px; TEXT-TRANSFORM: capitalize" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>' 
                            Width="51px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Color ID" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblgvColorID" runat="server" 
                            style="FONT-SIZE: 10px; TEXT-TRANSFORM: capitalize" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>' 
                            Width="51px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Style">
                    <FooterTemplate>
                        <asp:LinkButton ID="lbtnFUpdate" runat="server"  CssClass="btn btn-danger primaryBtn" onclick="lbtnFUpdate_Click">Final Update</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgvStyleDesc" runat="server" 
                            style="FONT-SIZE: 11px; TEXT-TRANSFORM: capitalize; " 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleDes")) %>' 
                            Width="136px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <FooterStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Color">
                    <FooterTemplate>
                        <asp:LinkButton ID="lbtnTotal" runat="server"  CssClass="btn btn-primary primaryBtn" onclick="lbtnTotal_Click">Total</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblgvColorDesc" runat="server" 
                            style="FONT-SIZE: 11px; TEXT-TRANSFORM: capitalize;" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Desc1")) %>' 
                            Width="84px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <FooterStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sl." Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblgvSLNO" runat="server" 
                            style="FONT-SIZE: 10px; TEXT-TRANSFORM: capitalize; COLOR: yellow; BACKGROUND-COLOR: black; TEXT-ALIGN: right" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "crtngrp"))+"." %>' 
                            Width="20px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblgvUnit1" runat="server" style="TEXT-TRANSFORM: capitalize" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unit1")) %>' 
                            Width="3px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-01">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201001" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201001")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-02">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201002" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201002")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-03" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201003" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201003")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-04" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201004" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;;font-size:11px" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201004")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-05" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201005" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201005")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-06" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201006" runat="server" BackColor="Transparent" 
                            BorderColor="Transparent" BorderStyle="None" 
                            style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201006")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-07" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201007" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201007")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-08" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201008" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201008")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-09" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201009" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201009")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-10" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201010" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201010")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-11" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201011" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201011")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-12" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201012" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align:right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201012")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-13" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201013" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201013")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-14" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201014" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align:right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201014")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size-15" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvF7201015" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201015")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblgvTotal1" runat="server" 
                            style="FONT-SIZE: 11px; TEXT-ALIGN: right" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Total1")).ToString("#,##0;(#,##0); ") %>' 
                            Width="40px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CTN Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvCrtnqty" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="text-align: right;font-size:11px;" 
                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crtnqty")).ToString("###0;(###0); ") %>' 
                            Width="40px"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle Font-Size="11px" ForeColor="#000" HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Font-Size="12px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CTN  Sl.No.">
                    <ItemTemplate>
                        <asp:TextBox ID="txtgvCrtnslno" runat="server" BackColor="Transparent" 
                            BorderStyle="None" style="font-size:11px;" 
                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Crtnslno")) %>' 
                            Width="60px"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
           <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />

        </asp:GridView>
                   <asp:Panel ID="PanelLC2" runat="server" Visible="False">

                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">
                                     <asp:Label ID="Label44" runat="server" Text="Ex. Fac Date :" CssClass="lblName lblTxt"></asp:Label>                                    
                                     <asp:TextBox ID="txtExfacdate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtExfacdate_CalendarExtender" runat="server" 
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExfacdate">
                                            </cc1:CalendarExtender>
                                     <asp:Label ID="Label45" runat="server"  Text="Ship Line:"  CssClass="lblName lblTxt" ></asp:Label>
                                     <asp:DropDownList ID="ddlShipLine" runat="server"  CssClass="ddlPage" Width="250px"></asp:DropDownList>

			                   </div>
			                </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">
                                     <asp:Label ID="Label7" runat="server" Text="Export No :" CssClass="lblName lblTxt"></asp:Label>                                    
                                     <asp:TextBox ID="txtEXPORTNO" runat="server"  CssClass="inputtextbox"></asp:TextBox>                                       
                                     <asp:Label ID="Label10" runat="server"  Text="B/L/AWB No :"  CssClass="lblName lblTxt" ></asp:Label>
                                     <asp:DropDownList ID="txtBLAWBNO" runat="server"  CssClass="ddlPage" Width="125px"></asp:DropDownList>
			                   </div>
			                </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">
                                     <asp:Label ID="Label8" runat="server" Text="Export Date :" CssClass="lblName lblTxt"></asp:Label>                                    
                                     <asp:TextBox ID="txtEXPORTDT" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                       <cc1:CalendarExtender ID="txtEXPORTDT_CalendarExtender2" runat="server" 
                                Enabled="True" TargetControlID="txtEXPORTDT" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>

                                     <asp:Label ID="Label9" runat="server"  Text="Stuffing Date :"  CssClass="lblName lblTxt" ></asp:Label>
                                     <asp:TextBox ID="txtBLAWBDT" runat="server" CssClass="inputtextbox"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBLAWBDT_CalendarExtender3" runat="server" 
                                Enabled="True" TargetControlID="txtBLAWBDT" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>

			                   </div>
			                </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">
                                     <asp:Label ID="Label31" runat="server" Text="Remarks :" CssClass="lblName lblTxt"></asp:Label>                                                                                            
                                     <asp:Label ID="Label32" runat="server"  Text="Container No.:"  CssClass="lblName lblTxt" ></asp:Label>
                                     <asp:TextBox ID="txtCNTNRNO" runat="server" CssClass="inputtextbox"></asp:TextBox>                           
                                     <asp:Label ID="Label37" runat="server"  Text="Shiping Mark :"  CssClass="lblName lblTxt" ></asp:Label>
			                   </div>
			                </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">                                  
                                     <asp:TextBox ID="txtINVRMRKS" runat="server" TextMode="MultiLine" Width="360px" CssClass="inputtextbox"></asp:TextBox>                                    
                                     <asp:TextBox ID="txtSHIPMARK" runat="server" CssClass="inputtextbox" TextMode="MultiLine" Width="360px"></asp:TextBox>                                                                      
			                  </div>
                       </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">  
                                     <asp:Label ID="Label33" runat="server" Text="Total Quantity:" CssClass="lblName lblTxt"></asp:Label>                                                                                                                                                                
                                     <asp:TextBox ID="txtTQTYDES" runat="server"  CssClass="inputtextbox" Width="240px"></asp:TextBox>  
                                        
                                     <asp:Label ID="Label35" runat="server" Text="Total Gross Weight:" Width="130px" CssClass="lblName lblTxt"></asp:Label>                                                                                                                                                             
                                     <asp:TextBox ID="txtTGWTDES" runat="server" CssClass="inputtextbox"  Width="225"></asp:TextBox>                                                                      
			                  </div>
                       </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">  
                                     <asp:Label ID="Label34" runat="server" Text="Total Net Weight:" CssClass="lblName lblTxt"></asp:Label>                                                                                                                                                                
                                     <asp:TextBox ID="txtTNWTDES" runat="server"  CssClass="inputtextbox" Width="240px"></asp:TextBox>  
                                        
                                     <asp:Label ID="Label36" runat="server" Text="Total CBM:"  CssClass="lblName lblTxt"></asp:Label>                                                                                                                                                             
                                     <asp:TextBox ID="txtTCBMDES" runat="server" CssClass="inputtextbox"  Width="225"></asp:TextBox>                                                                      
			                  </div>
                       </div>
                       <div class="form-group">
                                <div class="col-md-10  pading5px  asitCol10">  
                                      <asp:Label ID="Label5" runat="server" Text="Measurement:"  CssClass="lblName lblTxt" ></asp:Label>
                                      <asp:TextBox ID="txtMSURMNT" runat="server"  CssClass="inputtextbox"  Width="225px"></asp:TextBox>                                                            
			                  </div>
                       </div>

                   </asp:Panel>
              </div>
           </div>
      </div>


                <%--<tr>
                    <td align="left" class="style16">
                        <asp:Label ID="Label2" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: left; margin-left: 0px;" 
                            Text="Invoice ID :" Width="100px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:Label ID="lblInvID" runat="server" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 16px;COLOR: #000;BACKGROUND-COLOR: maroon; TEXT-ALIGN: center" 
                            Width="102px"></asp:Label>
                    </td>
                    <td align="left" class="style11">
                        <asp:Label ID="Label4" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                            Text="Invoice No :" Width="75px"></asp:Label>
                    </td>
                    <td align="left" class="style14">
                        <asp:TextBox ID="txtRefNo" runat="server" 
                            style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                            Width="140px"></asp:TextBox>
                    </td>
                    <td align="left" class="style15">
                        <asp:Label ID="Label1" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" Text="Inv.Date:" 
                            Width="70px"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDate" runat="server" 
                            style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                            Width="90px"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                            Format="dd-MMM-yyyy" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
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
                </tr>--%>
                <%--<tr>
                    <td align="left" class="style16">
                        <asp:Label ID="lblPrelist" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: left; margin-left: 0px;" 
                            Text="Previous List :" Width="100px"></asp:Label>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="txtPrevSearch" runat="server" 
                            style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                            Width="98px"></asp:TextBox>
                    </td>
                    <td align="left" class="style11">
                        <asp:ImageButton ID="lbtnPrevList" runat="server" Height="16px" 
                            ImageUrl="~/Image/find_images.jpg" onclick="lbtnPrevList_Click" Width="16px" />
                    </td>
                    <td align="left" class="style12" colspan="3">
                        <asp:DropDownList ID="DDLPrevIDList" runat="server" style="FONT-SIZE: 12px" 
                            Width="320px">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ListSearchExt1" runat="server" 
                            QueryPattern="Contains" TargetControlID="DDLPrevIDList">
                        </cc1:ListSearchExtender>
                    </td>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" BackColor="Red" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px;  TEXT-ALIGN: left"></asp:Label>
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
                </tr>--%>
        
                <%--<tr>
                    <td class="style6">
                        <asp:Label ID="Label40" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                            Text="Master LC No :" Width="95px"></asp:Label>
                    </td>
                    <td align="left" class="style18">
                        <asp:DropDownList ID="DDLMasterLC" runat="server" AutoPostBack="True" 
                            ForeColor="Black" OnSelectedIndexChanged="DDLMasterLC_SelectedIndexChanged" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Width="330px">
                        </asp:DropDownList>
                        <asp:Label ID="lblMLC" runat="server" 
                            style="BORDER-RIGHT: aqua 1px solid; BORDER-TOP: aqua 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: aqua 1px solid; COLOR: yellow; BORDER-BOTTOM: aqua 1px solid; BACKGROUND-COLOR: #330000; TEXT-ALIGN: left" 
                            Visible="False" Width="330px"></asp:Label>
                        <asp:LinkButton ID="lbtbExport" runat="server" BackColor="#003366" 
                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="#000" onclick="lbtbExport_Click" 
                            style="width: 21px">Ok</asp:LinkButton>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                <%--<tr>
                    <td class="style6">
                        <asp:Label ID="Label41" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: left" Text="Order No :" 
                            Width="95px"></asp:Label>
                    </td>
                    <td align="left" class="style18" valign="top">
                        <asp:DropDownList ID="DDLOrder" runat="server" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Width="330px" 
                            AutoPostBack="True" onselectedindexchanged="DDLOrder_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="lblOrderNo" runat="server" 
                            style="BORDER-RIGHT: aqua 1px solid; BORDER-TOP: aqua 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: aqua 1px solid; COLOR: yellow; BORDER-BOTTOM: aqua 1px solid; BACKGROUND-COLOR: #330000; TEXT-ALIGN: left" 
                            Visible="False" Width="330px"></asp:Label>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                <%--<tr>
                    <td align="right" class="style6">
                        <asp:Label ID="Label6" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: left" Text="Shipment :" 
                            Width="95px"></asp:Label>
                    </td>
                    <td align="left" class="style18" valign="top">
                        <asp:DropDownList ID="DDLShipment" runat="server" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Width="330px">
                        </asp:DropDownList>
                        <asp:Label ID="lblShipmentName" runat="server" 
                            style="BORDER-RIGHT: aqua 1px solid; BORDER-TOP: aqua 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: aqua 1px solid; COLOR: yellow; BORDER-BOTTOM: aqua 1px solid; BACKGROUND-COLOR: #330000; TEXT-ALIGN: left" 
                            Visible="False" Width="330px"></asp:Label>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                <%--<tr>
                    <td align="right" class="style6">
                        <asp:Label ID="Label3" runat="server" ForeColor="#000" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: left" Text="Shipment Type :" 
                            Width="105px"></asp:Label>
                    </td>
                    <td align="left" class="style18" valign="top">
                        <asp:DropDownList ID="DDLShipmentType" runat="server" 
                            style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Width="330px">
                        </asp:DropDownList>
                        <asp:Label ID="lblShipmentType" runat="server" 
                            style="BORDER-RIGHT: aqua 1px solid; BORDER-TOP: aqua 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: aqua 1px solid; COLOR: yellow; BORDER-BOTTOM: aqua 1px solid; BACKGROUND-COLOR: #330000; TEXT-ALIGN: left" 
                            Visible="False" Width="330px"></asp:Label>
                    </td>
                    <td class="style9">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
          
                    <%--<tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="5" style="TEXT-ALIGN: center">
                            <asp:Label ID="Label38" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; COLOR: maroon; TEXT-DECORATION: underline overline" 
                                Width="324px">Document Checklist in Forwarding Letter</asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="01." 
                                Width="19px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="BILL OF EXCHANGE" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 240px">
                            <asp:RadioButtonList ID="rblFwl01" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="102px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 303px">
                        </td>
                        <td style="WIDTH: 306px">
                        </td>
                        <td style="WIDTH: 86px">
                            <asp:Label ID="Label15" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="06." 
                                Width="19px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:Label ID="Label24" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="EXPORT LICENCE" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:RadioButtonList ID="rblFwl06" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <asp:Label ID="Label29" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="02." 
                                Width="19px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="COMMERCIAL INVOICE" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 240px">
                            <asp:RadioButtonList ID="rblFwl02" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 303px">
                        </td>
                        <td style="WIDTH: 306px">
                        </td>
                        <td style="WIDTH: 86px">
                            <asp:Label ID="Label16" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="07." 
                                Width="19px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:Label ID="Label25" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="INSPECTION CERTIFICATE" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:RadioButtonList ID="rblFwl07" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="03." 
                                Width="19px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label22" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="PACKAGING LIST" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 240px">
                            <asp:RadioButtonList ID="rblFwl03" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 303px">
                        </td>
                        <td style="WIDTH: 306px">
                        </td>
                        <td style="WIDTH: 86px">
                            <asp:Label ID="Label17" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="08." 
                                Width="19px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:Label ID="Label26" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" 
                                Text="BENEFICIARY CERTIFICATE" Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:RadioButtonList ID="rblFwl08" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="04." 
                                Width="19px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" Text="B/L/HAWB" 
                                Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 240px">
                            <asp:RadioButtonList ID="rblFwl04" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 303px">
                        </td>
                        <td style="WIDTH: 306px">
                        </td>
                        <td style="WIDTH: 86px">
                            <asp:Label ID="Label18" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="09." 
                                Width="19px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:Label ID="Label27" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" Text="EXP" 
                                Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:RadioButtonList ID="rblFwl09" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="05." 
                                Width="19px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label23" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" Text="GSP/CO" 
                                Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 240px">
                            <asp:RadioButtonList ID="rblFwl05" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 303px">
                        </td>
                        <td style="WIDTH: 306px">
                        </td>
                        <td style="WIDTH: 86px">
                            <asp:Label ID="Label19" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: right" Text="10." 
                                Width="19px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:Label ID="Label28" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 10px; TEXT-ALIGN: left" Text="ETC" 
                                Width="150px"></asp:Label>
                        </td>
                        <td style="WIDTH: 233px">
                            <asp:RadioButtonList ID="rblFwl10" runat="server" Height="1px" 
                                RepeatDirection="Horizontal" style="FONT-SIZE: 10px; TEXT-ALIGN: center" 
                                Width="84px">
                                <asp:ListItem Selected="True">None</asp:ListItem>
                                <asp:ListItem>Original</asp:ListItem>
                                <asp:ListItem>Copy</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="WIDTH: 233px">
                        </td>
                    </tr>--%>


                    <%--<tr>
                        <td style="WIDTH: 64px">
                            &nbsp;</td>
                        <td style="WIDTH: 139px">
                            <asp:Label ID="Label44" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Ex. Fac Date :" Width="110px"></asp:Label>
                        </td>
                        <td style="WIDTH: 13010901px">
                            <asp:TextBox ID="txtExfacdate" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtExfacdate_CalendarExtender" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExfacdate">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="WIDTH: 11179653px">
                            &nbsp;</td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label45" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" Text="Ship Line:" 
                                Width="130px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlShipLine" runat="server" BackColor="#D4FFFF" 
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                        <td style="WIDTH: 91833184px">
                            &nbsp;</td>
                        <td style="WIDTH: 21524130px">
                            &nbsp;</td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                        </td>
                        <td style="WIDTH: 139px">
                            <asp:Label ID="Label7" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Export No :" Width="110px"></asp:Label>
                        </td>
                        <td style="WIDTH: 13010901px">
                            <asp:TextBox ID="txtEXPORTNO" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label10" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="B/L/AWB No :" Width="130px"></asp:Label>
                        </td>
                        <td style="WIDTH: 70426px">
                            <asp:TextBox ID="txtBLAWBNO" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 4364804px">
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                        </td>
                        <td style="WIDTH: 139px">
                            <asp:Label ID="Label8" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Export Date :" Width="110px"></asp:Label>
                        </td>
                        <td style="WIDTH: 13010901px">
                            <asp:TextBox ID="txtEXPORTDT" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtEXPORTDT_CalendarExtender2" runat="server" 
                                Enabled="True" TargetControlID="txtEXPORTDT" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label9" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Stuffing Date :" Width="129px"></asp:Label>
                        </td>
                        <td style="WIDTH: 70426px">
                            <asp:TextBox ID="txtBLAWBDT" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtBLAWBDT_CalendarExtender3" runat="server" 
                                Enabled="True" TargetControlID="txtBLAWBDT" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td style="WIDTH: 4364804px">
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                            <asp:Label ID="Label31" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Text="Remarks :" Width="95px"></asp:Label>
                        </td>
                        <td style="WIDTH: 139px">
                            <asp:Label ID="Label32" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Container No.:" Width="110px"></asp:Label>
                        </td>
                        <td style="WIDTH: 13010901px">
                            <asp:TextBox ID="txtCNTNRNO" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="125px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label37" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px" Text="Shiping Mark :" Width="95px"></asp:Label>
                        </td>
                        <td style="WIDTH: 70426px">
                            &nbsp;</td>
                        <td style="WIDTH: 4364804px">
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtINVRMRKS" runat="server" Height="60px" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                TextMode="MultiLine" Width="360px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSHIPMARK" runat="server" Height="60px" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                TextMode="MultiLine" Width="360px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                            <asp:Label ID="Label33" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Total Quantity:" Width="110px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTQTYDES" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="240px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label35" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Total Gross Weight:" Width="130px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTGWTDES" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="225px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                            <asp:Label ID="Label34" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Total Net Weight:" Width="110px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTNWTDES" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="240px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label36" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" Text="Total CBM:" 
                                Width="130px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTCBMDES" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="225px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td style="WIDTH: 64px">
                        </td>
                        <td style="WIDTH: 139px">
                        </td>
                        <td style="WIDTH: 13010901px">
                        </td>
                        <td style="WIDTH: 11179653px">
                        </td>
                        <td style="WIDTH: 19px">
                            <asp:Label ID="Label39" runat="server" 
                                style="FONT-WEIGHT: bold; FONT-SIZE: 14px; TEXT-ALIGN: right" 
                                Text="Measurement:" Width="130px"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMSURMNT" runat="server" 
                                style="BORDER-RIGHT: blue 1px solid; BORDER-TOP: blue 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 14px; BORDER-LEFT: blue 1px solid; BORDER-BOTTOM: blue 1px solid; BACKGROUND-COLOR: #d4ffff" 
                                Width="225px"></asp:TextBox>
                        </td>
                        <td style="WIDTH: 91833184px">
                        </td>
                        <td style="WIDTH: 21524130px">
                        </td>
                    </tr>--%>
                 
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>

