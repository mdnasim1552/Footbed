<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkQutaAttached.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.LinkQutaAttached" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
               
                <fieldset class="scheduler-border fieldset_A">
                     <div class="col-md-12">
                    <div class="form-horizontal">

                     
                            <fieldset class="form-group">
                         
                                   <div class="col-md-3">
                                       <cc1:asyncfileupload onclientuploaderror="uploadError"
                                    onclientuploadcomplete="uploadComplete" runat="server"
                                    id="AsyncFileUpload1" uploaderstyle="Modern"
                                    completebackcolor="White"
                                    uploadingbackcolor="#CCFFFF" throbberid="imgLoader"
                                    onuploadedcomplete="FileUploadComplete" />
                                <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/Wait.gif" />
                                   </div>
                                   <div class="col-md-3">

                                <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-sm pull-left" Text="Show Image" OnClick="btnShowimg_Click" />

                                   </div>
                                
                               
                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>


                            </fieldset>
                            <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="col-xs-12 col-sm-2 col-md-2 listDiv" style="padding: 0 5px;">
                                        <div id="EmpAll" runat="server">
                                            <%--<a href="#"><i class="fa fa-archive"></i>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("desig")%>'></asp:Label></a>--%>
                                            <div style="margin-bottom: 2px; padding-bottom:20px">

                                                <a href="<%# Eval("filePath1") %>" target="_blank" class="block">                                                   
                                                    <i class="fa fa-picture-o" aria-hidden="true" style="font-size:80px"></i><br />
                                                    <span style="font-size:20px;"><%# Eval("supinfo") %></span>
                                                </a>


                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>

                       
                    </div>
                          </div>
                </fieldset>
            </div>
               
        </div>
    </div>
     <script language="javascript" type="text/javascript">

         $(document).ready(function () {
             //For navigating using left and right arrow of the keyboard
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         });
         function pageLoaded() {

             $("input, select").bind("keydown", function (event) {
                 var k1 = new KeyPress();
                 k1.textBoxHandler(event);
             });

         }
    </script>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>



