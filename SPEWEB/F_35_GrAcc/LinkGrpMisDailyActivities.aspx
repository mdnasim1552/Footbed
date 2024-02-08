<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpMisDailyActivities.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpMisDailyActivities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            var gvFeaAllPro = $('#<%=this.gvFeaAllPro.ClientID %>');
            var gvgpnp = $('#<%=this.gvgpnp.ClientID %>');
            var gvLcStatus = $('#<%=this.gvLcStatus.ClientID %>');
            gvFeaAllPro.Scrollable();
            gvgpnp.Scrollable();
            gvLcStatus.Scrollable();

        }
    </script>

    
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
                <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                 <asp:Panel ID="Panel1" runat="server">
                                   <div class="form-group"> 

                                         <div class="col-md-7 asitCol7 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"  Text="Page Size:"></asp:Label>

                                              <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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

                                             <asp:Label ID="lblDateRange" runat="server" CssClass="smLbl_to"  Text="Date:"></asp:Label>

                                             <asp:Label ID="lblAsDate" runat="server" CssClass=" inputtextbox" Text="" Width="200px"></asp:Label>

                                       
                                  
                                    </div>  

                                </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div  class="table table-responsive">
                         <asp:MultiView ID="MultiView1" runat="server">

                    <asp:View ID="ViewDetailsCollection" runat="server">
                       
                                    <asp:GridView ID="gvCollDet" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        OnPageIndexChanging="gvCollDet_PageIndexChanging" OnRowDataBound="gvCollDet_RowDataBound"
                                        ShowFooter="True" Style="text-align: left" Width="1052px">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MR. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrno" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "mrno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")).Trim() : "")
                                                                         
                                                                    %>' Width="150px"> </asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmrDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate"))%>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAccCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="49px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcUcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                        Width="50px"></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acc. Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcPactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="130px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name Of Client">
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="80px">Total:</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcustname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                        Width="150px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flat No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="150px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrmrks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCheNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chq. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvchqDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBaName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvBranch" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash Amount">
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="70px"> </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcashamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque Amount">
                                                <FooterTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="70px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvchqamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                       <FooterStyle CssClass="grvFooter"/>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                               
                    </asp:View>
                    <asp:View ID="ViewRecAPay" runat="server">
                      
                                    <asp:GridView ID="gvrecandpay" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvrecandpay_RowDataBound"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="973px">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RecCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrecpcode" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>'
                                                        Width="50px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head Accounts Head">
                                                <FooterTemplate>
                                                    <table style="width: 12%;">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Text="Total Receipts:" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Text="Net Balance" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Head Accounts Head"
                                                                    Width="180px"></asp:Label>
                                                            </td>
                                                            <td class="style60">
                                                                &nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnRcvPayCdataExel" runat="server" CssClass="btn  btn-primary primaryBtn"  Style="text-align: center"
                                                                    >Export Excel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRecDesc" runat="server" Font-Underline="False" OnClick="btnRecDesc_Click"
                                                        Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                        Width="300px"></asp:LinkButton></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receipt Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrecpam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 12%;">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblgvFrecpam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:HyperLink ID="lgvFNetBalance" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Target="_blank" Width="100px"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PayCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpaycode" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                                        Width="50px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head of Accounts">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPayDesc" runat="server" Font-Underline="False" OnClick="btnPayDesc_Click"
                                                        Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                                        Width="300px"></asp:LinkButton></ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 12%;">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Text="Total Payments:" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpayam1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="100px"></asp:Label></ItemTemplate>
                                                <FooterTemplate>
                                                    <table style="width: 12%;">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lgvFpayam1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                    Style="text-align: right" Width="100px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                       <FooterStyle CssClass="grvFooter"/>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                            
                              
                                    <asp:Panel ID="PanelNote" runat="server" BorderColor="Maroon" BorderStyle="Solid"
                                        BorderWidth="1px" Visible="False">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style91">
                                                    <asp:Label ID="lblBankstatus" runat="server" BackColor="#000066" BorderColor="#000"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Yellow" Text="Bank Status:"
                                                        Width="120px"></asp:Label>
                                                </td>
                                                <td class="style92">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style91">
                                                    <asp:Label ID="lblopnliabilities" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99" Text="Opening Liabilities"
                                                        Width="120px"></asp:Label>
                                                </td>
                                                <td class="style92">
                                                    <asp:Label ID="lblopnliabilitiesval" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99; text-align: right;"
                                                        Text="Opening Liabilities" Width="120px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style91">
                                                    <asp:Label ID="lblclsliabilities" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99" Text="Closing Liabilities"
                                                        Width="120px"></asp:Label>
                                                </td>
                                                <td class="style92">
                                                    <asp:Label ID="lblclsliabilitiesval" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99; text-align: right;"
                                                        Text="Closing Liabilities" Width="120px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style91">
                                                    <asp:Label ID="lblnetLiabilities" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99" Text="Net Liabilities"
                                                        Width="120px"></asp:Label>
                                                </td>
                                                <td class="style92">
                                                    <asp:Label ID="lblnetLiabilitiesval" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#660033" Height="16px" Style="color: #FFFF99; text-align: right;"
                                                        Text="Net Liabilities" Width="120px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                             
                    </asp:View>
                    <asp:View ID="ViewBankPosition" runat="server">
                      
                                    <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="973px" OnRowDataBound="gvBankPosition_RowDataBound">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblserialnoid1" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="20px"></asp:Label></ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodebank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                        Width="90px"></asp:Label></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Description of Accounts">
                                                <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td class="style58" style="width: auto">
                                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="Description of Accounts"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnbnkpdataExel" runat="server"  CssClass="btn  btn-primary primaryBtn" >Export Excel</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDescbank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="400px"></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnbal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Opening Liabilities" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvopnliabilities" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Deposit" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDramtbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Withdrawn" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCramtbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Closing Balance" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvclobalbank" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Closing Liabilities" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcloliabilities" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Bank Limit" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbankLim" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                                HeaderText="Bank Available Balance" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbankBal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label></ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                      <FooterStyle CssClass="grvFooter"/>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                               
                    </asp:View>
                    <asp:View ID="ViewSalesStock" runat="server">
                        <asp:GridView ID="gvMMPlanVsAch" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="662px">
                            <PagerSettings Position="Top" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pactcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactcode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="100px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc1" runat="server" Font-Bold="false" Font-Size="11PX" Font-Underline="false"
                                            ForeColor="Black" Style="text-align: left" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="300px">
                                                                         
                                                                  
                                                                
                                        </asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Master Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmasterplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "masplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmasPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monthly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmonplan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "monplan")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmonPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Execution">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvExecutionpAC" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "excution")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFExecutionpAC" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Mas. Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMasPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acmasplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acheivement (%) on Monthly Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerMunthPlan" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acmonplan")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewOrderexpaRealized" runat="server">
                        <asp:GridView ID="gvOrdexarlz" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvOrdexarlz_PageIndexChanging" ShowFooter="True" Style="margin-right: 0px"
                            Width="16px">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Job #">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtxttotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="#000" Style="text-align: right">Total</asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvjobnooer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Buyer">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbuyeroer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ship Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshipdateoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export LC">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlcnameoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LC Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlcdateoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcurrencyoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText=" Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordrratoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvqtyoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Order Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordramtoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvFordramtoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />
                                    <ItemStyle HorizontalAlign="right" />
                                   
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText=" Export #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvexportnooer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Export Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvexqtyoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvexpamtoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFexpamtoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />

                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderbaloer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvForderbaloer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />

                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperoordoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroord")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lgvFperoordoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Ref.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbankrefoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Realized Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrlzdateoer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rlzdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Realize Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrealizeamtoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lgvFrealizeamtoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />

                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperoshipoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroship")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvFperoshipoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />

                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short/Excess">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshortoexcessoer" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <asp:Label ID="lgvFshortoexcessoer" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                     <FooterStyle HorizontalAlign='Right' />


                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewSoldUnsold" runat="server"> 
                        <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="dgvAccRec03_PageIndexChanging" OnRowDataBound="dgvAccRec03_RowDataBound"
                            ShowFooter="True" Style="text-align: left" Width="654px">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                            ForeColor="Black" Style="text-align: left" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                            Width="250px"></asp:HyperLink></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value Of Stock">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtstkamal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtstkamal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tstkam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size(Av.)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFususizeal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ususize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Amt.(Av.)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFusuamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusuamtal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpercental" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Flat Size">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFusizeal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Avg. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrateal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apartment Price">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFaptcostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvaptcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking &amp; Others">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcpaocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcpaocostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpaocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocsotal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Encash">
                                    <FooterTemplate>
                                        <asp:Label ID="lgFEncashal" runat="server" ForeColor="#000"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEncashal" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Returned">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtretamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtretamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Today's">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtframtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtframtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post Dated">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtpdamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtpdamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoreceivedal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotreceivedal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFatoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtatoduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues Upto Dec">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotalduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues &amp; OverDue">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Booking">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpbduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Installment">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpinsduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Booking ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCbookingal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Installment ">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCinstallmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Current Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCInstalmental" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCoCInstalmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFvbaamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvbaamtal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vbamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delay Charge">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdelchargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Cheque">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdischargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdischargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnettoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnettoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewIssueVsCollection" runat="server">
                        <asp:GridView ID="gvarecandpay" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="973px">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcodeac" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Total Receipts:" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Net Balance" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="Head Accounts Head"
                                                        Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnacRcvPayCdataExel" runat="server"   CssClass="btn  btn-primary primaryBtn" >Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescac" runat="server" Font-Bold="True" Font-Underline="False"
                                            OnClick="btnRecDesc_Click" Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                            Width="300px"></asp:LinkButton></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamac" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblgvFrecpamac" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFNetBalanceac" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycodeac" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescac" runat="server" Font-Bold="True" Font-Underline="False"
                                            OnClick="btnPayDesc_Click" Style="color: Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="300px"></asp:LinkButton></ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Total Payments:" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamac" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFpayamac" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewbblcSt" runat="server">
                        <asp:GridView ID="gvRptBBLCPay" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvRptBBLCPay_PageIndexChanging" ShowFooter="True">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo12" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrder" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                            Width="150px"></asp:Label></ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBLC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBBLC" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blcdesc")) %>'
                                            Width="150px"></asp:Label></ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSupName" runat="server" Style="text-align: Left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                            Width="135px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal0" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="Total"></asp:Label></FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Settlement Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSettDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "settldat")) %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbblcbill" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbillamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due  Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdueamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Not Yet Due">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnydueamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nydueamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnydueamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewFeasibility" runat="server">
                        <asp:GridView ID="gvFeaAllPro" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFeaAllPro_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfoCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Info Desc" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInfodesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="120px"></asp:Label></ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDescfea" runat="server" Style="font-size: 12px; color: Black;
                                            text-decoration: none;" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "infdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")).Trim(): "")
                                                                    %>' Width="300px">  
                                                                    
                                                                    
                                                                    
                                                                    
                                        </asp:HyperLink></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Text="Grand Total:  " Width="70px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Revenue">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvoRev" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orevamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvoFRevenue" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="logvCost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ocostamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="logvoFCost" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Orginal&lt;br /&gt;Margin">
                                    <ItemTemplate>
                                        <asp:Label ID="logvmargin" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "omargin")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="logvoFmargin" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% on Orginal&lt;br /&gt; Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperorCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perocost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Revenue">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRev" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRevenue" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCost" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised&lt;br /&gt;Margin">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProfit" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prolos")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmargin" runat="server" Font-Bold="True" Font-Size="11px" ForeColor="#000"
                                            Style="text-align: right" Width="70px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% on&lt;br /&gt;Revised&lt;br /&gt; Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvperCost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Changed %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvPerRev" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "change")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewGPNP" runat="server">
                        <asp:GridView ID="gvgpnp" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvgpnp_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="651px">
                          
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProjectgn" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")   %>'
                                            Width="180px">
                                                                         
                                                                         
                                        </asp:Label></ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Const. Area">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconarea" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conarea")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFconarea" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsaleamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFsaleamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Const. Cost(a)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconsct" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conscost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFconsct" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Interest Of Const.(b)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbinterest" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "binterest")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbinterest" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total(c=a+b)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtconabin" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tconabin")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtconabit" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Land Cost(d)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "landcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFlcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Of Fund(e)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcoffund" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfundamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcoffund" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total(f=d+e)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtocfaland" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcfaland")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtocfaland" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Cost(g)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvadcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFadcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Product(a+b+d+e+g)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtprcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tprcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtprcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgp" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gprofit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFgp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Over Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvovrhead" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovrcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFovrhead" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RF &amp; Provision">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrfund" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rfapcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrfund" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtopcost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtopcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtocost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFtocost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnp" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nprofit")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnp" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP % On Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvperoncost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pgponct")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFperoncost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GP % On  Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvperonsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pgponsl")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFperonsl" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP % On Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnpperoncost" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnponct")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnpperoncost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NP % On Sales">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnpperonsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pnponsl")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFnpperonsl" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label></FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewLCst" runat="server">
                        <asp:GridView ID="gvLcStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvLcStatus_RowDataBound">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                    <HeaderTemplate>
                                        <table style="width: 150px">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label30" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                        Width="70px"></asp:Label>
                                                </td>
                                                <td class="style60">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCdataExells" runat="server"  CssClass="btn  btn-primary primaryBtn" >Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvActDescls" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:HyperLink></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Order Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordramtlc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvordramtlc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Shipment Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFshiqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshiqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shiqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short/Excess Shipment">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFseqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvseqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "seqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of Shipment">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshiper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shiper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Value">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOB per DZN">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvfobpdz" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fobpdz")).ToString("#,##00.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R1">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R2">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R3">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R4">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R5">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R6">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Direct Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtcostls" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CM">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcm" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cm")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CM per DZN">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcmpdz" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmpdz")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of CM">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcmper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R7">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R8">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R9">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R10">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R11">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R12">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R13">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvRls13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFRls13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCostls" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoCostls" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Position">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetpositionls" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetpositionls" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% of Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetper" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescdupls" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvserial0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewMonProStatus" runat="server">
                        <asp:GridView ID="gvMonPorStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="616px">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialno0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="25px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                    <HeaderTemplate>
                                        <table style="width: 150px">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Height="16px" Text="Description"
                                                        Width="70px"></asp:Label>
                                                </td>
                                                <td class="style60">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCdataExel" runat="server"  CssClass="btn  btn-primary primaryBtn" >Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R1">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R2">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR2" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R3">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR3" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R4">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR4" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R5">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR5" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R6">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR6" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R7">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR7" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R8">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR8" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R9">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR9" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R10">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR10" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R11">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR11" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R12">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR12" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R13">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR13" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R14">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR14" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r14")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R15">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR15" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r15")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R16">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR16" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r16")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R17">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR17" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r17")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R18">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR18" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r18")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R19">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR19" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r19")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="R20">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvR20" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r20")).ToString("#,##0;(#,##0); ")%>'
                                            Width="65px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFR20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtocost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Collection">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoCollection" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoCollection" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocollamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Position">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetposition" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnetposition" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescdup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvserial" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewPDCSummary" runat="server">
                        <asp:GridView ID="gvpdc" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpdc_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="399px">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlpsum" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDescpaysum" runat="server" Font-Size="12px" Font-Underline="False"
                                            Style="font-size: 11px; color: Black;" Target="_blank" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pgrpdesc")) + "</B>"+
                                                  (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length > 0 ? 
                                                  (Convert.ToString(DataBinder.Eval(Container.DataItem, "pgrpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                  "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim() : "")%>'
                                            Width="300px"> 
                                           
                                                   
                                                   
                                                   
                                                   


                                            
                                            
                                        </asp:HyperLink></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtoamtpdc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due to Pay">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDC">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpdc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdcam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewDaywiseSales" runat="server">
                        <asp:GridView ID="gvOrderrec" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Style="margin-right: 0px" Width="16px" AllowPaging="True" OnPageIndexChanging="gvOrderrec_PageIndexChanging">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order #">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvlcorderno" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                            Width="200px">
                                        </asp:HyperLink></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCurrency" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curreny")) %>'
                                            Width="50px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Conversion Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvConRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Amount(FC)">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvForderamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderamtrec" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvproamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFproamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshipamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFshipamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderrecqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvForderrecqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvproqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFproqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvshipqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFshipqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label></FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                           <FooterStyle CssClass="grvFooter"/>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>

                     <asp:View ID="ViewLcDetails" runat="server">
                            <asp:GridView ID="gvLcDetails" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" OnPageIndexChanging="gvLcDetails_PageIndexChanging"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Style="margin-right: 0px" Width="16px" 
                                onrowdatabound="gvLcDetails_RowDataBound">
                              
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo1" runat="server" Font-Bold="True" 
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Buyer">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbuyeroerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>' 
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                   
                                   

                                    <asp:TemplateField HeaderText="Export LC">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtxttotallc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" Style="text-align: right">Grand Total</asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlcnameoerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcname")) %>' 
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LC Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlcdateoerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdate")) %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   

                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderoerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>' 
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Job #">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvjobnooerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job")) %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvshipdateoerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdate")) %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Order Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvordramtoerlc" runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFordramtlc" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="BBLC  App. Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbblctotallc" runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bblcamt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbblcTotal" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Payable">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlcpayable" runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payable")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFlcpayable" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Export #">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvexportnooerlc" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>' 
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Export Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvexqtyoerlc" runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Export Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvexpamtoerlc" runat="server" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFexpamtlc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    
                                  
                                   
                                </Columns>
                              <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            </asp:View>

                     <asp:View ID="ViewLcSummary" runat="server">
                                       <asp:GridView ID="gvLcSummary" runat="server" AllowPaging="True"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                           AutoGenerateColumns="False" OnPageIndexChanging="gvLcSummary_PageIndexChanging" 
                                           onrowdatabound="gvLcSummary_RowDataBound" ShowFooter="True" 
                                           Style="margin-right: 0px" Width="16px">
                                          
                                           <Columns>
                                               <asp:TemplateField HeaderText="Sl.No.">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblgvDSlNo2" runat="server" Font-Bold="True" 
                                                           Style="text-align: right" 
                                                           Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                   </ItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Buyer">
                                                   <ItemTemplate>
                                                      

                                                               <asp:HyperLink ID="hlnkgvbuyeroerlcsum" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>'
                                            Width="100px"></asp:HyperLink>

                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Export LC">
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFtxttotallcsum" runat="server" Font-Bold="True" 
                                                           Font-Size="12px" ForeColor="#000" Style="text-align: right">Grand Total</asp:Label>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvlcnameoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcname")) %>' 
                                                           Width="150px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                                   <FooterStyle HorizontalAlign="right" />
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Export Status">
                                                 
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvlcstatus" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcstatus")) %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                                   <FooterStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="LC Date">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvlcdateoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdate")) %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Order No">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvorderoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>' 
                                                           Width="150px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Job #">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvjobnooerlcsum" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "job")) %>' 
                                                           Width="80px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Ship Date">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvshipdateoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdate")) %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <ItemStyle HorizontalAlign="Left" />
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="L/C Value">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvordramtoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcval")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFordramtlcsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Exported">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvexpamtoerlcsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFexpamtlcsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>



                                                <asp:TemplateField HeaderText="UnExported">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvexbalsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrbal")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFexbalsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="Realization">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvrealizesum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzamt")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFrealizesum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Short Realization">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvsrealizesum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipbal")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFsrealizesum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="FC Held">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvfcheldsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheld")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFfcheldsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="BBLC Value">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvbblctotallcsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrval")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFbblcTotalsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="App. Value">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvbblcrcvsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFbblcrcvsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>


                                               
                                               <asp:TemplateField HeaderText="Paid">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvbblcpaymentsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFbblcpaymentsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Unpaid">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgvlcpayablesum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payable")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFlcpayablesum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>
                                              
                                             
                                              <asp:TemplateField HeaderText="Excess/Short FC Held">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lgveosfcheldsum" runat="server" 
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eosfcheld")).ToString("#,##0;(#,##0); ") %>' 
                                                           Width="70px"></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:Label ID="lgvFeosfcheldsum" runat="server" Font-Bold="True" Font-Size="12px" 
                                                           ForeColor="#000" Style="text-align: right"></asp:Label>
                                                   </FooterTemplate>
                                                   <FooterStyle HorizontalAlign="Right" />
                                                   <ItemStyle HorizontalAlign="right" />
                                               </asp:TemplateField>
                                               
                                           </Columns>
                                         <FooterStyle CssClass="grvFooter"/>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                       </asp:GridView>
                                    </asp:View>

                     <asp:View ID="ViewBgdLCStatus" runat="server">
                                        <asp:GridView ID="gvbgdLcStatus" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvbgdLcStatus_RowDataBound" ShowFooter="True">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialno1" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterText="Total" HeaderText=" Description">
                                                    <HeaderTemplate>
                                                        <table style="width: 150px">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label31" runat="server" Font-Bold="True" Height="16px" 
                                                                        Text="Description" Width="70px"></asp:Label>
                                                                </td>
                                                                <td class="style60">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtnCdataExellsbgd" runat="server"  CssClass="btn  btn-primary primaryBtn" >Export Exel</asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkgvActDesclsbgd" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" Style="background-color: Transparent;
                                            color: Black;" Target="_blank" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                            Width="180px"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" 
                                                        HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtQtybgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvtQtybgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFordramtlcbgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvordramtlcbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shipment Qty">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFshiqtybgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvshiqtybgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shiqty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short/Excess Shipment">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFseqtybgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvseqtybgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "seqty")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="% of Shipment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvshiperbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shiper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Export Value">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtramtbgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvtramt0" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FOB per DZN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvfobpdzbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fobpdz")).ToString("#,##00.00;(#,##0.00); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R1">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd1" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0;(#,##0); ")%>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd2" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0;(#,##0); ")%>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd3" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R4">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd4" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R5">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd5" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R6">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd6" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r6")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Direct Cost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvtcostbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcost")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtcostlsbgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcmbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cm")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CM per DZN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcmpdzbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmpdz")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="% of CM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcmperbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R7">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd7" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r7")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R8">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd8" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r8")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R9">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd9" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r9")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd9" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd10" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r10")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd10" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd11" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r11")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd11" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd12" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r12")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd12" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="R13">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRlsbgd13" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r13")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFRlsbgd13" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="65px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Cost">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFtoCostlsbgd" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvtoCostlsbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toramt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Position">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFnetpositionlsbgd" runat="server" Font-Bold="True" 
                                                            Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="75px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvnetpositionlsbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="75px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="% of Profit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvnetperbgd" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netper")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvActDescduplsbgd" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" 
                                                        HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvserial1" runat="server" Style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter"/>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                     </asp:View>

                         </asp:MultiView>
                    </div>
                </div>
            </div>



   
    <%--<asp:TemplateField HeaderText="Project Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false" Font-Underline="false" ForeColor="Black"
                                            style="text-align: left" 
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc1").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                        "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim(): "")+"</B>"  + 
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc1")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")%>'
                                            Width="250px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                               </asp:TemplateField>--%>
</asp:Content>
