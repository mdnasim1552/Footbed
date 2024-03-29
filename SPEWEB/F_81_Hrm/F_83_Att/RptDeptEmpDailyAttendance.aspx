﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptDeptEmpDailyAttendance.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptDeptEmpDailyAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            var gvempattn = $('#<%=this.gvDailyAttn.ClientID %>');
            gvempattn.Scrollable();
            var gvMoLateAttn = $('#<%=this.gvMoLateAttn.ClientID %>');
            gvMoLateAttn.Scrollable();



        }

    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName">To</asp:Label>
                                        <asp:TextBox ID="txttoDate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCompanyName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcDept" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnDeptSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black"
                                        MaxDropDownHeight="200" TabIndex="8" TransitionalMode="True" Width="300px">
                                    </cc1:DropCheck>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol2">
                                        <asp:Label ID="lblfrmDesig" runat="server" CssClass="lblTxt lblName">Form</asp:Label>
                                        <asp:DropDownList ID="ddlfrmDesig" runat="server" Width="100" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="lbltoDesig" runat="server" CssClass=" smLbl_to">To</asp:Label>


                                        <asp:DropDownList ID="ddlToDesig" runat="server" Width="100" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewDailyAttendance" runat="server">
                            <asp:Panel ID="pnlGroup" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblGenerationinfo" runat="server" CssClass="lblTxt lblName">A. General Information</asp:Label>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblLate" runat="server" CssClass="lblTxt lblName160">B. Late</asp:Label>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblOutTime" runat="server" CssClass="lblTxt lblName160">C. Out Time</asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblTotalEmploye" runat="server" CssClass="lblTxt lblName">Total Employee</asp:Label>
                                                <asp:Label ID="label1" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalTotalemployee" runat="server" CssClass="inpPixedWidth"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbllatew5mi" runat="server" CssClass="lblTxt lblName160">Late within 5 Minutes</asp:Label>
                                                <asp:Label ID="label17" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvallatew5mi" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblEarlyLate" runat="server" CssClass="lblTxt lblName160">Early Left(Before 6:30 PM)</asp:Label>
                                                <asp:Label ID="label20" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblValEarlyLate" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblAbsent" runat="server" CssClass="lblTxt lblName">Absent</asp:Label>
                                                <asp:Label ID="label13" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalAbsent" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbllatewi6to10mi" runat="server" CssClass="lblTxt lblName160">Late within 6 to  10 Minutes</asp:Label>
                                                <asp:Label ID="label18" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblVallatewi6to10mi" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbloutw30mi" runat="server" CssClass="lblTxt lblName160">6:30 PM to 7:00 PM</asp:Label>
                                                <asp:Label ID="label21" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblValoutw30mi" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblLeave" runat="server" CssClass="lblTxt lblName">Leave</asp:Label>
                                                <asp:Label ID="label14" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalLeave" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbllate11onwards" runat="server" CssClass="lblTxt lblName160">Late 11 Minutes onwards</asp:Label>
                                                <asp:Label ID="label19" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblVallate11onwards" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbloutw31to60mi" runat="server" CssClass="lblTxt lblName160">7:01 PM to 7:30 PM</asp:Label>
                                                <asp:Label ID="label22" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblValoutw31to60mi" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblPresent" runat="server" CssClass="lblTxt lblName">Present</asp:Label>
                                                <asp:Label ID="label15" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalPresent" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbloutw61to90mi" runat="server" CssClass="lblTxt lblName160">7:31 PM to 8:00 PM</asp:Label>
                                                <asp:Label ID="label23" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblValoutw61to90mi" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblResign" runat="server" CssClass="lblTxt lblName">Resign</asp:Label>
                                                <asp:Label ID="label16" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalResign" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px">
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lbloutw91toabove" runat="server" CssClass="lblTxt lblName160">8:01 PM to Above</asp:Label>
                                                <asp:Label ID="label24" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblValoutw91toabove" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px">
                                            </div>
                                            <div class="col-md-3 pading5px">
                                            </div>
                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblvisit" runat="server" CssClass="lblTxt lblName160">Visit</asp:Label>
                                                <asp:Label ID="label6" runat="server" CssClass="smalldiv">:</asp:Label>
                                                <asp:TextBox ID="lblvalvisit" runat="server" CssClass="inpPixedWidth" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>



                                    </div>
                                </fieldset>



                            </asp:Panel>

                            <div class=" table table-responsive">
                                <asp:GridView ID="gvDailyAttn" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvDailyAttn_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsection" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation &amp; Department ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesginationAndDept" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigadept")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Intime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIntime" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "status")).Trim())=="" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Outtime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvouttime" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "status")).Trim())=="" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlate" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "late")) %>'
                                                    Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "late")).Trim())!="0.00" %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late This Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlatethismon" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcmday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late Last Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlatelastmon" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpmday")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave/absent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleaveorabsent" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "status")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave This Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleavethistmon" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvcurm")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Absent This Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvabsentthismon" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abscurm")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrmrks" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                    Width="120px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
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
                        </asp:View>

                        <asp:View ID="ViewMonthlyLateAttnedance" runat="server">
                            <div class=" table table-responsive">
                                <asp:GridView ID="gvMoLateAttn" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvMoLateAttn_PageIndexChanging" ShowFooter="True" Width="694px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNom" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsectionm" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpIDCardm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation &amp; Department ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesginationAndDeptm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigadept")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Late Day">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlateday" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "today")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="View1" runat="server">
                            <div class=" table table-responsive">
                                <asp:GridView ID="gvDailyOvr" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvDailyOvr_PageIndexChanging"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoov" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsectionov" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Card #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpIDCardov" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation &amp; Department ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesginationAndDeptov" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigadept")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Intime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIntimeov" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Outtime">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvouttimeov" runat="server" Height="16px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                    Width="60px"></asp:Label>
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
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


