<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SalesRealCertificate.aspx.cs" Inherits="SPEWEB.F_19_EXP.SalesRealCertificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body"  style="min-height: 100px">
                    <div class="row">
                        
                        <div class="col-md-2 ">
                            <asp:Label ID="lblCurDate" runat="server" CssClass="lblTxt lblName" Text="Inv. From Date"></asp:Label>

                            <asp:TextBox ID="TextFdate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="TextFdate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="TextFdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-2 ">
                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to" Text="To Date"></asp:Label>

                            <asp:TextBox ID="TextTodate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="TextTodate_CalendarExtender" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="TextTodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1 ">
                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Type"></asp:Label>

                            <asp:DropDownList ID="ddlRptType" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" Width="76" OnSelectedIndexChanged="ddlpagesize_OnSelectedIndexChanged">
                                <asp:ListItem Value="0">Details</asp:ListItem>
                                <asp:ListItem Value="1">Summary</asp:ListItem>
                                <asp:ListItem Value="2">Buyer Wise</asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 " style="margin-top: 18px">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_OnClick" CssClass="btn btn-sm btn-primary" TabIndex="4"></asp:LinkButton>
                        </div>
                        
                        <div class="col-md-1 ">
                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to ">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" Width="76" OnSelectedIndexChanged="ddlpagesize_OnSelectedIndexChanged">
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
                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body"  style="min-height: 450px">
                    <div class="row mb-5">
                        <div class="table-responsive">

                            <asp:GridView ID="gvProceedRelSheet" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Invoice No." >
                                        <ItemTemplate>
                                            <asp:Label ID="lvlmlccod" runat="server" Font-Bold="True" Style="text-align: right" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))%>' Width="40px"></asp:Label>
                                            <asp:Label ID="lblbuyerid" runat="server" Font-Bold="True" Style="text-align: right" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerid"))%>' Width="40px"></asp:Label>
                                            <asp:Label ID="lblinvno" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno"))%>' Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Buyer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbuyerdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                Width="130"></asp:Label>
                                            <%--  <br />--%>
                                            <%--  <asp:Label ID="lbllcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'
                                                Width="130"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                Width="30"></asp:Label>
                                            <%--  <br />--%>
                                            <%--  <asp:Label ID="lbllcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'
                                                Width="130"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Bill Amount <br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalefc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salefc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Proceeds Received <br>FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvrcvfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Dues">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duesfc")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Margin<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmarginfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marginfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Exvess Margine A/C <br>FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvexmacfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exmacfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ERQ A/C <br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgverqacfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "erqacfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CM<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcmfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bank Charge<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfcbnkcharge" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ex.Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbuyerdesc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invexrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="130"></asp:Label>
                                            <%--  <br />--%>
                                            <%--  <asp:Label ID="lbllcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'
                                                Width="130"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sales Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalesamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salesamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Exchanges Gain/(L0SS) Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsalesamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exgainlossamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Margine A/C Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvmarginamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marginamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Excess MagineA/C <br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvexmacamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exmacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ERQ A/C  Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgverqacamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "erqacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CM  Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcmamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Charge<br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbnkchargeamtt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bnkchargeamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of LC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>

                        </div>
                    </div>

                    <div class="row">
                        <div class="table-responsive">
                            
                            <asp:GridView ID="gvProcdRelCert" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcSl" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Export Form No. 'Exp'" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcExpFrmNo" runat="server" 
                                                Text="" Width="80"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Description On Commodity Exported" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcDescComExp" runat="server" 
                                                Text="" Width="80"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Bill No. & Date And Country Of Destination" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcBillInfo" runat="server" 
                                                Text="" Width="120"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="120" />
                                        <ItemStyle Width="120px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice No." >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcMlccod" runat="server" Font-Bold="True" Style="text-align: right" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))%>' ></asp:Label>
                                            <asp:Label ID="lblgvprcBuyerid" runat="server" Font-Bold="True" Style="text-align: right" Visible="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerid"))%>' ></asp:Label>
                                            <asp:Label ID="lblgvprcInvno" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno"))%>' Width="180px" ></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Width="180px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <table>
                                                <tr><td>Amount Realisation</td></tr>
                                                <tr><td>Foreign <br /> Currency (US$/Euro)</td></tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcAmtRelFC" runat="server" CssClass="text-right" Text='<%# string.Concat( Convert.ToString((DataBinder.Eval(Container.DataItem, "cursymbol"))), " ", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ")) %>'
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <table>
                                                <tr><td>Amount Realisation</td></tr>
                                                <tr><td>Bangladesh <br /> Taka</td></tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcAmtRelBd" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramtbdt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Date Of Realisation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcRelDate" runat="server" 
                                                Width="80"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="1) Freight Paid <br /> 2) Commission <br /> 3) Insurance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcFrtCommIns" runat="server" 
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <table>
                                                <tr><td> Net FOB Value</td></tr>
                                                <tr><td> Foreign <br /> Currency</td></tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcNetFobFc" runat="server" CssClass="text-right" Text='<%# string.Concat( Convert.ToString((DataBinder.Eval(Container.DataItem, "cursymbol"))), " ", Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ")) %>'
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <table>
                                                <tr><td> Net FOB Value</td></tr>
                                                <tr><td> Bangladesh <br /> Taka </td></tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcNetFobBdt" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramtbdt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Date Of Submission <br/> of Triplicate/ <br/> Copies Bangladesh Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcSubmitDate" runat="server" 
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Reference Of The Schedule statement in which transaction has been">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcShdulStatRef" runat="server" 
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Bank Reference">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcBankRef" runat="server" 
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Buyer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcBuyrDsc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                Width="130"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcCurr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                Width="30"></asp:Label>
                                            <%--  <br />--%>
                                            <%--  <asp:Label ID="lbllcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'
                                                Width="130"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Bill Amount <br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcSalefc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salefc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Proceeds Received <br>FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcRcvfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Dues">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcDues" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duesfc")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Margin<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcMrginFc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marginfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Exvess Margine A/C <br>FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcExmacFc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exmacfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ERQ A/C <br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcErqAcFc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "erqacfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CM<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcCmfc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bank Charge<br> FC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcBnkChrg" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ex.Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcExRate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invexrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="130"></asp:Label>
                                            <%--  <br />--%>
                                            <%--  <asp:Label ID="lbllcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'
                                                Width="130"></asp:Label>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Sales Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcSaleAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salesamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Exchanges Gain/(LOSS) Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcExGainLsAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exgainlossamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Margin A/C Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcMarginamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "marginamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Excess Magin A/C <br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcExmacamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exmacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ERQ A/C  Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcErqacamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "erqacamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CM  Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcCmamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bank Charge<br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcBnkChrgAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bnkchargeamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of LC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvprcLcDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

