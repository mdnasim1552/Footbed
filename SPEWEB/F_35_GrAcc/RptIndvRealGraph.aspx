<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptIndvRealGraph.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.RptIndvRealGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>



<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Date "></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">OK</asp:LinkButton>

                                        </div>

                                    </div>
                                </div>


                            </div>
                        </fieldset>
                    </div>
                
                    <div class="row">
                        <div class="col-md-5">
                            <br />
                            <asp:GridView ID="gvOrderrec" runat="server" AutoGenerateColumns="False" 
                        ShowFooter="True" Width="200px" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Order Amount(FC)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvorderamtrec" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Production Amount(FC)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvproamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Export Amount(FC)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvshipamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Realized Amount(FC)">
                                <ItemTemplate>
                                    <asp:Label ID="lgvrealizedamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzamt")).ToString("#,##0;-#,##0; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                    </asp:GridView>


                            </div>
                        <div class="col-md-7">


                            <asp:Panel ID="pnlbarchart" Visible="false" runat="server">
                                 <div id="barchart_material" style="width: 600px; height: 500px;"></div>

                            </asp:Panel>
                           

                        </div>

                        <div style="display:none">
                            <asp:TextBox ID="txOrderamt" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txProduction" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txShipment" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtRealization" runat="server"></asp:TextBox>
                        </div>
                    </div>


                </div>
            </div>










<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ShowComInfo);


        });


        function ShowComInfo() {



            google.charts.load('current', { 'packages': ['bar'] });
            google.charts.setOnLoadCallback(drawChart);


          
            function drawChart() {

                var OrderAmt = this.parseFloat($("#<%=this.txOrderamt.ClientID %>").val());
                var Prod = this.parseInt($("#<%=this.txProduction.ClientID %>").val());
                var Ship = this.parseInt($("#<%=this.txShipment.ClientID %>").val());
                var Realiz = this.parseInt($("#<%=this.txtRealization.ClientID %>").val());
             
                var sdate = document.getElementById('<%=txtDate.ClientID %>').value;
                var edate = document.getElementById('<%=txttodate.ClientID %>').value;

               // var myYYYYmmddDate = sdate(new Date(), 'yyyy-mm-dd');
               

              

                var data = google.visualization.arrayToDataTable([
                  ['Date Range', 'Order', 'Production', 'Shipment', 'Realization'],
                  ['From ' + sdate + " To " + edate, OrderAmt, Prod, Ship, Realiz],

                ]);

                var options = {
                    chart: {
                        //title: 'Graph ',
                        //subtitle: 'Order, Production, Shipment and Realization: From 01-Jan-2014 to 31-jan-2014',
                    },
                    bars: 'vertical ' // Required for Material Bar Charts.
                };

                var chart = new google.charts.Bar(document.getElementById('barchart_material'));

                chart.draw(data, options);
            }
        }
    </script>
    <script type="text/javascript">
        <%-- window.onload = function () {
            setInterval(function () {
                document.getElementById("<%=lbtnOk.ClientID %>").click();
    }, 3000);
};--%>
    </script>
    
    <script src="../Scripts/GoogleChart.js"></script>
</asp:Content>

