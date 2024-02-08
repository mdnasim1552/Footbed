<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="HRDashboard.aspx.cs" Inherits="SPEWEB.F_99_Allinterface.HRDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <%-- <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="container moduleItemWrpper hrnDashboard">
                <div class="contentPart">

                    <section class="col-lg-12 connectedSortable">

                        <div style="height: 500px;">
                            <div id="Tabs" role="tabpanel" style="padding-top: 10px;">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs responsive" role="tablist">
                                    <li class="active"><a href="#tabs-1" class="tabs" aria-controls="personal" role="tab" data-toggle="tab">Home </a></li>

                                    <li class="dropdown">
                                        <a href="#" data-toggle="dropdown">Profile <span class="caret"></span></a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a href="#tabs-2" class="tabs" aria-controls="personald" role="tab" data-toggle="tab">View Profile</a></li>
                                            <li><a href="#tabs-3" class="tabs" aria-controls="photo" role="tab" data-toggle="tab">Change Photo</a></li>
                                            <li><a href="#tabs-4" class="tabs" aria-controls="Password" role="tab" data-toggle="tab">Change Password</a></li>
                                        </ul>
                                    </li>



                                    <li><a href="#tabs-7" class="tabs" id="tabsys1" runat="server" aria-controls="shortlist" role="tab" data-toggle="tab">Shortlisted Applications<asp:Label ID="lblShortlisted" runat="server" Text=""></asp:Label></a></li>
                                    <li><a href="#tabs-5" class="tabs" aria-controls="UnSuccessful" role="tab" data-toggle="tab">Un-Successful Applications<asp:Label ID="lblunsuccessful" runat="server" Text=""></asp:Label></a></li>
                                    <li><a href="#tabs-6" class="tabs" aria-controls="Reserved" role="tab" data-toggle="tab">Reserved List<asp:Label ID="lblreservedlist" runat="server" Text=""></asp:Label></a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content responsive">

                                    <div role="tabpanel" class="tab-pane" id="tabs-1">
                                        dd
                                    </div>
                                    <div role="tabpanel" class="tab-pane " id="tabs-2">
                                        Tab profile

                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="tabs-3">


                                        <div class="row">
                                            <asp:Panel ID="Panel2" runat="server">

                                                <div class="col-md-3">
                                                    <fieldset class="scheduler-border fieldset_A">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">


                                                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName text-warning">Employee Image</asp:Label>
                                                                <asp:Image ID="EmpImg" runat="server" Height="100px" Width="100px" />
                                                                <div>

                                                                    <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                                        onchange="submitform();" ToolTip="Employee Image" Width="216px" />
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>

                                                <div class="col-md-3">
                                                    <fieldset class="scheduler-border fieldset_A">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">

                                                                <div class="msgHandSt">
                                                                    <asp:Label ID="Label2" CssClass=" smLbl_to" runat="server">Employee Signature</asp:Label>
                                                                </div>
                                                                <asp:Image ID="EmpSig" runat="server" Height="100px" Style="margin-left: 0px"
                                                                    Width="100px" />
                                                                <div>

                                                                    <asp:FileUpload ID="imgSigFileUpload" runat="server" Height="26px"
                                                                        onchange="submitform();" ToolTip="Employee Signature" Width="216px" />


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>


                                            </asp:Panel>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">

                                                <div class="col-md-4 pading5px col-md-offset-2">
                                                    <asp:LinkButton ID="lbtnUpdateImg" runat="server" CssClass="btn btn-danger primaryBtn margin5px" OnClick="lbtnUpdateImg_Click">Update</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-info primaryBtn" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>



                                                </div>

                                            </div>
                                        </div>

                                        

                                    </div>


                                    <div role="tabpanel" class="tab-pane " id="tabs-4">
                                        Tab pwd
                                    </div>



                                    <div role="tabpanel" class="tab-pane" id="tabs-7">
                                        ddd
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="tabs-5">
                                        <asp:Button ID="lnkUnSuccessful" runat="server"  Text="dd"
                                            CssClass="btn btn-primary"></asp:Button>
                                        dhdc
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="tabs-6">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="lnkEditShortList3" runat="server" Text="Add to Shortlisted" CssClass="btn btn-success"></asp:Button>
                                                <asp:Button ID="lnkEditReserved3" runat="server" Text="Add to Reserved" CssClass="btn btn-warning"></asp:Button>
                                                <asp:Button ID="lnkPrintReserved" runat="server" Text="Print Multiple Applicant"
                                                    CssClass="btn btn-info"></asp:Button>
                                                <asp:Button ID="lnkUnSuccessfulReserved" Visible="false" runat="server" Text="Notify Un-successful Reserve List"
                                                    CssClass="btn btn-primary"></asp:Button>
                                            </div>
                                        </div>


                                    </div>


                                    <asp:HiddenField ID="TabName" runat="server" />

                                </div>
                            </div>
                        </div>
                    </section>

                    <script type="text/javascript">
                        $(function () {
                            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
                            $('#Tabs a[href="#' + tabName + '"]').tab('show');
                            $(".tabs").click(function () {
                                //  debugger;
                                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                            });
                        });
                    </script>

                </div>
            </div>


      <%--  </ContentTemplate>--%>
       <%-- <Triggers>
<asp:PostBackTrigger ControlID="imgFileUpload" />
            <asp:PostBackTrigger ControlID="imgSigFileUpload" />
            
</Triggers>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>

