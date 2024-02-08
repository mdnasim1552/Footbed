<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptHREmpStatus.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptHREmpStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            $('.chzn-select').chosen({ search_contains: true });


        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                
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
                                    <div class="col-md-12">
                                        <asp:RadioButtonList ID="rbtnlst" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl margin5px" Width="100%"
                                            OnSelectedIndexChanged="rbtnlst_SelectedIndexChanged" RepeatColumns="9"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>All Employee </asp:ListItem>
                                            <asp:ListItem>Technical</asp:ListItem>
                                            <asp:ListItem>Nontechnical</asp:ListItem>
                                            <asp:ListItem>Aceadecmic Degree</asp:ListItem>
                                            <asp:ListItem>Retired Employee</asp:ListItem>
                                            <asp:ListItem>Joining Date Wise</asp:ListItem>
                                            <asp:ListItem>Designation Wise</asp:ListItem>
                                            <asp:ListItem>Confirmation Due</asp:ListItem>
                                             <asp:ListItem>Birth Date Wise</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                        <asp:DropDownList ID="ddlSection" runat="server" Width="150" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                                    </div>

                                    <div class="col-md-1 col-sm-1 col-lg-1 pading5px">
                                        
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">ok</asp:LinkButton>
                                    </div>
                                </div>

                                                                 
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" Visible="false" CssClass=" lblTxt lblName ">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" Visible="false" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <div class="col-md-9 pading5px ">
                                        <asp:LinkButton ID="lblimg" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <%--   <asp:Image ID="lblimg" runat="server" Height="16px"
                                            ImageUrl="~/Image/find_images.jpg" Visible="False" Width="16px"
                                            TabIndex="10" />--%>

                                        <asp:TextBox ID="txtDegree" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false" TabIndex="11"></asp:TextBox>
                                        <asp:TextBox ID="txtDesig" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="False"
                                            TabIndex="12"></asp:TextBox>


                                        <asp:Label ID="lblfrmdate" runat="server" Visible="false" CssClass=" smLbl_to">Date</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" Visible="false" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltodate" runat="server" Visible="false" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" Visible="false" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>

                                </div>


                            </div>
                        </fieldset>
                    </div>
                   
                    <div class="row table-responsive">
                        <asp:MultiView ID="MultiView" runat="server">
                            <asp:View ID="EmpStatus" runat="server">
                                       <asp:GridView ID="gvEmpStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpStatus_PageIndexChanging"
                            ShowFooter="True" Width="899px">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCompany" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                            Width="120px" Font-Bold="true"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "departmentname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSection" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Floor Line">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvfline" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fline")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CARD #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgIdCard" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvname" runat="server"
                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvndesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNetSal" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFNetSal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining.Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgJoinDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aca.Qualification">
                                    <ItemTemplate>
                                        <asp:Label ID="lgAcaDeg" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acadeg")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passing Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lgPasYear" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "passyear")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retired Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgretiredDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retiredate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Confirmation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcondateDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "condate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Skill Grade">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSkill" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "skilldesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Blood Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBloodGrp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bldgrpdesc")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Birth Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgdobDate" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dobdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Religion">
                                    <ItemTemplate>
                                        <asp:Label ID="lgdobDate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "religion")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Marital Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lgMaritalStatus" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "maritalstatus")) %>'
                                            Width="80px"></asp:Label>
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
                            </asp:View>
                             <asp:View ID="EmpInfo" runat="server">
                                  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpStatus_PageIndexChanging"
                            ShowFooter="True" Width="899px">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Idcard No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCompany" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empnamebn")) %>'
                                            Width="120px" Font-Bold="true"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionbn")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptnamebn")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Gross Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Basic Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bsal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Medical Allowance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mallow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="House Rent">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hrent")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Food Allowance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "foodallow")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Total Salary">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Father's Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fathnme")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mother's Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mothnme")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Join Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joiningdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Date of Birth">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dob")).ToString("dd-MMM-yyyy")  %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                           <asp:TemplateField HeaderText="merrit. Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrtlstatusbn")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Religion">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "religionbn")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "genderbn")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                          
                                
                                <asp:TemplateField HeaderText="National ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lgProName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nid")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
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
                             </asp:View>
                        </asp:MultiView>
                 
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

