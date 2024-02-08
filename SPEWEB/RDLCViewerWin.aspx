﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDLCViewerWin.aspx.cs" Inherits="SPEWEB.RDLCViewerWin" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Preview1</title>
    <script language="javascript">
        function WStartUpSize() {
            self.resizeTo(400, 400);
            self.moveTo(screen.width / 2 - 200, screen.height / 2 - 200);
        }
        function WResizeTo() {
            self.moveTo(0, 0);
            self.resizeTo(screen.width, screen.height);
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 85px;
        }

        .style2 {
            height: 21px;
            width: 85px;
        }

        .style3 {
            width: 21px;
        }

        .style4 {
            height: 21px;
            width: 21px;
        }
    </style>
</head>

<body onload="WStartUpSize()" bgcolor="#F0FEF0">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>

