﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptGroupTask.aspx.cs" Inherits="SPEWEB.F_34_Mgt.RptGroupTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
              
                function openModal() {
                    //    $('#myModal').modal('show');
                    $('#myModal').modal('toggle');
                }
                $(document).ready(function (e) {
                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                  
                    $('.search-panel .dropdown-menu').find('a').click(function (e) {
                        e.preventDefault();
                        var param = $(this).attr("href").replace("#", "");
                        var concept = $(this).text();
                        $('.search-panel span#search_concept').text(concept);
                        $('.input-group #search_param').val(param);
                    });
                });


                function pageLoaded() {
                    $('.chzn-select').chosen({ search_contains: true });
                }
            </script>
            <style>
                p {
                    margin: 0;
                }

                .h1, .h2, .h3, h1, h2, h3 {
                    /* margin-top: 20px; */
                    margin: 0;
                }
            </style>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <asp:Panel ID="firstFilter" runat="server">
                        <div class="list-filter">
                            <div class="col-sm-2">

                                <div class="input-group">
                                   <%-- <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to">Customer <span id="totalcustomer" runat="server">0</span></asp:Label>--%>

                                 <%--   <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Sort by:</asp:Label>          --%>                    
                                      


                                </div>
                            </div>
                            <div class="col-sm-2">
                                  <asp:DropDownList ID="ddlfilter" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfilter_SelectedIndexChanged">
                                            <asp:ListItem Value="recentadded">Recently added</asp:ListItem>
                                              <asp:ListItem Value="name">Name</asp:ListItem>
                                             <asp:ListItem Value="nextapoin">Next Appointment</asp:ListItem>
                                              <asp:ListItem Value="other">Other Filter</asp:ListItem>
                                        </asp:DropDownList>
                                        
                            </div>
                            <%--<div class="col-sm-2">
                                <asp:TextBox ID="txtSearch"  runat="server"  Width="145px" placeholder="Product Details" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                               
                            </div>--%>
                            <div id="OtherFilter" runat="server" visible="false">
                                <div class="col-sm-2">
                                  <asp:DropDownList ID="ddlproject" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlproject_SelectedIndexChanged">
                                           
                              </asp:DropDownList>
                                        
                            </div>
                             <div class="col-sm-2">
                                  <asp:DropDownList ID="ddlprofession" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprofession_SelectedIndexChanged">
                                           
                              </asp:DropDownList>
                                        
                            </div>
                             <div class="col-sm-2">
                                  <asp:DropDownList ID="ddllocation" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddllocation_SelectedIndexChanged">
                                           
                              </asp:DropDownList>
                                        
                            </div>
                          <div class="col-sm-1">
                                  <asp:DropDownList ID="ddlgrade" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged">
                                           
                              </asp:DropDownList>
                                        
                            </div>
                                <div class="col-sm-1">
                                  <asp:DropDownList ID="ddlvisit" CssClass="form-control chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlvisit_SelectedIndexChanged">
                                           <asp:ListItem>Yes</asp:ListItem>
                                         <asp:ListItem>No</asp:ListItem>
                              </asp:DropDownList>
                                        
                            </div>
                                </div>
                        </div>

                    </asp:Panel>

                    <div class="row">


                        <asp:Repeater ID="repteUserinfo" OnItemDataBound="repUserDetails_ItemDataBound" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" CssClass="hidden" Text='<% #Eval("userid")%>' ID="Label1"></asp:Label>

                                <div class="col-md-12  col-lg-12">
                                    <div class="well profile">

                                        <div class="col-sm-12 panel">
                                            <div class="col-xs-12 col-sm-1 pading5px">
                                                <div class="profile-userpic">
                                                      <asp:Image ID="lblImageUrl" runat="server" ImageUrl='<%#Eval("userimg") %>' class="img-responsive" ></asp:Image>
                                                </div>

                                                <div class="emphasis" style="margin-top: 5px;">
                                                    <%--<a class="btn btn-sm btn-block btn-success" href="<%=this.ResolveUrl("~/F_21_MKT/ClientProfileView.aspx?Type=Mgt&clientid=")%>" target="_blank">--%>
                                                    <asp:HyperLink ID="HypView" CssClass="btn btn-sm btn-block btn-success" runat="server" Target="_blank"> View Profile</asp:HyperLink>


                                                    <%-- <span class="fa fa-user"></span>View Profile 
                                                    </a>--%>
                                                </div>

                                            </div>
                                            <div class="col-xs-12 col-sm-3">
                                                <h4>
                                                    <asp:Label runat="server" Text='<% #Eval("postedname")%>' ID="lbl"></asp:Label>
                                                </h4>
                                                <p>
                                                    <strong>Reg: </strong>
                                                    <%# DataBinder.Eval(Container.DataItem, "userjoinind", "{0:dd/MMM/yyyyEmail}") %>
                                                </p>
                                                <p><strong>Profession: </strong><%# DataBinder.Eval(Container, "DataItem.udegn")%> </p>

                                                <p><strong>Email: </strong><%# DataBinder.Eval(Container, "DataItem.usermail")%> </p>
                                                <p><strong>Phone: </strong><%# DataBinder.Eval(Container, "DataItem.umobile")%> </p>

                                            </div>

                                            <div class="col-xs-12 col-sm-8 pading5px panel">
                                               <asp:Repeater ID="repUserDetails" runat="server" >
                                                    <HeaderTemplate>
                                                        <table class="table no-border custom_table dataTable no-footer dtr-inline">
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 30px;" class="text-center">Sl.</th>
                                                                    <th style="width: 100px;" class="text-center">Assign</th>
                                                                    <th style="width: 100px;" class="text-center">Project Name</th>
                                                                    <th style="width: 100px;" class="text-center">Task Name </th>
                                                                    <th style="width: 100px;" class="text-center">From Message</th>
                                                                    <th style="width: 120px;" class="text-center">To Message </th>
                                                                    <th style="width: 120px;" class="text-center">Target Date</th>
                                                                </tr>
                                                            </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tbody>

                                                            <tr>
                                                                <td style="width: 30px;"><%# Container.ItemIndex + 1 %></td>
                                                                 <td style="width: 100px;">
                                                                    <asp:Label runat="server" Text='<% #Eval("asinnamne")%>' ID="Label7"></asp:Label>
                                                                </td>
                                                              
                                                                <td style="width: 150px;">                                                    
                                                                    <asp:Label runat="server" Text='<% #Eval("actdesc")%>' ID="Label3"></asp:Label>
                                                                </td>
                                                                <td style="width: 100px;">
                                                                    <asp:Label runat="server" Text='<% #Eval("taskname")%>' ID="Label5"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" Text='<% #Eval("fmessage")%>' ID="Label2"></asp:Label></td>
                                                                <td style="width: 150px;">

                                                                    <asp:Label runat="server" Text='<% #Eval("cstatus")%>' ID="Label4"></asp:Label>
                                                                </td>
                                                                 <td style="width: 150px;">                                                       
                                                                    <asp:Label runat="server" Text='<% #Eval("probledat")%>' ID="Label6"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                                <hr />


                                              <%--  <div class="btn-group dropup btn-sm  pull-right">
                                                    <button type="button" class="btn btn-primary btn-sm "><span class="fa fa-gear"></span>Options </button>
                                                    <button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown">
                                                        <span class="caret"></span>
                                                        <span class="sr-only">Toggle Dropdown</span>
                                                    </button>
                                                    <ul class="dropdown-menu text-left" role="menu">
                                                        <li>
                                                            <asp:HyperLink ID="hyplink" runat="server" Target="_blank"> Add Discussion</asp:HyperLink>
                                                            <a href="#"   target="_blank"> Add Discussion 
                                                            </a>
                                                            <li>
                                                                <asp:LinkButton ID="lnkbtnEmail" runat="server" OnClick="lnkbtnEmail_Click"><span class="fa fa-envelope pull-right"></span> Send an email </asp:LinkButton>

                                                            </li>
                                                            <li>
                                                                <asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"><span class="fa fa-list pull-right"></span> Remove from a list</asp:LinkButton>

                                                            </li>
                                                            <li class="divider"></li>
                                                            <li><a href="#"><span class="fa fa-warning pull-right"></span>Report this user for spam  &nbsp; &nbsp;</a></li>
                                                    </ul>
                                                </div>--%>
                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>
                                </div>

                            </ItemTemplate>
                            <FooterTemplate>
                                </table>  
            <div id="dvNoRecords" runat="server" visible="false" style="text-align: center; color: Red;">
                <font>  
                    <b>  
                        <i>No Product for display.</i>  
                    </b>  
                </font>
            </div>
                            </FooterTemplate>
                        </asp:Repeater>



                    </div>

                </div>
            </div>

            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content" style="background: #fff;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Contact Client</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-3">Client Email</label>
                                    <div class="col-md-9">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span>
                                            </span>
                                            <asp:TextBox runat="server" ID="sup_email" CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Subject</label>
                                    <div class="col-md-9">
                                        <asp:TextBox runat="server" ID="subject" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3">Message</label>
                                    <div class="col-md-9">
                                        <asp:TextBox TextMode="MultiLine" runat="server" ID="msgbody" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="SubmitMsg_OnClick" OnClientClick="CloseModal()" Class="btn btn-primary"><span class="glyphicon glyphicon-send"></span> Send Email</asp:LinkButton>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


