<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRMShiftChanger.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.HRMShiftChanger" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <style>
        .dropdown-toggle:after {
            display: none !important;
        }

        .multiselect-native-select .btn-group {
            width: 100% !important;
            border: 1px solid #808080;
        }

        .multiselect-native-select .show {
            width: 100% !important;
            border: 1px solid #808080;
        }



         .multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 500px !important;
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }




    </style>


    <script type="text/javascript" language="javascript">
    

 

        $(function () {


            $('[id*=DropCheck1]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true
            })

        });





        $(document).ready(function () {

            $('.chzn-select').chosen({ search_contains: true });

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $('[id*=DropCheck1]').multiselect({
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true
            })





        }


    </script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
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
            <div class="card card-fluid">
                <div class="card-body" style="height: 100vh;">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation1" runat="server" CssClass="chzn-select form-control form-control-sm"  AutoPostBack="true" OnSelectedIndexChanged="ddlWstation1_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                                <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Working Shift Plan</asp:Label>
                                <asp:DropDownList ID="ddlExtShift" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlExtShift_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label15" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision1" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlDivision1_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept1" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"  OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged"></asp:DropDownList>
                            </div>       
                            <div class="form-group">
                                <asp:Label ID="Label18" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection1" runat="server" CssClass="chzn-select form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlSection1_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlLine" runat="server" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>                        

                              <div class="form-group">
                                <asp:Label ID="lblgrade" runat="server" CssClass="label">Grade</asp:Label>
                                <asp:DropDownList ID="ddlgrade" runat="server" CssClass="chzn-select form-control form-control-sm"  AutoPostBack="true" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                             <div class="form-group">
                                <asp:Label ID="lbldesig" runat="server" CssClass="label">Designation</asp:Label>
                                <asp:DropDownList ID="ddldesig" runat="server" CssClass="chzn-select form-control form-control-sm"  AutoPostBack="true" OnSelectedIndexChanged="ddldesig_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                  <asp:Label ID="lblLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm" ></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblshiftemp" runat="server" CssClass="label" Style="color: blue; font-size: 14px; font-weight: bold;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Select Employee</asp:Label>
                                <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control"  SelectionMode="Multiple"  data-mdb-filter="true"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Working Shift Plan</asp:Label>
                                <asp:DropDownList ID="ddlWorkShfitTo" runat="server" CssClass="chzn-select form-control"></asp:DropDownList>
                                <asp:HiddenField ID="Hiddshiftid" runat="server" />
                                <asp:HiddenField ID="Hiddabstime" runat="server" />
                                <asp:HiddenField ID="Hiddlatemarg" runat="server" />
                                <asp:HiddenField ID="hiddmacstarttime" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Working Shift Plan From Date</asp:Label>
                                <asp:TextBox ID="txtShiftFrom" runat="server" AutoCompleteType="Disabled" CssClass=" form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtShiftFrom_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtShiftFrom"></cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Working Shift Plan To Date</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
