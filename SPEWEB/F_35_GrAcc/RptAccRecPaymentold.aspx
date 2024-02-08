<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccRecPaymentold.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.RptAccRecPaymentold" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            height: 32px;
        }
        .style10
        {
            width: 17px;
            height: 32px;
        }
        .style11
        {
            width: 42px;
            height: 32px;
        }
        .style12
        {
            width: 56px;
        }
        .style13
        {
            width: 90px;
            height: 32px;
        }
        .style14
        {
            width: 25px;
            height: 32px;
        }
        
                        
        .style43
        {
            width: 157px;
        }
        .StyleCheckBoxList
        {
            text-transform: capitalize;
            margin-bottom: 0px;
        }
        
        .style56
        {
            width: 310px;
        }
        
        .style57
    {
        width: 246px;
            height: 32px;
        }
        
        .style58
    {
        width: 212px;
    }
        
        .style59
        {
            width: 56px;
            height: 32px;
        }
        .style60
        {
            height: 32px;
        }
        
        .style61
        {
            width: 551px;
        }
        .style62
        {
            width: 85px;
        }
        .style63
        {
            width: 82px;
        }
        .style64
        {
            width: 14px;
        }
        
        </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">

    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

    });
    function pageLoaded() {

        var gv1 = $('#<%=this.grvBankDet.ClientID %>');
        var gvGrpRP = $('#<%=this.gvGrpRP.ClientID %>');
        var gvGrpBB = $('#<%=this.gvGrpBB.ClientID %>');
        var gvGrpTB = $('#<%=this.gvGprTB.ClientID %>');
        var grvCashFlow = $('#<%=this.grvCashFlow.ClientID %>');
        var gvGrpIVsC = $('#<%=this.gvGrpIVsC.ClientID %>');
        
        
        gv1.Scrollable();
        gvGrpRP.Scrollable();
        gvGrpBB.Scrollable();
        gvGrpTB.Scrollable();
        grvCashFlow.Scrollable();
        gvGrpIVsC.Scrollable();
    }
       </script>
    
    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="EMPLOYEE INFORMATION" Width="667px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style58">
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
                    <td colspan="12">
                      
                                 <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                     BorderWidth="1px">
                               <table style="width:100%;">
                                   <tr>
                                       <td class="style8">
                                           </td>
                                       <td align="right" class="style11">
                                           <asp:Label ID="lblDatefrom" runat="server" Text=" From:" 
                                               Width="80px" Font-Bold="True" Font-Size="12px" ForeColor="White"></asp:Label>
                                       </td>
                                       <td class="style10">
                                           <asp:TextBox ID="txtDateFrom" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                           <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" 
                                               Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateFrom">
                                           </cc1:CalendarExtender>
                                       </td>
                                       <td class="style59">
                                           <asp:Label ID="lblDateTo" runat="server" Text="To:" 
                                               Width="120px" BorderStyle="None" Font-Bold="True" Font-Size="12px" 
                                               ForeColor="White" style="text-align: right"></asp:Label>
                                       </td>
                                       <td class="style13">
                                           <asp:TextBox ID="txtDateto" runat="server" BorderStyle="None" Width="80px"></asp:TextBox>
                                           <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" 
                                               Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateto">
                                           </cc1:CalendarExtender>
                                       </td>
                                       <td class="style14">
                                           <asp:Label ID="lblGroup" runat="server" Font-Bold="True" Font-Size="12px" 
                                               ForeColor="White" style="text-align: right" Text="Group:" Width="50px"></asp:Label>
                                       </td>
                                       <td class="style57">
                                           <asp:CheckBoxList ID="chkListGroup" runat="server" AppendDataBoundItems="True"
                                               BackColor="#BBBB99" BorderColor="#FFCC00"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="14px" 
                                              RepeatDirection="Horizontal" Width="250px" >
                                           
                                               <asp:ListItem>Main</asp:ListItem>
                                               <asp:ListItem >Sub-1</asp:ListItem>
                                               <asp:ListItem>Sub-2</asp:ListItem>
                                               <asp:ListItem Selected="True">Details</asp:ListItem>
                                           </asp:CheckBoxList>
                                       </td>
                                       <td class="style60">
                                           <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                               BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                               Font-Size="12px" Height="16px" onclick="lbtnShow_Click" 
                                               style="text-align: center; color: #FFFFFF;">Show</asp:LinkButton>
                                       </td>
                                       <td class="style60">
                                           <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                               <ProgressTemplate>
                                                   <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                                       BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="18px" 
                                                       ForeColor="Yellow" style="text-align:center" Text="Please wait . . . . . . ." 
                                                       Width="218px"></asp:Label>
                                               </ProgressTemplate>
                                           </asp:UpdateProgress>
                                       </td>
                                       <td class="style60">
                                           </td>
                                       <td class="style60">
                                           </td>
                                       <td class="style60">
                                           </td>
                                   </tr>
                                   <tr>
                                       <td class="style8">
                                           &nbsp;</td>
                                       <td align="right" class="style11">
                                           <asp:Label ID="lblDateOpening" runat="server" Font-Bold="True" Font-Size="12px" 
                                               ForeColor="White" Text=" Opening:" Width="80px" Visible="false"></asp:Label>
                                       </td>
                                       <td class="style10">
                                           <asp:TextBox ID="txtDateOpening" runat="server" BorderStyle="None" Width="80px" Visible="false"></asp:TextBox>
                                           <cc1:CalendarExtender ID="txtDateOpening_CalendarExtender" runat="server" 
                                               Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDateOpening">
                                           </cc1:CalendarExtender>
                                       </td>
                                       <td class="style59">
                                           &nbsp;</td>
                                       <td class="style13">
                                           &nbsp;</td>
                                       <td class="style14">
                                           &nbsp;</td>
                                       <td class="style57">
                                           &nbsp;</td>
                                       <td class="style60">
                                           &nbsp;</td>
                                       <td class="style60">
                                           &nbsp;</td>
                                       <td class="style60">
                                           &nbsp;</td>
                                       <td class="style60">
                                           &nbsp;</td>
                                       <td class="style60">
                                           &nbsp;</td>
                                   </tr>
                               </table>
                                </asp:Panel>
                       
                       
                    </td>
                </tr>

                  <tr>
                    <td colspan="12">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style56">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px" 
                                            ForeColor="Yellow" 
                                            style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                            Text="Company Name:"></asp:Label>
                                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" 
                                            BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                            oncheckedchanged="chkall_CheckedChanged" Text="Check All" Width="80px" />
                                    </td>
                                    <td class="style56">
                                        <asp:CheckBox ID="chkConsolidate" runat="server" AutoPostBack="True" 
                                            BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                            oncheckedchanged="chkConsolidate_CheckedChanged" Text="With Consolidate" 
                                            Width="120px" />
                                    </td>
                                    <td class="style43">
                                        <asp:Label ID="lbltakaInLac" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" Width="160px"></asp:Label>
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
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="13">
                                        <asp:CheckBoxList ID="cblCompany" runat="server" BorderColor="#FFCC00" 
                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="0" 
                                            CssClass="StyleCheckBoxList" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Height="12px" RepeatColumns="6" Width="1000px" 
                                            RepeatDirection="Horizontal">
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
                                </tr>
                            </table>
                        </asp:Panel>
                      </td>
                    </tr>
                <tr>
                    <td colspan="12">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewRecAndPayment" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                       
                                       
                                             <asp:GridView ID="gvGrpRP" runat="server" AutoGenerateColumns="False" 
                                                 onrowdatabound="gvGrpRP_RowDataBound" ShowFooter="True">
                                                 <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl.No."><ItemTemplate><asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" /><ItemStyle Font-Size="12px" /></asp:TemplateField>
                                                  
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts"><ItemTemplate><asp:HyperLink ID="HLgvDesc" runat="server" __designer:wfdid="w38" 
                                                                 Font-Size="12px" Font-Underline="False" Target="_blank" 
                                                           Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc"))  + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  %>'     Width="350px"> 

 

                                                                
                                                               </asp:HyperLink></ItemTemplate><HeaderStyle HorizontalAlign="Left" /><ItemStyle HorizontalAlign="left" /><FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                             HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                         ItemStyle-HorizontalAlign="Right">                                                         
                                                         <ItemTemplate><asp:Label ID="lblgvtotamt" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                         ItemStyle-HorizontalAlign="Right">
                                                        
                                                         
                                                         <ItemTemplate><asp:Label ID="lblgvamt01" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02"  ItemStyle-HorizontalAlign="Right">
                                                        
                                                       
                                                         
                                                         <ItemTemplate><asp:Label ID="lblgvamt02" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03"  ItemStyle-HorizontalAlign="Right">
                                                     <ItemTemplate><asp:Label ID="lblgvamt03" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04"  ItemStyle-HorizontalAlign="Right">
                                                         
                                                    <ItemTemplate><asp:Label ID="lblgvamt04" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05"    ItemStyle-HorizontalAlign="Right">
                                                      
                                                        <ItemTemplate><asp:Label ID="lblgvamt05" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" />
                                                                 
                                                                 </asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                         ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate><asp:Label ID="lblgvamt06" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                         ItemStyle-HorizontalAlign="Right">
                                                         
                                                      <ItemTemplate><asp:Label ID="lblgvamt07" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                     <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                         FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                         ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate><asp:Label ID="lblgvamt08" runat="server" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="80px"></asp:Label></ItemTemplate><FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /></asp:TemplateField>
                                                 </Columns>
                                                 <FooterStyle BackColor="#333333" />
                                                 <PagerSettings Position="Top" />
                                                 <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                 <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                     ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                 <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                 <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                             </asp:GridView>
                                       
                                       
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlbStatus" runat="server" Visible="False">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" 
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                ForeColor="Yellow" Text="Bank Status:" Width="120px"></asp:Label>
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
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="12">
                                                            <asp:GridView ID="gvGrpRPBS" runat="server" AutoGenerateColumns="False" 
                                                                ShowFooter="True">
                                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblserialnoid2" runat="server" style="text-align: right" 
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                        <ItemStyle Font-Size="12px" />
                                                                    </asp:TemplateField>

                                                                  
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Description ">
                                                                        <ItemTemplate>
                                                                           <asp:Label ID="lblgvgrpdesc" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))%>' 
                                                                                Width="350px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                            HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvtotamtbs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt01bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt02bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt03bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt04bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt05bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt06bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt07bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                        FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                        ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt08bs" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                        <ItemStyle HorizontalAlign="Right" />
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
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                                     <asp:View ID="ViewSchedule" runat="server">
                                         <table style="width:100%;">

                                           <tr>
                    <td>
                        <asp:Panel ID="Panelschedule" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style62">
                                        <asp:Label ID="lblAcccode" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" 
                                            style="text-align: right; margin-left: 0px; margin-bottom: 0px" 
                                            Text="Accounts Code:" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style63">
                                        <asp:TextBox ID="txtScrchAccCode" runat="server" BorderStyle="None" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style64">
                                        <asp:ImageButton ID="ibtnFindAccCode" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindAccCode_Click" />
                                    </td>
                                    <td class="style61">
                                        <asp:DropDownList ID="ddlAccHead" runat="server" Width="400px">
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

                                                   
                                                         
                                                  
                                                     <asp:GridView ID="gvGrpCS" runat="server" AutoGenerateColumns="False" 
                                                         ShowFooter="True">
                                                         <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                         <Columns>
                                                             <asp:TemplateField HeaderText="Sl.No.">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblserialnoid1" runat="server" style="text-align: right" 
                                                                         Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                 <ItemStyle Font-Size="12px" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" FooterText="Total" 
                                                                 HeaderText="Description of Accounts">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvaccdesccs" runat="server" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                         Width="200px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <HeaderStyle HorizontalAlign="Left" />
                                                                 <ItemStyle HorizontalAlign="left" />
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                     HorizontalAlign="Left" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblftotamtcs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvtotamtcs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt01cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt01cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt02cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt02cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt03cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt03cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt04cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt04cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt05cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt05cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt06cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt06cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt07cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt07cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <FooterTemplate>
                                                                     <asp:Label ID="lblfamt08cs" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                 </FooterTemplate>
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="lblgvamt08cs" runat="server" 
                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                         Width="80px"></asp:Label>
                                                                 </ItemTemplate>
                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                 <ItemStyle HorizontalAlign="Right" />
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

                                                   
                                                         
                                                  
                                                 </td>
                                             </tr>
                                         </table>
                                         </asp:View>

                                         <asp:View ID="ViewTrialbalance" runat="server">
                                         <table style="width:100%;">

                                             <tr>
                                                 <td>

                                                    
                                                         <asp:GridView ID="gvGprTB" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True" >
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoid0" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total Dr.&lt;br&gt; Totol Cr.">
                                                                     <ItemTemplate>
                                                                              <asp:Label ID="lblgvaccdesc" runat="server" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                             Width="200px"></asp:Label>                                                          
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotamtbb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt01" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt01" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt01bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt02" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt02" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt02bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt03" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt03" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt03bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt04" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt04" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt04bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt05" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt05" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt05bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt06" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt06" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt06bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right"   />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt07" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt07" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt07bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <table style="width:100%;">
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotdramt08" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                             <tr>
                                                                                 <td>
                                                                                     &nbsp;</td>
                                                                                 <td>
                                                                                     <asp:Label ID="lblftotcramt08" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                         ForeColor="White" style="text-align: right" Width="80px"></asp:Label>
                                                                                 </td>
                                                                             </tr>
                                                                         </table>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt08bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
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
                                                    
                                                 </td>
                                             </tr>
                                         </table>
                                         </asp:View>
                                         
                                         <asp:View ID="ViewInComeStatement" runat="server">
                                         <table>
                                            <tr>
                                                <td>
                                                    
                                                   
                                                         <asp:GridView ID="gvIncomeSt" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True" Width="616px" onrowdatabound="gvIncomeSt_RowDataBound">
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoidIS" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbgraccodIS" runat="server" Font-Size="12px" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                         Width="70px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" /></asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total">
                                                                     <ItemTemplate>
                                                                              <asp:Label ID="lblgvaccdescIS" runat="server" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                             Width="200px"></asp:Label>                                                          
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotamtIS" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotamtIS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt01IS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt01IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt02IS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt02IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt03IS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt03IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt04IS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt04IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt05IS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt05IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt06IS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt06IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right"   />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt07IS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt07IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt08IS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt08IS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
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
                                                
                                                </td>
                                            </tr>
                                         </table>
                                           </asp:View>
                                            <asp:View ID="ViewBalanceSheet" runat="server">
                                         <table>
                                            <tr>
                                                <td>
                                                    
                                                     <asp:Panel ID="Panel4" runat="server" ScrollBars="Horizontal" Width="1000px">
                                                         <asp:GridView ID="gvBalSheet" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True" Width="616px" onrowdatabound="gvBalSheet_RowDataBound">
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoidBS" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbgraccodBS" runat="server" Font-Size="12px" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                         Width="100px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" /></asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total">
                                                                     <ItemTemplate>
                                                                              <asp:Label ID="lblgvaccdescBS" runat="server" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                             Width="200px"></asp:Label>                                                          
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotamtBS" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotamtBS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt01BS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt01BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt02BS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt02BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt03BS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt03BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt04BS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt04BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt05BS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt05BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt06BS" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt06BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right"   />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt07BS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt07BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt08BS" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt08BS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
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
                                           </asp:View>
                                            <asp:View ID="ViewBankDetails" runat="server">
                                         <table>
                                            <tr>
                                                <td>
                                                    
                                                   <%--  <asp:Panel ID="Panel5" runat="server" ScrollBars="Horizontal" Width="1000px">--%>
                                                         <asp:GridView ID="grvBankDet" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True" Width="616px">
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl No">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoidBD" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbgraccodBD" runat="server" Font-Size="12px" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                         Width="100px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" /></asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total">
                                                                     <ItemTemplate>
                                                                              <asp:Label ID="lblgvaccdescBD" runat="server" 
                                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                                             Width="200px"></asp:Label>                                                          
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Bank Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotbamtBD" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotbamtBS" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totbamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Liabilities Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotliamtBS" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotliamtBD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totliamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total available Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotavamtBD" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotavamtBD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totavamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Bamt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfbamt01BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvbamt01BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Liamt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfliamt01BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvliamt01BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liamt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Avamt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfavamt01BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvavamt01BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avamt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 

                                                                  <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Aamt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfbamt02BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvbamt02BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Liamt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfliamt02BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvliamt02BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liamt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Avamt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfavamt02BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvavamt02BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avamt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>

                                                                  <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Bamt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfbamt03BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvbamt03BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Liamt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfliamt03BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvliamt03BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liamt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Avamt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfavamt03BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvavamt03BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avamt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>

                                                                  <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Bamt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfbamt04BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvbamt04BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Liamt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfliamt04BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvliamt04BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liamt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Avamt -04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfavamt04BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvavamt04BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avamt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>

                                                                  <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Bamt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfbamt05BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvbamt05BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Liamt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfliamt05BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvliamt05BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "liamt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Avamt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfavamt05BD" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvavamt05BD" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avamt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
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
                                                    <%-- </asp:Panel>--%>
                                                </td>
                                            </tr>
                                         </table>
                                           </asp:View>
                                            <asp:View ID="ViewCashFlow" runat="server">
                                         <table>
                                            <tr>
                                                <td>
                                                    
                                                  
                                                         <asp:GridView ID="grvCashFlow" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True"  onrowdatabound="grvCashFlow_RowDataBound">
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoidBS" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbgraccodBS" runat="server" Font-Size="12px" 
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                         Width="100px"></asp:Label></ItemTemplate><HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" /></asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvDesc" runat="server" 
                                                                             Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc1").ToString().Trim().Length>0 ? 
                                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim(): "") + "</B>"  + 
                                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")
                                                                                    %>'   Width="370px"></asp:Label>
                                                                    </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotamtCF" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotamtCF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt01CF" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt01CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt02CF" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt02CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt03CF" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt03CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt04CF" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt04CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt05CF" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt05CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt06CF" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt06CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right"   />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt07CF" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt07CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt08CF" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt08CF" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
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
                                                    
                                                </td>
                                            </tr>
                                             <tr>
                                                 <td>
                                                     <asp:Panel ID="PnlbStatuscf" runat="server" Visible="False">
                                                         <table style="width:100%;">
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
                                                                 <td>
                                                                     &nbsp;</td>
                                                                 <td>
                                                                     &nbsp;</td>
                                                             </tr>
                                                             <tr>
                                                                 <td colspan="12">
                                                                     <asp:GridView ID="gvGrpCFBS" runat="server" AutoGenerateColumns="False" 
                                                                         ShowFooter="True">
                                                                         <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                         <Columns>
                                                                             <asp:TemplateField HeaderText="Sl.No.">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblserialnoid3" runat="server" style="text-align: right" 
                                                                                         Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                                 <ItemStyle Font-Size="12px" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Description ">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvgrpdesccf" runat="server" 
                                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc"))%>' 
                                                                                         Width="350px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <HeaderStyle HorizontalAlign="Left" />
                                                                                 <ItemStyle HorizontalAlign="left" />
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                                     HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvtotamtbscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt01bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt02bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt03bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt04bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt05bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt06bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt07bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                                 FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                                 ItemStyle-HorizontalAlign="Right">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblgvamt08bscf" runat="server" 
                                                                                         Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                                         Width="80px"></asp:Label>
                                                                                 </ItemTemplate>
                                                                                 <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                                 <ItemStyle HorizontalAlign="Right" />
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
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </asp:Panel>
                                                 </td>
                                             </tr>
                                         </table>
                                           </asp:View>

                                           <asp:View ID="ViewBankBal" runat="server">
                                               <table style="width:100%;">
                                                   <tr>
                                                       <td colspan="12">
                                                           <asp:GridView ID="gvGrpBB" runat="server" AutoGenerateColumns="False" 
                                                             ShowFooter="True" onrowdatabound="gvGrpBB_RowDataBound" >
                                                             <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                             <Columns>
                                                                 <asp:TemplateField HeaderText="Sl.No.">
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblserialnoid0" runat="server" style="text-align: right" 
                                                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                     <ItemStyle Font-Size="12px" />
                                                                 </asp:TemplateField>
                                                                
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts" 
                                                                     FooterText="Total">
                                                                     <ItemTemplate>
                                                                              <asp:Label ID="lblgvaccdesc" runat="server" 
                                                                             
                                                                             
                                                                             Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'   Width="300px">
                                                                             
                                                                             
                                                                             </asp:Label>                                                          
                                                                     </ItemTemplate>
                                                                     <HeaderStyle HorizontalAlign="Left" />
                                                                     <ItemStyle HorizontalAlign="left" />
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                         HorizontalAlign="Left" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblftotamtbb" runat="server" Font-Bold="True" Width="80px" 
                                                                             Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvtotamtbb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt01bb" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt01bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt02bb" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt02bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt03bb" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt03bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt04bb" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt04bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt05bb" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt05bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt06bb" runat="server" Font-Bold="True" Width="80px"  Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt06bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right"   />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt07bb" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt07bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                                 <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                                     FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                                     ItemStyle-HorizontalAlign="Right">
                                                                     <FooterTemplate>
                                                                         <asp:Label ID="lblfamt08bb" runat="server" Font-Bold="True" Width="80px"   Font-Size="12px" ForeColor="White" style="text-align: right"></asp:Label>
                                                                     </FooterTemplate>
                                                                     <ItemTemplate>
                                                                         <asp:Label ID="lblgvamt08bb" runat="server" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                             Width="80px"></asp:Label>
                                                                     </ItemTemplate>
                                                                     <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                                     <ItemStyle HorizontalAlign="Right" />
                                                                 </asp:TemplateField>
                                                             </Columns>
                                                             <FooterStyle BackColor="#333333" />
                                                             <PagerSettings Position="Top" />
                                                             <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                             <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                             <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                             <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                                         </asp:GridView></td>
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
                                            </asp:View>
                                             <asp:View ID="ViewIssuVsCollection" runat="server">
                                                 <asp:GridView ID="gvGrpIVsC" runat="server" AutoGenerateColumns="False" 
                                                     onrowdatabound="gvGrpIVsC_RowDataBound" ShowFooter="True">
                                                     <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                     <Columns>
                                                         <asp:TemplateField HeaderText="Sl.No.">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblserialnoid4" runat="server" style="text-align: right" 
                                                                     Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                             </ItemTemplate>
                                                             <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                             <ItemStyle Font-Size="12px" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                                             <ItemTemplate>
                                                                 <asp:HyperLink ID="HLgvDescIVsC" runat="server" __designer:wfdid="w38" 
                                                                     Font-Size="12px" Font-Underline="False" Target="_blank" 
                                                                     Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc"))  + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "")  %>' 
                                                                     Width="350px"> 

 

                                                                
                                                               </asp:HyperLink>
                                                             </ItemTemplate>
                                                             <HeaderStyle HorizontalAlign="Left" />
                                                             <ItemStyle HorizontalAlign="left" />
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                 HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Total Amt" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvtotamtIVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-01" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt01IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt01")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-02" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt02IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt02")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-03" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt03IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt03")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-04" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt04IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt04")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-05" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt05IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt05")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-06" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt06IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt06")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-07" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt07IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt07")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
                                                         </asp:TemplateField>
                                                         <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                                             FooterStyle-HorizontalAlign="Right" HeaderText="Amt-08" 
                                                             ItemStyle-HorizontalAlign="Right">
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lblgvamt08IVsC" runat="server" 
                                                                     Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt08")).ToString("#,##0;(#,##0); ") %>' 
                                                                     Width="80px"></asp:Label>
                                                             </ItemTemplate>
                                                             <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                             <ItemStyle HorizontalAlign="Right" />
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
                                              </asp:View>




                        </asp:MultiView>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

