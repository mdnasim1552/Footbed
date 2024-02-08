<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkRealInOutFlow.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkRealInOutFlow" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gvMonPayment = $('#<%=this.gvMonPayment.ClientID %>');

            
            gvMonPayment.Scrollable();



        }

    </script>

                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                 <div class="contentPart">
                        <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel1" runat="server">
                                       <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">

                                             <asp:Label ID="lblFdate" runat="server" CssClass=" lblName lblTxt" Text="From:"></asp:Label>

                                             <asp:TextBox ID="txtfromdate" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                              <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtfromdate"> </cc1:CalendarExtender>

                                             <asp:Label ID="lblTdate" runat="server" CssClass=" lblName  smLbl_to" Text="To:" ></asp:Label>

                                             <asp:TextBox ID="txttodate" runat="server"  CssClass="inputtextbox" style="margin-left:-69px;"></asp:TextBox>
                                             <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txttodate"> </cc1:CalendarExtender>

                                            <asp:Label ID="lblPage" runat="server" CssClass=" lblName  smLbl_to" Text="Page Size:"></asp:Label>

                                                   <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"  onselectedindexchanged="ddlpagesize_SelectedIndexChanged"  CssClass=" ddlPage" style="margin-left:-35px;">
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

                                             <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click"   TabIndex="8">Ok</asp:LinkButton>


                                                </div>
                                           </div>
                                  </asp:Panel>
                                 <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">
                                                    <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="Tk in Crore"   ChartHeight="450" ChartType="Column" ChartWidth="1430">  </cc1:BarChart></td>                                            
                                                </div>
                                           </div>
                                </div>
                            </fieldset>
                        </div>
                      <div class="table table-responsive">
                             <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View runat="Server" ID="ViewPaySummary">
                                    <asp:GridView ID="gvMonPayment" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                 ShowFooter="True" Width="616px" onrowdatabound="gvMonPayment_RowDataBound">
                                                
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="Sl. No.">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblgvSlNomonpay" runat="server" Font-Bold="True" 
                                                                 style="text-align: right" 
                                                                 Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText=" Description">
                                                         
                                                         
                                                          <ItemTemplate>
                                                                <asp:Label ID="lgvActdescmpay" runat="server" 
                                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flowdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "flowdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "flowdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")%>'
                                                                                Width="300px"></asp:Label>
                                                                           </ItemTemplate>
                                                         
                                                         
                                                         <ItemStyle HorizontalAlign="Left" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />




                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Total Amount">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvtoamtmpay" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="75px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFtoamtmpay" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt1">
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay1" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay1" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt2">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay2" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay2" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt3">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay3" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay3" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt4">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay4" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay4" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt5">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay5" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay5" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt6">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay6" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt7">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay7" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt8">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay8" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay8" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt9">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay9" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay9" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt10">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay10" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay10" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt11">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay11" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay11" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
                                                         <ItemStyle HorizontalAlign="Right" />
                                                         <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="amt12">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lgvamtmpay12" runat="server" style="text-align: right" 
                                                                 Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>' 
                                                                 Width="70px"></asp:Label>
                                                         </ItemTemplate>
                                                         <FooterTemplate>
                                                             <asp:Label ID="lgvFamtmpay12" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                 ForeColor="#000" style="text-align: right" Width="80px"></asp:Label>
                                                         </FooterTemplate>
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

                        </asp:MultiView>

                      </div>
                     </div>
                </div>






                                <%--<tr>
                                    <td class="style29" width="100px">
                                        <asp:Label ID="lblFdate" runat="server" CssClass="style30" Font-Bold="True" 
                                            Font-Size="12px" style="text-align: left" Text="From :" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderColor="#660033" 
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" 
                                            Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " 
                                            TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style65">
                                        <asp:Label ID="lblTdate" runat="server" CssClass="style30" Font-Bold="True" 
                                            Font-Size="12px" Text="To:"></asp:Label>
                                    </td>
                                    <td align="left" class="style64">
                                        <asp:TextBox ID="txttodate" runat="server" BorderColor="#660033" 
                                            BorderStyle="Solid" BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" 
                                            TabIndex="1" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy" 
                                            TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="left" class="style64">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" style="color: #FFFFFF; text-align: right;" Text="Page Size:" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td align="left" class="style66">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                            style="margin-left: 0px" TabIndex="2" Width="85px">
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
                                    <td class="style4">
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                                            BorderColor="#000" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" onclick="lbtnOk_Click" 
                                            style="color: #FFFFFF; text-align: center;" TabIndex="5" Width="29px">Ok</asp:LinkButton>
                                    </td>
                                    <td class="style69">
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
                                </tr>--%>


                <%--<tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="Tk in Crore"   ChartHeight="450" ChartType="Column" ChartWidth="1430">  </cc1:BarChart></td>
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
                </tr>--%>


               
                     
                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

