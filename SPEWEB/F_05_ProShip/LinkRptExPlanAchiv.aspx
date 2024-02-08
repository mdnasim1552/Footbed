<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkRptExPlanAchiv.aspx.cs" Inherits="SPEWEB.F_05_ProShip.LinkRptExPlanAchiv" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="Label7" runat="server" Text="Order No:" CssClass="lblName lblTxt"></asp:Label>
                                            <asp:Label ID="lblLCName" runat="server" CssClass="inputtextbox" Width="240px"></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-10  pading5px  asitCol10">
                                                <asp:Label ID="lblPage" runat="server" Text="Page Size" Visible="False" CssClass="lblName lblTxt"></asp:Label>
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" Width="72">
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:Label ID="Label12" runat="server" CssClass=" lblName lblTxt" Text="Date:"></asp:Label>
                                                <asp:Label ID="lblAsDate" runat="server" CssClass="inputtextbox"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelExPlan" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">
                                            <asp:Label ID="Label11" runat="server" Text="Starting Date:" CssClass="lblName lblTxt"></asp:Label>
                                            <asp:TextBox ID="txtCurStartDate" runat="server" ToolTip="(dd.mm.yyyy)" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurStartDate_CalendarExtender" runat="server" Format="dd.MM.yyyy"
                                                TargetControlID="txtCurStartDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label10" runat="server" CssClass="lblName lblTxt" Text="Ending Date:" ></asp:Label>
                                            <asp:TextBox ID="txtCurEndDate" runat="server" ToolTip="(dd.mm.yyyy)" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurEndDate_CalendarExtender" runat="server" Format="dd.MM.yyyy"
                                                TargetControlID="txtCurEndDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblOrderDetails" runat="server" CssClass="btn btn-primary primaryBtn" Text="A. Order Details" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvOrDer" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="162px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo10" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style Name">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotalord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Text="Total" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvressdescord" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunitord" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderqtyord" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderqtyord" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Rate(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderrateord" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderamtord" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordramtord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblpbeforeproduction" runat="server" CssClass="btn btn-primary primaryBtn" Text="B. Planning Before Production" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvLcpjInfo" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Style="margin-right: 0px" Width="16px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotallcpinfo" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" Text="Total" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Planning Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPlnDate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "plndate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoplntime" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Achieved Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAchievedDate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Font-Size="11px" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "achivedate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoadchtime" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delay">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdelay" runat="server" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delday")).ToString("#,##0;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdelday" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="50px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Planning Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcPlnAmt" runat="server" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "plnamt")).ToString("#,##0;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Achieved">
                                    <ItemTemplate>
                                        <asp:Label ID="lgachAmt" runat="server" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "achamt")).ToString("#,##0;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblacbeforepro" runat="server" CssClass="btn btn-primary primaryBtn" Text="C. Achievement Before Production" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvachieved" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="501px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBLC #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgbblcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgordrdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgrcvdate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Materials Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgMatDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Order Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvorderamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFordramt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrcvQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Received Amount(FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrcvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFrcvamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short/Excess">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsoexQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soexqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblpvsacastoday" runat="server" CssClass="btn btn-primary primaryBtn" Text="D. Production &amp; Shipment As of Today" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvexpastoday" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="523px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment No">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPNoat" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipmentno")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Plan">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFProPlnQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProPlqtyat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proplanqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvworktargetat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFworktarget" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Achieved">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProqtyat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFProqty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFProPar" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>--%>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFproachieved" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProPerat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercent")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPeroProPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPlqtyat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipplnqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFShipPlan" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Acheived">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiQtyat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFShiQty" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFShiPar" runat="server" Font-Bold="True" Font-Size="12px" 
                                                                            ForeColor="#000" style="text-align: right;"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>--%>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFShipAchieved" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPerat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shippercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFPeroShipPlan" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblpvsac" runat="server" CssClass="btn btn-primary primaryBtn" Text="F. Export Documentation" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvRptExPlnAch" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvRptExPlnAch_PageIndexChanging" OnRowDataBound="gvRptExPlnAch_RowDataBound"
                            ShowFooter="True" Width="450px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date Range">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOrdDat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "plandate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Total"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFTotalPar" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Text="Percentage"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPNo" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipmentno")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shipment Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPlqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipplnqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Acheived">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFShiQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFShiPar" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPer" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shippercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Production Plan">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProPlqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proplanqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvworktarget" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production Achieved">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFProqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFProPar" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Production %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvProPer" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propercent")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shipment Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvShiPType" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipdesc")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lbldocumentation" runat="server" CssClass="btn btn-primary primaryBtn" Text="F. Export Documentation" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvExport_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvExSlNo" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvressdescexp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblExportRealization" runat="server" CssClass="btn btn-primary primaryBtn" Text="G. Export Realization" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvshipvsrlz" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="16px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinvno" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvexportdate" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipmdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFTotalrlz" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Text="Total" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvTotalBal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Text="Balance" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>



                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvexportamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFexportamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvNone" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px" Text="  "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>


                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Realized Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrlzdate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Realized Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrlzamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFrlzamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFBalamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Variance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvvaramt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "varamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFvaramt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFBnone2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px" Text=" "></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblBBlCduestatus" runat="server" CssClass="btn btn-primary primaryBtn" Text="H. BBLC  Due Status" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvduebblc" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBLC  Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvressdescduebblc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotalduebblc" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Text="Total" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Settlement Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsettlementdate" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "settldat")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbillamtdbblc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbillamtdbblc" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueamtdbblc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdueamtdbblc" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Not Yet Due">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvnydueamtdbblc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nydueamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnydueamtdbblc" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                    <div class="form-group">
                        <div class="col-md-10  pading5px  asitCol10">
                            <asp:Label ID="lblIncomeSt" runat="server" CssClass="btn btn-primary primaryBtn" Text="I. IncomeStatement" Visible="False" Width="300px"></asp:Label>
                        </div>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvinstment" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvinstment_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" Width="193px">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItemDesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="250px">


                                                               
  
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                
                                                                    
                                                                    
                                                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Amount (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBudgetedFC" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Amount (FC) ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Variance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBalAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Variance %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvariance" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "variance")).ToString("#,##0.00;(#,##0.00); ")+"%" %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </div>

                </div>
            </div>

            <%--<tr>
                                    <td class="style35" style="text-align: left">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="14px" Style="text-align: right; color: #FFFFFF;"
                                            Text="Order No:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style36" style="text-align: left">&nbsp;</td>
                                    <td class="style44" style="text-align: left">&nbsp;</td>
                                    <td class="style67" colspan="2" style="text-align: left">
                                        <asp:Label ID="lblLCName" runat="server" BackColor="#000066"
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Yellow" Width="300px"></asp:Label>
                                    </td>
                                    <td class="style67" colspan="2" style="text-align: left">&nbsp;</td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style35" style="text-align: left">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px" ForeColor="#993300"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size" Visible="False" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style36" style="text-align: left">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                            Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Visible="False">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style44" style="text-align: left">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: left" Text="Date:"></asp:Label>
                                    </td>
                                    <td class="style68" style="text-align: left">
                                        <asp:Label ID="lblAsDate" runat="server" BackColor="#000066"
                                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style62" colspan="2" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style62" style="text-align: left">&nbsp;
                                    </td>
                                </tr>--%>
            <%--<tr>
                                    <td class="style73" style="text-align: left">
                                        <asp:Label ID="Label11" runat="server" CssClass="style16" Font-Bold="True" Font-Size="12px"
                                            Height="16px" Style="text-align: right" Text="Starting Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style74" style="text-align: left">
                                        <asp:TextBox ID="txtCurStartDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" Width="85px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurStartDate_CalendarExtender" runat="server" Format="dd.MM.yyyy"
                                            TargetControlID="txtCurStartDate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style75" style="text-align: left">
                                        <asp:Label ID="Label10" runat="server" CssClass="style16" Font-Bold="True" Font-Size="12px"
                                            Text="Ending Date:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style35" style="text-align: left">
                                        <asp:TextBox ID="txtCurEndDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" Width="85px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurEndDate_CalendarExtender" runat="server" Format="dd.MM.yyyy"
                                            TargetControlID="txtCurEndDate"></cc1:CalendarExtender>
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style35" style="text-align: left">&nbsp;
                                    </td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                    <td class="style69" style="text-align: left"></td>
                                </tr>--%>
            <%-- <asp:Label ID="lblOrderDetails" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="A. Order Details" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lblpbeforeproduction" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="B. Planning Before Production" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lblacbeforepro" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="C. Achievement Before Production" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lblpvsacastoday" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="D. Production &amp; Shipment As of Today" Visible="False" Width="300px"></asp:Label>--%>
            <%--<asp:Label ID="lblpvsac" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="E. Production &amp; Shipment Details" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lbldocumentation" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="F. Export Documentation" Visible="False" Width="300px"></asp:Label>--%>
            <%--<asp:Label ID="lblExportRealization" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="G. Export Realization" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lblBBlCduestatus" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="H. BBLC  Due Status" Visible="False" Width="300px"></asp:Label>--%>
            <%-- <asp:Label ID="lblIncomeSt" runat="server" BackColor="#000066" BorderColor="Yellow"
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow"
                        Text="I. IncomeStatement" Visible="False" Width="300px"></asp:Label>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
