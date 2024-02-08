﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccTranSearch.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccTranSearch" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style40
        {
            color: #FFFFFF;
            }
        .style41
        {
            width: 630px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
     <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
     <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="14px" 
                    ForeColor="Yellow" Text="TRANSACTION SEARCH" Width="513px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style45">
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
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button" 
                    ForeColor="White">PRINT</asp:LinkButton>
                                                </td>
        </tr>
        </table>
              
                
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style41">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px" 
                                                            ForeColor="Yellow" 
                                                            style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                            Text="Field Information:"></asp:Label>
                                                        <asp:CheckBox ID="chkallTransList" runat="server" AutoPostBack="True" 
                                                            BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                            oncheckedchanged="chkallTransList_CheckedChanged" Text="Check All" 
                                                            Width="80px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label22" runat="server" CssClass="style40" Font-Bold="True" 
                                                            Height="16px" Style="text-align: left" Text="From:" Width="50px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None" 
                                                            Font-Bold="True" Height="16px" Width="87px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style83">
                                                        <asp:Label ID="Label23" runat="server" CssClass="style40" Font-Bold="True" 
                                                            Height="16px" Style="text-align: right" Text="To:" Width="40px"></asp:Label>
                                                    </td>
                                                    <td class="style83">
                                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True" 
                                                            Height="16px" Width="87px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        <asp:CheckBox ID="chkneNar" runat="server" BackColor="#000066" 
                                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                            Font-Size="12px" ForeColor="Yellow" TabIndex="10" 
                                                            Text="Without Narration &amp; Total" Width="180px" />
                                                    </td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                    <td class="style83">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style83" colspan="65">
                                                        <asp:CheckBoxList ID="cblTransList" runat="server" BorderColor="#FFCC00" 
                                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="0" 
                                                            CssClass="StyleCheckBoxList" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" Height="16px" RepeatColumns="12" Width="1067px" 
                                                            onselectedindexchanged="cblTransList_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem>aa</asp:ListItem>
                                                            <asp:ListItem>bb</asp:ListItem>
                                                            <asp:ListItem>cc</asp:ListItem>
                                                            <asp:ListItem>dd</asp:ListItem>
                                                            <asp:ListItem>ee</asp:ListItem>
                                                            <asp:ListItem>ff</asp:ListItem>
                                                            <asp:ListItem>gg</asp:ListItem>
                                                            <asp:ListItem>hh</asp:ListItem>
                                                            <asp:ListItem>mm</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td class="style43" valign="bottom">
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
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px" Width="1074px" >
                                              <table style="width: 87%;">
                                                  <tr>
                                                      <td class="style110">
                                                          <asp:Label ID="lblSearchlist" runat="server" Font-Bold="True" Font-Size="14px" 
                                                              ForeColor="Yellow" 
                                                              style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                              Text="Search List:" Width="80px"></asp:Label>
                                                      </td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList1" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch1" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch1" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          <asp:DropDownList ID="ddlOperator1" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="60px">
                                                              <asp:ListItem Value="and">And</asp:ListItem>
                                                              <asp:ListItem Value="or">Or</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style46" rowspan="3">
                                                          <asp:Panel ID="Panel5" runat="server">
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td class="style114">
                                                                          <asp:Label ID="lblOrderList" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                              ForeColor="Yellow" 
                                                                              style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                                              Text="Order Field:" Width="80px"></asp:Label>
                                                                      </td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder1" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad1" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          <asp:LinkButton ID="lbtnSearch" runat="server" BackColor="#003366" 
                                                                              BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                              Font-Size="12px" Height="16px" onclick="lbtnSearch_Click" 
                                                                              style="text-align: center; color: #FFFFFF; width: 19px;">Ok</asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                              <ProgressTemplate>
                                                                                  <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                                                                      BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                                                      ForeColor="Yellow" style="text-align: left" Text="Please wait . . . . . . ." 
                                                                                      Width="120px"></asp:Label>
                                                                              </ProgressTemplate>
                                                                          </asp:UpdateProgress>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td class="style114">
                                                                          &nbsp;</td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder2" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad2" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          &nbsp;</td>
                                                                      <td>
                                                                          &nbsp;</td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td class="style114">
                                                                          &nbsp;</td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder3" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad3" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          &nbsp;</td>
                                                                      <td>
                                                                          &nbsp;</td>
                                                                  </tr>
                                                              </table>
                                                          </asp:Panel>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          &nbsp;</td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList2" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch2" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch2" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          <asp:DropDownList ID="ddlOperator2" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="60px">
                                                              <asp:ListItem Value="and">And</asp:ListItem>
                                                              <asp:ListItem Value="or">Or</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          &nbsp;</td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList3" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch3" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch3" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          &nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                              style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                                              Width="70px"></asp:Label>
                                                      </td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                              BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                              onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                                              Width="120px">
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
                                                      <td class="style111">
                                                          &nbsp;</td>
                                                      <td class="style112">
                                                          &nbsp;</td>
                                                      <td class="style113">
                                                          &nbsp;</td>
                                                  </tr>
                                              </table>
                                          </asp:Panel>
                                    </td>
                                </tr>
                                 
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel10" runat="server" Width="1260px">
                                            <asp:GridView ID="gvAcTransSearch" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="616px" 
                                                onrowdatabound="gvAcTransSearch_RowDataBound" AllowPaging="True" 
                                                onpageindexchanging="gvAcTransSearch_PageIndexChanging">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                

                                                  <asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVouNo" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false" Font-Underline="false" ForeColor="Black"
                                            style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>'
                                            Width="150px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                               </asp:TemplateField>


                                                






                                                    <asp:TemplateField HeaderText="Voucher Date">
                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvVouDate" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudate")).ToString("dd-MMM-yyyy") %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAcode" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDesc" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc")) %>' 
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUnit" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvVnar" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                   <asp:TemplateField HeaderText="Res Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvResamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Debit Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "debit")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Credit Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "credit")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpc3" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cheqrefno")) %>' 
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Other ref.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvotherref" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "otherref")) %>' 
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText="Voucher No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvVouNo02" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum2")) %>' 
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                            HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerSettings Position="Top" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

