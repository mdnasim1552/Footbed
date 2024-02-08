<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="ProjTrialBalanc.aspx.cs" Inherits="SPEWEB.F_31_Mis.ProjTrialBalanc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style28
        {
            width: 412px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
      <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv = $('#<%=this.gvPrjtrbal.ClientID %>');
            var gv1 = $('#<%=this.grvTrBal2.ClientID %>');

            gv.Scrollable();
            gv1.Scrollable();


           
        }

    </script>
    <table style="width: 903px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="LblTitle" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                    Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1;"
                    Text="Project Trial Balance" Width="429px" BorderStyle="Inset"
                    BackColor="Transparent" BorderWidth="2px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style58">
                <asp:Label ID="lblRptType" runat="server" Visible="False" Width="99px"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Width="70px" OnClick="lnkPrint_Click"
                    Font-Underline="False" CssClass="button" BackColor="#000066" BorderColor="White"
                    BorderStyle="Solid" BorderWidth="1px">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <asp:Panel ID="panel11" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Width="90%" >
            <table style="width: 100%;">

                <tr>
                    <td class="style61" width="125px">
                        <asp:Label ID="lblDatefrom" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                            Style="text-align: right; color: #FFFFFF;" Text="As on Date:" 
                            Width="120px"></asp:Label>
                    </td>
                    <td class="style62" width="80px">
                        <asp:TextBox ID="txtDatefrom" runat="server" Width="80px" BorderStyle="None"></asp:TextBox>
                    </td>
                    <td class="style63" width="22px">
                        &nbsp;</td>
                    <td class="style28">
                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="16px" ForeColor="White" Height="20px" OnClick="lbtnOk_Click" 
                            Style="text-align: center;" Width="52px">Ok</asp:LinkButton>
                    </td>
                    <td class="style63">
                        &nbsp;
                        </td>
                    <td class="style66">
                        &nbsp;</td>
                    <td class="style67">
                        &nbsp;</td>
                    <td class="style68">
                        &nbsp;</td>
                    <td>
                        <cc1:CalendarExtender ID="Calfr" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtDatefrom">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                
            <tr>
                            <td class="style87">
                                <asp:Label ID="lblPrjName" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                    Style="text-align: right; color: #FFFFFF;" Text="Project Name:" 
                                    Width="120px"></asp:Label>
                            </td>
                            <td class="style98">
                                <asp:TextBox ID="txtSearchpIndp" runat="server" Style="border-style: solid; border-width: 1px"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td class="style63">
                                <asp:ImageButton ID="ImgbtnFindProjind" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                    OnClick="ImgbtnFindProjind_Click" Width="21px" />
                            </td>
                            <td class="style28">
                                <asp:DropDownList ID="ddlProjectInd" runat="server" Font-Size="12px"
                                    Width="400px">
                                </asp:DropDownList>
                               
                            </td>
                            <td>
                                &nbsp;&nbsp;
                                </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                          
                        </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGrp" runat="server" Font-Bold="True" Font-Size="12px" 
                            Height="16px" Style="text-align: right; color: #FFFFFF;" Text="Group:" 
                            Width="120px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" 
                            Font-Size="12px" Height="21px" Style="text-transform: capitalize" Width="85px">
                          
                            <asp:ListItem>Sub-1</asp:ListItem>
                            <asp:ListItem>Sub-2</asp:ListItem>
                            <asp:ListItem>Sub-3</asp:ListItem>
                            <asp:ListItem Selected="True">Details</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td class="style28">
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
                </tr>
                </table>
                </asp:Panel>
                <asp:MultiView ID="MultiView1" runat="server">
                       <asp:View ID="ProTrailBal" runat="server">
                            <table width="100%">
                <tr>
                <td colspan="10">
                    <asp:GridView ID="gvPrjtrbal" runat="server" AutoGenerateColumns="False" 
                        OnRowDataBound="gvPrjtrbal_RowDataBound" ShowFooter="True" Width="658px">
                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server"  Font-Bold="True" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCode" runat="server" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText=" Description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>' Width="300px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText=" Description">
                                <HeaderTemplate>
                                    <table style="width:47%;">
                                        <tr>
                                            <td class="style58">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                    Text="Description Of Accounts" Width="180px"></asp:Label>
                                            </td>
                                            <td class="style60">
                                                &nbsp;</td>
                                            <td>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" 
                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                    ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                   </HeaderTemplate>

                                <ItemTemplate>
                               
                                    <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                        Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>' Width="300px"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" HLgvDesc
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lgvqty" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRate" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="75px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Debit(in Tk.)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                               
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Credit(in Tk.)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCre" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                        <asp:View ID="TrailsBal2" runat="server">
                            <table width="100%">
                                 <tr>
                <td colspan="10">
                    <asp:GridView ID="grvTrBal2" runat="server" AutoGenerateColumns="False" 
                        OnRowDataBound="grvTrBal2_RowDataBound" ShowFooter="True" Width="658px">
                        <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server"  Font-Bold="True" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvCode" runat="server" Height="16px" 
                                        Style="text-align: right" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2")) %>' Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText=" Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc2").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc2")).Trim(): "")  %>' 
                                                                            Width="350px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                          
                            <asp:TemplateField HeaderText="Debit(in Tk.)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                               <FooterTemplate>
                                    <asp:Label ID="lgvFTDrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align:right" ></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Credit(in Tk.)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCre" runat="server" Style="text-align: right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>' 
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFTCrAmt" runat="server" ForeColor="White" Font-Size="12px" Style="text-align:right" ></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
