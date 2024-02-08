<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptAccPayUpdate.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptAccPayUpdate" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    

  
    <style type="text/css">
        .style7
        {
            width: 34px;
        }
        .style9
        {
            width: 83px;
        }
        .style11
        {
            width: 853px;
        }
        .style12
        {
            width: 973px;
        }
        .style13
        {
            width: 18px;
        }
        .style14
        {
            width: 97px;
        }
        .style15
        {
            width: 82px;
        }
        .style17
        {
            width: 43px;
        }
        .style18
        {
            width: 84px;
        }
    </style>
    

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <script type="text/javascript" language="javascript">
           $(document).ready(function () {
               Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           });
           function pageLoaded() {

               var gv1 = $('#<%=this.dgv1.ClientID %>');
               var gvgrpchqissued = $('#<%=this.gvgrpchqissued.ClientID %>');

               gv1.Scrollable();
               gvgrpchqissued.Scrollable();


               $("input, select").bind("keydown", function (event) {
                   var k1 = new KeyPress();
                   k1.textBoxHandler(event);

               });
           }

    </script>
         

    
        <table style="width:95%; height: 2px;" >
            <tr>
                <td class="style20">
                    <asp:Label ID="lblGeneralAcc" runat="server" Text="Day Wise Issued (Cheque Date)" 
                        CssClass="label" Width="476px"></asp:Label>
                </td>
                <td class="style203" >
                                        &nbsp;</td>
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
                    <asp:LinkButton ID="lnkPrint" runat="server" CssClass="button" 
                        onclick="lnkPrint_Click" ForeColor="White" 
                        style="text-align: center; font-weight: 700" Font-Size="12px">PRINT</asp:LinkButton>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            
                    
                    
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                           
                           
                           
                            <table style="width: 100%;">
                                <tr>
                                    <td colspan="12">
                                       
                                        <asp:Panel ID="P1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px">
                                            <table style="width:99%; height: 2px;">
                                                <tr>
                                                    <td class="style223" colspan="12">
                                                        <asp:Panel ID="Panel1" runat="server" BorderStyle="None">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td class="style7">
                                                                        <asp:Label ID="lblBankCode" runat="server" CssClass="label2" Text="Bank Des.:" 
                                                                            Width="78px"></asp:Label>
                                                                    </td>
                                                                    <td class="style14">
                                                                        <asp:TextBox ID="txtserchBankName" runat="server" BorderStyle="None" 
                                                                            Width="97px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="style233">
                                                                        <asp:ImageButton ID="imgbtnSrchBank" runat="server" Height="16px" 
                                                                            ImageUrl="~/Image/find_images.jpg" onclick="imgbtnSrchBank_Click" TabIndex="1" 
                                                                            Width="16px" />
                                                                    </td>
                                                                    <td class="style12">
                                                                        <asp:DropDownList ID="ddlBankName" runat="server" Font-Bold="True" 
                                                                            Font-Size="12px" TabIndex="2" Width="400px">
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
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style15">
                                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="label2" Text="From :" 
                                                            Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style9">
                                                        <asp:TextBox ID="txtfrmdate" runat="server" 
                                                            BorderStyle="None" BorderWidth="1px" Width="97px" TabIndex="6"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" 
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style13">
                                                        <asp:Label ID="lbltodate" runat="server" CssClass="label2" Height="16px" 
                                                            Text="To:"></asp:Label>
                                                    </td>
                                                    <td class="style207">
                                                        <asp:TextBox ID="txttodate" runat="server" 
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Width="97px" 
                                                            TabIndex="7"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style208">
                                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                                            onclick="lnkOk_Click" Width="78px" TabIndex="8">Ok</asp:LinkButton>
                                                    </td>
                                                    <td class="style11">
                                                        &nbsp;</td>
                                                    <td class="style224">
                                                        &nbsp;</td>
                                                    <td class="style224">
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
                                    <td colspan="12">
                                      
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" 
                                                                onrowdatabound="dgv1_RowDataBound" PageSize="100" ShowFooter="True" 
                                                                style="text-align: left" Width="963px">
                                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" 
                                                                                style="text-align: right" 
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvAccCod0" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' 
                                                                                Width="60px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgcatCod0" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>' 
                                                                                Width="60px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ResCode" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgcUcode0" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' 
                                                                                Width="60px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bank Name">
                                                                        <HeaderTemplate>
                                                                            <table style="width:220px;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Bank Name" 
                                                                                            Width="100px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style60">
                                                                                        <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066" 
                                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                                            ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvbankname" runat="server" Font-Bold="true" 
                                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>' 
                                                                                Width="220px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvPVnum" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Issue #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvvounum" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvPVDate" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Acc. Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgactdesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>' 
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Issue Ref">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvisunum" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cheque No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvchnono0" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvchdat" runat="server" style="text-align: left" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvcramt" runat="server" style="text-align: right" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvdramt" runat="server" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Party Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvParName" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Voucher Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvNewVoNum" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>' 
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bill No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvBill0" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' 
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Reconcilaition Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvReconDat" runat="server" BackColor="Transparent" 
                                                                                BorderStyle="None" style="text-align: left; font-size:11px;" 
                                                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")%>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                </table>
                                            </asp:View>
                                            <asp:View ID="View2" runat="server">
                                                <table style="width:100%;">
                                                    <tr>
                                                        <td colspan="12">
                                                            <asp:Panel ID="Panel2" runat="server" BorderColor="Maroon" BorderStyle="Solid" 
                                                                BorderWidth="1px">
                                                                <table style="width:100%;">
                                                                    <tr>
                                                                        <td class="style17">
                                                                            <asp:Label ID="lblGroupDesc" runat="server" CssClass="label2" 
                                                                                Text="Group Des.:" Width="78px"></asp:Label>
                                                                        </td>
                                                                        <td class="style18">
                                                                            <asp:TextBox ID="txtserchGrpName" runat="server" BorderStyle="None" 
                                                                                TabIndex="9" Width="97px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="style13">
                                                                            <asp:ImageButton ID="imgbtnSrchGroup" runat="server" Height="16px" 
                                                                                ImageUrl="~/Image/find_images.jpg" onclick="imgbtnSrchGroup_Click" TabIndex="10" 
                                                                                Width="16px" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlGroupDesc" runat="server" Font-Bold="True" 
                                                                                Font-Size="12px" TabIndex="11" Width="400px">
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
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="12">
                                                            <asp:GridView ID="gvgrpchqissued" runat="server" AutoGenerateColumns="False" 
                                                                onrowdatabound="gvgrpchqissued_RowDataBound" PageSize="100" ShowFooter="True" 
                                                                style="text-align: left" Width="963px">
                                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" 
                                                                                style="text-align: right" 
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                   
                                                                  
                                                                   
                                                                    <asp:TemplateField HeaderText="Voucher #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvvounumgp" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderText="Issue #">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvissuenumgp" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvPVDategp" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    
                                                                    
                                                                    <asp:TemplateField HeaderText="Acc. Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgactdescgp" runat="server" 
                                                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>' 
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                       
                                                                    <asp:TemplateField HeaderText="Resource Description" >
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFGrandTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                ForeColor="White" Text="Grand Total"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvresdescgp" runat="server" 
                                                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))  %>' 
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <FooterStyle HorizontalAlign="right" />
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderText="Bank Name">
                                                                        <HeaderTemplate>
                                                                            <table style="width:220px;">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Bank Name" 
                                                                                            Width="100px"></asp:Label>
                                                                                    </td>
                                                                                    <td class="style60">
                                                                                        <asp:HyperLink ID="hlbtnbtbCdataExelgp" runat="server" BackColor="#000066" 
                                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                                            ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvbanknamegp" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim() %>' 
                                                                       
                                                                                Width="220px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>


                                                                   

                                                                    <asp:TemplateField HeaderText="Cheque No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvchnono1" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>' 
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvchdat1" runat="server" style="text-align: left" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvpayam" runat="server" style="text-align: right" 
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>' 
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                                ForeColor="White" style="text-align: right" Width="70px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="right" />
                                                                    </asp:TemplateField>
                                                                  
                                                                    <asp:TemplateField HeaderText="Party Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvParName1" runat="server" 
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>' 
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

