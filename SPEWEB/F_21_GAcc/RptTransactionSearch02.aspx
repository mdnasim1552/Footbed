<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptTransactionSearch02.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptTransactionSearch02" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            width: 496px;
        }
        .style8
        {
        }
        .style9
        {
            width: 38px;
        }
        .style10
        {
            width: 57px;
        }
        .style11
        {
            width: 41px;
        }
        .style12
        {
            width: 107px;
        }
        .style13
        {
            width: 38px;
            height: 22px;
        }
        .style14
        {
            width: 57px;
            height: 22px;
        }
        .style15
        {
            width: 41px;
            height: 22px;
        }
        .style16
        {
            width: 107px;
            height: 22px;
        }
        .style20
        {
            width: 103px;
        }
        .style21
        {
            height: 22px;
            width: 103px;
        }
        .style33
        {
            width: 38px;
            height: 21px;
        }
        .style34
        {
            width: 57px;
            height: 21px;
        }
        .style35
        {
            width: 41px;
            height: 21px;
        }
        .style36
        {
            width: 107px;
            height: 21px;
        }
        .style37
        {
            width: 103px;
            height: 21px;
        }
        .style38
        {
            width: 50px;
        }
        .style39
        {
            width: 81px;
        }
        .style17
        {
            width: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#<%=this.txtvoudate.ClientID %>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtTransSearch(event);



            });

           


        }

       
    </script>

    <table style="width:95%; height: 2px;">
            <tr>
                <td class="style5">
                    <asp:Label runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="Post Dated Cheque" Width="590px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" 
                    ID="lblGeneralAcc" ></asp:Label>
                    
                </td>
                <td class="style7">
                                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                </td>
                <td class="style6">
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
                        onclick="lnkPrint_Click" ForeColor="White" 
                        style="text-align: center; font-weight: 700; height: 16px;" Width="60px" 
                        Font-Size="12px">PRINT</asp:LinkButton>
                </td>
            </tr>
            </table>           
                    
                    


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td class="style8" colspan="12">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px" Width="572px">
                            <table style="width:160%;">
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="lblVouDate" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Vou Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtvoudate" runat="server" BorderStyle="None" Width="120px" 
                                            TabIndex="1" AutoPostBack="True" ontextchanged="txtvoudate_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style11">
                                        <asp:Label ID="lblConAccount" runat="server" CssClass="label2" 
                                            ForeColor="White" Text="Bank:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtBankDesc" runat="server" BorderStyle="None" TabIndex="2" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style20">
                                        </td>
                                    <td rowspan="7" valign="top">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td colspan="3">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:ListBox ID="lstVouname" runat="server" AutoPostBack="True" 
                                                                        BackColor="Aqua" Font-Bold="True" Font-Size="12px" Height="120px" 
                                                                        onselectedindexchanged="lstVouname_SelectedIndexChanged" 
                                                                        SelectionMode="Multiple" style="margin-left: 12px" TabIndex="12" Width="320px">
                                                                    </asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30px;">
                                                      </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True" 
                                                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                            oncheckedchanged="chkPrint_CheckedChanged" 
                                                            style="text-align: left; margin-left: 0px;" TabIndex="55" Text="Cheque Print" 
                                                            Width="95px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlChqList" runat="server" Height="19px" 
                                                            style="margin-left: 2px" TabIndex="56" Visible="False" Width="230px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="lblacchead" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Account Head:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtAccountHead" runat="server" BorderStyle="None" TabIndex="3" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style11">
                                        <asp:Label ID="lblDetailsHead" runat="server" CssClass="label2" 
                                            ForeColor="White" Text="Details Head:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtDetailsHead" runat="server" BorderStyle="None" TabIndex="4" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style20">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style13">
                                        <asp:Label ID="lblamount0" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Amount:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style14">
                                        <asp:TextBox ID="txtamount" runat="server" BorderStyle="None" TabIndex="5" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style15">
                                        <asp:Label ID="lblchequeno" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Cheque No:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style16">
                                        <asp:TextBox ID="txtchequeno" runat="server" BorderStyle="None" TabIndex="6" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style21">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            Font-Size="12px" onclick="lnkOk_Click" TabIndex="11" Text="Ok" 
                                            Width="90px"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="lblIssueNo" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Issue No:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtissueno" runat="server" BorderStyle="None" TabIndex="7" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style11">
                                        <asp:Label ID="lblChequedate" runat="server" CssClass="label2" 
                                            ForeColor="White" Text="Cheque Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtchequedate" runat="server" BorderStyle="None" TabIndex="8" 
                                            Width="120px" AutoPostBack="True" 
                                            ontextchanged="txtchequedate_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="style20">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        <asp:Label ID="lblPayto0" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Pay To:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
                                        <asp:TextBox ID="txtpayto" runat="server" BorderStyle="None" TabIndex="9" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style11">
                                        <asp:Label ID="lblNerration" runat="server" CssClass="label2" ForeColor="White" 
                                            Text="Nerration:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtnarration" runat="server" BorderStyle="None" TabIndex="10" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="style20">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="style33">
                                    </td>
                                    <td class="style34">
                                    </td>
                                    <td class="style35">
                                    </td>
                                    <td class="style36">
                                    </td>
                                    <td class="style37">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style9">
                                        &nbsp;</td>
                                    <td class="style10">
                                        &nbsp;</td>
                                    <td class="style11">
                                        &nbsp;</td>
                                    <td class="style12">
                                        &nbsp;</td>
                                    <td class="style20">
                                        <asp:LinkButton ID="lnkRefresh" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="12px" onclick="lnkRefresh_Click" TabIndex="99" 
                                            Text="Refresh" Width="90px"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                                        

                    </td>
                </tr>
                <tr>
                    <td class="style8" colspan="12">
                        <asp:Panel ID="Pnlmain" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style38">
                                        <asp:Label ID="lblVoucherNo" runat="server" CssClass="label2" Font-Size="12px" 
                                            Text="Voucher No. :" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style39">
                                        <asp:Label ID="lblvalVoucherNo" runat="server" BackColor="White" 
                                            Font-Size="12px" Width="90px"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <asp:Label ID="lblVouDate0" runat="server" CssClass="label2" Font-Size="12px" 
                                            Text="Voucher Date :" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblvalVoucherDate" runat="server" BackColor="White" 
                                            Font-Size="12px" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style10">
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
                                    <td class="style38">
                                        <asp:Label ID="lblBankDescription" runat="server" CssClass="label2" 
                                            Font-Size="12px" Text="Bank Name :" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style8" colspan="3">
                                        <asp:Label ID="lblValBankDescription" runat="server" BackColor="#000066" 
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                    </td>
                                    <td class="style10">
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
                    <td class="style8" colspan="12">
                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" style="text-align: left" Width="685px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server" 
                                                                        Font-Size="11px" 
                                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "          " + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "") %>' 
                                                                        Width="250px" TabIndex="75" ></asp:Label>
                                        
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Issue No">
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisuno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                           
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                                            Width="60px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                               

                                                 <asp:TemplateField HeaderText="Cheque No">
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvChequeno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                           
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                                            Width="100px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cheque Date">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvChequeDate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                        
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>'  width="80px"></asp:Label>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                              
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="12px" 
                                            ForeColor="Black" style="text-align: right" TabIndex="81" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblFgvDrAmt" runat="server" BackColor="Transparent" 
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" ReadOnly="True" style="text-align: right" 
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Pay To">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPayto" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                                            Width="140px" ForeColor="Black" TabIndex="80"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Acvounum" Visible="False">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvacvounm" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>' 
                                                            Width="50px" ForeColor="Black" TabIndex="80"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill No">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBillno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                                            Width="100px" ForeColor="Black" TabIndex="99"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Instade Of Issue">                                          
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinsissueno" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextboxL" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>' 
                                                            Width="80px" ForeColor="Black" TabIndex="99"></asp:Label>
                                                    </ItemTemplate>
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
                    <td class="style8" colspan="12">
                        <asp:Panel ID="PnlNarration" runat="server" BorderColor="Maroon" 
                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style11">
                                        <asp:Label ID="lblNarration" runat="server" CssClass="label2" Font-Size="12px" 
                                            Text="Narration :" Width="120px"></asp:Label>
                                    </td>
                                    <td class="style13" colspan="5">
                                        <asp:Label ID="lblvalNarration" runat="server" BackColor="White" 
                                            Font-Size="12px" Height="40px" Width="590px"></asp:Label>
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
                                </tr>
                                <tr>
                                    <td class="style11">
                                        &nbsp;</td>
                                    <td class="style17">
                                        &nbsp;</td>
                                    <td class="style14">
                                        &nbsp;</td>
                                    <td class="style15">
                                        &nbsp;</td>
                                    <td class="style16">
                                        &nbsp;</td>
                                    <td class="style12">
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
                    <td class="style8">
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

