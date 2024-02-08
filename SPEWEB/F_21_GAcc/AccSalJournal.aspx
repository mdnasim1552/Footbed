<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccSalJournal.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccSalJournal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style18
        {
            width: 570px;
        }
        .style175
        {
            width: 239px;
        }
        .style176
        {
            width: 215px;
        }
        .style178
        {
            width: 194px;
        }
        .style190
        {
        }
        .style191
        {
            width: 101px;
        }
        .style192
        {
            width: 230px;
        }
        .style193
        {
            width: 236px;
        }
        .style195
        {
            height: 20px;
        }
        .style199
        {
        }
        .style200
        {
            width: 95px;
        }
        .style201
        {
            height: 20px;
            width: 17px;
        }
        .style202
        {
            width: 17px;
        }
        .style203
        {
            width: 13px;
        }
        .style204
        {
            width: 8px;
        }
        .style206
        {
            height: 20px;
            width: 11px;
        }
        .style207
        {
            width: 11px;
        }
        .style208
        {
            width: 99px;
        }
        .style209
        {
            width: 107px;
        }
        </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
   
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);        
       
       

        });

    });
    function pageLoaded() {
     $("input, select").bind("keydown", function (event) {
            var k1 = new KeyPress();
            k1.textBoxHandler(event);
      
    }


</script>
   
    <table style="width:100%;">
    <tr>
        <td class="style18">
                    <asp:Label ID="LblPageTitle" runat="server" CssClass="HeaderTitle" 
                        Font-Bold="True" Font-Overline="False" Font-Size="18px" Font-Underline="False" 
                        style="FONT-SIZE: 18px" 
                        Text="SALES JOURNAL INFORMATION" Width="450px"></asp:Label>
                </td>
        <td class="style209">
                                &nbsp;</td>
        <td class="style175">
                                <asp:Label ID="lblprint" runat="server" Height="18px"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
        <td>
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click" Font-Size="12px">PRINT</asp:LinkButton>
                </td>
        <td class="style175">
                    &nbsp;</td>
    </tr>
    </table>
    
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                 <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                        BorderWidth="1px">
                    <table style="width:100%; height: 20px; margin-bottom: 0px;">
                        <tr>
                            <td colspan="3" align="center" class="style195">
                                <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" 
                                    ImageUrl="~/Image/movie_26.gif" onclick="ibtnvounu_Click" Width="145px" 
                                    Visible="False" />
                            </td>
                            <td class="style195">
                                </td>
                            <td class="style206">
                                <asp:Label ID="lblDate" runat="server" CssClass="label2" Text="Voucher Date" 
                                    Width="100px"></asp:Label>
                            </td>
                            <td class="style201">
                                <asp:TextBox ID="txtdate" runat="server" BorderStyle="None" Width="80px" 
                                    TabIndex="1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style195">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="button" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                    style="text-align: center; " Width="70px" Text="Ok" TabIndex="2"></asp:LinkButton>
                                </td>
                            <td class="style195" >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style200"> 
                           
                                
                                <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                    Text="Current Voucher No." Width="120px"></asp:Label>
                            </td>
                            <td class="style204">
                                <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="True" 
                                    CssClass="ddl" ReadOnly="True" Width="40px" TabIndex="3"></asp:TextBox>
                            </td>
                            <td  align="left" class="style203">
                                <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True" 
                                    CssClass="ddl" ToolTip="You Can Change Voucher Number." Width="40px" 
                                    ReadOnly="True" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                </td>
                            <td class="style207">
                                </td>
                            <td class="style202">
                                </td>
                            <td>
                                <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White"></asp:Label>
                                </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                       </asp:Panel>
                    <asp:Panel ID="pnlBill" runat="server" Visible="False" BorderStyle="None">
                    <table style="width:95%;">
                        <tr>
                            <td class="style199">
                                <asp:Label ID="lblProject" runat="server" style="text-align: right" 
                                    Text="Project:" Width="90px" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="White"></asp:Label>
                            </td>
                            <td class="style199">
                                <asp:TextBox ID="txtSrchProject" runat="server" BorderStyle="None" Width="60px" 
                                    TabIndex="5"></asp:TextBox>
                            </td>
                            <td class="style208">
                                <asp:ImageButton ID="imgSearchProject" runat="server" Height="16px" 
                                    ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchProject_Click" 
                                    Style="margin-left: 0px" Width="16px" TabIndex="6" />
                            </td>
                            <td class="style176" colspan="3">
                                <asp:DropDownList ID="ddlProject" runat="server" Width="500px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlProject_SelectedIndexChanged" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnSelec" runat="server" CssClass="button" 
                                    Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                    style="text-align: center; " Width="90px" TabIndex="11" 
                                    onclick="lbtnSelec_Click">Select</asp:LinkButton>
                            </td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style199">
                                <asp:Label ID="lblUnit" runat="server" Font-Bold="True" Font-Size="12px" 
                                    ForeColor="White" style="text-align: right" Text="Unit Name:" Width="90px"></asp:Label>
                            </td>
                            <td class="style199">
                                <asp:TextBox ID="txtSrchUnit" runat="server" BorderStyle="None" Width="60px" 
                                    TabIndex="8"></asp:TextBox>
                            </td>
                            <td class="style208">
                                <asp:ImageButton ID="imgSearchUnit" runat="server" Height="16px" 
                                    ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchUnit_Click" 
                                    Style="margin-left: 0px" Width="16px" TabIndex="9" />
                            </td>
                            <td class="style176" colspan="3">
                                <asp:DropDownList ID="ddlUnitName" runat="server" Width="500px" TabIndex="10">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                       
                        
                         <tr>
                            <td class="style199" colspan="10">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" style="text-align: left" Width="685px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px" 
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")   
                                                                        %>' 
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFDrAmt" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                    Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="White" 
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="White" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCrAmt" runat="server" BackColor="Transparent" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="White" 
                                                HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                    <%-- <FooterStyle BackColor="#0066CC" />
                                    <HeaderStyle BackColor="#0066CC" BorderStyle="Solid" BorderWidth="2px" 
                                        Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFCCFF" />--%>
                                </asp:GridView>
                             </td>
                        </tr>
                         <tr>
                            <td class="style199" colspan="3">
                                &nbsp;</td>
                            <td class="style190">
                                &nbsp;</td>
                            <td class="style191">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                         <tr>
                            <td class="style199" colspan="3">
                                &nbsp;</td>
                            <td class="style190">
                                &nbsp;</td>
                            <td class="style191">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                            <td class="style199" colspan="3" align="right">
                                <asp:Label ID="lblRefNum" runat="server" CssClass="label2" 
                                    Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                            </td>
                            <td class="style190">
                                <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Width="166px"></asp:TextBox>
                            </td>
                            <td class="style191">
                                <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" 
                                    Text="Other ref.(if any)" Width="120px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" 
                                    Font-Bold="True" Font-Size="12px" onclick="lnkFinalUpdate_Click" 
                                    Width="90px">Final Update</asp:LinkButton>
                            </td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align:top" class="style199" colspan="3">
                                <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                    Width="120px"></asp:Label>
                            </td>
                            <td class="style190" colspan="3">
                                <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Height="42px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                    </table>
                      </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        
</asp:Content>


