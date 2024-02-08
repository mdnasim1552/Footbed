<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="DailtyAttenManually.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.DailtyAttenManually" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div class="container moduleItemWrpper">
               <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Visible="false">To</asp:Label>
                                        <asp:TextBox ID="txtdateto" runat="server" CssClass=" inputDateBox"  Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>

                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenUpload.aspx")%>">Auto</a>

                                    </div>
                                    <div class="col-md-2 pull-right hidden ">
                                        <a href="#" class="btn btn-info primaryBtn margin5px" onclick="history.go(-1)">Back</a>
                                        <a class="btn btn-info primaryBtn margin5px" href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>">Next</a>

                                    </div>

                                    <div class="col-md-5">
                                          <asp:RadioButtonList ID="rbtnAtten" runat="server" AutoPostBack="True"
                                            BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                            Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                          
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="29001" >Manual Attendance</asp:ListItem>
                                            <asp:ListItem Value="28001">Machine  Attendance</asp:ListItem>
                                              <asp:ListItem Value="00000">Both Attendance </asp:ListItem>
                                              </asp:RadioButtonList>
                                    </div>

                                </div>

                                 <div class="form-group">

                                      <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="205" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                      <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                      <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="250" CssClass="chzn-select pull-left  inputTxt" ></asp:DropDownList>

                                    </div>

                                     
                                    
                                </div>


                                <div class="form-group">
                                    <%--<div class="col-md-3 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn hidden" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    
                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="210" CssClass="form-control inputTxt pull-left chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        
                                    </div>--%>

                                 
<%--                                    <div class="col-md-3 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn hidden" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Width="215" CssClass="form-control inputTxt chzn-select" TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>--%>

                                
                                   <%-- <div class="col-md-3 pading5px ">
                                        <asp:Label ID="Label4" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                        <asp:TextBox ID="txtsrchsection" runat="server" CssClass="inputTxt inputName inpPixedWidth hidden"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnSection" runat="server" CssClass="btn btn-primary srearchBtn hidden" OnClick="lbtnSection_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    
                                        <asp:DropDownList ID="ddlsection" runat="server" Width="215" CssClass="form-control inputTxt chzn-select" TabIndex="7">
                                        </asp:DropDownList>
                                    </div>--%>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" >
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
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl">Code</asp:Label>
                                        <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    
                                     <asp:LinkButton ID="lFinalUpdatedwise" runat="server" CssClass="btn btn-danger primaryBtn"  Visible="false">Final Update</asp:LinkButton>
                                    <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                                        </div>
                                    </div>
                                     

                                </div>
                            </div>
                        </fieldset>


    <div class="row table-responsive">
                        <asp:GridView ID="gvDailyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True"  CssClass="table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsection" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderTemplate>
                                          <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Resource Description" Width="120"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-success" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
        </div>
                        </div>
                   </div>
      
       </div>
</asp:Content>

