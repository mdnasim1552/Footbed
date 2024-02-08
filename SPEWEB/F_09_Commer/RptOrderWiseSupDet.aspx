<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptOrderWiseSupDet.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptOrderWiseSupDet" %>

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
                                <asp:Panel ID="Panel12" runat="server">
                                       <div class="form-group">
                                            <div class="col-md-7 asitCol7 pading5px">
                                                <asp:Label ID="Label5" runat="server" CssClass=" lblName lblTxt" Text="Date:"  ></asp:Label>

                                             <asp:TextBox ID="txtFDate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                               <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                        </cc1:CalendarExtender>


                                               <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>

                                                 <asp:TextBox ID="txttodate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                        <div class="col-md-7 asitCol7 pading5px">

                                             <asp:Label ID="Label7" runat="server" CssClass=" lblName lblTxt" Text="Order No:"></asp:Label>

                                             <asp:TextBox ID="txtSrcOrder" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                             <asp:LinkButton ID="imgbtnFindOrder" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindOrder_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                               <asp:DropDownList ID="ddlOrderList" runat="server" CssClass="ddlPage"   TabIndex="18" Width="300px"> </asp:DropDownList>

                                             <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnOk_Click" TabIndex="8"> Ok </asp:LinkButton>

                                        
                                        </div>
                                    </div>

                                       <div class="form-group">
                                        <div class="col-md-6 asitCol6 pading5px">

                                             <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Page Size:"  Visible="False"></asp:Label>

                                          <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"   CssClass="ddlPage" onselectedindexchanged="ddlpagesize_SelectedIndexChanged"   Visible="False">
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

                                   <asp:GridView ID="gvOrderWiseSupdet" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" onpageindexchanging="gvOrderWiseSupdet_PageIndexChanging" 
                            ShowFooter="True" Width="501px" 
                            onrowdatabound="gvOrderWiseSupdet_RowDataBound">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                                 

                                <asp:TemplateField HeaderText="Job #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvJobNo" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobno")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvorder" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>' 
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="LC Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlcamt" runat="server" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="BBLC #">
                                    <ItemTemplate>
                                       <asp:Label ID="lgbblcdesc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Materials Description">
                                    <ItemTemplate>
                                       <asp:Label ID="lgMatDesc" runat="server" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <ItemStyle HorizontalAlign="Left" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvUnit" runat="server"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvQty" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRate" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbblcamt" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount(TK)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbblcamtTk" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0;(#,##0); ") %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                   
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                               
                                   <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                       <asp:Label ID="lgvsupplier" runat="server"
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
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
                                    <td class="style56">
                                    </td>
                                    <td class="style57">
                                        <asp:Label ID="Label5" runat="server" CssClass="style42" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: right" Text="Date:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtFDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style59">
                                        <asp:Label ID="Label6" runat="server" CssClass="style42" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: right" Text="To:"></asp:Label>
                                    </td>
                                    <td class="style71">
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style61">
                                        &nbsp;</td>
                                    <td class="style62">
                                    </td>
                                    <td class="style63">
                                    </td>
                                    <td class="style63">
                                    </td>
                                      <td class="style63">
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="style56">
                                        &nbsp;</td>
                                    <td class="style57">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="14px" 
                                            style="text-align: right; color: #FFFFFF;" Text="Order No:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtSrcOrder" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style59">
                                        <asp:ImageButton ID="imgbtnFindOrder" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnFindOrder_Click" 
                                            Width="16px" />
                                    </td>
                                    <td class="style71">
                                        <asp:DropDownList ID="ddlOrderList" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlOrderList_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlOrderList">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td class="style61">
                                        <asp:LinkButton ID="lnkbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="18px" onclick="lnkbtnOk_Click" 
                                            style="text-align: center" Width="60px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style62">
                                        &nbsp;</td>
                                    <td class="style63">
                                        &nbsp;</td>
                                    <td class="style63">
                                        &nbsp;</td>
                                                  <td class="style63">
                                        &nbsp;</td>
                                </tr>--%>
                                <%--<tr>
                                    <td class="style56">
                                        &nbsp;</td>
                                    <td class="style57">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="100px" 
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="80px" 
                                            Visible="False">
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
                                    <td class="style59">
                                        &nbsp;</td>
                                    <td class="style71">
                                        &nbsp;</td>
                                    <td class="style61">
                                        &nbsp;</td>
                                    <td class="style62">
                                        &nbsp;</td>
                                    <td class="style63">
                                        &nbsp;</td>
                                    <td class="style63">
                                        &nbsp;</td>
                                    <td class="style63">
                                        &nbsp;</td>
                                </tr>--%>
    

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


