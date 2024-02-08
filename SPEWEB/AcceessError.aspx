<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AcceessError.aspx.cs" Inherits="SPEWEB.Content.AcceessError" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style>
    .btnRegView {
    animation: 1s ease 0s alternate none infinite running color_change;
    color: #ffffff;
    padding: 5px 15px;
    font-size:20px;
}
    .btnRegView h4{
        font-size:22px;
    }
@keyframes color_change {
0% {
    background-color: blue;
}
100% {
    background-color: red;
}
}
@keyframes color_change {
0% {
    background-color: blue;
}
100% {
    background-color: red;
}
}

</style>
   
    
            <div class="card card-fluid">
                <div class="card-body">
                     <div class="row">
                  <div class="col-md-2"></div>
                <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading btnRegView" style=" background:#ff0000;" >
                    <h4 class="panel-title">
                       <asp:Label ID="lblAccessError" runat="server" style="text-align:center;" 
        Text="Required Authentication Not Found. Access Is Denied,Contact with administrator."></asp:Label>

                    </h4>
                    
                </div>
                <div class="panel-body text-center">     
    <img src="Content/Theme/images/403.jpg" class="img-fluid rounded" width="100%" />   
    <button class="btn btn-sm btn-info " type="button" onclick="history.back()">Go Back</button>

            </div>
        </div>

                 
            </div>
                  
                    </div>
                </div>
   
    


</asp:Content>

