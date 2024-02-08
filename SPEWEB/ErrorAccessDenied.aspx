<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ErrorAccessDenied.aspx.cs" Inherits="SPEWEB.ErrorAccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="empty-state">
            <!-- .empty-state-container -->
            <div class="empty-state-container">
              <div class="state-figure">
                <img class="img-fluid" src="Content/Theme/images/avatars/img-2.svg" alt="" style="max-width: 320px">
              </div>
              <h3 class="state-header"> Page Not found! </h3>
              <p class="state-description lead text-muted"> Sorry, we've misplaced that URL or it's pointing to something that doesn't exist. </p>
              <div class="state-action">
                <a href="/" class="btn btn-lg btn-light"><i class="fa fa-angle-right"></i> Go Back</a>
              </div>
            </div><!-- /.empty-state-container -->
          </div>
</asp:Content>



