<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="SPEWEB.F_04_Sampling.Print" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>

  
      function SetTarget(type) {
            window.open('../RdlcViews.aspx?PrintOpt=' + type, '_blank');
      }
    </script>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>
</asp:Content>


