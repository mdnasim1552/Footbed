<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpExportRLZ.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpExportRLZ" %>
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
                                              <div class="col-md-10 asitCol10 pading5px">

                                             <asp:Label ID="Label6" runat="server" CssClass=" lblName lblTxt"  Text="Bank Name :"></asp:Label>

                                             <asp:TextBox ID="txtBank" runat="server" AutoCompleteType="Disabled"  CssClass="inputtextbox"></asp:TextBox>

                                             <asp:LinkButton ID="imgbtnFindBank" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindBank_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                               <asp:DropDownList  ID="ddlBank" runat="server"  CssClass="ddlPage"  AutoPostBack="True" onselectedindexchanged="ddlBank_SelectedIndexChanged" TabIndex="18" Width="372"> </asp:DropDownList>

                                                  <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShow_Click"   TabIndex="8">Show</asp:LinkButton>
                                                </div>
                                            </div>

                                    <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">

                                             <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt"  Text="Page Size:"></asp:Label>

                                                   <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  CssClass="ddlPage"  onselectedindexchanged="ddlpagesize_SelectedIndexChanged"   Width="80px">
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>50</asp:ListItem>
                                                                <asp:ListItem>100</asp:ListItem>
                                                                <asp:ListItem>150</asp:ListItem>
                                                                <asp:ListItem>200</asp:ListItem>
                                                                <asp:ListItem>300</asp:ListItem>
                                                            </asp:DropDownList>
                                            
                                                    <asp:Label ID="lblmsg" runat="server" CssClass=" smLbl"  Text=""></asp:Label>
                                                </div>
                                            </div>


                                  </asp:Panel>
                                </div>
                            </fieldset>
                        </div>
                    <div class="table table-responsive">

                           <asp:GridView ID="gvExRezInc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="16px" 
                            onpageindexchanging="gvExRezInc_PageIndexChanging" AllowPaging="True">
                            <PagerSettings Position="Top" />
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Master LC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMSLCDesc" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcmdesc")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" 
                                            Font-Size="13px" Height="16px" 
                                            style="text-align: center;" Width="80px" 
                                            onclick="lbtnTotal_Click" ForeColor="#000">Total</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderNo" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" 
                                            Font-Size="13px" Height="16px" 
                                            style="text-align: center; " Width="120px" class="UpdateButton"
                                            onclick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Order Value">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderVal" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInvoiceNo" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Value">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvExVal" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Realize Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRezVal" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Confirm Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdBillDat" runat="server" style="text-align: left; font-size:11px;" 
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy")%>' 
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvdBillDat_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdBillDat">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Incentive Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdIncDat" runat="server" style="text-align: left; font-size:11px;" 
                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "incentivedat")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "incentivedat")).ToString("dd-MMM-yyyy")%>' 
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvdIncDat_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdIncDat">
                                        </cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Incentive Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvIncVal" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incentiveval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receiving Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRecVal" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                            style="text-align: right; background-color: Transparent" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receival")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Balance Amout">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalAmt" runat="server" Font-Size="11px" 
                                            style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFooterTBalAmt" runat="server" Width="80px"></asp:Label>
                                    </FooterTemplate>
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
                    </div>
                    </div>
                  </div>
                                                  
                                                                      
                            
                     <%--<tr>
                                                    <td class="style19">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" style="text-align: left" Text="Bank Name :" 
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style21">
                                                        <asp:TextBox ID="txtBank" runat="server" AutoCompleteType="Disabled" 
                                                            BorderColor="#FFFF66" BorderStyle="Solid" BorderWidth="1px" Width="80px"></asp:TextBox>
                                                    </td>
                                                    <td align="left" class="style42">
                                                        <asp:ImageButton ID="imgbtnFindBank" runat="server" Height="17px" 
                                                            ImageUrl="~/Image/search-button-on.gif" 
                                                            Width="60px" onclick="imgbtnFindBank_Click" />
                                                    </td>
                                                    <td class="style24">
                                                        <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" 
                                                            Font-Bold="True" Font-Size="12px" Width="320px" 
                                                            onselectedindexchanged="ddlBank_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="lsePMLc" runat="server" QueryPattern="Contains" 
                                                            TargetControlID="ddlBank">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td class="style45">
                                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                            Font-Size="15px" Font-Underline="False" ForeColor="#000" 
                                                            onclick="lbtnShow_Click" style="margin-right: 6px">Show</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    </tr>--%>
                                      
              <%--<tr>
                                                        <td class="style19">
                                                            <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                ForeColor="#993300" style="color: #FFFFFF; text-align: left;" 
                                                                Text="Page Size:" Width="100px"></asp:Label>
                                                        </td>
                                                        <td class="style21">
                                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                                BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                                                Width="80px">
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
                                                        <td align="left" class="style42">
                                                            &nbsp;</td>
                                                        <td class="style24">
                                                            <asp:Label ID="lblmsg" runat="server" BackColor="Red" 
                                                                ForeColor="#000" Font-Bold="True" Font-Size="12px" 
                                                                style="FONT-SIZE: 12px; TEXT-ALIGN: left"></asp:Label>
                                                        </td>
                                                        <td class="style45">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>--%>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

