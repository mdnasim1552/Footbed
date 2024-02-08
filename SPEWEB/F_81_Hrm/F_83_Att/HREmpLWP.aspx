<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="HREmpLWP.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HREmpLWP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

          $(document).ready(function () {

              Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

          });
        function pageLoaded() {

           
            $('.chzn-select').chosen({ search_contains: true });

         
        }

    </script>

    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                
                                 <div class="form-group">

                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label6" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                        <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                        <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                        <asp:DropDownList ID="ddlSection" runat="server" Width="200"  CssClass="chzn-select pull-left" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                         
                                    </div>
                                     
                                    
                                </div>




                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="lblTxt lblName" 
                                            OnClick="imgbtnEmployee_Click">Emp.  Name</asp:LinkButton>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="inputTxt hidden inputName inpPixedWidth"></asp:TextBox>
                                        
                                   
                                        <asp:DropDownList ID="ddlEmpName" runat="server" Width="233" CssClass="form-control inputTxt chzn-select" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblCompany" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblSection" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Designation</asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDesignation" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lmsg11" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Month</asp:Label>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <asp:CheckBoxList ID="chkDate" runat="server" Font-Bold="True" CssClass="chkBoxControl"
                                ForeColor="#000" RepeatDirection="Horizontal" Width="900px"
                                RepeatColumns="7">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>




            <%--<table style="width:100%;">
            <tr>
                <td colspan="10">
                    <asp:Panel ID="Panel2" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                        BorderWidth="1px">
                        <table style="width: 100%;">
                         <tr>
                                <td class="style33">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Text="Emp.  Name:" Width="81px"></asp:Label>
                                </td>
                                <td class="style36">
                                    <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass="txtboxformat" 
                                        Width="80px" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td class="style30">
                                    <asp:ImageButton ID="imgbtnEmployee" runat="server" Height="16px" 
                                        ImageUrl="~/Image/find_images.jpg" onclick="imgbtnEmployee_Click" 
                                        Width="16px" />
                                </td>
                                <td class="style37">
                                    <asp:DropDownList ID="ddlEmpName" runat="server" AutoPostBack="True" 
                                        Font-Bold="True" Font-Size="12px" 
                                        onselectedindexchanged="ddlEmpName_SelectedIndexChanged" Width="300px">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ddlEmpName_ListSearchExtender" runat="server" 
                                        QueryPattern="Contains" TargetControlID="ddlEmpName">
                                    </cc1:ListSearchExtender>
                                </td>
                                <td class="style39">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style33">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Text="Company:" Width="81px"></asp:Label>
                                </td>
                                <td class="style34" colspan="3">
                                    <asp:Label ID="lblCompany" runat="server" BackColor="White" Font-Size="12px" 
                                        ForeColor="Black" Height="16px" Width="405px"></asp:Label>
                                </td>
                                <td class="style39">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style33">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Text="Section:" Width="81px"></asp:Label>
                                </td>
                                <td class="style35" colspan="3">
                                    <asp:Label ID="lblSection" runat="server" BackColor="White" Font-Size="12px" 
                                        ForeColor="Black" Height="16px" Width="405px"></asp:Label>
                                </td>
                                <td class="style39">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style33">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Text=" Designation:" Width="81px"></asp:Label>
                                </td>
                                <td class="style35" colspan="3">
                                    <asp:Label ID="lblDesignation" runat="server" BackColor="White" 
                                        Font-Size="12px" ForeColor="Black" Height="16px" Width="405px"></asp:Label>
                                </td>
                                <td class="style39">
                                    
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style33">
                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" style="text-align: right" Text=" Month:" Width="81px"></asp:Label>
                                </td>
                                <td class="style35" colspan="3">
                                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" 
                                        Font-Bold="True" Font-Size="12px" 
                                        onselectedindexchanged="ddlMonth_SelectedIndexChanged" Width="405px">
                                    </asp:DropDownList>
                                    <cc1:ListSearchExtender ID="ddlMonth_ListSearchExtender" runat="server" 
                                        QueryPattern="Contains" TargetControlID="ddlMonth">
                                    </cc1:ListSearchExtender>
                                </td>
                                <td class="style39">
                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" BackColor="#003366" 
                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                        Font-Size="12px" ForeColor="White" onclick="lnkbtnUpdate_Click" 
                                        style="text-align: center" Width="50px">Update</asp:LinkButton>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="9">
                    <asp:CheckBoxList ID="chkDate" runat="server" Font-Bold="True" Font-Size="12px" 
                        ForeColor="Yellow" RepeatDirection="Horizontal" Width="900px" 
                        RepeatColumns="7" BorderColor="#660066" BorderStyle="Solid" 
                        BorderWidth="1px">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

