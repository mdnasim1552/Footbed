<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptMgtInterface.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkRptMgtInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            margin-left: 0px;
        }
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
        .style48
        {
            width: 1%;
        }
        .style50
        {
            width: 10%;
        }
        .style51
        {
            width: 8%;
        }
        .style52
        {
            width: 11%;
        }
        .style53
        {
            width: 82px;
        }
        .style55
        {
            width: 150px;
        }
        .style56
        {
            width: 89px;
        }
        .style57
        {
            width: 348px;
        }
        .style58
        {
            width: 81px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
    </script>
    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="EMPLOYEE SALARY INFORMATION" Width="667px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;" Height="16px"></asp:Label>
            </td>
            <td class="style33">
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" OnClick="lbtnPrint_Click"
                    CssClass="button" ForeColor="White">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style51">
                                        <asp:Label ID="lblfrm" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="From:" Width="75px"></asp:Label>
                                    </td>
                                    <td class="style50">
                                        <asp:Label ID="lblfrmdate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style48">
                                        <asp:Label ID="lblto" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Style="text-align: right" Text="To:" Width="15px"></asp:Label>
                                    </td>
                                    <td align="left" class="style51">
                                        <asp:Label ID="lbltodate" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                    <td class="style52">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Style="text-align: left"
                                                    Text="Please wait . . . . . . ." Width="120px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style53">
                                        <asp:Label ID="lblOrderList" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;" Text="Order Field:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:DropDownList ID="ddlOrder" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style56">
                                        <asp:DropDownList ID="ddlOrderad1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Width="90px">
                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style58">
                                        <asp:Label ID="lblOrderList0" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;" Text="Lebel Field:"
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style64">
                                        <asp:DropDownList ID="ddlReportLevel" runat="server" Width="120px" Font-Bold="True"
                                            Font-Size="12px" OnSelectedIndexChanged="ddlReportLevel_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Company Wise</asp:ListItem>
                                            <asp:ListItem Value="2">Department Wise</asp:ListItem>
                                            <asp:ListItem Value="3">Section Wise</asp:ListItem>
                                            <asp:ListItem Value="4">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style57">
                                        <asp:LinkButton ID="lnkbtnShow" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="16px" OnClick="lnkbtnShow_Click" Style="text-align: center;" TabIndex="12"
                                            Width="50px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style57">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style53">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Style="color: #FFFFFF;
                                            text-align: right;" Text="Page Size:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style55">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="105px">
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
                                    <td class="style56">
                                        &nbsp;
                                    </td>
                                    <td class="style58">
                                        &nbsp;
                                    </td>
                                    <td class="style64">
                                        &nbsp;
                                    </td>
                                    <td class="style57">
                                        &nbsp;
                                    </td>
                                    <td class="style57">
                                        &nbsp;
                                    </td>
                                    <td class="style48">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvEmpList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvEmpList_PageIndexChanging" ShowFooter="True" Width="420px"
                            OnRowDataBound="gvEmpList_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDept" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesignationemp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvDes" runat="server" ForeColor="White" Font-Size="12px" Width="70px">Total:</asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Employee">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNoEmp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "noemp")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFNoEmp" runat="server" ForeColor="White" Font-Bold="true" Font-Size="12px"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnksalary" runat="server" Font-Underline="false" Target="_blank" Style="text-align: right" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFsalary" runat="server" ForeColor="White" Font-Bold="true" Font-Size="12px"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindate")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAbst" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tabst")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvLate" runat="server" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Period" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvserperiod" runat="server" Font-Underline="false" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serperiod")) %>'
                                            Width="150px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle Font-Bold="True" Font-Size="16px" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
