<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ShowAllDoc.aspx.cs" Inherits="SPEWEB.F_33_Doc.ShowAllDoc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                       <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">         
                        <div class="col-md-3">
                            <div class="form-group">
                                    <asp:Label ID="lblDocNo" runat="server" CssClass="control-label" Text="Select Document No:"></asp:Label>

                                        <asp:DropDownList ID="DdlDocNo" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control"></asp:Label>
                        </div>
                        </div>
                                
                      
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkok"  runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary " style="margin-top:20px;"></asp:LinkButton>
                                    </div>
                               
                                </div>

                    </div>
                    </div>

                <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">         
                    <div class="page-inner">
              <!-- .page-title-bar -->
              <header class="page-title-bar">              
                <h1 class="page-title"> Activities </h1>
              </header>
              <!-- /.page-title-bar -->
              <!-- .page-section -->
              <div class="page-section">
                <!-- .section-block -->
                <div class="section-block" id="ActivityLog" runat="server">
                
                  <!-- /.timeline -->
                </div>
                <!-- /.section-block -->
               <%-- <p class="text-center">
                  <button type="button" class="btn btn-light">
                    <i class="fa fa-fw fa-angle-double-down"></i> Load more</button>
                </p>--%>
              </div>
              <!-- /.page-section -->
            </div>
                        </div>
                    </div>
                 </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

