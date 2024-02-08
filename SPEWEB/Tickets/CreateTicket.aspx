<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="CreateTicket.aspx.cs" Inherits="SPEWEB.Tickets.CreateTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            
                        <div class="col-md-12 card">
                            <div class="card-body" style="height:450px">
                                <div>

                                    <div>
                                        <asp:Label ID="TicketID" runat="server" Visible="false" Text="0"></asp:Label>
                                        <div class="row">
                                            <div class="col-lg-3 col-md-3 col-sm-12">
                                                <div class="form-group">
                                                    <label for="ticket type">Ticket Type</label>
                                                    <asp:DropDownList ID="ddlTicketType" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="10116">Service</asp:ListItem>
                                                        <asp:ListItem Value="10111">Data Delete</asp:ListItem>
                                                        <asp:ListItem Value="10112">Data Correction</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-12">
                                                <div class="form-group">
                                                    <label for="create date">Date</label>
                                                    <asp:TextBox ID="txtTdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtTdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy h: mm tt" TargetControlID="txtTdate"></cc1:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="col-lg-5 col-md-5 col-sm-12">
                                                
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-12">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="lnkListbtn" NavigateUrl="~/Tickets/Index.aspx" runat="server" CssClass="btn  btn-primary">Ticket List</asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <label for="task desc" id="tsklbl" runat="server">
                                                    Ticket Description       
                                                </label>
                                                <asp:TextBox ID="txtTicketDesc" required="required" runat="server" class="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4">

                                                <div class="form-group">
                                                    <label for="priority">File Upload</label>
                                                    <cc1:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Visible="true" Width="322px" />
                                                    <br />

                                                </div>

                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-12">
                                                <div class="form-group">
                                                    <label for="priority">Priority</label>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlPriority">
                                                        <asp:ListItem Value="99101">Normal</asp:ListItem>
                                                        <asp:ListItem Value="99102">Important</asp:ListItem>
                                                        <asp:ListItem Value="99103">Urgent</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-12">
                                            <div class="form-group">
                                                <span class="remember">
                                                    <asp:CheckBox ID="ChkChangePass" runat="server"
                                                        AutoPostBack="True" />Send Mail</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <button type="button" runat="server" id="btnSave" data-dismiss="modal" aria-hidden="true" onserverclick="btnSave_ServerClick" class="btn btn-primary">Save changes</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                   
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />

            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
