<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMyInterface.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptMyInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">





    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewServices" runat="server">
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="log-divider" id="lblServHeadh" runat="server">
                        <span>
                            <i class="fa fa-fw fa-dollar-sign"></i>SERVICE HISTORY</span>
                    </div>
                    <div class="row">

                        <div class="col-md-9 col-sm-9 col-lg-9 ">

                            <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="678px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Increment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                             <a href="<%= this.ResolveUrl("~") %>" class="user-avatar user-avatar-xxl my-3">
                                                        <asp:Image ID="EmpUserImg" runat="server" />
                                                    </a>


                           <%-- <p>
                                <asp:Image ID="EmpUserImg" runat="server" Visible="false" ImageUrl="~/Image/empImg.png" Height="105" Width="105" CssClass="img-thumbnail img-responsive" />
                            </p>--%>

                            <asp:LinkButton ID="hyplPreviewCv" CssClass="btn btn-success  btn-circle" Visible="false" runat="server" OnClick="hyplPreviewCv_Click1"> View Profile <i class="fa fa-search fa-spin"></i> </asp:LinkButton>


                        </div>

                    </div>


                    <div class="log-divider" id="lbAttHeadh" runat="server">
                        <span>
                            <i class="fa fa-fw fa-dollar-sign"></i>ATTENDANCE HISTORY</span>
                    </div>


                    <div class="row">

                        <div class="col-md-5 col-sm-5 col-lg-5 ">

                            <asp:Repeater ID="RptAttHistroy" runat="server" OnItemDataBound="RptAttHistroy_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="table-striped table-hover table-bordered grvContentarea grvHeader grvFooter" style="width: 350px;">
                                        <tr>
                                            <th>Month </th>
                                            <th>Intime</th>
                                            <th>Late </th>
                                            <th>Absent </th>
                                            <th>Leave </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 80px">
                                            <asp:Label ID="lblymonid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymonid")).ToString() %>'></asp:Label>

                                            <asp:HyperLink ID="hlnkbtnadd" runat="server" Target="_blank" Text='<%# Eval("yearmon") %>'></asp:HyperLink>

                                        </td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lblacintime" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acintime")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                        </td>

                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lblLate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aclate")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                        </td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lblAbsent" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                        </td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lblLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td style="width: 80px">
                                            <asp:Label ID="ttl" runat="server" CssClass=" smLbl_to" Text="Total"></asp:Label></td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lblacintime" runat="server" Style="text-align: right"></asp:Label>
                                        </td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lbltotallate" runat="server" Style="text-align: right"></asp:Label></td>
                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lbltotalabs" runat="server" Style="text-align: right"></asp:Label>
                                        </td>

                                        <td style="width: 80px; text-align: right !important;">
                                            <asp:Label ID="lbltotalleave" runat="server" Style="text-align: right"></asp:Label></td>

                                    </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>


                        <div class="col-md-7 col-sm-7 col-lg-7 ">
                            <asp:Chart ID="AttHistoryGraph" runat="server" Visible="false" Width="750px" Height="300px">
                                <Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#008000" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Actual in Time" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#029ACF" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Late" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#FF2A2D" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Absent" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                    <asp:Series ChartArea="ChartArea1" Color="#4E69A2" IsValueShownAsLabel="true"
                                        MarkerColor="black" MarkerStyle="None" Name="Leave" MarkerSize="4" YValuesPerPoint="6">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisX MaximumAutoSize="100" Interval="1">
                                            <MajorGrid Enabled="False" />

                                        </AxisX>
                                        <AxisY>
                                            <MajorGrid Enabled="False" />
                                        </AxisY>

                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend></asp:Legend>
                                </Legends>
                            </asp:Chart>
                        </div>
                    </div>



                    <div class="log-divider" id="Div1" runat="server">
                        <span>
                            <i class="fa fa-fw fa-dollar-sign"></i>LEAVE HISTORY & JOB RESPONSIBILITIES</span>
                    </div>


                    <div class="row">

                        <div class="col-md-7 col-sm-7 col-lg-7 ">
                            <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                Width="600px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desription">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblgvDescr" runat="server" Text='Desription'></asp:Label>

                                            <asp:HyperLink ID="hlnkbtnNext" runat="server" NavigateUrl="#" Target="_blank" CssClass="btn btn-primary primaryBtn pull-right" Text="Next">

                                            </asp:HyperLink>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="120px"></asp:Label>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entitlement">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Availed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlentitled01" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Last Leave End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Leave Day's">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
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


                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="400px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode1" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Responsibilities">
                                        <ItemTemplate>

                                            <asp:Label ID="lgvgval1Job" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobresp")) %>'></asp:Label>

                                        </ItemTemplate>



                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgval1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                        </ItemTemplate>
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
            </div>







        </asp:View>









    </asp:MultiView>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>
</asp:Content>

