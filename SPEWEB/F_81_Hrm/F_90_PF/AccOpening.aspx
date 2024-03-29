﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccOpening.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_90_PF.AccOpening" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblopndate" runat="server" CssClass="lblTxt lblName">Opening Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>





                                </div>



                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblacccode1" runat="server" CssClass="lblTxt lblName">Accounts Code</asp:Label>
                                        <asp:TextBox ID="txtFilter" runat="server" CssClass=" inputtextbox"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ImageButton1" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImageButton1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>


                                </div>

                            </div>




                        </fieldset>

                    </div>

                    <div class="row">

                        <asp:GridView ID="dgv2" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCreated="dgv2_RowCreated"
                            PagerSettings-Position="Bottom" PagerStyle-BackColor="#4A89BC"
                            PagerSettings-Visible="false"
                            PagerStyle-HorizontalAlign="Center" RowStyle-Font-Size="12px" ShowFooter="True"
                            Width="600px" OnRowCommand="dgv2_RowCommand" PageSize="15" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>
                                      

                                         <asp:LinkButton ID="lnkFinalUpdate" runat="server"
                                                OnClick="lnkFinalUpdate_Click"
                                                CssClass="btn btn-danger primarygrdBtn" >Final Update</asp:LinkButton>
                                      

                                            <asp:DropDownList ID="dgv2ddlPageNo" runat="server" AutoPostBack="True"
                                                Font-Bold="True" Font-Size="14px"
                                                OnSelectedIndexChanged="dgv2ddlPageNo_SelectedIndexChanged"
                                                Style="margin:0 0 0 10px; border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                Width="180px">
                                            </asp:DropDownList>
                                      
                                           
                                       
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Font-Size="11px" Width="400px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level"
                                    ItemStyle-HorizontalAlign="Center">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                            onmouseover="style.color='#FF9999'" onmouseout="style.color='blue'"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="50px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>



                    <asp:Panel ID="pnlsub" runat="server">

                        <div class="row">

                            <fieldset class="scheduler-border fieldset_B">

                                <div class="form-horizontal">

                                    <div class="form-group">

                                        <asp:Label ID="lblacccode2" runat="server" Font-Bold="True"
                                            Font-Names="Verdana" Font-Size="16px" Text="Resource Entry Screen"></asp:Label>


                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px ">
                                            <asp:Label ID="lblacccode" runat="server" CssClass="lblTxt lblName">Accounts Code</asp:Label>
                                            <asp:TextBox ID="txtActcode" runat="server" CssClass=" form-control inputTxt" Width="450px" ReadOnly="true"></asp:TextBox>

                                        </div>


                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                                        <div class="col-md-1  pull-right">
                                             <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkSubmit_Click">Home</asp:LinkButton>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Resource Code</asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImageButton2" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImageButton2_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg01" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                            </div>


                                        </div>

                                    </div>


                                </div>


                            </fieldset>



                            <asp:GridView ID="dgv3" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging"
                                ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Card No"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                Width="70px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    

                                   
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True" CssClass="btn btn-danger  primarygrdBtn"
                                               OnClick="lnkbtnUpdateRes_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="gvlnkFTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="gvlnkFTotal_Click">Total 
                                                                    :</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr. Amount" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" style="text-align:right"
                                                 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                Font-Bold="True" 
                                                BorderColor="Transparent" BorderStyle="None" 
                                                Width="116px" ReadOnly="True" style="text-align:right">></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True"  HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount" >
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" 
                                                
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" style="text-align:right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                               
                                                BorderStyle="None"
                                                Width="106px" style="text-align:right">></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>





                        </div>




                    </asp:Panel>




                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

