<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccPurchaseBBLC.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccPurchaseBBLC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style19
        {
            width: 115px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:97%;">
    <tr>
        <td class="style18">
                    <asp:Label ID="lblGeneralAcc" runat="server" Text=" Purchase Back To Back LC" 
                        CssClass="label" Width="500px"></asp:Label>
                </td>
        <td class="style189">
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
                    <table style="width:95%; height: 83px;">
                        <tr>
                            <td colspan="3" align="center">
                                <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" 
                                    ImageUrl="~/Image/movie_26.gif" onclick="ibtnvounu_Click" Width="145px" 
                                    Visible="False" />
                            </td>
                            <td class="style183">
                                &nbsp;</td>
                            <td class="style187">
                                <asp:Label ID="lblDate" runat="server" CssClass="label2" Text="Voucher Date" 
                                    Width="100px"></asp:Label>
                            </td>
                            <td class="style179">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="ddl"></asp:TextBox>
                            </td>
                            <td class="style188">
                                <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click">Ok</asp:LinkButton>
                            </td>
                            <td >
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                                    <ProgressTemplate>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                                            Font-Size="14px" ForeColor="White" Text="Please wait....." Width="200px"></asp:Label>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td class="style184">
                                <asp:Label ID="lbllstVouno" runat="server" CssClass="label2" 
                                    Text="Last Voucher No." Width="120px" Visible="False"></asp:Label>
                            </td>
                            <td class="style184" colspan="2">
                                <asp:TextBox ID="txtLastVou" runat="server" CssClass="ddl" ReadOnly="True" 
                                    Width="90px" BackColor="Aqua" Visible="False"></asp:TextBox>
                            </td>
                            <td class="style183">
                                &nbsp;</td>
                            <td class="style187">
                                &nbsp;</td>
                            <td class="style179">
                                &nbsp;</td>
                            <td class="style188" colspan="2">
                                <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Font-Size="13px" 
                                    ForeColor="Yellow" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style181">
                                
                                <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                    Text="Current Voucher No." Width="120px" Visible="False"></asp:Label>
                            </td>
                            <td class="style185">
                                <asp:TextBox ID="txtcurrentvou" runat="server" AutoPostBack="True" 
                                    CssClass="ddl" ReadOnly="True" Width="40px" Visible="False"></asp:TextBox>
                            </td>
                            <td class="style182" align="left">
                                <asp:TextBox ID="txtCurrntlast6" runat="server" AutoPostBack="True" 
                                    CssClass="ddl" ToolTip="You Can Change Voucher Number." Width="40px" 
                                    ReadOnly="True" Visible="False"></asp:TextBox>
                            </td>
                            <td class="style183">
                                &nbsp;</td>
                            <td class="style179">
                                &nbsp;</td>
                            <td class="style179">
                                &nbsp;</td>
                            <td class="style179">
                                &nbsp;</td>
                            <td class="style179">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table style="width:95%;">
                        <tr>
                            <td class="style19">
                                <asp:Label ID="lblMRRList" runat="server" style="text-align: right" 
                                    Text="MRR List" Width="100px" Font-Bold="True" Font-Size="14px" 
                                    ForeColor="White" Visible="False"></asp:Label>
                            </td>
                            <td class="style176" colspan="3">
                                <asp:DropDownList ID="ddlMRRList" runat="server" Width="600px" Visible="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnSelectMRR" runat="server" Font-Bold="True" 
                                    Font-Size="14px" onclick="lbtnSelectMRR_Click" 
                                    style="text-align: center; " Width="97px" CssClass="button" 
                                    ForeColor="White" Visible="False">Select MRR</asp:LinkButton>
                            </td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#7FBF41" BorderStyle="Solid" BorderWidth="2px" 
                                    Height="16px" ShowFooter="True" Width="355px">
                                    <RowStyle BackColor="#99CCFF" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
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
                                                <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL" 
                                                    Font-Names="Verdana" Font-Size="11px" 
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>' 
                                                    Width="300px"></asp:Label>
                                                <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                    Visible="False" Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details Description" ItemStyle-Font-Size="9px" 
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>' 
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification" ItemStyle-Font-Size="9px" 
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Text="Total:"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Font-Size="9px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity" 
                                            ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent" ReadOnly="true"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                                    CssClass="GridTextbox" Height="22px" Visible="False" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Con.Rate" 
                                            ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCRate" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="21px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle ForeColor="White" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField  HeaderText="Rate(FC)" 
                                            ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRatefc" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="21px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnratefc")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="65px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle ForeColor="White" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Rate" 
                                            ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="21px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle ForeColor="White" />
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                      
                                       
                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Height="22px" Width="103px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                    CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Height="22px" Width="103px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" 
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                    CssClass="GridLebelL" Height="22px" ReadOnly="True" ForeColor="Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="11px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRR No." Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMrrno" runat="server" CssClass="GridLebel" ForeColor="Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#0066CC" />
                                    <HeaderStyle BackColor="#0066CC" BorderStyle="Solid" BorderWidth="2px" 
                                        Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#FFCCFF" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="style19">
                                <asp:Label ID="lblRefNum" runat="server" CssClass="label2" 
                                    Text="Ref./Cheq No/Slip No." Visible="False"></asp:Label>
                            </td>
                            <td class="style190">
                                <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Width="166px" Visible="False"></asp:TextBox>
                            </td>
                            <td class="style191">
                                <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" 
                                    Text="Other ref.(if any)" Width="120px" Visible="False"></asp:Label>
                            </td>
                            <td class="style176">
                                <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Width="265px" Visible="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" 
                                    Font-Bold="True" Font-Size="14px" onclick="lnkFinalUpdate_Click" 
                                    Width="100px" Visible="False">Final Update</asp:LinkButton>
                            </td>
                            <td class="style178">
                                &nbsp;</td>
                            <td class="style192">
                                &nbsp;</td>
                            <td class="style193">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; vertical-align:top" class="style19">
                                <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td class="style190" colspan="3">
                                <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled" 
                                    CssClass="ddl" Height="42px" TextMode="MultiLine" Width="596px" 
                                    Visible="False"></asp:TextBox>
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
                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>


