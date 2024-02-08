<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="SubsistanceBonus.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_89_Pay.SubsistanceBonus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="~/Content/GridViewScrooling.css" rel="stylesheet" type="text/css" />
      
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            var gvpayroll = $('#<%=this.gvCarSub.ClientID %>');

            //gvpayroll.gridviewScroll({
            //    width: 1200,
            //    height: 420,
            //    arrowsize: 30,
            //    railsize: 16,
            //    barsize: 9,
            //    varrowtopimg: "../../Image/arrowvt.png",
            //    varrowbottomimg: "../../Image/arrowvb.png",
            //    harrowleftimg: "../../Image/arrowhl.png",
            //    harrowrightimg: "../../Image/arrowhr.png",
            //    freezesize: 10
            //});
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <%--  <div class="loader"></div> --%>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label18" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="205" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label22" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label24" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Panel ID="dpt" runat="server">
                                            <asp:Label ID="Label25" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                            <asp:DropDownList ID="ddlSection" runat="server" Width="250" CssClass="chzn-select pull-left  inputTxt"></asp:DropDownList>

                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-2 pading5px ">
                                        <asp:Label ID="lbldate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>

                                        <asp:DropDownList ID="ddlyearmon" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px ">
                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click"  Text="Ok"></asp:LinkButton>
                                        </div>
                                    </div>
                                    


                                </div>
                                </div>
                            </fieldset>


                        <asp:GridView ID="gvCarSub" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnPageIndexChanging="gvCarSub_PageIndexChanging"
                                    AutoGenerateColumns="False" PageSize="1200" 
                                    ShowFooter="True" >
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNod5cs" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Emp ID" Visible="False" >
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpIdcs" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="180px" Font-Bold="True" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSectioncs" runat="server" Font-Bold="true" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Card #">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCardnocs" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name & Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpNamecs" runat="server"
                                                    Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br />"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                          
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date of Joining">
                                            <ItemTemplate>
                                                <asp:Label ID="lbglDjoin" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lbglgrade" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grdesc")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cost Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbcostcnt" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "costcnt")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Location" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbjobdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Gross Salary 1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvgsalary1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gsalary")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveded1" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Gross<br>  Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvgsalary" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grsalry1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFleaveded" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Car <br>  Allowance">
                                            <ItemTemplate>

                                                <asp:Label ID="txtgvcarallow" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carallow")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                               
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFcarallow" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Subsistance <br> Allowance">
                                            <ItemTemplate>

 
                                                <asp:Label ID="Label1sub" runat="server" BackColor="Transparent"  
                                                    Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suballowance")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFsuballowance" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Subsistance <br> Basic">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvsubbonus" runat="server" BackColor="Transparent"
                                                           Font-Size="12px" Style="text-align: right"
                                                           Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subbasic")).ToString("#,##0;(#,##0); ") %>'
                                                           Width="80px"></asp:Label>
                                                
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                      
                                         <asp:TemplateField HeaderText="Duration <br>(Days)">
                                            <ItemTemplate>
                                               

                                                <asp:Label ID="gvsubDuration" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus(%)">
                                            <ItemTemplate>
                                                 <asp:TextBox ID="gvpersubbons" runat="server" BackColor="Transparent"
                                                    BorderStyle="Solid" BorderWidth="1" BorderColor="Green" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perbon")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:TextBox>

                                               
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus Amt.">
                                            <ItemTemplate>
                                                  <asp:Label ID="gvsubbons" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bonamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFBonusAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
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

            </ContentTemplate>
          </asp:UpdatePanel>

</asp:Content>

