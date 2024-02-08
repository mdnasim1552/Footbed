<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="SPEWEB.Tickets.TicketDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid mt-3">
                <div class="RealProgressbar">
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
                <div class="row">
                    <div class="col-md-5 col-xl-5 col-lg-5">
                        <div class="card">
                            <div class="card-body">
                                <div class="row" style="background-color:aliceblue">
                                    <div class="col-md-4">
                                        <p class="mt-3 mb-1 font-weight-bold">Fixed By</p>
                                        <h6 id="assignEngName" runat="server"></h6>
                                    </div>
                                    <div class="col-md-4">
                                        <p class="mt-3 mb-1 font-weight-bold">Creation Date</p>
                                        <h6 id="creatDate" runat="server"></h6>
                                    </div>
                                    <div class="col-md-4 font-weight-bold">
                                        <p class="mt-3 mb-1">Creation by</p>
                                        <h6 id="creatBy" runat="server"></h6>
                                    </div>
                                </div>
                              
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12 " style="align-content:center">
                                        <p class="row font-weight-bold" >Ticket Details:</p>
                                        <p id="ticketDesc" class="mb-4" runat="server"></p>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-7 col-xl-7 col-lg-7">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-title mb-3">Attachments</div>





                                <asp:Panel ID="Panel2" runat="server">

                                    <asp:Image ID="BindImg" runat="server" CssClass="img img-thumbnail" />
                                    
                                </asp:Panel>
                               

                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Literal ID="embedPdf" runat="server"/>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
