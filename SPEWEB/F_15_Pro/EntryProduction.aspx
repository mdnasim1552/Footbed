<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryProduction.aspx.cs" Inherits="SPEWEB.F_15_Pro.EntryProduction" %>

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
                                <asp:Panel ID="Panel1" runat="server">

                                    <asp:Panel ID="Panel2" runat="server">

                                        <div class="form-group">
                                            <div class="col-md-8 asitCol8 pading5px">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Production ID :"></asp:Label>

                                                <asp:TextBox ID="lblProdID" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                                <asp:Label ID="Label4" runat="server" CssClass=" smLbl" Text="Ref. No :"></asp:Label>

                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                                <asp:Label ID="Label9" runat="server" CssClass="  smLbl_to" Text="Date:"></asp:Label>

                                                <asp:TextBox ID="txtDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="ClndrExt1" runat="server" __designer:wfdid="w38" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtDate"></cc1:CalendarExtender>

                                                <asp:Label ID="lblMessage" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-8 asitCol8 pading5px">

                                                <asp:TextBox ID="txtPrevSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                                <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="btn btn-primary primaryBtn lblTxt lblName" OnClick="lbtnPrevList_Click" TabIndex="8">Previous List : </asp:LinkButton>

                                                <asp:DropDownList ID="DDLPrevIDList" runat="server" CssClass="ddlPage" TabIndex="18" Width="300px"></asp:DropDownList>

                                            </div>
                                        </div>

                                    </asp:Panel>


                                </asp:Panel>


                                <div class="form-group">
                                    <div class="col-md-7 asitCol7 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Master LC No :"></asp:Label>

                                        <asp:DropDownList ID="DDLMasterLC" runat="server" CssClass=" ddlPage" Width="300px" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="DDLMasterLC_SelectedIndexChanged"></asp:DropDownList>

                                        <asp:Label ID="lblMLC" runat="server" CssClass="ddlPage" Visible="False" Style="width: 300px;"></asp:Label>

                                        <asp:LinkButton ID="lbtnProOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnProOk_Click" TabIndex="8">Ok</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-7 asitCol7 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Order No :"></asp:Label>

                                        <asp:DropDownList ID="DDLOrder" runat="server" CssClass=" ddlPage" Width="350px" TabIndex="2"></asp:DropDownList>
                                        <asp:Label ID="lblOrder" runat="server" CssClass="ddlPage" Visible="False" Width="350px"></asp:Label>

                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-7 asitCol7 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Line :"></asp:Label>

                                        <asp:DropDownList ID="ddlLine" runat="server" CssClass=" ddlPage" Width="350px" TabIndex="2"></asp:DropDownList>
                                        <asp:Label ID="lblline" runat="server" CssClass="ddlPage" Visible="False" Width="350px"></asp:Label>


                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class=" col-md-6">
                            <asp:Label ID="lblbalinformation" runat="server" CssClass="lblTxt lblName" Text="Production In Hand" Width="122px" Visible="False"></asp:Label>

                            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True"
                                Style="text-align: left" Width="514px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" __designer:dtid="281483566645405" __designer:wfdid="w8"
                                                Style="font-size: 10px; text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorID" runat="server" __designer:dtid="281483566645405" __designer:wfdid="w9"
                                                Style="font-size: 10px; text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Style">
                                                                             <ItemTemplate>
                                            <asp:Label ID="lblgvStyleDesc" runat="server" __designer:wfdid="w2" Style="font-size: 10px; text-transform: capitalize; text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleDes")) %>'
                                                Width="136px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color">
                                        <FooterTemplate>
                                            <asp:Label ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                              >Total</asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorDesc" runat="server" __designer:dtid="281483566645405" __designer:wfdid="w13"
                                                Style="font-size: 10px; text-transform: capitalize; text-align: right" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Desc1")) %>'
                                                Width="84px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit1" runat="server" __designer:dtid="281483566645405" __designer:wfdid="w12"
                                                Style="font-size: 10px; text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unit1")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201001" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w7" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201001")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201002" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201002")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201003" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201003")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201004" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201004")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201005" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201005")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201006" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201006")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201007" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w35" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201007")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201008" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w82" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201008")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201009" runat="server" __designer:dtid="281483566645346"
                                                __designer:wfdid="w7" BackColor="Transparent" BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201009")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201010" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201010")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201011" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201011")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201012" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201012")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201013" runat="server" BackColor="Transparent" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201013")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201014" runat="server" BackColor="Transparent" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201014")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7201015" runat="server" BackColor="Transparent" BorderStyle="None"
                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "F7201015")).ToString("###0;(###0); ") %>'
                                                Width="40px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotal1" runat="server" __designer:dtid="281483566645416" __designer:wfdid="w8"
                                                Style="font-size: 11px; text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Total1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="60px" />
                                        <FooterStyle HorizontalAlign="Right" Font-Size="11px" ForeColor="White" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#333333" />
                                <PagerStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                    Height="20px" HorizontalAlign="Center" />
                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                            </asp:GridView>
                        </div>
                        <div class=" col-md-6">

                            <asp:Panel ID="ProBal" runat="server" Visible="false">
                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Visible="False" Text="Production Balance" Width="227px"></asp:Label>

                                <asp:GridView ID="gvBalPro" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvBalPro_PageIndexChanging"
                                    ShowFooter="True" Style="text-align: left">
                                    <PagerSettings Position="Top" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyle" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColor1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                    Width="84px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="S">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProqty1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProQty1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101001")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="M">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProqty2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProQty2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101002")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProqty3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProQty3" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101003")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="XL">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProqty4" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProQty4" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101004")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="XXL">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFProqty5" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProQty5" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101005")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Total Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotal1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                    Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotal1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Total1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>


                            </asp:Panel>

                        </div>


                    </div>

                    <asp:GridView ID="gvBalpro02" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Style">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStyle" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                        Width="110px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                        Width="84px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvunit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="S">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty1" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty1" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101001")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="M">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty2" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty2" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101002")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="L">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty3" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty3" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101003")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="XL">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty4" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty4" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101004")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="XXL">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFProqty5" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvProQty5" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "f720100101005")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total">
                                <FooterTemplate>
                                    <asp:Label ID="lblgvFbalqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                        Width="40px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbalqty" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                        </Columns>
                        <%--<Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvStylebal" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunitbal" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>--%>
                        <FooterStyle CssClass="grvFooter" />
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

