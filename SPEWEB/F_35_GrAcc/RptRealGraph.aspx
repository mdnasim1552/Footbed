<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptRealGraph.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.RptRealGraph" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    

                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                
                                 <cc1:BarChart ID="BarChart1" runat="server" CategoriesAxis="1,2,3" ChartTitle="Amount in Million (FC)"  
                          ChartHeight="450" ChartType="Column" ChartWidth="500">  </cc1:BarChart></td>

                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>






                       
                    
              
   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

