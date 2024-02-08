<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkAttDetails.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_99_MgtAct.LinkAttDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
          
            <div class="col-md-4" style="margin-top:20px;"></div>
            <div class="col-md-4"  style="margin-top:20px;">
                <asp:Repeater ID="rplellaabsemp" runat="server">
                                <HeaderTemplate>
                                    <table id="tblrplellaabsemp" class="table-striped table-hover table-bordered grvContentarea">
                                        

                                        <tr>

                                            <th style="width: 30px;">Sl</th>
                                            <th style="width: 80px;">ID</th>
                                            <th style="width: 130px;">Name</th>
                                          
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lrplempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))  %>'
                                                Width="80px"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lrplcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                                Width="130"></asp:Label>
                                        </td>


                                       




                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>


                            </asp:Repeater>
            </div>
                
            </div>
       
    </div>
  
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

