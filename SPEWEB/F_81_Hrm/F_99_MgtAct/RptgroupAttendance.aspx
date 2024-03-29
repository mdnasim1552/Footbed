﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptgroupAttendance.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_99_MgtAct.RptgroupAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 pading5px">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">From</asp:Label>
                                <asp:TextBox ID="txtFdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>

                                <cc1:CalendarExtender ID="txtFdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFdate"
                                    PopupButtonID="Image2"></cc1:CalendarExtender>

                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>

                            </div>

                        </div>



                    </div>
                </fieldset>

                <div class="row table-responsive">
                    <div class="col-md-7 col-sm-7 col-lg-7">
                        <asp:GridView ID="gvRptAttn" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvRptAttn_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                              <%--  <asp:TemplateField HeaderText="Dpt Name" Visible="false">
                                    <ItemTemplate>
                                          <asp:Label ID="lbldptcode" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="text-align: left; background-color: Transparent" Target="_blank" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dpt")) %>'
                                            Width="200px"></asp:Label>

                                        
                                    </ItemTemplate>
                                    
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                          <asp:HyperLink ID="hlnkgvcomname" runat="server" BorderColor="#99CCFF" 
                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black" 
                                            Style="text-align: left; background-color: Transparent" Target="_blank" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) %>'
                                            Width="200px"></asp:HyperLink>

                                        
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam")) %>'
                                            Width="200px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Staff">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttStaff" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlstap")).ToString("#,##0;(#,##0); ")  %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="lblgvPresent" Width="50px" runat="server" Target="_blank"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ")  %>'></asp:HyperLink>

                                       
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="lblgvLate" Width="50px" runat="server" Target="_blank"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ")  %>'></asp:HyperLink>

                                       
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Early Leave">
                                    <ItemTemplate>

                                         <asp:HyperLink ID="lblgverLate" Width="50px" runat="server" Target="_blank"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyLev")).ToString("#,##0;(#,##0); ")  %>'></asp:HyperLink>

                                       
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Leave">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="lgvOnleav" Width="50px" runat="server" Target="_blank"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onlev")).ToString("#,##0;(#,##0); ")  %>'></asp:HyperLink>

                                        
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Absent">
                                    <ItemTemplate>
                                         <asp:HyperLink ID="lblgvAbst" Width="50px" runat="server" Target="_blank"  Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ")  %>'></asp:HyperLink>

                                       
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

                    <div class="col-md-5 col-sm-5 col-lg-5">
                        <asp:GridView ID="gvAttPersent" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1pr" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total Staff">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttStaffpr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlstap")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblstaf" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present">
                                    <ItemTemplate>



                                        <asp:Label ID="lblgvPresentpr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present"))==0.00 ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ") :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ") +"%"%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblprs" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late"))==0.00 ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ") :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ") +"%"%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblfotlate" Font-Bold="true" Style="text-align: right" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Early Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEarlyLvpr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyLev"))==0.00 ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyLev")).ToString("#,##0;(#,##0); ") :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyLev")).ToString("#,##0;(#,##0); ") +"%"%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbleleave" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOnleavpr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onlev"))==0.00 ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onlev")).ToString("#,##0;(#,##0); ") :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onlev")).ToString("#,##0;(#,##0); ") +"%"%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblol" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAbstpr" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt"))==0.00 ? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ") :Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ") +"%"%>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblabs" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
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
                </div>
            </div>

            <div style="display: none">

                <asp:TextBox ID="txtcomp1" runat="server"></asp:TextBox>


                <asp:TextBox ID="txtpresent" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtlate" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtearlylev" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtonleave" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtabsent" runat="server"></asp:TextBox>
            </div>
            <div class="row">
              
                    <div id="donutchart1" class="col-sm-4 col-md-4 col-lg-4" style="height:250px" ></div>
                    <div id="donutchart2" class="col-sm-4 col-md-4 col-lg-4" style="height:250px"></div>
                    <div id="donutchart3" class="col-sm-4 col-md-4 col-lg-4" style="height:250px"></div>
                    <div id="donutchart4" class="col-sm-4 col-md-4 col-lg-4" style="height:250px"></div>
                    <div id="donutchart5" class="col-sm-4 col-md-4 col-lg-4" style="height:250px"></div>

             

            </div>
        </div>
    </div>


  

    <%--<script type="text/javascript">
                                google.charts.load("current", { packages: ["corechart"] });
                                google.charts.setOnLoadCallback(drawChart);

                                function drawChart() {

                                    var p =parseInt($("#<%=this.txtpresent.ClientID %>").val());
                                    var l = parseInt($("#<%=this.txtlate.ClientID %>").val());
                                    var el = parseInt($("#<%=this.txtearlylev.ClientID %>").val());
                                    var ol = parseInt($("#<%=this.txtonleave.ClientID %>").val());
                                    var ab = parseInt($("#<%=this.txtabsent.ClientID %>").val());
                                    
                                    //alert(p+','+l+','+el+','+ol+','+ab+',');

                                    var data = google.visualization.arrayToDataTable([

                                     ['Task', 'Present per Day'],
                                      //['TotalStaf', 52],
                                      ['Present', p],
                                      ['Late', l],
                                      ['Early Leave', el],
                                      ['On Leave', ol],
                                      ['Absent', ab]
                                    ]);

                                    var options = {
                                        title: 'Group Daily Attendance',
                                        is3D: true,                                       
                                                    
                                        pieStartAngle: 100,
                                    };

                                    var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
                                    chart.draw(data, options);
                                }
                            </script>--%>

    <script src="../../Scripts/GoogleChart.js"></script>
        
    <script src="../../Scripts/jsapi.js"></script>
    <script src="../../Scripts/uds_api_contents.js"></script>
    <script type="text/javascript">


        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(ShowComInfo);


        });


        function ShowComInfo() {


            google.charts.load("current", { packages: ["corechart"] });
            var i = 1;
            var date = $('#<%=this.txtFdate.ClientID%>').val();

                                    var objcominfo = new RealERPScript();
                                    var lstcominfo = objcominfo.GetCompInf(date);
                                    /*left , Middle and Right UL */
                                    $.each(lstcominfo, function (index, lstcominfo) {

                                      
                                        //var p = parseInt(lstcominfo.present);
                                        //var l = parseInt(lstcominfo.late);
                                        //var el = (lstcominfo.earlyLev);
                                        //var ol = parseInt(lstcominfo.onlev);
                                        //var ab = parseInt(lstcominfo.absnt)



                                        google.charts.setOnLoadCallback(drawChart(lstcominfo, i));
                                        i++;

                                    });


                                }

                                function drawChart(val, i) {

                                    var p = parseInt(val.present);
                                    var l = parseInt(val.late);
                                    var el = parseInt(val.earlyLev);
                                    var ol = parseInt(val.onlev);
                                    var ab = parseInt(val.absnt);
                                



<%--                                    var p = parseInt($("#<%=this.txtpresent.ClientID %>").val());
                                    var l = parseInt($("#<%=this.txtlate.ClientID %>").val());
                                    var el = parseInt($("#<%=this.txtearlylev.ClientID %>").val());
                                    var ol = parseInt($("#<%=this.txtonleave.ClientID %>").val());
                                    var ab = parseInt($("#<%=this.txtabsent.ClientID %>").val());--%>


                                    var data = google.visualization.arrayToDataTable([
                                     ['Task', 'Present per Day'],
                                      ['Present', p],
                                      ['Late', l],
                                      ['Early Leave', el],
                                      ['On Leave', ol],
                                      ['Absent', ab]
                                    ]);

                                    var options = {
                                        title: val.comnam,
                                        pieHole: 0.4,
                                    };


                                    var chart = new google.visualization.PieChart(document.getElementById('donutchart' + i));
                                    chart.draw(data, options);
                                }
    </script>


    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
