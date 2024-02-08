<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="SalesDetailsSchedule.aspx.cs" Inherits="SPEWEB.F_21_GAcc.SalesDetailsSchedule" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <%--<style type="text/css">
        .style6
        {
            width: 21px;
        }
        .style7
        {
            width: 121px;
        }
        .style8
        {
        }
        .style12
        {
            width: 31px;
        }
        .style19
        {
            width: 34px;
        }
        .style20
        {
            width: 58px;
        }
        .style21
        {
        }
        .style23
        {
            width: 44px;
            }
        .style27
        {
            width: 68px;
        }
        .style28
        {
            width: 93px;
        }
        </style>--%>
    <style type="text/css">
        .style19
        {
            width: 65px;
        }
        .style20
        {
            width: 53px;
        }
        .style22
        {
            width: 228px;
        }
        .style24
        {
            width: 43px;
        }
        .style25
        {
            width: 516px;
        }
        .style26
        {
            width: 82px;
        }
        .style27
        {
            width: 145px;
        }
        .style28
        {
            width: 126px;
        }
        .style30
        {
        }
        .style31
        {
            width: 2px;
        }
        .style33
        {
            width: 92px;
        }
        .style34
        {
        }
        .style35
        {
            width: 4px;
        }
        .style36
        {
            width: 39px;
        }
        .style37
        {
            width: 19px;
        }
        .style38
        {
            width: 84px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:99%;" >
    <tr>
        <td class="style25">
                    <asp:Label ID="lblAccschedule" runat="server" Text="Sales Details Schedule" 
                        CssClass="label" Width="473px" Height="16px"></asp:Label>
                </td>
        <td class="style22">
                                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
        <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="label1" Width="60px" 
                        onclick="lnkPrint_Click">Print</asp:LinkButton>
                </td>
        <td>
                    &nbsp;</td>
    </tr>
    </table>
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width:100%;">
                        <tr>
                            <td class="style8" colspan="11">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style26">
                                                &nbsp;</td>
                                            <td class="style33">
                                                &nbsp;</td>
                                            <td class="style38">
                                                <asp:Label ID="lbldateRange" runat="server" CssClass="label2" Text="From" 
                                                    Width="65px"></asp:Label>
                                            </td>
                                            <td class="style36">
                                                <asp:TextBox ID="txtFromdat" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFromdat_CalendarExtender" runat="server" 
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFromdat">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td class="style31">
                                                <asp:Label ID="lblTo" runat="server" CssClass="label2" Text="To"></asp:Label>
                                            </td>
                                            <td class="style35">
                                                <asp:TextBox ID="txtTodat" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtTodat_CalendarExtender" runat="server" 
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtTodat">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkok" runat="server" CssClass="button" 
                                                    onclick="lnkok_Click" Width="71px">Ok</asp:LinkButton>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style26">
                                                <asp:Label ID="lblAcchead" runat="server" CssClass="label2" 
                                                    Text="Project  Head" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style33">
                                                <asp:TextBox ID="txtSearch" runat="server" BorderStyle="None"></asp:TextBox>
                                            </td>
                                            <td class="style38">
                                                <asp:ImageButton ID="imgsearch" runat="server" Height="17px" 
                                                    ImageUrl="~/Image/search-button-on.gif" onclick="imgsearch_Click" 
                                                    style="margin-left: 0px" Width="65px" />
                                            </td>
                                            <td class="style30" colspan="5">
                                                <asp:DropDownList ID="ddlAccHeads" runat="server" 
                                                    Width="354px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="Lis1" runat="server" QueryPattern="Contains" 
                                                    TargetControlID="ddlAccHeads">
                                                </cc1:ListSearchExtender>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style26">
                                                <asp:Label ID="lblreshead" runat="server" CssClass="label2" 
                                                    Text="Resource Head" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style33">
                                                <asp:TextBox ID="txtSrcRes" runat="server" BorderStyle="None"></asp:TextBox>
                                            </td>
                                            <td class="style38">
                                                <asp:ImageButton ID="imgsrcres" runat="server" Height="17px" 
                                                    ImageUrl="~/Image/search-button-on.gif" onclick="imgsrcres_Click" 
                                                    style="margin-left: 0px" Width="65px" />
                                            </td>
                                            <td class="style30" colspan="5">
                                                <asp:DropDownList ID="ddlResHead" runat="server" 
                                                    Width="354px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style26">
                                                &nbsp;</td>
                                            <td class="style33">
                                                &nbsp;</td>
                                            <td class="style38">
                                                <asp:Label ID="lblreportlevel" runat="server" CssClass="label2" 
                                                    Text="Level" Width="65px"></asp:Label>
                                            </td>
                                            <td class="style36">
                                                <asp:DropDownList ID="ddlRptlbl" runat="server" Height="18px" 
                                                    Width="105px">
                                                    <asp:ListItem>Level1</asp:ListItem>
                                                    <asp:ListItem>Level2</asp:ListItem>
                                                    <asp:ListItem>Level3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Level4</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style31">
                                                &nbsp;</td>
                                            <td class="style35">
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
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="style26">
                                                &nbsp;</td>
                                            <td class="style33">
                                                &nbsp;</td>
                                            <td class="style38">
                                                <asp:Label ID="lbldateRange1" runat="server" CssClass="label2" Text="Message" 
                                                    Width="65px"></asp:Label>
                                            </td>
                                            <td class="style34" colspan="5">
                                                <asp:Label ID="lblmsg" runat="server" BackColor="#FFECFF" BorderColor="#996633" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FF0066"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" colspan="11">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DDFFEE" ShowFooter="True" Width="911px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "subcode1").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Dr. &lt;br&gt; Cr." FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Description of Account">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "custname").ToString()+"<br />"+DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>' 
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Amt." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel" 
                                                    
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#66CCFF" />
                                    <HeaderStyle BackColor="#66CCFF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                &nbsp;</td>
                            <td class="style19">
                                &nbsp;</td>
                            <td class="style20">
                                &nbsp;</td>
                            <td class="style24">
                                &nbsp;</td>
                            <td class="style27">
                                &nbsp;</td>
                            <td class="style28">
                                &nbsp;</td>
                            <td class="style6">
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
                </ContentTemplate>
            </asp:UpdatePanel>
        
</asp:Content>

