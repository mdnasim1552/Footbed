﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="PuchasePrint.aspx.cs" Inherits="SPEWEB.F_10_Procur.PuchasePrint"%>


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