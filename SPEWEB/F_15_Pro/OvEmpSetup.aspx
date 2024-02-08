<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="OvEmpSetup.aspx.cs" Inherits="SPEWEB.F_15_Pro.OvEmpSetup" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

    </script>
  
    <div class="container moduleItemWrpper">
        <div class="contentPart">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">


                                <div class="form-group">
                                   

                                    <div class="col-md-12 pading5px ">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" >Information :</asp:Label>
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass=" inputTxt lblName txtAlgLeft" Font-Bold="true" Font-Size="14px" Width="606px"></asp:TextBox>

                                    </div>
                                   

                                </div>
                                <div class="form-group">
                                  <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName">Page Size :</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass=" ddlPage">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True" CssClass=" smLbl_to" Width="93px"
                                            OnCheckedChanged="chkAllSInf_CheckedChanged" Text="Show All" />
                                    </div>
                                     <div class="col-md-2 pading5px">
                                        <div class="colMdbtn pading5px">
                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                        </div>



                                    </div>
                                   
                                </div>

                            </div>
                        </fieldset>
                    </div>



                    <asp:GridView ID="gvSetupDet" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnPageIndexChanging="gvSetupDet_PageIndexChanging" ShowFooter="True" OnRowDeleting="gvSetupDet_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />

                            <asp:TemplateField HeaderText="Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvActcode" runat="server"
                                        ForeColor="Black" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department & Employee Name">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>

                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvActivi" runat="server"
                                             Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                            Width="300px">
                                        </asp:Label>
                                   
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Designation" >
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesg" runat="server"
                                        ForeColor="Black"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of </br> Joining" >
                                <ItemTemplate>
                                    <asp:Label ID="lblgvjoindat" runat="server"
                                        ForeColor="Black"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindat")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            


                            <asp:TemplateField HeaderText="Active">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvmrno" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estatus"))=="True" %>'
                                        Width="50px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Active" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvother" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "otherst"))=="True" %>'
                                        Width="50px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
