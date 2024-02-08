<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="HREmpTransfer.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.HREmpTransfer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style15
        {
            width: 38px;
        }
        .style16
        {
        }
        .style17
        {
            width: 92px;
        }
        .style18
        {
            width: 294px;
        }
        .style19
        {
            width: 21px;
        }
        .style20
        {
            width: 26px;
        }
        .style21
        {
            width: 36px;
        }
        .style22
        {
            width: 4px;
        }
        .style23
        {
            width: 129px;
        }
        .style24
        {
            width: 103px;
        }
        .style26
        {
            width: 2px;
        }
        .style27
        {
            width: 40px;
        }
        .style29
        {
            width: 272px;
        }
        .style31
        {
            width: 70px;
        }
        .style33
        {
            width: 235px;
        }
        .style34
        {
            width: 317px;
        }
        .style35
        {
            width: 86px;
        }
        .style37
        {
            width: 143px;
        }
        .style38
        {
            width: 1571px;
        }
        .style41
        {
            width: 82px;
        }
        .style42
        {
            width: 55px;
        }
        </style>
 

    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 930px;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE TRANSFER INFORMATION" Width="500px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td>
                                                                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" 
                    onclick="lbtnPrint_Click" style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
        </table>
      
                
                

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:930px;">
                <tr>
                    <td class="style16" colspan="12">
                        <asp:Panel ID="Panel1" runat="server" Width="930px">
                         <fieldset style="border:1px solid yellow;">
                        <legend>  <asp:Label ID="Label4" runat="server" Text="Employe Transfer Information" style="color:white; font-size:14px; font-weight:bold;"></asp:Label></legend>
                            <table style=" height: 41px;">
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td class="style19">
                                        &nbsp;</td>
                                    <td class="style17">
                                        <asp:LinkButton ID="lbtnPrevTransList" runat="server" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnPrevTransList_Click" 
                                            style="text-align: right; height: 15px; color: #FFFFFF;" Width="90px">Prev. Trans List:</asp:LinkButton>
                                    </td>
                                    <td class="style18" colspan="3">
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" Font-Size="12px" 
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style24">
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
                                    <td class="style35">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        </td>
                                    <td class="style19">
                                        </td>
                                    <td class="style17">
                                        <asp:Label ID="Label7" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Transfer Date:" Width="90px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtCurTransDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" 
                                            Format="dd.MM.yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style22">
                                        <asp:Label ID="Label8" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Transfer No:" Width="85px"></asp:Label>
                                    </td>
                                    <td class="style23">
                                        <asp:Label ID="lblCurTransNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                                            Width="45px"></asp:Label>
                                        <asp:TextBox ID="txtCurTransNo2" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                            Width="45px">00001</asp:TextBox>
                                    </td>
                                    <td class="style24">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="height: 17px; width: 19px;" >Ok</asp:LinkButton>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td>
                                        </td>
                                    <td class="style35">
                                        </td>
                                    <td class="style34">
                                        </td>
                                </tr>
                            </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="12">
                        <asp:Panel ID="pnlprj" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px" Visible="False" Width="930px">
                            <table style=" height: 116px;">

                                 <tr>
                                  
                                    <td class="style15">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="text-align: right" Text="Company:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style44">
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="txtboxformat" 
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style42">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgbtnCompany" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnCompany_Click" Width="16px" />
                                    </td>
                                    <td colspan="13" align="left">
                                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="12px" 
                                            onselectedindexchanged="ddlCompany_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
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
                                </tr>
                                <tr>
                                    <td class="style26">
                                        &nbsp;</td>
                                    <td class="style27">
                                        &nbsp;</td>
                                    <td class="style42">
                                        <asp:Label ID="lblProjectFromList" runat="server" CssClass="style16" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" Height="16px" Text="From:" 
                                            Width="30px"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlprjlistfrom" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlprjlistfrom_SelectedIndexChanged" Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="14px" ForeColor="White"></asp:Label>
                                    </td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        &nbsp;</td>
                                    <td class="style27">
                                        &nbsp;</td>
                                    <td class="style42">
                                        <asp:Label ID="lblProjectFromList0" runat="server" CssClass="style16" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" Height="16px" 
                                            style="TEXT-ALIGN: right" Text="To:" Width="30px"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlprjlistto" runat="server" style="margin-left: 0px" 
                                            Width="300px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        &nbsp;</td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="TEXT-ALIGN: right" Text="Employee List:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtsrchEmp" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="right" class="style42">
                                        <asp:ImageButton ID="ibtnEmpList" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnEmpList_Click" Width="16px" />
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlEmpList" runat="server" AutoPostBack="True" 
                                            Width="300px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ListSearchExt2" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlEmpList">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkselect" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lnkselect_Click" 
                                            style="text-align: left">Select</asp:LinkButton>
                                    </td>
                                    <td class="style38">
                                        &nbsp;</td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="lblResList0" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" style="TEXT-ALIGN: right" Text="Present At Place:" 
                                            Width="90px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtpatplacedate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtpatplacedate_CalendarExtender" runat="server" 
                                            Format="dd.MM.yyyy" TargetControlID="txtpatplacedate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right" class="style42">
                                        &nbsp;</td>
                                    <td class="style29">
                                        <asp:RadioButtonList ID="rbtTrnstype" runat="server" BackColor="#BBBB99" 
                                            BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" Height="16px" RepeatColumns="6" RepeatDirection="Horizontal" 
                                            Width="156px">
                                            <asp:ListItem>Type 1</asp:ListItem>
                                            <asp:ListItem>Type 2</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style38">
                                        &nbsp;</td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="12">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" style="margin-right: 0px" Width="919px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>' 
                                            Width="50px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>' 
                                            Width="140px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkupdate" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" onclick="lnkupdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesig" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                            Width="150px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfprjdesc" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfprjdesc")) %>' 
                                            Width="150px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttprjdesc" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttprjdesc")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Present At Place">
                                    <ItemTemplate>
                                        <asp:Label ID="txtpatplace" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pplacedate")).ToString("dd-MMM-yyyy") %>' 
                                            Width="80px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" style="text-align: left" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>' 
                                            Width="100px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="style16" colspan="12">
                        <asp:Panel ID="pnlremarks" runat="server" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style31">
                                        <asp:Label ID="Label12" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                                            Text="Information of Finalcial matters:" Width="130px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfmaters" runat="server" BorderStyle="None" 
                                            TextMode="MultiLine" Width="400px" Height="45px"></asp:TextBox>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style31">
                                        <asp:Label ID="Label13" runat="server" CssClass="style16" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" style="text-align: right" 
                                            Text="Special Note:" Width="130px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtspnote" runat="server" BorderStyle="None" 
                                            TextMode="MultiLine" Width="400px" Height="45px"></asp:TextBox>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style31">
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
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        &nbsp;</td>
                    <td class="style15">
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
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

