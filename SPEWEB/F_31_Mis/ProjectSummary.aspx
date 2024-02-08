<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="ProjectSummary.aspx.cs" Inherits="SPEWEB.F_31_Mis.ProjectSummary" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 12px;
	    font-weight: normal;
	margin-right: 0px;
   text-align: left;
}
        .style20
        {
            width: 82px;
            height: 23px;
        }
        .style21
        {
            width: 81px;
            height: 23px;
        }
        .style23
        {
            height: 23px;
        }
        .style24
        {
            height: 23px;
            width: 656px;
        }
        .style25
        {
            height: 23px;
            width: 10px;
        }
        .style26
        {
            width: 475px;
            height: 17px;
        }
        .style27
        {
            height: 17px;
        }
        .style28
        {
            height: 17px;
            width: 213px;
        }
 
      
        .style34
        {
            height: 23px;
            width: 85px;
        }
        .style35
        {
            height: 23px;
            width: 12px;
        }
        
      
        </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



 <table style="width: 99%;">
        <tr>
            <td class="style26">
                <asp:Label ID="lblHeadtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="PROJECT SUMMARY" Width="523px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style28">
                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style27">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td class="style27">
                &nbsp;</td>
            <td class="style27">
                </td>
        </tr>
        
        </table>
                
                

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td colspan="10">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style20">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                                            style="text-align: left; color: #FFFFFF;" Text="Project Name:" 
                                            Width="80px" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="txtboxformat" 
                                            Font-Bold="True" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:ImageButton ID="imgbtnFindProject" runat="server" Height="17px" 
                                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                                            onclick="imgbtnFindProject_Click" />
                                    </td>
                                    <td class="style24" colspan="9">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Width="300px" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                    <td class="style23">
                                        </td>
                                </tr>
                              
                             
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="10">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
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
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            
                    </td>
                </tr>
                <tr>
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

