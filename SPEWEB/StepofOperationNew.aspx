﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="StepofOperationNew.aspx.cs" Inherits="SPEWEB.StepofOperationNew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            if ($.browser.chrome || $.browser.mozilla) {
                GetMenu();

            }



            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(GetMenu);

        });         
        function GetMenu() {

            try {

                var Moduleid = $('#<%=this.txtmodid.ClientID%>').val();
                var leftul = $('#Leftul');
                leftul.html('');
                var Midul = $('#Midul');
                Midul.html('');
                var Rightul = $('#Rightul');
                Rightul.html('');

                //var Moduleid = "27";
                var lInputName = "02%";
                var MInputName = "03%";
                var RInputName = "04%";

                var Moduleobj = new RealERPScript();
                var leftlist = Moduleobj.GetModule(Moduleid, lInputName);
                var Midtlist = Moduleobj.GetModule(Moduleid, MInputName);
                var Righttlist = Moduleobj.GetModule(Moduleid, RInputName);
                console.log(Midtlist);
                /*left , Middle and Right UL */
                $.each(leftlist, function (index, leftlist) {
                    if (leftlist.itemslct == false) {
                        leftul.append('<li><h5>' + leftlist.itemdesc + '</h5></li>');

                    }
                    else if (leftlist.itemslct == true && leftlist.itemdesc == "") {
                            ;

                    }

                    else {
                        leftul.append('<li><a href=' + encodeURI(leftlist.itemurl) + '>' + leftlist.itemdesc + '</a></li>');
                    }


                });
                $.each(Midtlist, function (index, Midtlist) {
                    if (Midtlist.itemslct == false) {
                        Midul.append('<li><h5>' + Midtlist.itemdesc + '</h5></li>');

                    }
                    else if (Midtlist.itemslct == true && Midtlist.itemdesc == "") {
                            ;
                    }

                    else {

                        Midul.append('<li><a href=' + encodeURI(Midtlist.itemurl) + '>' + Midtlist.itemdesc + '</a></li>');
                    }


                });
                $.each(Righttlist, function (index, Righttlist) {
                    if (Righttlist.itemslct == false) {
                        Rightul.append('<li><h5>' + Righttlist.itemdesc + '</h5></li>');

                    }
                    else if (Righttlist.itemslct == true && Righttlist.itemdesc == "") {
                            ;
                    }

                    else {
                        Rightul.append('<li><a href=' + encodeURI(Righttlist.itemurl) + '>' + Righttlist.itemdesc + '</a></li>');
                    }


                });

                /* end left , Middle and Right UL */
                leftul.show();
                Midul.show();
                Rightul.show();
            }

            catch (e) {

                alert(e);
            }

        }




    </script>


   
       
            <div class="card card-fluid">
                <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlModuleName" class="form-control ClCompAndMod" runat="server" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged"></asp:DropDownList>

                    </div>
                    <div class="col-md-4">
                       
                       
                       <h3>
                            <div class="spinner-grow text-primary" role="status">
                        <span class="sr-only">Loading...</span>
                      </div>
                           <asp:Label ID="modulenam" runat="server">Quick Tour</asp:Label></h3>
                    </div>
                    <div class="col-md-4" style="display:none;">
                        <asp:DropDownList ID="ddlCompanyName" class="form-control ClCompAndMod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" TabIndex="1"></asp:DropDownList>
                    </div>



                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="card">
                        <div class="card-header"> 
                             
                           A. One Time Inputs </div><!-- .card-body -->
                        <div class="card-body">
                         <ul id="Leftul" class="list-unstyled colLeft">
                        </ul>
                        </div><!-- /.card-body -->
                        <!-- .card-footer -->
                       <%-- <div class="card-footer">
                          <a href="#" class="card-footer-item card-footer-item-bordered">Save</a> <a href="#" class="card-footer-item card-footer-item-bordered">Edit</a> <a href="#" class="card-footer-item card-footer-item-bordered">Delete</a>
                        </div>--%><!-- /.card-footer -->
                      </div><!-- /.card -->
                      
                       
                    </div>
                    <div class="col-md-4">
                        <div class="card bg-secondary">
                        <div class="card-header"> B. Operational Menu </div><!-- .card-body -->
                        <div class="card-body">
                         <ul id="Midul" class="list-unstyled colMid ">
                        </ul>
                        </div><!-- /.card-body -->
                        <!-- .card-footer -->
                     <%--   <div class="card-footer">
                          <a href="#" class="card-footer-item card-footer-item-bordered card-link">Save</a> <a href="#" class="card-footer-item card-footer-item-bordered card-link">Edit</a> <a href="#" class="card-footer-item card-footer-item-bordered card-link">Delete</a>
                        </div>--%><!-- /.card-footer -->
                      </div>
                        <h3></h3>
                        

                    </div>
                    <div class="col-md-4">
                        <div class="card">
                        <div class="card-header"> C. Reports </div><!-- .card-body -->
                        <div class="card-body">
                        <ul id="Rightul" class="list-unstyled colRight">
                        </ul>
                        </div><!-- /.card-body -->
                        <!-- .card-footer -->
                      <%--  <div class="card-footer">
                          <a href="#" class="card-footer-item card-footer-item-bordered card-link">Save</a> <a href="#" class="card-footer-item card-footer-item-bordered card-link">Edit</a> <a href="#" class="card-footer-item card-footer-item-bordered card-link">Delete</a>
                        </div>--%><!-- /.card-footer -->
                      </div>
                        <h3></h3>
                       
                    </div>
                </div>
                <asp:TextBox ID="txtmodid" runat="server" Style="display: none;"></asp:TextBox>
                    </div>
            </div>
</asp:Content>


