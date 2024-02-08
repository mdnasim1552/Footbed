<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptProtarVsAchieve.aspx.cs" Inherits="SPEWEB.F_15_Pro.RptProtarVsAchieve" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv1 = $('#<%=this.RptIndPro.ClientID %>');
            gv1.Scrollable();
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Text="From:"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lbltodate" runat="server" CssClass="label" Text="To:"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px;" OnClick="lbtnShow_Click" TabIndex="8">Show</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblGraph" runat="server" CssClass="label" Visible="false"></asp:Label>
                                <asp:CheckBox ID="chkGraph" runat="server" CssClass="checkbox" Text="Graph" Visible="False" />
                                <asp:Label ID="lblRpType" runat="server" CssClass="label" Visible="false">Report Type</asp:Label>
                                <asp:DropDownList ID="DdlRpType" runat="server" CssClass="form-control form-control-sm" Visible="false">
                                    <asp:ListItem Value="Details">Details</asp:ListItem>
                                    <asp:ListItem Value="Summary">Summary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">

                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="gvtvsach" runat="server" AllowPaging="false" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvtvsach_RowDataBound" ShowFooter="True" Width="501px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo8" runat="server" CssClass="lblName lblTxt"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTardate" runat="server" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "linedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Buyer">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbuyer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvorderno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc"))%>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvstyle" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Capacity">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcapacity" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Works Hour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvwhour" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whour")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtarget" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "protqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Production">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvproduction" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proacqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short/Excess">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsorexcess" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "soexqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hourly Production">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvhproduction" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hproqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lay of <br> M/C">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmacno" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "layofmac")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </asp:View>

                            <asp:View ID="View2" runat="server">


                                <asp:Panel ID="Panel3" runat="server">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">

                                            <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"
                                                Text="Order No :"></asp:Label>

                                            <asp:TextBox ID="txtpmlcsrch" runat="server" AutoCompleteType="Disabled"
                                                CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindPMlc" runat="server" CssClass="glyphicon" Visible="false"
                                                OnClick="imgbtnFindPMlc_Click"
                                                Width="16px" />

                                            <asp:DropDownList ID="ddlMLc" runat="server" CssClass="ddlPage"
                                                Width="320px">
                                            </asp:DropDownList>


                                        </div>
                                    </div>



                                </asp:Panel>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <asp:GridView ID="RptIndPro" runat="server" AllowPaging="false"
                                        AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="315px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo36" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdate36" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFProqtyTTL" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prod. Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgvortarget" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFortarget" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="10px" ForeColor="Black" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Export Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcapacity37" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFProqty2" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prod </br>  Achiev.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproqty36" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFProqty1" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Export </br>  Achiev.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvexpqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFexpqty" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Achiev. </br>  in % on</br>  Capacity">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproCapPersentAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroncap")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoproCapPersent" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Achiev. </br>  in %  on </br> Target">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproTarPersent" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoproTarPersentAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br> Orginal </br> Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcumortarget" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comppqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br> Working </br> Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproduction36" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br> Achiev.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtarget36" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comproqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cum </br> Export">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcuexpqty" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cuexpqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
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

                                <div class="col-md-6 ">

                                    <asp:Chart ID="Chart1" runat="server" Height="264px" Width="663px">
                                        <Series>

                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="yellow"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Production Target" MarkerSize="4">
                                            </asp:Series>

                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Export Target" MarkerSize="4">
                                            </asp:Series>
                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Prod Achiev" MarkerSize="4">
                                            </asp:Series>
                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="#ff3300"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Export Achiev" MarkerSize="4">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY Title="Amount">
                                                </AxisY>

                                            </asp:ChartArea>
                                        </ChartAreas>
                                        <Titles>
                                            <asp:Title Font="Time New Romans, 16px" Name="Title1"
                                                Text="Producion Status (Order Wise)">
                                            </asp:Title>
                                        </Titles>
                                        <Legends>
                                            <asp:Legend
                                                Name="Legend1"
                                                BackColor="AliceBlue"
                                                ForeColor="CadetBlue"
                                                BorderColor="LightBlue"
                                                Docking="Bottom"
                                                Alignment="Center">
                                            </asp:Legend>
                                        </Legends>
                                    </asp:Chart>

                                </div>

                            </asp:View>

                            <asp:View ID="View3" runat="server">
                                <div class="col-md-6">

                                    <asp:GridView ID="RptAllPro" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True" Width="315px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoAll" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdateAll" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFProqtyTTLAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="40px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Capacity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCapacity1" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvCapacityAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Target </br> Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTargetQunAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoTargetQunAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Achieve</br>ment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproqtyAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFoProqtyAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Achiev</br> in % on </br>capacity">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproCapPersentAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peroncap")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoproCapPersentAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Achiev.</br> in %  on</br> Target">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvproTarPersentAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontar")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFoproTarPersentAll" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="White" Style="text-align: right"
                                                        Width="50px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br>Capacity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCumCap" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comcap")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br>Target">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCumproductionAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comtqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cum </br>Produc</br>tion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCumProdAll" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comproqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
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
                                <div class="col-md-6" style="margin-left: -35px;">

                                    <asp:Chart ID="Chart2" runat="server" Height="264px" Width="663px">
                                        <Series>
                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="green"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Capacity" MarkerSize="4">
                                            </asp:Series>

                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="red"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Production Quantity" MarkerSize="4">
                                            </asp:Series>

                                            <asp:Series ChartArea="ChartArea1" ChartType="Line" Color="blue"
                                                MarkerColor="black" MarkerStyle="Circle" Name="Cum Quantity" MarkerSize="4">
                                            </asp:Series>
                                        </Series>
                                        <Titles>
                                            <asp:Title Font="Time New Romans, 16px" Name="Title1"
                                                Text="All Producion Status (Order Wise)">
                                            </asp:Title>

                                        </Titles>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1">
                                                <AxisY Title="Amount">
                                                </AxisY>
                                                <InnerPlotPosition X="10" Y="0" Height="90" Width="90" />
                                                <AxisY LineColor="#CACACA">

                                                    <MajorGrid Enabled="true" LineColor="#CACACA" />
                                                    <MajorTickMark Enabled="true" />
                                                    <MinorGrid LineWidth="1" Enabled="true" LineColor="#CACACA" />
                                                </AxisY>
                                                <AxisX LineColor="#CACACA">

                                                    <MinorGrid LineWidth="1" Enabled="true" LineColor="#CACACA" />
                                                    <MajorGrid Enabled="true" LineColor="#CACACA" />
                                                    <MajorTickMark Enabled="true" LineColor="#CACACA" />
                                                </AxisX>
                                            </asp:ChartArea>
                                        </ChartAreas>

                                        <Legends>
                                            <asp:Legend
                                                Name="Legend1"
                                                BackColor="AliceBlue"
                                                ForeColor="CadetBlue"
                                                BorderColor="LightBlue"
                                                Docking="Bottom">
                                            </asp:Legend>
                                        </Legends>
                                    </asp:Chart>
                                </div>

                            </asp:View>
                            <asp:View ID="DetailsBgd" runat="server">
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

