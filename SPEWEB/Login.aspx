<%@ Page Title="" Language="C#" MasterPageFile="~/SPE01.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SPEWEB.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function ChPaModalOpen() {

            $('#ChPaModal').modal('show');
        }
        function ChPaModalClose() {

            $('#ChPaModal').modal('hide');
        }
        function leave() {    ///// using festival option
            //  window.location = "http://webdesign.about.com";

            $('#WCModal').modal('hide');
        }
        //setTimeout("leave()", 4000); 



        $(document).ready(function () {

            //$('#WCModal').modal('show');

            $('#<%=this.txtuserid.ClientID%>').focus();
            $("#txtuserpass").keypress(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == "13") {
                    document.getElementById("loginBtn").click();
                }
            });
            //Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        };


        function searchKeyPress(e) {

            e = e || window.event;

            if (e.keyCode == 13) {

                document.getElementById("<%=loginBtn.ClientID %>").click();

                return false;
            }
            return true;
        }

    </script>
    <style>
 
    </style>
    <div class="auth-form">
        <!-- .form-group -->
        <div class="form-group">
            <div class="form-label-group">
                <asp:DropDownList ID="listComName" class="form-control form-control-sm" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="listComName_SelectedIndexChanged"></asp:DropDownList>


            </div>
        </div>
        <!-- /.form-group -->
        <!-- .form-group -->
        <div class="form-group">



            <div class="input-group">
                <label class="input-group-prepend" for="pi1">
                    <span class="input-group-text">
                        <span class="far  fa-user"></span>
                    </span>
                </label>
                <asp:TextBox ID="txtuserid" runat="server" class="form-control form-control-sm" name="txtuserid" required="required" title="Please enter you username" onkeypress="return searchKeyPress(event);" placeholder="User Name" AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>

            </div>


        </div>
        <!-- /.form-group -->
        <!-- .form-group -->
        <div class="form-group">

            <div class="input-group">
                <label class="input-group-prepend" for="pi1">
                    <span class="input-group-text">
                        <span class="far fa fa-key"></span>
                    </span>
                </label>
                <asp:TextBox ID="txtuserpass" runat="server" class="form-control form-control-sm pwd" name="txtuserpass" TextMode="Password" required="required" onkeypress="return searchKeyPress(event);" placeholder="Password" TabIndex="4"></asp:TextBox>


                <label class="input-group-append reveal" for="ai1">
                    <span class="input-group-text">
                        <span class="far fa-eye"></span>
                    </span>
                </label>
            </div>

        </div>
        <!-- /.form-group -->
        <!-- .form-group -->
        <div class="form-group">
            <asp:LinkButton ID="loginBtn" runat="server" OnClick="loginBtn_Click" Class="btn  btn-primary btn-block" TabIndex="5"> Sign In</asp:LinkButton>

        </div>
        <!-- /.form-group -->
        <!-- .form-group -->
        <div class="form-group text-center">
            <div class="custom-control custom-control-inline custom-checkbox">
                <input type="checkbox" class="custom-control-input" id="remember-me">
                <label class="custom-control-label" for="remember-me">Keep me sign in</label>
            </div>
            <br />
            <asp:LinkButton ID="cahngeBtn" runat="server" OnClick="cahngeBtn_Click" Class="Link" TabIndex="5">Change Password?</asp:LinkButton>

            <span class="mx-2">·</span>
            <a href="#" class="link">Forgot Password?</a>
        </div>
        <!-- /.form-group -->
        <!-- recovery links -->
        <div class="text-center pt-3">
            <div class="custom-control custom-control-inline">

                <img src="Image/LoginImg/erplogo.png" alt="img" height="70" width="160" />
            </div>
        </div>
        <!-- /recovery links -->
    </div>
    <!------------------ welcome--------------------------------->

    <div class="modal animated" id="WCModal" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Image ID="Image2" CssClass="img img-responsive" runat="server" Height="510" Width="770" ImageUrl="~/Images/NewYear-2024.jpg" />
                        </div>

                        <%--<img src="<?php echo base_url();?>images/NewYear2018.gif" class="img img-responsive">--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!---modal---->
    <div id="myModal" class="modal  animated rollIn " role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content col-md-7 col-md-offset-3">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Forgot Password</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal forgotform" id="">
                        <div class="form-group">
                            <div style="margin-bottom: 1px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox ID="TxtUserName" runat="server" class="form-control" name="txtuserid" value="" required="required" title="Please enter you username" placeholder="User Name or Id" TabIndex="3"></asp:TextBox>


                            </div>

                            <div style="margin-bottom: 1px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                                <asp:TextBox ID="TxtEmail" runat="server" class="form-control" TextMode="Email" required="required" placeholder="Email" TabIndex="4"></asp:TextBox>

                            </div>
                            <div style="margin-bottom: 1px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                                <asp:TextBox ID="TxtPhone" runat="server" class="form-control" TextMode="Phone" required="required" placeholder="Phone Number" TabIndex="4"></asp:TextBox>

                            </div>
                            <div style="margin-bottom: 1px" class="input-group">
                                <span class="pull-left" style="color: black; font-weight: bold; margin-right: 3px; margin-top: 2px; font-size: 12px;">Notification On: </span>
                                <asp:RadioButtonList ID="rbtnAtten" CssClass="pull-right marrbtn" runat="server"
                                    Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected=""> Email</asp:ListItem>
                                    <asp:ListItem> Mobile</asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div style="margin-bottom: 1px" class="input-group">

                                <asp:Label ID="smsg" runat="server" class="control-label" Visible="false"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer ">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <%-- <asp:LinkButton ID="ForgotSubmit" runat="server" OnClick="ForgotSubmit_click" Class="btn btn-primary"> Submit</asp:LinkButton>--%>
                </div>
            </div>
        </div>
    </div>
    <!------------------ Change pass--------------------------------->


    <div id="ChPaModal" class="modal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content ">
                <div class="modal-header">

                    <h4 class="modal-title"><span class="fa fa-table"></span>Details Information </h4>
                    <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>
                </div>
                <div class="modal-body form-horizontal">

                    <div class="form-group">
                        <div class="input-group">
                            <span id="lbloldPass" runat="server" visible="false" class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox ID="txtuserOldrpass" runat="server" AutoCompleteType="Disabled" class="form-control" name="txtuserpass" TextMode="Password" placeholder="Old Password" TabIndex="6"></asp:TextBox>

                        </div>

                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon" id="lblNewPass" runat="server" visible="false"><i class="glyphicon glyphicon-lock"></i></span>
                            <asp:TextBox ID="txtuserNewrpass" runat="server" AutoCompleteType="Disabled" class="form-control" name="txtuserpass" TextMode="Password" placeholder="New Password" TabIndex="7"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlmsgbox" runat="server" Visible="false">

                    <p id="lblalrtmsg" runat="server"></p>
                </asp:Panel>

                <div class="modal-footer ">
                    <asp:LinkButton ID="ModalUpdateBtn" OnClick="ModalUpdateBtn_Click" OnClientClick="CLoseMOdal();"
                        runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

